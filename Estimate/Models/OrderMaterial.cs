using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estimate.Models
{
    public class OrderMaterial
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MaterialId { get; set; }
        public decimal Quantity { get; set; }

        public Order Order { get; set; }
        public Material Material { get; set; }
    }
}
