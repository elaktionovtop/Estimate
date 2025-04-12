using CommunityToolkit.Mvvm.Input;

using Estimate.Data;
using Estimate.Models;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Estimate.Services;

namespace Estimate.ViewModels
{
    public partial class OrderViewModel : ObservableObject
    {
        IOrderService _orderService;

        [ObservableProperty]
        private List<Customer> _customers;
        [ObservableProperty]
        private Customer? _selectedCustomer;

        [ObservableProperty]
        private List<Employee> _employees;
        [ObservableProperty]
        private Employee? _selectedEmployee;

        [ObservableProperty]
        private List<Construction> _constructions;
        [ObservableProperty]
        private Construction? _selectedConstruction;

        [ObservableProperty]
        private List<OrderStatus> _statusList;
        [ObservableProperty]
        private OrderStatus _currentStatus;

        [ObservableProperty]
        private DateOnly _creationDate;
        [ObservableProperty]
        private DateOnly? _completionDate;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private bool _isOK = false;

        public Order Order { get; set; }

        //public Order Order => new Order
        //{
        //    Customer = SelectedCustomer,
        //    Employee = SelectedEmployee,
        //    Construction = SelectedConstruction,
        //    Status = CurrentStatus,
        //    CreationdDate = CreationDate,
        //    CompletionDate = CompletionDate,
        //    Description = Description
        //};

        // кнопка ОК должна быть доступна или нет
        // в зависимости от этой переменной !!! ???

        public OrderViewModel(IOrderService orderService)
            => _orderService = orderService;

        public OrderViewModel(IOrderService orderService, Order order)
        {
            _orderService = orderService;
            Order = order;
        }

        public OrderViewModel(Order order)
        {
            SelectedCustomer = order.Customer;
            SelectedEmployee = order.Employee;
            SelectedConstruction = order.Construction;
            CurrentStatus = order.Status;
            CreationDate = order.CreationdDate;
            CompletionDate = order.CompletionDate;
            Description = order.Description;
        }

        // как закрыть окно из ViewModel ?
        [RelayCommand]
        private void Complete()
        {
            Order.Customer = SelectedCustomer;
            Order.Employee = SelectedEmployee;
            Order.Construction = SelectedConstruction;
            Order.Status = CurrentStatus;
            Order.CreationdDate = CreationDate;
            Order.CompletionDate = CompletionDate;
            Order.Description = Description;

            IsOK = _orderService.IsValid(Order);
        }

        // закрытие окна по кнопке закрытия
        // или триггер на кнопку Canscek ???
        //[RelayCommand]
        //private void Cancel()
        //{
        //}

        bool CheckData()
        {
            bool result = true;
            result = result && string.IsNullOrEmpty(Description);
            return result;
        }

        [RelayCommand]
        private void AddWork()
        {
        }

        [RelayCommand]
        private void DeleteWork()
        {
        }

        [RelayCommand]
        private void AddMaterial()
        {
        }

        [RelayCommand]
        private void DeleteMaterial()
        {
        }
    }
}
