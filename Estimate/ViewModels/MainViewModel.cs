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
    public partial class MainViewModel : BaseCollectionViewModel<Order>
    {
       readonly OrderService? _orderService;

        //[ObservableProperty]
        //private ObservableCollection<Order> _items;

        //[ObservableProperty]
        //private Order? _selectedItem;

        //[ObservableProperty]
        //ObservableCollection<Order> _orders;

        //[ObservableProperty]
        //Order _currentOrder;

        //[ObservableProperty]
        //int _orderCustomerId;

        //[ObservableProperty]
        //int _orderEmployeeId;

        //[ObservableProperty]
        //int _orderConstructionId;

        //[ObservableProperty]
        //string _orderDescription;

        //public MainViewModel(ObservableCollection<Order> items) : base(items) 
        //{
        //}

        public MainViewModel() : base
            (new ObservableCollection<Order>())
        {
        }

        public MainViewModel(IOrderService orderService) : base
            (new ObservableCollection<Order>(orderService.GetAllOrders()))
        {
        }

        //[RelayCommand]
        //private void CreateOrder()
        //{
        //    OrderViewModel orderViewModel = new OrderViewModel();
        //    OrderWindow orderWindow = new OrderWindow(orderViewModel);
        //    if(orderWindow.ShowDialog() == true)
        //    {
        //        Order newOrder = orderViewModel.Order;
        //        Items.Add(newOrder);
        //        SelectedItem = newOrder;
        //    }
        //}

        [RelayCommand]
        private void CreateOrder()
        {
            var viewModel = CreateOrderViewModel();

            var window = new OrderWindow(viewModel);
            if(window.ShowDialog() == true)
            {
                Items.Add(viewModel.Order);
                SelectedItem = viewModel.Order;
            }
        }

        [RelayCommand]
        private void DeleteOrder()
        { 
            if (SelectedItem is not null)
            {
                Items.Remove(SelectedItem);
            }
        }

        [RelayCommand]
        private void EditOrder()
        {
            if(SelectedItem is null)
            {
                return;
            }
            OrderViewModel orderViewModel = new OrderViewModel(SelectedItem);
            OrderWindow orderWindow = new OrderWindow(orderViewModel);
            if(orderWindow.ShowDialog() == true)
            {
                Order newOrder = orderViewModel.Order;
                Items.Add(newOrder);
                SelectedItem = newOrder;
            }
        }

        protected virtual OrderViewModel CreateOrderViewModel()
            => new OrderViewModel(new Order());
        
        //[RelayCommand]
        //private void CompleteOrder()
        //{
        //    if(SelectedItem is not null)
        //    {
        //        SelectedItem.Status = OrderStatus.Completed;
        //    }
        //}

        //[RelayCommand]
        //private void CancelOrder()
        //{
        //    if(SelectedItem is not null)
        //    {
        //        SelectedItem.Status = OrderStatus.Cancelled;
        //    }
        //}
    }
}


//try
//{
//    _orderService.CreateOrder(new Order
//    {
//        CustomerId = OrderCustomerId,
//        EmployeeId = OrderEmployeeId,
//        ConstractionId = OrderConstructionId,
//        Description = OrderDescription

//    });
//    Orders.Add(CurrentOrder); // Обновляем UI ???
//}
//catch(Exception ex)
//{
//    MessageBox.Show(ex.Message);
//}

