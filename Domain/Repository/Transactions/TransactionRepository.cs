using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain.Entitites;
using DomainModel.CommonModel;
using DomainModel.Transactions;
using log4net;
using log4net.Config;

namespace Domain.Repository.Transactions
{
    public class TransactionRepository : ITransactionRepository
    {
        protected readonly ILog Logger;

        protected TransactionRepository()
        {
            Logger = LogManager.GetLogger(typeof(TransactionRepository));
            XmlConfigurator.Configure();
        }

        public virtual IList<Transaction> GetAllTransactions()
        {
            try
            {
                using (var context = new MyBudget())
                {
                    var result = context.activities.ToList();
                    Mapper.CreateMap<Entitites.activity, Transaction>()
                        .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => src.amount))
                        .ForMember(dest => dest.TransactionDate, opts => opts.MapFrom(src => src.time));
                    return result.Select(Mapper.Map<Entitites.activity, Transaction>).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occurred while fetching credit transactions");
                Logger.Error(ex.Message);
            }
            return null;
        }

        public virtual IList<CreditTransaction> GetAllCreditTransactions()
        {
            throw new NotImplementedException();
        }

        public virtual IList<DebitTransaction> GetAllDebitTransactions()
        {
            throw new NotImplementedException();
        }

        public virtual bool AddTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public virtual bool AddCreditTransaction(CreditTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public virtual bool AddDebitTransaction(DebitTransaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
