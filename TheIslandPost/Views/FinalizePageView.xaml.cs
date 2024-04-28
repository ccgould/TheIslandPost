using TheIslandPost.ViewModels;

namespace TheIslandPost.Views;

public partial class FinalizePageView : ContentPage
{
	public FinalizePageView(FinalizedPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}