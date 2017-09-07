using System.Collections.Generic;
using DomainModel.CommonModel;

namespace DomainModel.Transactions
{
    public interface ITransactionRepository
    {
        IList<Transaction> GetAllTransactions();
        IList<CreditTransaction> GetAllCreditTransactions();
        IList<DebitTransaction> GetAllDebitTransactions();

        bool AddTransaction(Transaction transaction);
        bool AddCreditTransaction(CreditTransaction transaction);
        bool AddDebitTransaction(DebitTransaction transaction);
    }
}