using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using TheIslandPostManagerWPF.Models;
using TheIslandPostManagerWPF.Services;
using TheIslandPostManagerWPF.Views;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace TheIslandPostManagerWPF.ViewModels;
public partial class FinalizeOrderViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<ImageObj> selectedImages = new();
    [ObservableProperty] private ImageService imageService;
    private IContentDialogService contentDialogService;
    private INavigationService _navigationService;
    [ObservableProperty] private string name;
    [ObservableProperty] private string number;
    [ObservableProperty] private string email;
    [ObservableProperty] private int totalPrints;

    public FinalizeOrderViewModel(ImageService service, INavigationService navigationService, IContentDialogService contentDialogService)
    {
        imageService = service;
        this.contentDialogService = contentDialogService;
        _navigationService = navigationService;
        var images = service.Images.Where(x => x.IsSelected).ToList();

        foreach (var image in images) 
        { 
            SelectedImages.Add(image);
        }

        GetTotalPrints();
    }

    private void GetTotalPrints()
    {
        TotalPrints = SelectedImages.Where(x => x.IsPrintable).Sum(x=>x.PrintAmount);
    }

    [RelayCommand]
    private async Task CompleteOrder()
    {
        if (!SelectedImages.Any())
        {
            var msg = new MessageBox
            {
                Title = "No Selected Images",
                Content = "There are no images for this order. Returning to the dashboard.",
                CloseButtonText = "Ok",
            };

            _ = _navigationService.Navigate(typeof(DashboardPage));

            return;
        }

        if (string.IsNullOrWhiteSpace(Name))
        {
            var msg = new MessageBox
            {
                Title = "Name Error",
                Content = "Please Set Name Field.",
                CloseButtonText = "Ok",
            };
            await msg.ShowDialogAsync();
            return;
        }

        if (!String.IsNullOrWhiteSpace(Number) && !IsPhoneNumber(Number))
        {
            var msg = new MessageBox
            {
                Title = "Invalid Number",
                Content = "Please check phone number.",
                CloseButtonText = "Ok",
            };

            await msg.ShowDialogAsync();
            return;
        }

        if (string.IsNullOrWhiteSpace(Number) && string.IsNullOrWhiteSpace(Email))
        {
            var msg = new MessageBox
            {
                Title = "No Contact Information",
                Content = "Please Set  Number or Email Field",
                CloseButtonText = "Ok",
            };

            await msg.ShowDialogAsync();
            return;
        }

        var imageDisplay = new WorkerValidationDialog(contentDialogService.GetDialogHost()!);
        ContentDialogResult result = await imageDisplay.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            await ImageService.CompleteOrder(Name, Number, Email);
        }
    }

    private bool IsPhoneNumber(string number)
    {
        return Regex.Match(number, @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$").Success;
    }

    [RelayCommand]
    private void TogglePrintClick(ImageObj obj)
    {
        obj.IsPrintable = !obj.IsPrintable;
        GetTotalPrints();
    }

    [RelayCommand]
    private async Task DeleteImageClick(ImageObj obj)
    {

        var result = await ImageService.DeleteImage(obj);

        if(result)
        {
            SelectedImages.Remove(obj);
            GetTotalPrints();
        }

    }

    [RelayCommand]
    private void IncreasePrintCountClick(ImageObj obj)
    {
        obj.PrintAmount += 1;
        GetTotalPrints();
    }

    [RelayCommand]
    private void DecreasePrintCountClick(ImageObj obj)
    {
        if(obj.PrintAmount > 1)
        {
            obj.PrintAmount -= 1;
            GetTotalPrints();
        }
    }

    [RelayCommand]
    private async Task OnShowSignInContentDialog(ImageObj image)
    {
        var imageDisplay = new ImageViewerContentDialogPage(contentDialogService.GetDialogHost()!);
        imageDisplay.ImageViewer.Source = image.HDImage;
        imageDisplay.Title = image.Name;
        _ = await imageDisplay.ShowAsync();
    }
}
