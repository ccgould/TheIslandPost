using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Windows;
using System.Windows.Threading;
using TheIslandPostManagerWPF.Models;
using TheIslandPostManagerWPF.Pages;
using TheIslandPostManagerWPF.Services;
using TheIslandPostManagerWPF.ViewModels;
using TheIslandPostManagerWPF.Views;
using Wpf.Ui;
using FluentEmail.Core;

namespace TheIslandPostManagerWPF;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    private static readonly IHost _host = Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration(c =>
        {
            c.SetBasePath(Path.GetDirectoryName(AppContext.BaseDirectory));
        })
        .ConfigureServices(
            (context, services) =>
            {
                services.AddSingleton<ISnackbarService, SnackbarService>();

                // App Host
                services.AddHostedService<ApplicationHostService>();

                // Page resolver service
                services.AddSingleton<IPageService, PageService>();

                // Theme manipulation
                services.AddSingleton<IThemeService, ThemeService>();

                // TaskBar manipulation
                services.AddSingleton<ITaskBarService, TaskBarService>();

                services.AddSingleton<ApplicationSaveService>();

                services.AddSingleton<ImageService>();

                services.AddSingleton<EmailService>();

                // Service containing navigation, same as INavigationWindow... but without window
                services.AddSingleton<INavigationService, NavigationService>();

                // Main window with navigation
                services.AddSingleton<INavigationWindow, MainWindow>();
                services.AddSingleton<MainWindowViewModel>();

                services.AddSingleton<IContentDialogService, ContentDialogService>();

                // Views and ViewModels
                services.AddSingleton<DashboardPage>();
                services.AddSingleton<DashBoardViewModel>();
                services.AddSingleton<DataPage>();
                services.AddSingleton<DataPageViewModel>();
                services.AddSingleton<SettingsPage>();
                services.AddSingleton<SettingsPageViewModel>();

                services.AddTransient<ImageViewer>();
                services.AddTransient<ImageViewerViewModel>();

                services.AddTransient<FinalizePageView>();
                services.AddTransient<FinalizeOrderViewModel>();

                services.AddTransient<LogsPageView>();
                services.AddTransient<LogsPageViewModel>();

                services.AddTransient<OrdersPage>();
                services.AddTransient<OrdersPageViewModel>();



                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential("electrotechin@gmail.com", "efom qzwk ocvz aeyh")
                };

      
                services.AddFluentEmail("electrotechin@gmail.com","Creswell Gould")
                        .AddRazorRenderer()
                        .AddSmtpSender(smtp);

                //services.AddTransient<ImageViewerContentDialogPage>();
                //services.AddTransient<ImageViewerContentDialogPageViewModel>();


                // Configuration
                services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
            }
        )
        .Build();

    /// <summary>
    /// Gets registered service.
    /// </summary>
    /// <typeparam name="T">Type of the service to get.</typeparam>
    /// <returns>Instance of the service or <see langword="null"/>.</returns>
    public static T? GetService<T>()
        where T : class
    {
        return _host.Services.GetService(typeof(T)) as T;
    }

    /// <summary>
    /// Occurs when the application is loading.
    /// </summary>
    private async void OnStartup(object sender, StartupEventArgs e)
    {
        await _host.StartAsync();
    }

    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();

        _host.Dispose();
    }

    /// <summary>
    /// Occurs when an exception is thrown by an application but not handled.
    /// </summary>
    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
    }
}

