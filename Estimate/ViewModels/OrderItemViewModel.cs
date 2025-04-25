using CommunityToolkit.Mvvm.ComponentModel;

using Estimate.Models;
using Estimate.Services;

using Microsoft.Extensions.DependencyInjection;

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
        CustomerService _customerService =
            App.Services.GetRequiredService<CustomerService>();
        EmployeeService _employeeService =
            App.Services.GetRequiredService<EmployeeService>();
        ConstructionService _constructionService =
            App.Services.GetRequiredService<ConstructionService>();

        public ObservableCollection<Customer> Customers { get; }

        public ObservableCollection<Employee> Employees { get; }

        public ObservableCollection<Construction> Constructions { get; }

        public ObservableCollection<EnumDisplay<OrderStatus>>Statuses
            { get; } = Order.Statuses;

        [ObservableProperty]
        private EnumDisplay<OrderStatus> _selectedStatus;

        [ObservableProperty]
        private ObservableCollection<OrderWork> _orderWorks;

        [ObservableProperty]
        private OrderWork? _selectedOrderWork;

        [ObservableProperty]
        private OrderWorkListViewModel _orderWorkListViewModel;

        public OrderItemViewModel(OrderService service) : base(service) 
        {
            Customers = _customerService
                .GetAll().ToObservableCollection();
            Employees = _employeeService
                .GetAll().ToObservableCollection();
            Constructions = _constructionService
                .GetAll().ToObservableCollection();

            SelectedStatus = Statuses[0];

            OrderWorkListViewModel = App.Services
                .GetRequiredService<OrderWorkListViewModel>();
            OrderWorkListViewModel.OrderId = Item.Id;
        }

        public OrderItemViewModel(OrderService service, Order order) 
            : base(service, order) 
        {
            Customers = _customerService
                .GetAll().ToObservableCollection();
            Employees = _employeeService
                .GetAll().ToObservableCollection();
            Constructions = _constructionService
                .GetAll().ToObservableCollection();

            SelectedStatus = Statuses.FirstOrDefault
                (s => s.Value == order.Status)
                ?? Statuses.First();

            OrderWorkListViewModel = App.Services
                .GetRequiredService<OrderWorkListViewModel>();
            OrderWorkListViewModel.OrderId = Item.Id;
        }

        public override void OnApply() 
        {
            if(SelectedStatus is not null)
            {
                Item.Status = SelectedStatus?.Value 
                    ?? OrderStatus.New;
            }
            Item.CustomerId = Item?.Customer?.Id ?? 0;
            Item.EmployeeId = Item?.Employee?.Id ?? 0;
            Item.ConstructionId = Item?.Construction?.Id ?? 0;
        }
    }
}
