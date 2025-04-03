using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estimate.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public OrderStatus Status { get; set; }
        public int UserId { get; set; }
        public AppUser? User { get; set; }
    }

    public enum OrderStatus { New, InProgress, Completed, Cancelled }
}
