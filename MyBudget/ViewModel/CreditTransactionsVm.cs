
using System;
using System.Collections.Generic;
using DomainModel;
using DomainModel.CommonModel;

namespace MyBudget.ViewModel
{
    public class CreditTransactionsVm
    {
        public string TransactionName { get; set; }
        public int Id { get; set; }
        public bool Recur { get; set; }
        public Utilities.RecurrenceInterval RecurrenceInterval { get; set; }
        public float Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public IList<PaymentSource> PaymentSources { get; set; } 
        public Dictionary<int,string> RecurrenceIntervalList { get; set; }
        public string Message { get; set; }
    }
}