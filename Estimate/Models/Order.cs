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
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public int ConstructionId { get; set; }
        public OrderStatus Status { get; set; } 
            = OrderStatus.New;
        public DateOnly CreationdDate { get; set; } 
            = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly? CompletionDate { get; set; }
        public string Description { get; set; }

        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public Construction Construction { get; set; }
        
        public List<OrderWork> Works { get; set; } = new();
        public List<OrderMaterial> Materials { get; set; } = new();

        public decimal Total
        {
            get
            {
                decimal total = 0;
                foreach(var item in Materials)
                {
                    total += item.Quantity
                        * item.Material.Price;
                }
                foreach(var item in Works)
                {
                    total += item.Quantity
                        * item.Work.Price;
                }
                return total;
            }
        }
    }

    public enum OrderStatus { New, InProgress, Completed, Cancelled }
}
