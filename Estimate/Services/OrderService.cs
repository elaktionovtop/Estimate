using Estimate.Data;
using Estimate.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estimate.Services
{
    public class OrderService : CrudService<Order>
    {
        public OrderService(AppDbContext db) : base(db) { }

        // удалить,когда будут соответствующие сервисы
        //public IEnumerable<Customer> GetAllCustomers() 
        //    => _db.Customers;
        //public IEnumerable<Employee> GetAllEmployees() 
        //    => _db.Employees;
        //public IEnumerable<Construction> GetAllConstructions() 
        //    => _db.Constructions;

        public override IEnumerable<Order> GetAll() 
            => _db.Orders
                .Include(o => o.Construction)
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Works)
                    .ThenInclude(w => w.Work)
                        .ThenInclude(mu => mu.MeasureUnit)
                .Include(o => o.Materials)
                    .ThenInclude(m => m.Material)
                        .ThenInclude(mu => mu.MeasureUnit)
                ;

        public override Order Clone(Order source)
        {
            return new Order
            {
                Id = source.Id,
                CustomerId = source.CustomerId,
                EmployeeId = source.EmployeeId,
                ConstructionId = source.ConstructionId,
                Status = source.Status,
                CreationdDate = source.CreationdDate,
                CompletionDate = source.CompletionDate,
                Description = source.Description,

                Customer = source.Customer,
                Employee = source.Employee,
                Construction = source.Construction
            };
        }

        public override void CopyTo(Order source, Order target)
        {
            target.CustomerId = source.CustomerId;
            target.EmployeeId = source.EmployeeId;
            target.ConstructionId = source.ConstructionId;
            target.Status = source.Status;
            target.CreationdDate = source.CreationdDate;
            target.CompletionDate = source.CompletionDate;
            target.Description = source.Description;

            target.Customer = source.Customer;
            target.Employee = source.Employee;
            target.Construction = source.Construction;
        }

        // можно проверить ссылки Customver и т.д.
        public override void Validate(Order order)
        {
            if(string.IsNullOrWhiteSpace(order.Description))
                throw new ArgumentException("Описание заказа обязательно");

            if(order.CustomerId == 0)
                throw new ArgumentException("Клиент не выбран");

            if(order.EmployeeId == 0)
                throw new ArgumentException("Сотрудник не выбран");

            if(order.ConstructionId == 0)
                throw new ArgumentException("Объект не выбран");
        }

        protected override string GetDeleteErrorMessage(Order order)
            => "Невозможно удалить заказ: " 
            + "он содержит связанные работы или материалы.";
    }
}
