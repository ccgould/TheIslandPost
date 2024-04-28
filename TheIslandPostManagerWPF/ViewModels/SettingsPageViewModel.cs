using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using TheIslandPostManagerWPF.Models;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace TheIslandPostManagerWPF.ViewModels;
public partial class SettingsPageViewModel : ObservableObject
{
    [ObservableProperty] private ApplicationSaveService applicationSaveService;
    private ISnackbarService _snackbarService;

    public SettingsPageViewModel(ApplicationSaveService applicationSaveService, ISnackbarService snackbarService)
    {
        ApplicationSaveService = applicationSaveService;
        _snackbarService = snackbarService;
    }

    [RelayCommand]
    private void OpenInputBrowser()
    {
        var dialog = new CommonOpenFileDialog();
        dialog.IsFolderPicker = true;
        CommonFileDialogResult result = dialog.ShowDialog();

        if (result == CommonFileDialogResult.Ok)
        {
            ApplicationSaveService.ApplicationSaveData.InputLocation = dialog.FileName;
            ApplicationSaveService.SaveData();
        }
    }

    [RelayCommand]
    private void OpenOutputBrowser()
    {
        var dialog = new CommonOpenFileDialog();
        dialog.IsFolderPicker = true;
        CommonFileDialogResult result = dialog.ShowDialog();

        if (result == CommonFileDialogResult.Ok)
        {
            ApplicationSaveService.ApplicationSaveData.OutputLocation = dialog.FileName;
            ApplicationSaveService.SaveData();
        }
    }

    [RelayCommand]
    private void OpenPrinterBrowser()
    {
        var dialog = new CommonOpenFileDialog();
        dialog.IsFolderPicker = true;
        CommonFileDialogResult result = dialog.ShowDialog();

        if(result == CommonFileDialogResult.Ok)
        {
            ApplicationSaveService.ApplicationSaveData.PrinterLocation = dialog.FileName;
            ApplicationSaveService.SaveData();
        }
    }

    [RelayCommand]
    private void OpenWatermarkBrowser()
    {
        var dialog = new CommonOpenFileDialog();
        dialog.IsFolderPicker = false;
        dialog.Filters.Add(new CommonFileDialogFilter("JPEG Files", "*.jpg"));
        dialog.Filters.Add(new CommonFileDialogFilter("PNG Files", "*.png"));

        CommonFileDialogResult result = dialog.ShowDialog();

        if (result == CommonFileDialogResult.Ok)
        {
            ApplicationSaveService.ApplicationSaveData.WatermarkLocation = dialog.FileName;
            ApplicationSaveService.SaveData();
        }
    }

    [RelayCommand]
    private void Save()
    {
        ApplicationSaveService.SaveData();

        _snackbarService.Show(
               "Settings Saved",
               "All settings have been saved successfully",
               ControlAppearance.Success,
               new SymbolIcon(SymbolRegular.Check24),
               TimeSpan.FromSeconds(5));
    }
}
