using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBudget.ViewModel
{
    public class PaymentDestinationVm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}