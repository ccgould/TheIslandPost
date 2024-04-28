using TheIslandPost.Views;

namespace TheIslandPost;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(HomePageView), typeof(HomePageView));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(PassenegerInfoPageView), typeof(PassenegerInfoPageView));
        Routing.RegisterRoute(nameof(FinalizePageView), typeof(FinalizePageView));
    }
}
