using TheIslandPostPhotoManager.ViewModels;

namespace TheIslandPostPhotoManager;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private void MyColview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if(e.CurrentSelection.Count >= 1)
        {
            var image = e.CurrentSelection[0] as ImageObj;

            if (image.IsSelected)
            {
                selectBtn.Text = "Deselect";
            }
            else
            {
                selectBtn.Text = "Select";
            }
        }

    }

    private void selectBtn_Clicked(object sender, EventArgs e)
    {
        if(MyColview.SelectedItem is not null)
        {
            var image = MyColview.SelectedItem as ImageObj;
            if (image.IsSelected)
            {
                selectBtn.Text = "Deselect";
            }
            else
            {
                selectBtn.Text = "Select";
            }

        }
    }

    private void deSelect_Clicked(object sender, EventArgs e)
    {
        selectBtn.Text = "Select";
    }

    private void selectAll_Clicked(object sender, EventArgs e)
    {
        if (MyColview.SelectedItem is not null)
        {
            selectBtn.Text = "Deselect";
        }
    }
}

