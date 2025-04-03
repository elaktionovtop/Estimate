using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Estimate.Services;
using Estimate.Views;

namespace Estimate.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string _login = string.Empty;

        //[ObservableProperty]
        //private string _password = "";

        public LoginViewModel(IAuthService authService)
            => _authService = authService;

        [RelayCommand]
        private void CheckLogin(PasswordBox passwordBox)
        {
            if(_authService.Login(Login, passwordBox.Password))
            {
                new MainWindow().Show();
                Application.Current.Windows.OfType<LoginWindow>().First().Close();
            }
        }
    }
}
