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
    public partial class EmployeeItemViewModel : ItemViewModel<Employee>
    {
        public EmployeeItemViewModel(EmployeeService service) : base(service)
        {
        }

        public EmployeeItemViewModel(EmployeeService service, Employee item)
            : base(service, item)
        {
        }
    }
}

