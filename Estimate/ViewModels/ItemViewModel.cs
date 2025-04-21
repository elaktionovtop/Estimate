using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Estimate.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Estimate.ViewModels
{
    public partial class ItemViewModel<T> : ObservableObject 
        where T : class, new()
    {
        readonly protected CrudService<T> _service ;

        public T Original { get; set; }

        [ObservableProperty]
        public T _item;

        public ItemViewModel(CrudService<T> service, T item)
        {
            _service = service;
            Original = item;
            Item = _service.Clone(item);
        }

        public ItemViewModel(CrudService<T> service)
        {
            _service = service;
            Item = new T();
        }

        public virtual void OnApply() { }

        [RelayCommand]
        public void Apply(Window itemDialog)
        {
            try
            {
                OnApply();
                _service.Validate(Item);
                if(Original != null)
                {
                    _service.CopyTo(Item, Original);
                }
                itemDialog.DialogResult = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
