using Estimate.Data;
using Estimate.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estimate.Services
{
    public interface IOrderService
    {
        bool IsValid(Order order);

        IEnumerable<Order> GetAllOrders();
        void AddOrder(Order order);
        void RemoveOrder(Order order);
        void AddWorkToOrder(int orderId, int workId, int quantity);

    }

    public class OrderService : IOrderService
    {
        private readonly AppDbContext _db;

        public OrderService(AppDbContext db) => _db = db;

        public bool IsValid(Order order) => true;

        public IEnumerable<Order> GetAllOrders() =>
            _db.Orders.ToList();

        public void AddOrder(Order order) { }
        public void RemoveOrder(Order order) { }

        public void CreateOrder(Order order)
        {
            if(string.IsNullOrWhiteSpace(order.Description))
                throw new ArgumentException("Описание заказа обязательно");

            _db.Orders.Add(order);
            _db.SaveChanges();
        }

        public void AddWorkToOrder(int orderId, int workId, int quantity)
        {
            var order = _db.Orders.Find(orderId);
            var work = _db.Works.Find(workId);

            var orderWork = new OrderWork
            {
                OrderId = orderId,
                WorkId = workId,
                Quantity = quantity
            };

            _db.OrderWorks.Add(orderWork);
            //order.Total += work.Price * quantity;
            _db.SaveChanges();
        }
    }
}
