using Estimate.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Estimate.Views
{
    public partial class OrderWorkItemWindow : Window
    {
        public OrderWorkItemWindow(OrderWorkItemViewModel orderWorkItemViewModel)
        {
            InitializeComponent();
            DataContext = orderWorkItemViewModel;
        }

        private void NameTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            WorkComboBox.Focus();
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            // Принудительное обновление binding'а у текущего элемента
            var element = FocusManager.GetFocusedElement(this) as FrameworkElement;
            if(element != null)
            {
                var binding = BindingOperations.GetBindingExpression(element, TextBox.TextProperty);
                binding?.UpdateSource();
            }
        }
    }
}

