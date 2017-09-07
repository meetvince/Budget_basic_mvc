using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain.Repository;
using Domain.Repository.Transactions;
using DomainModel;
using DomainModel.CommonModel;
using DomainModel.Transactions;
using Service.Income;
using ServiceModel.Income;

namespace Service.Transactions
{
    public class CreditTransactionService: TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPaymentSourceService _paymentSourceService;

        public CreditTransactionService()
        {
            _transactionRepository = new CreditTransactionRepository();
            _paymentSourceService=new PaymentSourceService();
        }

        public override bool AddCreditTransaction(CreditTransaction transaction)
        {
            return _transactionRepository.AddCreditTransaction(transaction);
        }

        public override IList<CreditTransaction> GetAllCreditTransactions()
        {
            var paymentSources = _paymentSourceService.GetAllPaymentSources();
            var transactions = _transactionRepository.GetAllCreditTransactions();

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < transactions.Count; i++)
            {
                transactions[i].TransactionName =
                    paymentSources.First(x => x.Id == transactions[i].TransactionSourceId).Name;
            }

            return transactions;
        }

        public override IList<CreditTransaction> GetExpandedCreditTransactions(IList<CreditTransaction> transactions, int times)
        {
            IList<CreditTransaction> expandedCreditTransactions = new List<CreditTransaction>();
            Mapper.CreateMap<CreditTransaction, CreditTransaction>();

            foreach (var trans in transactions)
            {
                expandedCreditTransactions.Add(trans);
                if (trans.Recur)
                {
                    var creditTransaction = Mapper.Map<CreditTransaction, CreditTransaction>(trans);
                    for (var i = 1; i < times; i++)
                    {
                        creditTransaction.TransactionDate = creditTransaction.TransactionDate.AddMonths(1);
                        var transRecord = Mapper.Map<CreditTransaction, CreditTransaction>(creditTransaction);
                        expandedCreditTransactions.Add(transRecord);
                    }
                }
            }

            return expandedCreditTransactions;
        }
    }
}
