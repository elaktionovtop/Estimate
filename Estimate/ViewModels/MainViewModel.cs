using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Estimate.Models;
using Estimate.Services;
using Estimate.Views;

namespace Estimate.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        OrderService _orderService;

        [ObservableProperty]
        ObservableCollection<Order> _orders;

        [ObservableProperty]
        Order _currentOrder;

        [ObservableProperty]
        string _description;

        [RelayCommand]
        private void CreateOrder()
        {
            try
            {
                _orderService.CreateOrder(new Order
                {
                    Description = Description,
                    //UserId = _authService.CurrentUser.Id
                });
                Orders.Add(CurrentOrder); // Обновляем UI
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
