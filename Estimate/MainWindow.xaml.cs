using Estimate.ViewModels;
using Microsoft.Extensions.DependencyInjection;

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Estimate;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = App.Services.GetRequiredService<MainViewModel>();
    }

    private void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        DataGrid grid = sender as DataGrid;
        if(grid.SelectedItem is not null)
        {
            grid.ScrollIntoView(grid.SelectedItem);
        }
    }

    private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        var vm = DataContext as MainViewModel;

        if(e.Key == Key.Enter && vm?.EditOrderCommand.CanExecute(null) == true)
        {
            vm.EditOrderCommand.Execute(null);
            e.Handled = true;
        }

        if(e.Key == Key.Delete && vm?.DeleteOrderCommand.CanExecute(null) == true)
        {
            vm.DeleteOrderCommand.Execute(null);
            e.Handled = true;
        }
    }

}