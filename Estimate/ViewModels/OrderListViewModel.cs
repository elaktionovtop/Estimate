using CommunityToolkit.Mvvm.Input;

using Estimate.Models;
using Estimate.Services;
using Estimate.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estimate.ViewModels
{
    public partial class OrderListViewModel : ItemListViewModel<Order>
    {
        public OrderListViewModel(OrderService service) : base(service)
        {
        }

        [RelayCommand]
        private void LoadCustomers() 
            => new CustomerListWindow().ShowDialog();

        [RelayCommand]
        private void LoadConstractions()
        {
            new ConstructionListWindow().ShowDialog();
        }

        [RelayCommand]
        private void LoadEmployees()
        {
            new EmployeeListWindow().ShowDialog();
        }

        [RelayCommand]
        private void LoadMeasureUnits()
        {
            new MeasureUnitListWindow().ShowDialog();
        }

        [RelayCommand]
        private void LoadWorks()
        {
            new WorkListWindow().ShowDialog();
        }

        protected override OrderItemViewModel CreateItemViewModel(CrudService<Order> service, Order item)
        {
            return new OrderItemViewModel((OrderService)service, item);
        }

        protected override OrderItemViewModel CreateItemViewModel(CrudService<Order> service)
        {
            return new OrderItemViewModel((OrderService)service);
        }

        protected override bool? ShowItemDialog(ItemViewModel<Order> itemViewModel)
            => new OrderItemWindow((OrderItemViewModel)itemViewModel).ShowDialog();
    }
}
