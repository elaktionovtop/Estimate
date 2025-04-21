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
    public class EmployeeListViewModel : ItemListViewModel<Employee>
    {
        public EmployeeListViewModel(EmployeeService service) : base(service)
        {
        }

        protected override EmployeeItemViewModel CreateItemViewModel(CrudService<Employee> service, Employee item)
        {
            return new EmployeeItemViewModel((EmployeeService)service, item);
        }

        protected override EmployeeItemViewModel CreateItemViewModel(CrudService<Employee> service)
        {
            return new EmployeeItemViewModel((EmployeeService)service);
        }

        protected override bool? ShowItemDialog(ItemViewModel<Employee> itemViewModel)
            => new EmployeeItemWindow((EmployeeItemViewModel)itemViewModel).ShowDialog();
    }
}

