using System;

namespace DomainModel.CommonModel
{
    public class Transaction
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransactionSourceId { get; set; }
        public string TransactionName { get; set; }
    }
}
