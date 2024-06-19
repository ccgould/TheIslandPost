using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentEmail.Core;
using TheIslandPostManagerWPF.Models;
using TheIslandPostManagerWPF.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TheIslandPostManagerWPF.Views;
using Wpf.Ui.Controls;
using Wpf.Ui;
using System.IO;
using System.Text.RegularExpressions;

namespace TheIslandPostManagerWPF.ViewModels;
public partial class CustomerViewWindowViewModel : ObservableObject
{
    [ObservableProperty] private ImageService service;
    [ObservableProperty] private int selectedCount = 0;
    [ObservableProperty] private int maybeCount = 0;
    [ObservableProperty] private int currentPosition = 0;
    [ObservableProperty] private string name;
    [ObservableProperty] private string number;
    [ObservableProperty] private string email;

    private readonly IContentDialogService contentDialogService;
    private readonly ApplicationSaveService applicationSaveData;
    private readonly IMessageService messageService;
    private readonly IOrderService orderService;
    [ObservableProperty] private bool isCompleteingOrder;


    public CustomerViewWindowViewModel(ImageService service, IContentDialogService contentDialogService, ApplicationSaveService applicationSaveData, IMessageService messageService,
IOrderService orderService)
    {
        Service = service;
        this.contentDialogService = contentDialogService;
        this.applicationSaveData = applicationSaveData;
        this.messageService = messageService;
        this.orderService = orderService;
        UpdateSelectedCount();
        Refresh();
        //service.SelectedImage.HDImage = service.LoadImageFile(service.SelectedImage.ImageUrl, true);
    }

    internal void OnDisplayCustomerInfoPanel()
    {
        IsCompleteingOrder = !IsCompleteingOrder;
    }


    private void Refresh()
    {
        CurrentPosition = Service.StudentsView.CurrentPosition + 1;
    }

    private void UpdateSelectedCount()
    {
        SelectedCount = Service.Images.Count(x => x.IsSelected);
        MaybeCount = Service.Images.Count(x => x.IsPending);
    }

    [RelayCommand]
    private void PreviousPhoto()
    {
        Service.ChangeSelectedImage(false);
        Refresh();
    }

    [RelayCommand]
    private void NextPhoto()
    {
        Service.ChangeSelectedImage(true);
        Refresh();
    }

    [RelayCommand]
    private void SelectPhoto()
    {
        var currentImage = Service.StudentsView.CurrentItem as ImageObj;

        if (currentImage is not null)
        {
            currentImage.IsSelected = true;
            UpdateSelectedCount();
            orderService.AddApprovedPhoto(currentImage.GetImageInfo());
        }
    }

    [RelayCommand]
    private void DeSelectPhoto()
    {
        var currentImage = Service.StudentsView.CurrentItem as ImageObj;
        if (currentImage is not null)
        {
            currentImage.IsSelected = false;
            UpdateSelectedCount();
            orderService.RemoveApprovedPhoto(currentImage.GetImageInfo());
        }
    }

    [RelayCommand]
    private void SetAsMaybe()
    {
        var currentImage = Service.StudentsView.CurrentItem as ImageObj;

        if (currentImage is not null)
        {
            currentImage.IsPending = !currentImage.IsPending;
            UpdateSelectedCount();
        }
    }

    [RelayCommand]
    private void AttemptDislikePhoto()
    {
        var currentImage = Service.StudentsView.CurrentItem as ImageObj;

        if (currentImage is not null)
        {
            if (currentImage.IsSelected)
            {
                DeSelectPhoto();
                return;
            }

            SetAsMaybe();
        }
    }

    [RelayCommand]
    private void PrintPhoto()
    {
        var currentImage = Service.StudentsView.CurrentItem as ImageObj;

        if (currentImage is not null)
        {
            currentImage.IsPrintable = !currentImage.IsPrintable;
        }
    }

    [RelayCommand]
    private async Task DeletePhoto(object obj)
    {
        var currentImage = Service.StudentsView.CurrentItem as ImageObj;

        if (currentImage is not null)
        {
            await Service.DeleteImage(currentImage);
            NextPhoto();
        }
    }

    [RelayCommand]
    private async Task CompleteOrder()
    {
        try
        {
            bool bypassEmail = false;
            Name = Name?.Trim() ?? string.Empty;
            Number = Number?.Trim() ?? string.Empty;
            Email = Email?.Trim() ?? string.Empty;


            if (!Service.GetSelectedImages().Where(x => x.IsSelected).Any())
            {
                var msg = new MessageBox
                {
                    Title = "No Selected Images",
                    Content = "There are no images for this order. Returning to the dashboard.",
                    CloseButtonText = "Ok",
                };

                //_ = _navigationService.Navigate(typeof(DashboardPage));

                return;
            }

            if (Service.GetSelectedImages().Count(x => x.IsPrintable) <= 0)
            {
                var msg = new MessageBox
                {
                    Title = "No Prints Selected",
                    Content = "There are no prints for this order. Are you sure you would like to continue?",
                    IsPrimaryButtonEnabled = true,
                    PrimaryButtonText = "No",
                    CloseButtonText = "Yes"

                };

                var msgResult = await msg.ShowDialogAsync();

                if (msgResult == MessageBoxResult.Primary)
                {
                    return;
                }
            }

            var watermark = applicationSaveData.ApplicationSaveData.WatermarkLocation;

            if (!File.Exists(watermark) && Service.GetSelectedImages().Any(x => x.IsPrintable))
            {
                var msg = new MessageBox
                {
                    Title = "No Watermark Selected",
                    Content = "You have checked Add Watermark to image in settings, but have not selected an image for the watermark. Please go to settings to select your watermark.",
                    CloseButtonText = "Ok",
                };

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

            if (!string.IsNullOrWhiteSpace(Number) && !IsPhoneNumber(Number))
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

            //if (string.IsNullOrWhiteSpace(Number) && string.IsNullOrWhiteSpace(Email))
            //{
            //    var msg = new MessageBox
            //    {
            //        Title = "No Contact Information",
            //        Content = "Please Set  Number or Email Field",
            //        CloseButtonText = "Ok",
            //    };

            //    await msg.ShowDialogAsync();
            //    return;
            //}

            if (string.IsNullOrWhiteSpace(Number) && string.IsNullOrWhiteSpace(Email))
            {
                var msg = new MessageBox
                {
                    Title = "No Contact Information",
                    Content = "There is no contact information provided.This will result in digitals not being sent by email. Do you wish to continue?",
                    CloseButtonText = "No",
                    PrimaryButtonText = "Yes"
                };

                var messageBoxResult = await msg.ShowDialogAsync();

                if (messageBoxResult != MessageBoxResult.Primary)
                {
                    return;
                }

                bypassEmail = true;
            }

            //var imageDisplay = new FinalizeDialog(contentDialogService.GetDialogHost()!, workerService);
            //ContentDialogResult result = await imageDisplay.ShowAsync();

            var imageDisplay = new WorkerValidationDialog(contentDialogService.GetDialogHost()!);
            ContentDialogResult result = await imageDisplay.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                await orderService.FinalizeOrder(bypassEmail);

                Name = string.Empty;
                Email = string.Empty;
                Number = string.Empty;
                //TotalPrints = 0;
                //SelectedImages.Clear();
            }
        }
        catch (Exception ex)
        {
            await messageService.ShowErrorMessage("Error", ex.Message, ex.StackTrace);
        }
    }

    private bool IsPhoneNumber(string number)
    {
        return Regex.Match(number, @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$").Success;
    }
}
