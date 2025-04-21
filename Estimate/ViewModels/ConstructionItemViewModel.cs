using Estimate.Models;
using Estimate.Services;

namespace Estimate.ViewModels
{
    public partial class ConstructionItemViewModel : ItemViewModel<Construction>
    {
        public ConstructionItemViewModel(ConstructionService service) : base(service)
        {
        }

        public ConstructionItemViewModel(ConstructionService service, Construction item) : base(service, item)
        {
        }
    }
}
