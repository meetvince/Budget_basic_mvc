
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using DomainModel.CommonModel;
using log4net;
using log4net.Config;
using Service.Transactions;
using ServiceModel.Balances;
using ServiceModel.Transactions;

namespace Service.Balances
{
    public class BalanceService : IBalanceService
    {
        private readonly ITransactionService _creditTransactionService;
        private readonly ITransactionService _debitTransactionService;
        protected readonly ILog Logger;

        public BalanceService()
        {
            _creditTransactionService = new CreditTransactionService();
            _debitTransactionService = new DebitTransactionService();
            Logger = LogManager.GetLogger(typeof(BalanceService));
            XmlConfigurator.Configure();
        }

        public BalanceContainer BuildBalanceContainer(DateTime? startDateTime=null,DateTime? endDateTime=null)
        {
            try
            {
                var currentDate = DateTime.Now;
                var beginDate = startDateTime ?? currentDate;
                var endDate = endDateTime ?? currentDate.AddMonths(int.Parse(ConfigurationManager.AppSettings["ProjectionMonths"]));

                var months = (((beginDate.Year - endDate.Year)*12) + beginDate.Month - endDate.Month) * -1;

                var creditTransactions = _creditTransactionService.GetAllCreditTransactions();
                var allCreditTransactions = _creditTransactionService.GetExpandedCreditTransactions(creditTransactions,
                    months);

                var debitTransactions = _debitTransactionService.GetAllDebitTransactions();

                var totalCredit = creditTransactions.Where(d=>d.TransactionDate<=DateTime.Now).Sum(x => x.Amount);
                var totalSpending = debitTransactions.Where(d=>d.TransactionDate<=DateTime.Now).Sum(x => x.Amount);

                var currentPeriodCredit = GetCreditAmount(creditTransactions,beginDate, endDate);

                var currentPeriodDebit = GetDebitAmount(debitTransactions, beginDate, endDate);

                IList<PeriodBalance> periodBalances = new List<PeriodBalance>();

                var currentMonth = beginDate;

                var sum = 0.0f;
                while (currentMonth.AddMonths(1) < endDate)
                {
                    
                    var periodBalance = new PeriodBalance
                    {
                        Id = currentMonth.Month,
                        Year = currentMonth.Year,
                        Name = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(currentMonth.Month),
                        Credit = GetCreditAmount(allCreditTransactions,currentMonth,currentMonth),
                        Debit = GetDebitAmount(debitTransactions,currentMonth,currentMonth)
                    };
                    sum += periodBalance.Credit - periodBalance.Debit;
                    periodBalance.Balance = sum ;
                    periodBalances.Add(periodBalance);

                    currentMonth = currentMonth.AddMonths(1);
                }

                return new BalanceContainer
                {
                    TotalCredit = totalCredit,
                    TotalDebit = totalSpending,
                    TotalBalance = totalCredit-totalSpending,
                    TotalCreditThisPeriod = currentPeriodCredit,
                    TotalDebitThisPeriod = currentPeriodDebit,
                    TotalBalanceThisPeriod = currentPeriodCredit-currentPeriodDebit,
                    PeriodBalances = periodBalances
                };

            }
            catch (Exception)
            {
               return null;
            }
        }


        private static float GetCreditAmount(IList<CreditTransaction> transactions, DateTime startDate, DateTime endDate)
        {
            if (transactions == null || !transactions.Any())
                return 0;

            return transactions.Where(
                d => d.TransactionDate >= new DateTime(startDate.Year, startDate.Month, 1) &&
                     d.TransactionDate <=
                     new DateTime(endDate.Year, endDate.Month,
                         DateTime.DaysInMonth(endDate.Year, endDate.Month))).Sum(x => x.Amount);
        }

        private static float GetDebitAmount(IList<DebitTransaction> transactions, DateTime startDate, DateTime endDate)
        {
            if (transactions == null || !transactions.Any())
                return 0;

            return transactions.Where(
                d => d.TransactionDate >= new DateTime(startDate.Year, startDate.Month, 1) &&
                     d.TransactionDate <=
                     new DateTime(endDate.Year, endDate.Month,
                         DateTime.DaysInMonth(endDate.Year, endDate.Month))).Sum(x => x.Amount);
        }
    }
}
