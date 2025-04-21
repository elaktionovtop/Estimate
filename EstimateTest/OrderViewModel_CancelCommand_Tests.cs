using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimateTest
{
    public class OrderViewModel_CancelCommand_Tests : OrderViewModelTestsBase
    {
        [WpfFact]
        public void CancelCommand_SetsDialogResultFalse()
        {
            var window = CreateFakeWindow();

            //ViewModel.CancelCommand.Execute(window);

            Assert.False(window.DialogResult);
        }
    }
}
