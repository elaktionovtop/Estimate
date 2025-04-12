using Azure.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estimate.Models
{
    public class OrderWork
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int WorkId { get; set; }
        public decimal Quantity { get; set; }

        public Order Order { get; set; }
        public Work Work { get; set; }
    }
}
