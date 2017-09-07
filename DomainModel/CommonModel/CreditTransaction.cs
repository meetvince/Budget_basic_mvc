
namespace DomainModel.CommonModel
{
    public class CreditTransaction :  Transaction
    {
        public Utilities.TransactionType TransactionType { get; set; }
        public bool Recur { get; set; }
        public Utilities.RecurrenceInterval RecurrenceInterval { get; set; }
    }
}
