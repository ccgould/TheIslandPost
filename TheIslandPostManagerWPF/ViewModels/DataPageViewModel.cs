using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheIslandPostManagerWPF.Models;
using TheIslandPostManagerWPF.Pages;
using TheIslandPostManagerWPF.Services;
using TheIslandPostManagerWPF.Views;
using Wpf.Ui;
using Wpf.Ui.Controls;
using MessageBox = Wpf.Ui.Controls.MessageBox;

namespace TheIslandPostManagerWPF.ViewModels;
public partial class DataPageViewModel : ObservableObject
{
    [ObservableProperty] private ImageService service;
    private readonly INavigationService _navigationService;

    public DataPageViewModel(INavigationService navigationService, ImageService service)
    {
        Service = service;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task ImageClick(ImageObj image)
    {
        Service.SelectedImage = image;
        Service.SelectedImage.HDImage = image.Image;
        _ = _navigationService.Navigate(typeof(ImageViewer));
        Service.SelectedImage.HDImage = await Service.LoadHDImage(image.ImageUrl);
    }

    [RelayCommand]
    private void SelectImageClick(ImageObj image)
    {
        image.IsSelected = !image.IsSelected;
    }

    [RelayCommand]
    private async Task DeleteImageClick(ImageObj image)
    {
        await Service.DeleteImage(image);
    }

    [RelayCommand]
    private void SelectAsMaybeClick(ImageObj image)
    {
        image.IsPending = !image.IsPending;
    }

    [RelayCommand]
    private void PrintImageClick(ImageObj image)
    {
       image.IsPrintable = !image.IsPrintable;
    }

    internal async Task FinializeOrder()
    {
        if(Service.Images.Any(x =>x.IsPending))
        {
            var uiMessageBox = new MessageBox
            {
                Title = "Maybe images.",
                Content = $"You have {Service.Images.Count(x => x.IsPending)} of maybe images. Please remove or select any maybe images before procceding!",
                CloseButtonText = "OK"
            };
            await uiMessageBox.ShowDialogAsync();
            return;
        }

        if (!Service.Images.Any(x => x.IsSelected))
        {
            var uiMessageBox = new MessageBox
            {
                Title = "No Images Selected",
                Content = $"You have no images selected. Would you like to select and print all images?",
                IsPrimaryButtonEnabled = true,
                PrimaryButtonText = "Yes",
                CloseButtonText = "No"
            };

            var result = await uiMessageBox.ShowDialogAsync();


            if (result == MessageBoxResult.Primary)
            {
                Service.SelectAllImages();
                Service.PrintAllImages();
                _ = _navigationService.Navigate(typeof(FinalizePageView));
            }
            else
            {
                return;
            }
        }
        else
        {
            _ = _navigationService.Navigate(typeof(FinalizePageView));
        }
    }

    internal async Task CancelOrder()
    {
        var uiMessageBox = new MessageBox
        {
            Title = "Cancel Order",
            Content = $"Are you sure you would like to cancel this order. This cannot be undone?",
            IsPrimaryButtonEnabled = true,
            PrimaryButtonText = "Yes",
            CloseButtonText = "No"
        };

        var result = await uiMessageBox.ShowDialogAsync();


        if (result == MessageBoxResult.Primary)
        {
            Service.ClearImages();
            _ = _navigationService.Navigate(typeof(DashboardPage));
        }
        else
        {
            return;
        }
    }
}
