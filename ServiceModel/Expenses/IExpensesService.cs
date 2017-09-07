using System.Collections.Generic;
using DomainModel;
using DomainModel.CommonModel;

namespace ServiceModel.Expenses
{
    public interface IExpensesService
    {
        void AddNewExpenseDestination(PaymentDestination expenseDestination);
        IList<PaymentDestination> GetAllPaymentDestinations();
    }
}