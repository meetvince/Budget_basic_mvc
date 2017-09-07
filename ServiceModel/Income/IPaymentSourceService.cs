using System.Collections.Generic;
using DomainModel;
using DomainModel.CommonModel;

namespace ServiceModel.Income
{
    public interface IPaymentSourceService
    {
        void AddNewPaymentSource(PaymentSource paymentSource);
        IList<PaymentSource> GetAllPaymentSources();
    }
}