using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain.Entitites;
using DomainModel;
using DomainModel.CommonModel;

namespace Domain.Repository.Transactions
{
    public class DebitTransactionRepository : TransactionRepository
    {
        public override bool AddDebitTransaction(DebitTransaction transaction)
        {
            try
            {
                Mapper.CreateMap<DebitTransaction, Entitites.activity>()
                    .ForMember(dest => dest.amount, opts => opts.MapFrom(src => src.Amount))
                    .ForMember(dest=>dest.transactionSourceId,opts=>opts.MapFrom(src=>src.TransactionSourceId));

                var debitActivity = Mapper.Map<DebitTransaction, Entitites.activity>(transaction);

                debitActivity.recur = false;
                debitActivity.type = Utilities.TransactionType.Debit.ToString("g");
                debitActivity.time = DateTime.Now;

                using (var dbContext = new MyBudget())
                {
                    dbContext.activities.Add(debitActivity);
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occurred while adding debit transaction to database");
                Logger.Error(ex.Message);
            }

            return false;
        }

        public override IList<DebitTransaction> GetAllDebitTransactions()
        {
            try
            {
                Mapper.CreateMap<activity, DebitTransaction>()
                    .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.amount))
                    .ForMember(dest => dest.TransactionDate, opts => opts.MapFrom(src => src.time))
                    .ForMember(dest => dest.TransactionSourceId, opts => opts.MapFrom(src => src.transactionSourceId));

                using (var dbContext = new MyBudget())
                {
                    var debitTransactions =
                        dbContext.activities.Where(x => x.type == Utilities.TransactionType.Debit.ToString()).ToList();
                    return debitTransactions.Select(Mapper.Map<activity, DebitTransaction>).ToList();
                }

            }
            catch (Exception ex)
            {
                Logger.Error("Exception occurred while retrieving debit transactions from database");
                Logger.Error(ex.Message);
            }
            return null;
        }
    }
}
