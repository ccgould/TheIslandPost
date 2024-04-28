using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using Plugin.Maui.KeyListener;
using Syncfusion.Maui.Core.Hosting;
using TheIslandPostPhotoManager.Services;
using TheIslandPostPhotoManager.ViewModels;
using TheIslandPostPhotoManager.Views;
using UraniumUI;
//using TheIslandPostPhotoManager.Views;
namespace TheIslandPostPhotoManager;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureSyncfusionCore()
            .UseKeyListener()
            // Initialize the .NET MAUI Community Toolkit by adding the below line of code
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFontAwesomeIconFonts(); // 👈 Add this line
            });

        var service = builder.Services;

        service.AddSingleton<ImageService>();

        service.AddSingleton<MainPage>();
        service.AddSingleton<MainPageViewModel>();

        service.AddTransient<ImageViewerPage>();
        service.AddTransient<ImageViewerPageViewModel>();

        service.AddTransient<OrderCompletionPage>();
        service.AddTransient<OrderCompletionPageViewModel>();

        

        //service.AddTransientPopup<ImageViewerPage, ImageViewerPageViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
