using Estimate.Data;
using Estimate.Models;
using Estimate.Services;
using Estimate.ViewModels;
using Estimate.Views;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

using static Estimate.Data.AppDbContext;

namespace Estimate;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();

        // Регистрируем зависимости
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(ContextFactory.ConnectionString),
            contextLifetime: ServiceLifetime.Singleton,
            optionsLifetime: ServiceLifetime.Singleton);

        services.AddSingleton<IAuthService, AuthService>();
           services.AddTransient<LoginViewModel>();
           services.AddTransient<LoginWindow>();

        //services.AddSingleton<ICrudService<Customer>, CustomerService>();
        //services.AddSingleton<ICrudService
        //    <Construction>, ConstructionService>();
        //services.AddSingleton<ICrudService<Employee>, EmployeeService>();

        services.AddSingleton<OrderService, OrderService>();
        services.AddTransient<OrderListViewModel>();
        services.AddTransient<MainWindow>();

        services.AddSingleton<CustomerService, CustomerService>();
        services.AddTransient<CustomerListViewModel>();
        services.AddTransient<CustomerListWindow>();

        services.AddSingleton<ConstructionService, ConstructionService>();
        services.AddTransient<ConstructionListViewModel>();
        services.AddTransient<ConstructionListWindow>();

        services.AddSingleton<EmployeeService, EmployeeService > ();
        services.AddTransient <EmployeeListViewModel > ();
        services.AddTransient <EmployeeListWindow > ();


        Services = services.BuildServiceProvider();

        // Проверка данных БД
        DbVerification.Perform(); 

        // для отладки отключаем авторизацию
        //Services.GetRequiredService<LoginWindow>().Show();

        App.Services.GetRequiredService<MainWindow>().Show();

    }
}

