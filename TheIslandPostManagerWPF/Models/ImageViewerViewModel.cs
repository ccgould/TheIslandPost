using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheIslandPostManagerWPF.Services;
using Wpf.Ui.Controls;


namespace TheIslandPostManagerWPF.Models;

public partial class ImageViewerViewModel : ObservableObject
{
    [ObservableProperty] private ImageService service;
    public ImageViewerViewModel(ImageService service)
    {
        Service = service;
        //service.SelectedImage.HDImage = service.LoadImageFile(service.SelectedImage.ImageUrl, true);
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
    private void SelectPhoto()
    {
        if (Service.SelectedImage is not null)
        {
            Service.SelectedImage.IsSelected = !Service.SelectedImage.IsSelected;
        }
    }

    [RelayCommand]
    private void DeSelectPhoto()
    {
        if (Service.SelectedImage is not null)
        {
            Service.SelectedImage.IsSelected = !Service.SelectedImage.IsSelected;
        }
    }

    [RelayCommand]
    private void SetAsMaybe()
    {
        if (Service.SelectedImage is not null)
        {
            Service.SelectedImage.IsPending = !Service.SelectedImage.IsPending;
        }
    }

    [RelayCommand]
    private void PrintPhoto()
    {
        if (Service.SelectedImage is not null)
        {
            Service.SelectedImage.IsPrintable = !Service.SelectedImage.IsPrintable;
        }
    }

    [RelayCommand]
    private async Task DeletePhoto(object obj)
    {
        if (Service.SelectedImage is not null)
        {
            await Service.DeleteImage(Service.SelectedImage);
            NextPhoto();
        }
    }
}
