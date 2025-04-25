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
    public partial class OrderWorkItemViewModel : ItemViewModel<OrderWork>
    {
        WorkService _workService =
            App.Services.GetRequiredService<WorkService>();

        [ObservableProperty]
        private ObservableCollection<Work> _works;

        public OrderWorkItemViewModel(OrderWorkService service, int OrderId) : base(service)
        {
            Item.OrderId = OrderId;
            Works = _workService
                .GetAll().ToObservableCollection();
        }

        public OrderWorkItemViewModel(OrderWorkService service, OrderWork item)
            : base(service, item)
        {
            Works = _workService
                .GetAll().ToObservableCollection();
        }

        public override void OnApply()
        {
            //Item.OrderId = Item?.Order?.Id ?? 0;
            Item.WorkId = Item?.Work?.Id ?? 0;
        }
    }
}
