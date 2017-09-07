
using System.Collections.Generic;

namespace DomainModel.CommonModel
{
    public class PeriodBalance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public float Balance { get; set; }
        public float Credit { get; set; }
        public float Debit { get; set; }
        public IEnumerable<string> Comments { get; set; }
    }
}
