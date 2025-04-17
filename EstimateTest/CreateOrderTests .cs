using Estimate.Models;
using Estimate.ViewModels;
using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimateTest
{
    public class CreateOrderTests : MainViewModelTestBase
    {
        private Order _created;

        protected override OrderViewModel CreateOrderViewModel(Order order)
        {
            _created = new Order
            {
                Id = 100, // эмулируем, что после сохранения БД дала ID
                CustomerId = 1,
                EmployeeId = 1,
                ConstructionId = 1,
                Description = "Новый заказ"
            };

            return new OrderViewModel(_created, null, null, null);
        }

        [Fact]
        public void CreateOrder_AddsNewOrderAndSetsSelection()
        {
            // Act
            ViewModel.CreateOrderCommand.Execute(null);

            // Assert
            Assert.Contains(_created, ViewModel.Orders);
            Assert.Equal(_created, ViewModel.SelectedOrder);

            _orderServiceMock.Verify(s => s.AddOrder(It.Is<Order>(
                o => o.Description == "Новый заказ")), Times.Once);
        }
    }
}
