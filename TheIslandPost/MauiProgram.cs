using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using TheIslandPost.ViewModels;
using TheIslandPost.Views;

namespace TheIslandPost;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureSyncfusionCore()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

        var services = builder.Services;

        services.AddSingleton<MainPageViewModel>();
        services.AddSingleton<MainPage>();

        services.TryAddTransient<HomePageViewModel>();
        services.AddTransient<HomePageView>();

        services.AddTransient<PassenegerInfoPageViewModel>();
        services.AddTransient<PassenegerInfoPageView>();

        services.AddTransient<FinalizedPageViewModel>();
        services.AddTransient<FinalizePageView>();

        return builder.Build();
    }
}
