using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimation.Service.Primitives.Models
{
    public class ExpenseResourceModel
    {
        public string CostCentre { get; set; }
        public decimal Total { get; set; }
        public decimal TotalExcludingGST { get; set; }
        public decimal GSTValue { get; set; }
        public string PaymentMethod { get; set; }
    }
}
