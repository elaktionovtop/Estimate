using Estimate.Models;
using Estimate.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EstimateTest
{
    public abstract class OrderViewModelTestsBase
    {
        protected Order _originalOrder;
        protected OrderViewModel ViewModel;

        protected OrderViewModelTestsBase()
        {
            _originalOrder = new Order
            {
                Id = 1,
                CustomerId = 1,
                EmployeeId = 2,
                ConstructionId = 3,
                Description = "Исходный заказ",
                Status = OrderStatus.New
            };

            ViewModel = new OrderViewModel(_originalOrder, null, null, null);
        }

        // вспомогательный метод, чтобы эмулировать поведение окна
        protected static Window CreateFakeWindow()
        {
            var window = new Window();
            window.Show(); // иначе не сработает DialogResult
            return window;
        }
    }
}
