using System;
using DomainModel.CommonModel;

namespace ServiceModel.Balances
{
    public interface IBalanceService
    {
        BalanceContainer BuildBalanceContainer(DateTime? startDate, DateTime? endDate);
    }
}