using Estimate.Models;
using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimateTest
{
    public class DeleteOrderTests : MainViewModelTestBase
    {
        [Fact]
        public void DeleteOrder_RemovesItemAndClearsSelection()
        {
            // Arrange
            ViewModel.SelectedOrder = ViewModel.Orders.First();

            // Act
            ViewModel.DeleteOrderCommand.Execute(null);

            // Assert
            Assert.Single(ViewModel.Orders); // было 2, стало 1
            Assert.Null(ViewModel.SelectedOrder);
            _orderServiceMock.Verify(s => s.Remove(It.IsAny<Order>()), Times.Once);
        }
    }
}
