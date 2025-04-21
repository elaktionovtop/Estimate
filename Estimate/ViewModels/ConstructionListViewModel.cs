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
    public class ConstructionListViewModel : ItemListViewModel<Construction>
    {
        public ConstructionListViewModel(ConstructionService service) : base(service)
        {
        }

        protected override ConstructionItemViewModel CreateItemViewModel(CrudService<Construction> service, Construction item)
        {
            return new ConstructionItemViewModel((ConstructionService)service, item);
        }

        protected override ConstructionItemViewModel CreateItemViewModel(CrudService<Construction> service)
        {
            return new ConstructionItemViewModel((ConstructionService)service);
        }

        protected override bool? ShowItemDialog(ItemViewModel<Construction> itemViewModel)
            => new ConstructionItemWindow((ConstructionItemViewModel)itemViewModel).ShowDialog();
    }
}
