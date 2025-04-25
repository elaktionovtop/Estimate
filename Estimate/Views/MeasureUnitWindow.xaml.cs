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
    /// <summary>
    /// Логика взаимодействия для MeasureUnitWindow.xaml
    /// </summary>
    public partial class MeasureUnitItemWindow : Window
    {
        public MeasureUnitItemWindow(MeasureUnitItemViewModel measureUnitItemViewModel)
        {
            InitializeComponent();
            DataContext = measureUnitItemViewModel;
        }

        private void NameTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            NameTextBox.Focus();
            NameTextBox.SelectAll();
        }
    }
}
