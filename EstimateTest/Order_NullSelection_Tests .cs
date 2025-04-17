using Estimate.Models;
using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimateTest
{
    public class Order_NullSelection_Tests : MainViewModelTestBase
    {
        [Fact]
        public void EditOrder_WhenSelectedItemIsNull_DoesNothing()
        {
            // Arrange
            ViewModel.SelectedOrder = null;

            // Act
            ViewModel.EditOrderCommand.Execute(null);

            // Assert
            _orderServiceMock.Verify(s => s.UpdateOrder(It.IsAny<Order>()), Times.Never);
            Assert.Equal(2, ViewModel.Orders.Count); // ничего не удалено, не заменено
        }
    }
}
