using Estimate.ViewModels;

using Microsoft.Extensions.DependencyInjection;

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
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderItemWindow : Window
    {
        public OrderItemWindow(OrderItemViewModel orderViewModel)
        {
            InitializeComponent();
            DataContext = orderViewModel;
        }

        private void DescriptionTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            DescriptionTextBox.Focus();
            DescriptionTextBox.SelectAll();
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
