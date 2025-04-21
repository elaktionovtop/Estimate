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
    // !!! добавить тест на удаление

    public class OrderServiceTests : TestBase
    {
        [Fact]
        public void CreateOrder_ShouldAddOrderToDatabase()
        {
            using var context = TestBase.CreateContext();
            var service = new OrderService(context);
            var newOrder = new Order
            {
                CustomerId = 1,
                EmployeeId = 1,
                ConstructionId = 1,
                Description = "Order_descr_1"
            };
            service.Add(newOrder);

            Assert.Single(context.Orders);
            Assert.Equal("Order_descr_1",
                context.Orders.First().Description);
        }

        [Theory]
        [InlineData("")]
        public void CreateOrder_WithInvalidParams_ThrowsException
            (string description)
        {
            using var context = TestBase.CreateContext();
            var service = new OrderService(context);

            Assert.Throws<ArgumentException>(() =>
                service.Add(new Order
                {
                    Description = description
                }));
        }
    }
}