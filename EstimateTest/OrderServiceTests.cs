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
    public class OrderServiceTests
    {
        [Fact]
        public void CreateOrder_ShouldAddOrderToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "OrderTest")
                .Options;

            using(var context = new AppDbContext(options))
            {
                context.Users.Add(new AppUser { Id = 1, Login = "manager" });
                context.SaveChanges();
            }

            var service = new OrderService(new AppDbContext(options));

            // Act
            var newOrder = new Order { Description = "Ремонт кухни", UserId = 1 };
            service.CreateOrder(newOrder);

            // Assert
            using(var context = new AppDbContext(options))
            {
                Assert.Single(context.Orders); // Проверяем, что заказ добавился
                Assert.Equal("Ремонт кухни", context.Orders.First().Description);
            }
        }

        [Fact]
        public void CreateOrder_WithEmptyDescription_ThrowsException()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "OrderTest")
                .Options;

            using var context = new AppDbContext(options);
            var service = new OrderService(context);

            Assert.Throws<ArgumentException>(() =>
                service.CreateOrder(new Order { Description = "", UserId = 1 }));
        }
    }
}
