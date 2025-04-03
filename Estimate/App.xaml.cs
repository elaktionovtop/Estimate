using Estimate.Data;
using Estimate.Services;
using Estimate.ViewModels;
using Estimate.Views;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace Estimate;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();

        // Регистрируем зависимости
        //services.AddDbContext<AppDbContext>(opti);
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database\\EstimateDB.mdf"),
            ServiceLifetime.Singleton); services.AddSingleton<IAuthService, AuthService>();
        services.AddTransient<LoginViewModel>();
        services.AddTransient<LoginWindow>();

        services.AddSingleton<IOrderService, OrderService>();

        // Инициализация БД
        var context = new AppDbContext();
        DbInitializer.Initialize(context); // Добавляем тестовых пользователей
        context.Dispose();

        Services = services.BuildServiceProvider();
        Services.GetRequiredService<LoginWindow>().Show();
    }
}
