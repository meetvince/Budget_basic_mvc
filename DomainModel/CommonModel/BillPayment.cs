using System;

namespace DomainModel.CommonModel
{
    public class BillPayment
    {
        public float Amount { get; set; }
        public int ExpenseDestinationId { get; set; }
        public DateTime TransactionDateTime { get; set; }
    }
}
