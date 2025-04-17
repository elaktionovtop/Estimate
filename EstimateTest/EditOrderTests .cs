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
    public class EditOrderTests : MainViewModelTestBase
    {
        private Order _original;
        private Order _edited;

        protected override OrderViewModel CreateOrderViewModel(Order order)
        {
            _original = order;

            _edited = new Order
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                ConstructionId = order.ConstructionId,
                Description = "Обновлённый заказ"
            };

            return new OrderViewModel(_edited, null, null, null);
        }

        [Fact]
        public void EditOrder_ReplacesItemAndUpdatesService()
        {
            // Arrange
            ViewModel.SelectedOrder = ViewModel.Orders.First();

            // Act
            ViewModel.EditOrderCommand.Execute(null);

            // Assert
            Assert.Equal("Обновлённый заказ", ViewModel.SelectedOrder.Description);
            Assert.Contains(ViewModel.SelectedOrder, ViewModel.Orders);

            _orderServiceMock.Verify(s => s.UpdateOrder(It.Is<Order>(
                o => o.Id == _original.Id && o.Description == "Обновлённый заказ"
            )), Times.Once);
        }
    }
}
