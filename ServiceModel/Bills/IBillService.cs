using System.Collections.Generic;
using DomainModel.CommonModel;

namespace ServiceModel.Bills
{
    public interface IBillService
    {
        IList<BillPayment> GetAllBillPayments();
        bool AddNewBill(BillPayment bill);
    }
}