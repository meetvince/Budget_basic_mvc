using System.Collections.Generic;
using Domain.Repository;
using Domain.Repository.Income;
using DomainModel;
using DomainModel.CommonModel;
using DomainModel.Income;
using ServiceModel.Income;

namespace Service.Income
{
    public class PaymentSourceService : IPaymentSourceService
    {
        private readonly ISourceRepository _sourceRepository;

        public PaymentSourceService()
        {
            _sourceRepository=new SourceRepository();
        }

        public void AddNewPaymentSource(PaymentSource paymentSource)
        {
            _sourceRepository.AddNewPaymentSource(paymentSource);
        }

        public IList<PaymentSource> GetAllPaymentSources()
        {
            return _sourceRepository.GetAllPaymentSources();
        }

    }
}
