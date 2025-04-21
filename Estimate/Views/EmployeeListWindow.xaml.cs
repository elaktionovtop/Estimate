using Estimate.Models;
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
    public partial class EmployeeListWindow : Window
    {
        public EmployeeListWindow()
        {
            InitializeComponent();
            DataContext = App.Services
                .GetRequiredService<EmployeeListViewModel>();
        }

        private void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if(grid.SelectedItem is not null)
            {
                grid.ScrollIntoView(grid.SelectedItem);
                grid.Focus();
            }
        }

        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var vm = DataContext as EmployeeListViewModel;

            if(e.Key == Key.Enter
                && vm?.EditItemCommand.CanExecute(null) == true)
            {
                vm.EditItemCommand.Execute(null);
                e.Handled = true;
            }

            if(e.Key == Key.Delete
                && vm?.DeleteItemCommand.CanExecute(null) == true)
            {
                vm.DeleteItemCommand.Execute(null);
                e.Handled = true;
            }
        }
    }
}


