using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.ViewModel
{
    public class TransactionsVm
    {
        public IList<CreditTransactionsVm> CreditTransactionsVm { get; set; }
        public IList<DebitTransactionsVm> DebitTransationsVm { get; set; }
        public IList<BillTransactionVm> BillTransactionVm { get; set; }
    }
}