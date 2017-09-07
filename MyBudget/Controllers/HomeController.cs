using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using DomainModel;
using DomainModel.CommonModel;
using MyBudget.ViewModel;
using Service;
using Service.Expenses;
using Service.Income;
using ServiceModel;
using ServiceModel.Expenses;
using ServiceModel.Income;

namespace MyBudget.Controllers
{
    public class HomeController : Controller
    {
        private readonly IExpensesService _expensesService;
        private readonly IPaymentSourceService _sourceService;

        public HomeController()
        {
            _expensesService = new ExpensesService();
            _sourceService = new PaymentSourceService();
        }

        public ActionResult Index()
        {
            var paymentDestinations = _expensesService.GetAllPaymentDestinations();
            Mapper.CreateMap<PaymentDestination, PaymentDestinationVm>();
            var paymentDestinationVm = paymentDestinations.Select(Mapper.Map<PaymentDestination, PaymentDestinationVm>).ToList();

            var paymentSources = _sourceService.GetAllPaymentSources();
            Mapper.CreateMap<PaymentSource, PaymentSourceVm>();
            var paymentSourceVm = paymentSources.Select(Mapper.Map<PaymentSource, PaymentSourceVm>).ToList();

            var dashBoardVm = new DashBoardVm
            {
                PaymentDestinationVm = paymentDestinationVm,
                PaymentSourceVm = paymentSourceVm
            };

            return View(dashBoardVm);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult AddNewPaymentDestination()
        {
            var paymentDestinationVm = new PaymentDestinationVm();
            return View(paymentDestinationVm);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult AddNewPaymentDestinationPost()
        {
            try
            {
                var paymentDestinationVm = new PaymentDestinationVm
                {
                    Name = HttpContext.Request.Form["paymentDestinationName"],
                    Description = HttpContext.Request.Form["destinationDescription"]
                };

                Mapper.CreateMap<PaymentDestinationVm, PaymentDestination>();

                var paymentDestination = Mapper.Map<PaymentDestinationVm, PaymentDestination>(paymentDestinationVm);

                _expensesService.AddNewExpenseDestination(paymentDestination);

                paymentDestinationVm.Message = "Successfully added new payment destination!!!";

                return View(paymentDestinationVm);

            }
            catch (Exception)
            {
                return View();
            }
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult AddNewPaymentSource()
        {
            return View(new PaymentSourceVm());

        }

        [System.Web.Http.HttpPost]
        public ActionResult AddNewPaymentSourcePost()
        {
            try
            {
                var paymentSourceVm = new PaymentSourceVm
                {
                    Name = HttpContext.Request.Form["paymentSourceName"],
                    Description = HttpContext.Request.Form["sourceDescription"]
                };

                Mapper.CreateMap<PaymentSourceVm, PaymentSource>();
                var paymentSource = Mapper.Map<PaymentSourceVm, PaymentSource>(paymentSourceVm);

                _sourceService.AddNewPaymentSource(paymentSource);

                paymentSourceVm.Message = "Successfully added new payment source!!!";

                return View(paymentSourceVm);

            }
            catch (Exception)
            {

                return View();
            }
        }

    }
}
