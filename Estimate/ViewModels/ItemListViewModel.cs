using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Estimate.Models;
using Estimate.Services;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Estimate.Views;
using System.Windows.Data;
using System.Windows.Threading;

namespace Estimate.ViewModels
{
    public abstract partial class ItemListViewModel<T> : ObservableObject 
        where T : class, new()
    {
        readonly CrudService<T> _service;

        [ObservableProperty]
        protected ObservableCollection<T> _items;

        [ObservableProperty]
        protected T? _selectedItem;

        public ItemListViewModel(CrudService<T> service)
        {
            _service = service;
            Items = _service.GetAll().ToObservableCollection();
            if(Items.Count > 0)
                SelectedItem = Items[0];
        }

        [RelayCommand]
        private void CreateItem()
        {
            var itemViewModel = CreateItemViewModel(_service);
            try
            {
                if(ShowItemDialog(itemViewModel) == true)
                {
                    _service.Add(itemViewModel.Item);
                    Items.Add(itemViewModel.Item);
                    SelectedItem = itemViewModel.Item;
                }
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message, 
                    "Ошибка в данных при добавление объекта",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        [RelayCommand]
        private void EditItem()
        {
            if(SelectedItem is null)
                return;

            var itemViewModel = CreateItemViewModel(_service, SelectedItem);
            try
            {
                if(ShowItemDialog(itemViewModel) == true)
                {
                    // а если ошибки - останутся в таблице?
                    _service.Update(SelectedItem);
                    CollectionViewSource.GetDefaultView(Items).Refresh();
                }
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message, 
                    "Ошибка в данных при изменении объекта",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        [RelayCommand]
        private void DeleteItem()
        {
            if(SelectedItem is null)
                return;

            try
            {
                int index = Items.IndexOf(SelectedItem);
                _service.Remove(SelectedItem);
                Items.Remove(SelectedItem);

                // установить новый выбор
                if(Items.Count > 0)
                {
                    if(index >= Items.Count)
                        index = Items.Count - 1;

                    SelectedItem = Items[index];
                }
                else
                {
                    SelectedItem = null;
                }
            }
            catch(InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка удаления",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        protected abstract ItemViewModel<T> CreateItemViewModel
            (CrudService<T> service, T item);

        protected abstract ItemViewModel<T> CreateItemViewModel
            (CrudService<T> service);

        protected abstract bool? ShowItemDialog(ItemViewModel<T> itemViewModel);
    }
}
