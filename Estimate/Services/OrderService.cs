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
    public interface IOrderService
    {
        IEnumerable<Customer> GetAllCustomers();
        IEnumerable<Employee> GetAllEmployees();
        IEnumerable<Construction> GetAllConstructions();

        IEnumerable<Order> GetAllOrders();
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void RemoveOrder(Order order);
        void ValidateOrder(Order order);
    }

    public class OrderService : IOrderService
    {
        private readonly AppDbContext _db;

        public OrderService(AppDbContext db) => _db = db;

        public IEnumerable<Customer> GetAllCustomers() =>
            _db.Customers;
        public IEnumerable<Employee> GetAllEmployees() =>
            _db.Employees;
        public IEnumerable<Construction> GetAllConstructions() =>
            _db.Constructions;

        public IEnumerable<Order> GetAllOrders() =>
            _db.Orders
                .Include(o => o.Construction)
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Works).ThenInclude(w => w.Work).ThenInclude(mu => mu.MeasureUnit)
                .Include(o => o.Materials).ThenInclude(m => m.Material).ThenInclude(mu => mu.MeasureUnit)
                ;

        public void AddOrder(Order order)
        {
            ValidateOrder(order);
            _db.Orders.Add(order);
            _db.SaveChanges();
        }

        public void UpdateOrder(Order updated)
        {
            ValidateOrder(updated);

            var existing = _db.Orders.First(o => o.Id == updated.Id);

            existing.CustomerId = updated.CustomerId;
            existing.EmployeeId = updated.EmployeeId;
            existing.ConstructionId = updated.ConstructionId;
            existing.Status = updated.Status;
            existing.CreationdDate = updated.CreationdDate;
            existing.CompletionDate = updated.CompletionDate;
            existing.Description = updated.Description;

            _db.SaveChanges();
        }


        public void RemoveOrder(Order order)
        {
            _db.Orders.Remove(order);
            try
            {
                _db.SaveChanges();
            }
            catch(DbUpdateException ex)
            {
                throw new InvalidOperationException(
                    "Невозможно удалить заказ: " +
                    "он содержит связанные работы или материалы.",
                    ex);
            }
        }

        public void ValidateOrder(Order order)
        {
            if(string.IsNullOrWhiteSpace(order.Description))
                throw new ArgumentException("Описание заказа обязательно");

            if(order.CustomerId <= 0)
                throw new ArgumentException("Заказ должен быть привязан к заказчику");

            if(order.EmployeeId <= 0)
                throw new ArgumentException("Не указан ответственный сотрудник");

            if(order.ConstructionId <= 0)
                throw new ArgumentException("Не указан объект строительства");

            // при необходимости — проверка на наличие работ или материалов
            // if (!order.Works.Any() && !order.Materials.Any())
            //     throw new ArgumentException("В заказе должны быть указаны работы или материалы");
        }
    }
}
