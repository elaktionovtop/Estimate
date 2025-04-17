using Estimate.Services;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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

        [NotMapped]
        public static ObservableCollection<EnumDisplay<OrderStatus>>
            Statuses { get; } = new()
            {
                new(OrderStatus.New, "Новый"),
                new(OrderStatus.InProgress, "В работе"),
                new(OrderStatus.Completed, "Завершён"),
                new(OrderStatus.Cancelled, "Отменён")
            };

        [NotMapped]
        public string StatusText => 
            Statuses.FirstOrDefault(s => s.Value == Status).Display;

        DateTime creationDateTime;
        [NotMapped]
        public DateTime CreationDateTime
        {
            get => CreationdDate.ToDateTime(TimeOnly.MinValue);
            set=> CreationdDate = DateOnly.FromDateTime(value);
        }

        DateTime? completionDateTime;
        [NotMapped]
        public DateTime? CompletionDateTime
        {
            get => CompletionDate.HasValue
                ? CompletionDate.Value.ToDateTime(TimeOnly.MinValue)
                : null;
            set => CompletionDate = value.HasValue
                ? DateOnly.FromDateTime(value.Value)
                : null;
        }
    }

    public enum OrderStatus { New, InProgress, Completed, Cancelled }
}
