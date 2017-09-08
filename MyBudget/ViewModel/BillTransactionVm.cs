using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.CommonModel;

namespace MyBudget.ViewModel
{
    public class BillTransactionVm
    {
        public float Amount { get; set; }
        public int ExpenseDestinationId { get; set; }
        public string ExpenseDestinationName { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public IList<PaymentDestination> PaymentDestinations { get; set; }
        public string Message { get; set; }
    }
}