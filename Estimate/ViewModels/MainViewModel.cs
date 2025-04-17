using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Estimate.Data;
using Estimate.Models;
using Estimate.Services;
using Estimate.Views;

using Microsoft.Extensions.DependencyInjection;

namespace Estimate.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        readonly IOrderService _orderService;

        [ObservableProperty]
        private ObservableCollection<Order> _orders;

        [ObservableProperty]
        private Order? _selectedOrder;

        public MainViewModel(IOrderService orderService)
        {
            _orderService = orderService;
            Orders = _orderService.GetAllOrders().ToObservableCollection();
            if(Orders.Count > 0)
                SelectedOrder = Orders[0];
        }

        [RelayCommand]
        private void CreateOrder()
        {
            var orderViewModel = CreateOrderViewModel(new Order());
            try
            {
                if(ShowOrderDialog(orderViewModel) == true)
                {
                    _orderService.AddOrder(orderViewModel.Order);
                    Orders.Add(orderViewModel.Order);
                    SelectedOrder = orderViewModel.Order;
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка в данных заказа",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        [RelayCommand]
        private void EditOrder()
        {
            if(SelectedOrder is null)
                return;

            var editedOrder = new Order
            {
                Id = SelectedOrder.Id,
                CustomerId = SelectedOrder.CustomerId,
                EmployeeId = SelectedOrder.EmployeeId,
                ConstructionId = SelectedOrder.ConstructionId,
                Customer = SelectedOrder.Customer!,
                Employee = SelectedOrder.Employee!,
                Construction = SelectedOrder.Construction!,
                Status = SelectedOrder.Status,
                CreationdDate = SelectedOrder.CreationdDate,
                CompletionDate = SelectedOrder.CompletionDate,
                Description = SelectedOrder.Description,
                Works = new(SelectedOrder.Works),
                Materials = new(SelectedOrder.Materials)
            };

            var orderViewModel = CreateOrderViewModel(editedOrder);
            try
            {
                if(ShowOrderDialog(orderViewModel) == true)
                {
                    _orderService.UpdateOrder(orderViewModel.Order);
                    int index = Orders.IndexOf(SelectedOrder);
                    Orders[index] = orderViewModel.Order;
                    SelectedOrder = orderViewModel.Order;
                }
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка в данных заказа",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        [RelayCommand]
        private void DeleteOrder()
        {
            if(SelectedOrder is null)
                return;

            try
            {
                int index = Orders.IndexOf(SelectedOrder);
                _orderService.RemoveOrder(SelectedOrder);
                Orders.Remove(SelectedOrder);

                // установить новый выбор
                if(Orders.Count > 0)
                {
                    if(index >= Orders.Count)
                        index = Orders.Count - 1;

                    SelectedOrder = Orders[index];
                }
                else
                {
                    SelectedOrder = null;
                }

            }
            catch(InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка удаления",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        protected virtual OrderViewModel CreateOrderViewModel(Order order)
            => new OrderViewModel(
                order,
                    _orderService.GetAllCustomers().ToObservableCollection(),
                    _orderService.GetAllEmployees().ToObservableCollection(),
                    _orderService.GetAllConstructions().ToObservableCollection());

        protected virtual bool? ShowOrderDialog(OrderViewModel orderViewModel)
            => new OrderWindow(orderViewModel).ShowDialog();
    }
}
