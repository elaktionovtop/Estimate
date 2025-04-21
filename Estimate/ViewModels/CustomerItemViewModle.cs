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
    public partial class CustomerItemViewModel : ItemViewModel<Customer>
    {
        public CustomerItemViewModel(CustomerService service) : base(service)
        {
        }

        public CustomerItemViewModel(CustomerService service, Customer item)
            : base(service, item)
        {
        }
    }
}
