using Castle.Core.Resource;

using Estimate.Data;
using Estimate.Models;
using Estimate.Services;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EstimateTest
{
    public class OrderServiceTests : TestBase
    {
        [Fact]
        public void CreateOrder_ShouldAddOrderToDatabase()
        {
            using var context = CreateTestContext();
            var service = new OrderService(context);
            var newOrder = new Order 
            {
                CustomerId = OrderCustomerId,
                EmployeeId = OrderEmployeeId,
                ConstructionId = OrderConstractionId,
                Description = OrderDescription
            };
            service.CreateOrder(newOrder);

            Assert.Single(context.Orders);
            Assert.Equal(OrderDescription, 
                context.Orders.First().Description);
        }

        [Theory]
        [InlineData("")]
        public void CreateOrder_WithInvalidParams_ThrowsException
            (string description)
        {
            using var context = CreateTestContext();
            var service = new OrderService(context);

            Assert.Throws<ArgumentException>(() =>
                service.CreateOrder(new Order
                {
                    Description = description
                }));
        }

//        [Fact]
//        public void AddWorkToOrder_ShouldCalculateTotal()
//        {
//            // Arrange
//            using var context = CreateTestContext();

//            var service = new OrderService(context);
//            var order = new Order
//            {
//                CustomerId = OrderCustomerId,
//                EmployeeId = OrderEmployeeId,
//                ConstructionId = OrderConstractionId,
//                Description = OrderDescription
//            };
//            service.CreateOrder(order);
//            context.Orders.Add(order);
//            //context.SaveChanges();

//            // Act
//            service.AddWorkToOrder(OrderWorkOrderId, OrderWorkWorkId,
//OrderWorkQuantity); // 2 услуги по 500 руб

//            // Assert
//            var savedOrder = context.Orders
//                .Include(o => o.Works)
//                .First();
//            Assert.Equal(OrderWorkQuantity * OrderWorkPrice, savedOrder.Total); // 500 * 2
//            Assert.Single(savedOrder.Works);
//        }
    }
}

/*
    SeedData(context, db =>
    {
        db.Users.Add(new AppUser 
        { 
            Login = "admin", 
            Password = "123", 
            Role = Role.Admin 
        });
    });
*/