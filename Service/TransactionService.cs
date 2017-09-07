using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using DomainModel.CommonModel;
using ServiceModel;
using ServiceModel.Transactions;

namespace Service
{
    public class TransactionService: ITransactionService
    {
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

        public virtual IList<Transaction> GetAllTransactions()
        {
            throw new NotImplementedException();
        }

        public virtual IList<CreditTransaction> GetAllCreditTransactions()
        {
            throw new NotImplementedException();
        }

        public virtual IList<DebitTransaction> GetAllDebitTransactions()
        {
            throw new NotImplementedException();
        }

        public virtual IList<CreditTransaction> GetExpandedCreditTransactions(IList<CreditTransaction> transactions, int times)
        {
            throw new NotImplementedException();
        }
    }
}
