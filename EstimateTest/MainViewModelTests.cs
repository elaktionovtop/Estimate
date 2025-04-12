using Estimate.Models;
using Estimate.Services;
using Estimate.ViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace EstimateTest
{
    public class MainViewModelTests
    {
        [Fact]
        public void CreateOrderCommand_AddsOrderToListAndSelectsIt()
        {
            // Arrange
            var testOrder = new Order { Description = "Test Order" };

            var vm = new TestMainViewModel(testOrder);

            // Act
            vm.CreateOrderCommand.Execute(null);

            // Assert
            Assert.Single(vm.Items);
            Assert.Equal(testOrder, vm.SelectedItem);
        }

        // Подкласс для подмены поведения
        private class TestMainViewModel : MainViewModel
        {
            private readonly Order _order;

            public TestMainViewModel(Order order)
                : base()
            {
                _order = order;
            }

            protected override OrderViewModel CreateOrderViewModel()
            {
                var viewModel = new OrderViewModel(_order);

                // эмулируем, что окно подтвердилось (ShowDialog() == true)
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var fakeWindow = new Window { DialogResult = true };
                    fakeWindow.Close();
                });

                return viewModel;
            }
        }
    }
}
