using CommunityToolkit.Mvvm.ComponentModel;

using Estimate.Models;
using Estimate.Services;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estimate.ViewModels
{
    public partial class OrderItemViewModel : ItemViewModel<Order>
    {
        public ObservableCollection<Customer> Customers { get; }
        [ObservableProperty]
        private Customer? _selectedCustomer;

        public ObservableCollection<Employee> Employees { get; }
        [ObservableProperty]
        private Employee? _selectedEmployee;

        public ObservableCollection<Construction> Constructions { get; }
        [ObservableProperty]
        private Construction? _selectedConstruction;

        public ObservableCollection<EnumDisplay<OrderStatus>>Statuses
        { get; } = Order.Statuses;
        [ObservableProperty]
        private EnumDisplay<OrderStatus> _selectedStatus;

        public OrderItemViewModel(OrderService service) : base(service) 
        {
            Customers = ((OrderService)_service)
                .GetAllCustomers().ToObservableCollection();
            Employees = ((OrderService)_service)
                .GetAllEmployees().ToObservableCollection();
            Constructions = ((OrderService)_service)
                .GetAllConstructions().ToObservableCollection();

            SelectedStatus = Statuses[0];
        }

        public OrderItemViewModel(OrderService service, Order order) 
            : base(service, order) 
        {
            Customers = ((OrderService)_service)
                .GetAllCustomers().ToObservableCollection();
            Employees = ((OrderService)_service)
                .GetAllEmployees().ToObservableCollection();
            Constructions = ((OrderService)_service)
                .GetAllConstructions().ToObservableCollection();

            SelectedStatus = Statuses.FirstOrDefault
                (s => s.Value == order.Status)
                ?? Statuses.First();
        }

        public override void OnApply() 
        {
            if(SelectedStatus is not null)
            {
                Item.Status = SelectedStatus?.Value 
                    ?? OrderStatus.New;
            }
            Item.CustomerId = Item.Customer?.Id ?? 0;
            Item.EmployeeId = Item.Employee?.Id ?? 0;
            Item.ConstructionId = Item.Construction?.Id ?? 0;
        }
    }
}
