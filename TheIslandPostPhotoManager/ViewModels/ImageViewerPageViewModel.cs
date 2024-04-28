using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheIslandPostPhotoManager.Services;

namespace TheIslandPostPhotoManager.ViewModels;
//[QueryProperty(nameof(SelectedImage), "SelectedImage")]
public partial class ImageViewerPageViewModel : ObservableObject
{
    [ObservableProperty] private ImageService service;
    public ImageViewerPageViewModel(ImageService service)
    {
        this.service = service;
    }

    //[ObservableProperty] private ImageObj selectedImage;

    internal void SetImage(ImageObj obj)
    {
        Service.SelectedImage = obj;
    }

    [RelayCommand]
    private void SelectPhoto()
    {
        if(Service.SelectedImage is not null)
        {
            Service.SelectedImage.IsSelected = true;
        }
    }

    [RelayCommand]
    private void DeSelectPhoto()
    {
        if (Service.SelectedImage is not null)
        {
            Service.SelectedImage.IsSelected = false;
        }
    }

    [RelayCommand]
    private void PreviousPhoto()
    {
        Service.ChangeSelectedImage(false);
    }

    [RelayCommand]
    private void NextPhoto()
    {
        Service.ChangeSelectedImage(true);
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }
}
