using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.ViewModel
{
    public class DashBoardVm
    {
        public IList<PaymentDestinationVm> PaymentDestinationVm { get; set; }
        public IList<PaymentSourceVm> PaymentSourceVm { get; set; }
    }
}