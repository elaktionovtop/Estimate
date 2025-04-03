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
        void CreateOrder(Order order);
    }

    public class OrderService : IOrderService
    {
        private readonly AppDbContext _db;

        public OrderService(AppDbContext db) => _db = db;

        public void CreateOrder(Order order)
        {
            if(string.IsNullOrWhiteSpace(order.Description))
                throw new ArgumentException("Описание заказа обязательно");

            _db.Orders.Add(order);
            _db.SaveChanges();
        }
    }
}
