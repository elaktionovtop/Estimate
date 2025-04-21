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
    public class CustomerListViewModel : ItemListViewModel<Customer>
    {
        public CustomerListViewModel(CustomerService service) : base(service)
        {
        }
        
        protected override CustomerItemViewModel CreateItemViewModel(CrudService<Customer> service, Customer item)
        {
            return new CustomerItemViewModel((CustomerService)service, item);
        }

        protected override CustomerItemViewModel CreateItemViewModel(CrudService<Customer> service)
        {
            return new CustomerItemViewModel((CustomerService)service);
        }

        protected override bool? ShowItemDialog(ItemViewModel<Customer> itemViewModel)
            => new CustomerItemWindow((CustomerItemViewModel)itemViewModel).ShowDialog();
    }
}
