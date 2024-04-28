using Plugin.Maui.KeyListener;
using TheIslandPostPhotoManager.ViewModels;

namespace TheIslandPostPhotoManager.Views;

public partial class ImageViewerPage : ContentPage
{
    KeyboardBehavior keyboardBehavior = new();
    public ImageViewerPage(ImageViewerPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        keyboardBehavior.KeyDown += GetOnKeyDown();
       // keyboardBehavior.KeyUp += OnKeyUp;
        this.Behaviors.Add(keyboardBehavior);

        base.OnNavigatedTo(args);
    }

    private EventHandler<KeyPressedEventArgs> GetOnKeyDown()
    {
        return OnKeyDown;
    }

    private void OnKeyDown(object? sender, KeyPressedEventArgs e)
    {
        throw new NotImplementedException();
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        keyboardBehavior.KeyDown -= OnKeyDown;
        //keyboardBehavior.KeyUp -= OnKeyUp;
        this.Behaviors.Remove(keyboardBehavior);

        base.OnNavigatedFrom(args);
    }

    //void OnKeyUp(object sender, KeyPressedEventArgs args)
    //{
    //    //args.Keys = KeyboardKeys.LeftArrow
    //    // do something
    //}

    //void OnKeyDown(object sender, KeyPressedEventArgs args)
    //{
    //    // do something
    //}

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {

    }

    private void PendingCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        selectedCheckBox.IsChecked = true;
    }
}