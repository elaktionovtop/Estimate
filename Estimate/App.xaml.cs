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

        services.AddSingleton<IOrderService, OrderService>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<MainWindow>();

        Services = services.BuildServiceProvider();

        // Проверка данных БД
        DbVerification.Perform(); 

        //Services.GetRequiredService<LoginWindow>().Show();
        App.Services.GetRequiredService<MainWindow>().Show();

    }
}

