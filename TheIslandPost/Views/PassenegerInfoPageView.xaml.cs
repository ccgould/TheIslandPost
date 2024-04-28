using TheIslandPost.ViewModels;

namespace TheIslandPost.Views;

public partial class PassenegerInfoPageView : ContentPage
{
	public PassenegerInfoPageView(PassenegerInfoPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}