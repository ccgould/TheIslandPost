using TheIslandPost.ViewModels;

namespace TheIslandPost.Views;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}

