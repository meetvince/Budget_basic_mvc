using System.Collections.Generic;
using DomainModel.CommonModel;

namespace DomainModel.Bills
{
    public interface IBillsRepository
    {
        bool AddNewBillPayment(BillPayment bill);
        IList<BillPayment> GetAllBills();
    }
}