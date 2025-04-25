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
    public class MeasureUnitListViewModel : ItemListViewModel<MeasureUnit>
    {
        public MeasureUnitListViewModel(MeasureUnitService service) : base(service)
        {
        }

        protected override MeasureUnitItemViewModel CreateItemViewModel(CrudService<MeasureUnit> service, MeasureUnit item)
        {
            return new MeasureUnitItemViewModel((MeasureUnitService)service, item);
        }

        protected override MeasureUnitItemViewModel CreateItemViewModel(CrudService<MeasureUnit> service)
        {
            return new MeasureUnitItemViewModel((MeasureUnitService)service);
        }

        protected override bool? ShowItemDialog(ItemViewModel<MeasureUnit> itemViewModel)
            => new MeasureUnitItemWindow((MeasureUnitItemViewModel)itemViewModel).ShowDialog();
    }
}

