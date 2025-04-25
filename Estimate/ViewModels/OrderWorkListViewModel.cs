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
    public class OrderWorkListViewModel : ItemListViewModel<OrderWork>
    {
        public int OrderId { get; set; }

        public OrderWorkListViewModel(OrderWorkService service) : base(service)
        {
        }

        protected override OrderWorkItemViewModel CreateItemViewModel(CrudService<OrderWork> service, OrderWork item)
        {
            return new OrderWorkItemViewModel((OrderWorkService)service, item);
        }

        protected override OrderWorkItemViewModel CreateItemViewModel(CrudService<OrderWork> service)
        {
            return new OrderWorkItemViewModel((OrderWorkService)service, OrderId);
        }

        protected override bool? ShowItemDialog(ItemViewModel<OrderWork> itemViewModel)
            => new OrderWorkItemWindow((OrderWorkItemViewModel)itemViewModel).ShowDialog();
    }
}

