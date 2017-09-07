using System.Collections.Generic;
using Domain.Repository;
using Domain.Repository.Income;
using DomainModel;
using DomainModel.CommonModel;
using DomainModel.Expenses;
using ServiceModel.Expenses;

namespace Service.Expenses
{
    public class ExpensesService : IExpensesService
    {
        private readonly IExpensesRepository _expensesRepository;

        public ExpensesService()
        {
            _expensesRepository = new ExpensesRepository();
        }

        public void AddNewExpenseDestination(PaymentDestination paymentDestination)
        {
            _expensesRepository.AddNewExpenseDestination(paymentDestination);
        }

        public IList<PaymentDestination> GetAllPaymentDestinations()
        {
            return _expensesRepository.GetAllExpenseDestinations();
        }
    }
}
