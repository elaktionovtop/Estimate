using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using Estimate.Models;
using Estimate.Services;
using System.Windows.Controls;
using Estimate.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Estimate.ViewModels
{
    public partial class OrderViewModel : ObservableObject
    {
        IOrderService _orderService = App.Services.GetRequiredService<IOrderService>();

        public Order Order { get; }

        public ObservableCollection<Customer> Customers { get; }
        public ObservableCollection<Employee> Employees { get; }
        public ObservableCollection<Construction> Constructions { get; }

        public ObservableCollection<EnumDisplay<OrderStatus>>
            Statuses { get; } = Order.Statuses;

        [ObservableProperty]
        private EnumDisplay<OrderStatus> selectedStatus;

        [ObservableProperty] private Customer? selectedCustomer;
        [ObservableProperty] private Employee? selectedEmployee;
        [ObservableProperty] private Construction? selectedConstruction;
        [ObservableProperty] private DateTime creationDateTime;
        [ObservableProperty] private DateTime? completionDateTime;
        [ObservableProperty] private string description = string.Empty;

        public OrderViewModel(
            Order order,
            ObservableCollection<Customer> customers,
            ObservableCollection<Employee> employees,
            ObservableCollection<Construction> constructions)
        {
            Order = order;

            Customers = customers;
            Employees = employees;
            Constructions = constructions;

            // заполняем поля из модели
            SelectedCustomer = order.Customer;
            SelectedEmployee = order.Employee;
            SelectedConstruction = order.Construction;
            SelectedStatus = Statuses.FirstOrDefault
                (s => s.Value == order.Status)
                ?? Statuses.First();
            CreationDateTime = order.CreationDateTime;
            CompletionDateTime = order.CompletionDateTime;
            Description = order.Description;
        }

        [RelayCommand]
        private void Apply(Window window)
        {
            try
            {
                Order.CustomerId = SelectedCustomer?.Id ?? 0;
                Order.EmployeeId = SelectedEmployee?.Id ?? 0;
                Order.ConstructionId = SelectedConstruction?.Id ?? 0;
                Order.Customer = SelectedCustomer!;
                Order.Employee = SelectedEmployee!;
                Order.Construction = SelectedConstruction!;
                Order.Status = SelectedStatus.Value;
                Order.CreationDateTime = CreationDateTime;
                Order.CompletionDateTime = CompletionDateTime;
                Order.Description = Description;

                window.DialogResult = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
