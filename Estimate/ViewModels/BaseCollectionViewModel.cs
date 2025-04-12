using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using Estimate.Data;

namespace Estimate.ViewModels
{
    public partial class BaseCollectionViewModel<T> : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<T> _items = new();

        [ObservableProperty]
        private T? _selectedItem;

        public BaseCollectionViewModel()
        {
        }

        // конструктор получает данные из БД
        public BaseCollectionViewModel(IEnumerable<T> items)
        {
            Items = new ObservableCollection<T>(items);
            if(Items.Count > 0)
            {
                SelectedItem = Items[0];
            }
            Items.CollectionChanged += Items_CollectionChanged;
        }

        // добавления и удаления Items передаем в репозиторий БД
        public void Items_CollectionChanged(object? sender,
                NotifyCollectionChangedEventArgs e)
        {
            Debug.WriteLine(e.Action);
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if(e.NewItems is not null)
                    {
                        foreach(var item in e.NewItems)
                        {
                            //App.Repository.Add(item);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if(e.OldItems is not null)
                    {
                        foreach(var item in e.OldItems)
                        {
                            //App.Repository.Remove(item);
                        }
                    }
                    break;
            }
        }
    }
}