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

using Microsoft.Extensions.DependencyInjection;

namespace Estimate.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string _login = "admin";

        [ObservableProperty]
        private int _attemptsCount = 3;

        public LoginViewModel(IAuthService authService)
            => _authService = authService;

        [RelayCommand]
        private void CheckLogin(PasswordBox passwordBox)
        {
            if (--AttemptsCount == 0)
            {
                MessageBox.Show("Число возможных попыток исчерпано");
                Application.Current.Shutdown();
            }

            if (_authService.IsLoginValid(Login, passwordBox.Password))
            {
                App.Services.GetRequiredService<MainWindow>().Show();
                Application.Current.Windows
                    .OfType<LoginWindow>().First().Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }
        [RelayCommand]
        private void Cancel(PasswordBox passwordBox)
        {
            Application.Current.Shutdown();
        }
    }
}
