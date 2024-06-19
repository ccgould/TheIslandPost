using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using TheIslandPostManagerWPF.Models;
using TheIslandPostManagerWPF.Services;
using TheIslandPostManagerWPF.Views;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace TheIslandPostManagerWPF.ViewModels;
public partial class PendingOrdersPageViewModel : ObservableObject
{
    [ObservableProperty] private ApplicationSaveService applicationSaveService;
    private readonly ImageService imageService;
    private readonly INavigationService navigationService;
    private readonly ISnackbarService snackbar;
    private readonly IMessageService messageService;
    private readonly IFileServices fileService;

    public PendingOrdersPageViewModel(
        ApplicationSaveService applicationSaveService,
        ImageService imageService, 
        INavigationService navigationService,
        ISnackbarService snackbar,
        IMessageService messageService,
        IFileServices fileService)
    {
        this.applicationSaveService = applicationSaveService;
        this.imageService = imageService;
        this.navigationService = navigationService;
        this.snackbar = snackbar;
        this.messageService = messageService;
        this.fileService = fileService;
        fileService.OnPendingUpdate += async () =>
        {
           await TryLoadReacords();
        };

    }

    private async Task TryLoadReacords()
    {
        try
        {
            await ApplicationSaveService.LoadAllRecords();
        }
        catch (Exception ex)
        {
            await messageService.ShowErrorMessage("Error", ex.Message, ex.StackTrace,false);
        }
    }

    [RelayCommand]
    private async Task RefreshClicked()
    {
        await TryLoadReacords();
    }


    [RelayCommand]
    private async Task OpenOrder(PendingOrder order)
    {
        try
        {
            await Copier.CopyFiles(CreatePaths(order.PendingImages), async (prog, fileName) =>
            {
                await imageService.AddImage(fileName);

                foreach (ImageObj item in order.PendingImages)
                {
                    var image = imageService.Images.FirstOrDefault(x => x.Name.Equals(item.Name));
                    if (image != null)
                    {
                        image.Copy(item);
                    }
                }
            });

            Delete(order);

            _ = navigationService.Navigate(typeof(DataPage));
        }
        catch (Exception ex)
        {
            snackbar.Show(
                "Error",
                ex.Message,
                ControlAppearance.Danger,
                new SymbolIcon(SymbolRegular.ThumbDislike24),
                TimeSpan.FromSeconds(5));
        }
    }

    [RelayCommand]
    private async Task DeletePending(PendingOrder order)
    {
        try
        {
            var uiMessageBox = new MessageBox
            {
                Title = "Delete Pending Order?",
                Content = "Are you sure you would like to remove this pending order from this collection?",
                IsPrimaryButtonEnabled = true,
                PrimaryButtonText = "Yes",
                CloseButtonText = "No ",

            };

            var result = await uiMessageBox.ShowDialogAsync();

            if (result == Wpf.Ui.Controls.MessageBoxResult.Primary)
            {
                Delete(order);
                imageService.OnImageCountUpdate?.Invoke();
            }

        }
        catch (Exception ex)
        {
            snackbar.Show(
                "Error",
                ex.Message,
                ControlAppearance.Danger,
                new SymbolIcon(SymbolRegular.ThumbDislike24),
                TimeSpan.FromSeconds(5));
            ApplicationSaveService.PendingOrders.Remove(order);
            ApplicationSaveService.RemovePendingOrder(order);
        }
    }

    private void Delete(PendingOrder order)
    {
        try
        {
            ApplicationSaveService.PendingOrders.Remove(order);
            ApplicationSaveService.RemovePendingOrder(order);
            Directory.Delete(Path.Combine(ApplicationSaveService.ApplicationSaveData.PendingLocation, order.Guid), true);
        }
        catch (Exception ex)
        {
            messageService.ShowErrorMessage("Error", ex.Message, ex.StackTrace);
        }
    }

    private List<Tuple<string, string>> CreatePaths(IEnumerable<string?> fileNames)
    {
        var result = new List<Tuple<string, string>>();
        foreach (var file in fileNames)
        {
            var fileName = Path.GetFileName(file);
            result.Add(new Tuple<string, string>(file, Path.Combine(ApplicationSaveService.ApplicationSaveData.InputLocation, fileName)));
        }

        return result;
    }

    private List<Tuple<string, string>> CreatePaths(List<ImageObj> pendingImages)
    {
        var paths = new List<string>();

        foreach (var image in pendingImages)
        {
            if (image.ImageUrl.Contains("thumbnail",StringComparison.OrdinalIgnoreCase)) continue;
            paths.Add(image.ImageUrl);
        }

        return CreatePaths(paths);
    }
}
