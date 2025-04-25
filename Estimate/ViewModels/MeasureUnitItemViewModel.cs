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
    public partial class MeasureUnitItemViewModel
 : ItemViewModel<MeasureUnit>
    {
        public MeasureUnitItemViewModel(MeasureUnitService service) : base(service)
        {
        }

        public MeasureUnitItemViewModel(MeasureUnitService service, MeasureUnit item)
            : base(service, item)
        {
        }
    }
}

