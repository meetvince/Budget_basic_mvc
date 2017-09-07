using System.Collections.Generic;

namespace DomainModel.CommonModel
{
    public class BalanceContainer
    {
        public float TotalBalance { get; set; }
        public float TotalCredit { get; set; }
        public float TotalDebit { get; set; }
        public float TotalBalanceThisPeriod { get; set; }
        public float TotalCreditThisPeriod { get; set; }
        public float TotalDebitThisPeriod { get; set; }
        public IEnumerable<PeriodBalance> PeriodBalances { get; set; }
    }
}
