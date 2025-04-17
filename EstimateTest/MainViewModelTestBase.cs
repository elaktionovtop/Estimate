using Estimate.Models;
using Estimate.Services;
using Estimate.ViewModels;
using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimateTest
{
    public abstract class MainViewModelTestBase
    {
        protected readonly List<Order> _dbOrders;
        protected readonly Mock<IOrderService> _orderServiceMock;

        protected MainViewModel ViewModel { get; private set; }

        protected MainViewModelTestBase()
        {
            _dbOrders = new List<Order>
        {
            new Order { Id = 1, Description = "Заказ 1",
                CustomerId = 1, EmployeeId = 1, ConstructionId = 1 },
            new Order { Id = 2, Description = "Заказ 2", 
                CustomerId = 1, EmployeeId = 1, ConstructionId = 1 }
        };

            _orderServiceMock = new Mock<IOrderService>();
            _orderServiceMock
                .Setup(s => s.GetAllOrders())
                .Returns(_dbOrders);

            ViewModel = new TestableMainViewModel(_orderServiceMock.Object, this);
        }

        // Эти методы будут переопределяться для подмены диалога
        protected virtual bool SimulateDialogResult => true;

        protected virtual OrderViewModel CreateOrderViewModel(Order order)
            => new OrderViewModel(order, null, null, null);

        private class TestableMainViewModel : MainViewModel
        {
            private readonly MainViewModelTestBase _test;

            public TestableMainViewModel(IOrderService service, MainViewModelTestBase test)
                : base(service)
            {
                _test = test;
            }

            protected override OrderViewModel CreateOrderViewModel(Order order)
                => _test.CreateOrderViewModel(order);

            protected override bool? ShowOrderDialog(OrderViewModel orderViewModel)
            {
                return _test.SimulateDialogResult;
            }
        }
    }
}
