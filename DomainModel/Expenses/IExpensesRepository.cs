using System.Collections.Generic;
using DomainModel.CommonModel;

namespace DomainModel.Expenses
{
    public interface IExpensesRepository
    {
        IList<PaymentDestination> GetAllExpenseDestinations();
        bool AddNewExpenseDestination(PaymentDestination expenseDestination);
        //bool UpdateExpenseDestination(PaymentDestination expenseDestination);
    }
}