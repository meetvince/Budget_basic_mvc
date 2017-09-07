using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using DomainModel.CommonModel;
using MyBudget.ViewModel;
using Service.Balances;
using ServiceModel.Balances;

namespace MyBudget.Controllers
{
    public class ProjectionController : Controller
    {
        private readonly IBalanceService _balanceService;

        public ProjectionController()
        {
            _balanceService=new BalanceService();
        }

        public ActionResult Index()
        {
            var balanceContainer = _balanceService.BuildBalanceContainer(DateTime.Now,DateTime.Now.AddMonths(12));

            Mapper.CreateMap<BalanceContainer, BalancesVm>();

            var balancesVm = Mapper.Map<BalanceContainer, BalancesVm>(balanceContainer);

            return View(balancesVm);
        }

    }
}
