using System.Collections.Generic;
using DomainModel;
using DomainModel.CommonModel;

namespace ServiceModel.Transactions
{
    public interface ITransactionService
    {
        bool AddTransaction(Transaction transaction);
        bool AddCreditTransaction(CreditTransaction transaction);
        bool AddDebitTransaction(DebitTransaction transaction);
        IList<Transaction> GetAllTransactions();
        IList<CreditTransaction> GetAllCreditTransactions();
        IList<DebitTransaction> GetAllDebitTransactions();
        IList<CreditTransaction> GetExpandedCreditTransactions(IList<CreditTransaction> transactions, int times);
    }
}
