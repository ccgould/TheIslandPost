using TheIslandPostPhotoManager.Views;

namespace TheIslandPostPhotoManager;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(ImageViewerPage), typeof(ImageViewerPage));
        Routing.RegisterRoute(nameof(OrderCompletionPage), typeof(OrderCompletionPage));
    }
}
