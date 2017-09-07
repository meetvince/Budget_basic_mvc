using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using DomainModel;
using DomainModel.CommonModel;
using MyBudget.ViewModel;
using Service;
using Service.Expenses;
using Service.Income;
using Service.Transactions;
using ServiceModel;
using ServiceModel.Expenses;
using ServiceModel.Income;
using ServiceModel.Transactions;

namespace MyBudget.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IPaymentSourceService _paymentSourceService;
        private readonly IExpensesService _expensesService;
        private readonly ITransactionService _creditTransactionService;
        private readonly ITransactionService _debitTransactionService;

        public TransactionController()
        {
            _paymentSourceService = new PaymentSourceService();
            _creditTransactionService = new CreditTransactionService();
            _debitTransactionService = new DebitTransactionService();
            _expensesService= new ExpensesService();
        }

        public ActionResult Index()
        {
            Mapper.CreateMap<CreditTransaction, CreditTransactionsVm>();
            Mapper.CreateMap<DebitTransaction, DebitTransactionsVm>();

            var transactionsVm = new TransactionsVm
            {
                CreditTransactionsVm = _creditTransactionService.GetAllCreditTransactions().Select(Mapper.Map<CreditTransaction, CreditTransactionsVm>).ToList(),
                DebitTransationsVm = _debitTransactionService.GetAllDebitTransactions().Select(Mapper.Map<DebitTransaction,DebitTransactionsVm>).ToList()
            };

            return View(transactionsVm);
        }

        [HttpGet]
        public ActionResult AddNewCreditTransaction()
        {
            var paymentSources = _paymentSourceService.GetAllPaymentSources();
            var today = DateTime.Now;
            var creditTransactionsVm = new CreditTransactionsVm
            {
                Amount = default(float),
                PaymentSources = paymentSources,
                RecurrenceIntervalList = new Dictionary<int, string>(),
                TransactionDate = new DateTime(today.Year, today.Month, 1)
            };

            foreach (var r in Enum.GetValues(typeof(Utilities.RecurrenceInterval)))
            {
                creditTransactionsVm.RecurrenceIntervalList.Add((int)r, r.ToString());
            }

            return View(creditTransactionsVm);
        }

        [HttpPost]
        public ActionResult AddNewCreditTransactionPost(CreditTransactionsVm creditTransactionsVm)
        {
            var creditTransaction = new CreditTransaction
            {
                Amount = creditTransactionsVm.Amount,
                Recur = creditTransactionsVm.Recur,
                TransactionSourceId = int.Parse(HttpContext.Request.Form["paymentSource"]),
                RecurrenceInterval = (Utilities.RecurrenceInterval)Enum.Parse(typeof(Utilities.RecurrenceInterval), HttpContext.Request.Form["recurrenceFrequency"]),
                TransactionDate = creditTransactionsVm.TransactionDate,
                TransactionType = Utilities.TransactionType.Credit
            };

            var paymentSources = _paymentSourceService.GetAllPaymentSources();

            creditTransactionsVm.TransactionName =
                paymentSources.First(x => x.Id == creditTransaction.TransactionSourceId).Name;

            var success = _creditTransactionService.AddCreditTransaction(creditTransaction);

            creditTransactionsVm.Message = success
                ? "Successfully added credit transaction"
                : "Failed to add credit transaction";

            return View(creditTransactionsVm);
        }

        [HttpGet]
        public ActionResult AddNewDebitTransaction()
        {
            var paymentDestinations = _expensesService.GetAllPaymentDestinations();
            var debitTransactionsVm = new DebitTransactionsVm
            {
                PaymentDestinations = paymentDestinations,
                Amount = default(float),
                TransactionDate = DateTime.Now
            };

            return View(debitTransactionsVm);
        }

        [HttpPost]
        public ActionResult AddNewDebitTransactionPost(DebitTransactionsVm debitTransactionsVm)
        {
            var debitTransaction = new DebitTransaction
            {
                Amount = float.Parse(HttpContext.Request.Form["amount"]),
                TransactionSourceId = int.Parse(HttpContext.Request.Form["paymentDestination"]),
                TransactionDate = DateTime.Now,
                TransactionType = Utilities.TransactionType.Debit
            };

            var paymentDestinations = _expensesService.GetAllPaymentDestinations();

            debitTransactionsVm.TransactionName =
                paymentDestinations.First(x => x.Id == debitTransaction.TransactionSourceId).Name;

            var success = _debitTransactionService.AddDebitTransaction(debitTransaction);

            debitTransactionsVm.Message = success
                ? "Successfully added debit transaction"
                : "Failed to add credit transaction";

            return View(debitTransactionsVm);
        }

    }
}
