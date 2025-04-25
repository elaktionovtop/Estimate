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
    public partial class WorkListViewModel : ItemListViewModel<Work>
    {
        public WorkListViewModel(WorkService service) : base(service)
        {
        }

        protected override WorkItemViewModel CreateItemViewModel(CrudService<Work> service, Work item)
        {
            return new WorkItemViewModel((WorkService)service, item);
        }

        protected override WorkItemViewModel CreateItemViewModel(CrudService<Work> service)
        {
            return new WorkItemViewModel((WorkService)service);
        }

        protected override bool? ShowItemDialog(ItemViewModel<Work> itemViewModel)
            => new WorkItemWindow((WorkItemViewModel)itemViewModel).ShowDialog();
    }
}

