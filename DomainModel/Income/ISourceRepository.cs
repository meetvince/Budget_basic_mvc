using System.Collections.Generic;
using DomainModel.CommonModel;

namespace DomainModel.Income
{
    public interface ISourceRepository
    {
        IList<PaymentSource> GetAllPaymentSources();
        bool AddNewPaymentSource(PaymentSource source);
    }
}