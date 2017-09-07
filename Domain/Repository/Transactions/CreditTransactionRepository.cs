using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using AutoMapper;
using Domain.Entitites;
using DomainModel;
using DomainModel.CommonModel;

namespace Domain.Repository.Transactions
{
    public class CreditTransactionRepository : TransactionRepository
    {
        public override bool AddCreditTransaction(CreditTransaction transaction)
        {
            try
            {
                using (var context = new MyBudget())
                {
                    Mapper.CreateMap<CreditTransaction, Entitites.activity>()
                        .ForMember(dest => dest.amount, opts => opts.MapFrom(src => src.Amount))
                        .ForMember(dest => dest.time, opts => opts.MapFrom(src => src.TransactionDate))
                        .ForMember(dest => dest.transactionSourceId,
                            opts => opts.MapFrom(src => src.TransactionSourceId))
                        .ForMember(dest => dest.recur, opts => opts.MapFrom(src => src.Recur ? 1 : 0));

                    var creditTransaction = Mapper.Map<CreditTransaction, Entitites.activity>(transaction);

                    creditTransaction.type = Utilities.TransactionType.Credit.ToString("g");

                    context.activities.Add(creditTransaction);
                    context.SaveChanges();

                    //Return if no recurrence
                    if(!transaction.Recur)
                        return true;

                    //Insert into Recurrence table
                    var recurrenceTransaction = new recurrence
                    {
                        frequency = transaction.RecurrenceInterval.ToString("g"),
                        transactionId = creditTransaction.id,
                        updatedDate = DateTime.Now
                    };

                    context.recurrences.Add(recurrenceTransaction);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occurred while fetching credit transactions");
                Logger.Error(ex.Message);
            }
            return false;
        }

        public override IList<CreditTransaction> GetAllCreditTransactions()
        {
            try
            {
                using (var context = new MyBudget())
                {
                    Mapper.CreateMap<activity, CreditTransaction>()
                        .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => src.amount))
                        .ForMember(dest => dest.TransactionDate, opts => opts.MapFrom(src => src.time))
                        .ForMember(dest => dest.TransactionSourceId, opts => opts.MapFrom(src => src.transactionSourceId))
                        .ForMember(dest=>dest.Id,opts=>opts.MapFrom(src=>src.id));

                    var creditTransactions = context.activities.Where(x => x.type == Utilities.TransactionType.Credit.ToString()).ToList();

                    var transactions= creditTransactions.Select(Mapper.Map<activity, CreditTransaction>).ToList();
                    var recurrences = GetAllRecurrences();

                    // ReSharper disable once ForCanBeConvertedToForeach
                    for (var i = 0; i < transactions.Count; i++)
                    {
                        if (transactions[i].Recur)
                        {
                            transactions[i].RecurrenceInterval = (Utilities.RecurrenceInterval) Enum.Parse(typeof(Utilities.RecurrenceInterval),recurrences.First(x => x.TransactionId == transactions[i].Id).RecurrenceInterval);
                        }
                    }

                    return transactions;
                }

            }
            catch (Exception ex)
            {
                Logger.Error("Error occurred while fetching Credit transactions");
                Logger.Error(ex.Message);
            }
            return null;
        }

        private IList<Recurrence> GetAllRecurrences()
        {
            try
            {
                Mapper.CreateMap<recurrence, Recurrence>()
                          .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.id))
                          .ForMember(dest => dest.TransactionId, opts => opts.MapFrom(src => src.transactionId))
                          .ForMember(dest => dest.RecurrenceInterval, opts => opts.MapFrom(src => src.frequency));

                using (var context = new MyBudget())
                {
                    return context.recurrences.ToList().Select(Mapper.Map<recurrence, Recurrence>).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error occurred while fetching Credit transactions");
                Logger.Error(ex.Message); ;
            }
            return null;
        }
    }
}
