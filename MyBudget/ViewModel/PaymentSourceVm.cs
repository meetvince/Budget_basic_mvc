using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.ViewModel
{
    public class PaymentSourceVm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Message { get; set; }
    }
}