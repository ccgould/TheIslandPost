using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheIslandPostPhotoManager.Services;
using TheIslandPostPhotoManager.Views;

namespace TheIslandPostPhotoManager.ViewModels;
public partial class MainPageViewModel : ObservableObject
{
    private readonly IPopupService popupService;

    [ObservableProperty] private ImageService service;

    public MainPageViewModel(IPopupService popupService, ImageService service)
    {
        this.popupService = popupService;
        this.Service = service;
    }

    [RelayCommand]
    private async Task SelectionChanged(object obj)
    {
        await DisplayPopup();
    }

    public async Task DisplayPopup()
    {
        if (Service.SelectedImage is not null && Service.SelectedImage is ImageObj)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "SelectedImage", Service.SelectedImage }
            };
            await Shell.Current.GoToAsync(nameof(ImageViewerPage), navigationParameter);//this.popupService.ShowPopup<ImageViewerPageViewModel>(onPresenting: viewModel => viewModel.SetImage(SelectedImage as ImageObj));
        }
    }

    
    [RelayCommand]
    public async Task CompleteOrder()
    {
        await Shell.Current.GoToAsync(nameof(OrderCompletionPage));
    }

    [RelayCommand]
    private void SelectCurrent()
    {
        if(service.SelectedImage is not null)
        {
            service.SelectedImage.IsSelected = !service.SelectedImage.IsSelected;
        }
    }

    [RelayCommand]
    private void SelectAll()
    {
        if (service.Images is not null)
        {
            foreach (var image in service.Images)
            {
                image.IsSelected = true;
            }
        }
    }

    [RelayCommand]
    private void DeSelectAll()
    {
        if (service.Images is not null)
        {
            foreach (var image in service.Images)
            {
                image.IsSelected = false;
            }
        }
    }
}