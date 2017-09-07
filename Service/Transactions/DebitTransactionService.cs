using System.Collections.Generic;
using System.Linq;
using Domain.Repository;
using Domain.Repository.Transactions;
using DomainModel;
using DomainModel.CommonModel;
using DomainModel.Transactions;
using Service.Expenses;

namespace Service.Transactions
{
    public class DebitTransactionService:TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ExpensesService _expensesService;

        public DebitTransactionService()
        {
            _transactionRepository = new DebitTransactionRepository();
            _expensesService=new ExpensesService();
        }

        public override bool AddDebitTransaction(DebitTransaction transaction)
        {
            return _transactionRepository.AddDebitTransaction(transaction);
        }

        public override IList<DebitTransaction> GetAllDebitTransactions()
        {
            var expenses = _expensesService.GetAllPaymentDestinations();
            var transactions =_transactionRepository.GetAllDebitTransactions();

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < transactions.Count; i++)
            {
                transactions[i].TransactionName =
                    expenses.First(x => x.Id == transactions[i].TransactionSourceId).Name;
            }

            return transactions;
        }
    }
}
