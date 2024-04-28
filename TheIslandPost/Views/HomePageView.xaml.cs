using TheIslandPost.ViewModels;

namespace TheIslandPost.Views;

public partial class HomePageView : ContentPage
{
	public HomePageView(HomePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}