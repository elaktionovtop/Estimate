using Estimate.Data;
using Estimate.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Estimate.Services
{
    public class OrderWorkService : CrudService<OrderWork>
    {
        public OrderWorkService(AppDbContext db) : base(db) { }

        public override IEnumerable<OrderWork> GetAll()
            => _db.OrderWorks
            .Include(ow => ow.Work)
                .ThenInclude(w => w.MeasureUnit);

        public override OrderWork Clone(OrderWork source) => new()
        {
            Id = source.Id,
            OrderId = source.OrderId,
            WorkId = source.WorkId,
            Quantity = source.Quantity,
            
            Order = source.Order,
            Work = source.Work
        };

        public override void CopyTo(OrderWork source, OrderWork target)
        {
            target.OrderId = source.OrderId;
            target.WorkId = source.WorkId;
            target.Quantity = source.Quantity;

            target.Order = source.Order;
            target.Work = source.Work;
        }

        public override void Validate(OrderWork orderWork)
        {
            if(orderWork.WorkId == 0)
                throw new ArgumentException("Вид работы не выбран");

            if(orderWork.Quantity <= 0)
                throw new ArgumentException("Количество должно быть положительным");
        }
    }
}

