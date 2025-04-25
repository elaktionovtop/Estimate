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
    public partial class WorkItemViewModel : ItemViewModel<Work>
    {
       MeasureUnitService _unitService =
            App.Services.GetRequiredService<MeasureUnitService>();


        public ObservableCollection<MeasureUnit> Units { get; }
        [ObservableProperty]
        private Customer? _selectedUnit;

        public WorkItemViewModel(WorkService service) : base(service)
        {
            Units = _unitService
                .GetAll().ToObservableCollection();
        }

        public WorkItemViewModel(WorkService service, Work work)
            : base(service, work)
        {
            Units = _unitService
                .GetAll().ToObservableCollection();
        }

        public override void OnApply()
        {
            Item.UnitId = Item?.MeasureUnit?.Id ?? 0;
        }
    }
}

