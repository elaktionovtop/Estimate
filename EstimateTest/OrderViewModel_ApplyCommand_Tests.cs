using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimateTest
{
    public class OrderViewModel_ApplyCommand_Tests : OrderViewModelTestsBase
    {
        [WpfFact]
        public void ApplyCommand_SetsDialogResultTrue()
        {
            // Arrange
            var window = CreateFakeWindow();
            window.Show();

            // Act
            ViewModel.ApplyCommand.Execute(window);

            // Assert
            Assert.True(window.DialogResult);
        }
    }
}
