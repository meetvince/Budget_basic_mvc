using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.CommonModel
{
    public class Recurrence
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public string RecurrenceInterval { get; set; }
    }
}
