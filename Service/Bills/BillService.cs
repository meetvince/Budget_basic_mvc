using System.Collections.Generic;
using Domain.Repository.Bills;
using DomainModel.CommonModel;
using ServiceModel.Bills;

namespace Service.Bills
{
    public class BillService : IBillService
    {
        private readonly BillsRepository _billsRepository;

        public BillService()
        {
            _billsRepository = new BillsRepository();
        }

        public IList<BillPayment> GetAllBillPayments()
        {
            return _billsRepository.GetAllBills();
        }

        public bool AddNewBill(BillPayment bill)
        {
            return _billsRepository.AddNewBillPayment(bill);
        }
    }
}
