using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using TheIslandPostManagerWPF.Models;
using TheIslandPostManagerWPF.Services;
using Wpf.Ui;
using Wpf.Ui.Controls;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace TheIslandPostManagerWPF.ViewModels;
public partial class DashBoardViewModel : ObservableObject
{
    [ObservableProperty] private ApplicationSaveService applicationSaveService;
    private ImageService _imageService;
    private ISnackbarService _snackbarService;
    [ObservableProperty] private bool isBusy;

    [ObservableProperty] private int totalImages;
    [ObservableProperty] private int currentAmount;
    [ObservableProperty] private int progressBarValue;
    public DashBoardViewModel(ApplicationSaveService applicationSaveService, ImageService imageService,ISnackbarService snackbarService, EmailService emailService)
    {
        emailService.SendToWhatsapp();
        this.applicationSaveService = applicationSaveService;
        _imageService = imageService;
        _snackbarService = snackbarService;
    }

    [RelayCommand]
    private async Task ImportImages()
    {
        var dialog = new CommonOpenFileDialog();
        dialog.Multiselect = true;
        dialog.Filters.Add(new CommonFileDialogFilter("JPEG Files", "*.jpg"));
        dialog.Filters.Add(new CommonFileDialogFilter("PNG Files", "*.png"));

        CommonFileDialogResult result = dialog.ShowDialog();

        if (result == CommonFileDialogResult.Ok)
        {
            _imageService.ClearImages();
            CurrentAmount = 0;
            ProgressBarValue = 0;
            TotalImages = dialog.FileNames.Count();

            await Copier.CopyFiles(CreatePaths(dialog.FileNames), (prog,fileName) =>
            {
                IsBusy = true;
                CurrentAmount++;
                ProgressBarValue = prog;
                _imageService.AddImage(fileName);
            });

            TotalImages = 0;
            CurrentAmount = 0;
            ProgressBarValue = 0;
            _snackbarService.Show(
                "Import Finished", 
                "All Images have been imported successfully", 
                Wpf.Ui.Controls.ControlAppearance.Info,
                new SymbolIcon(SymbolRegular.Info24),
                TimeSpan.FromSeconds(5));
            IsBusy = false;
        }
    }

    private List<Tuple<string, string>> CreatePaths(IEnumerable<string?> fileNames)
    {
        var result = new List<Tuple<string, string>>();
        foreach (var file in fileNames)
        {
            var fileName = Path.GetFileName(file);
            result.Add(new Tuple<string,string>(file, Path.Combine(ApplicationSaveService.ApplicationSaveData.InputLocation, fileName)));
        }

        return result;
    }

    private static void ProcessImage(string file,string location)
    {
        if (Directory.Exists(location))
        {
            var fileName = Path.GetFileName(file);
            System.IO.File.Copy(file, Path.Combine(location, fileName));
        }
    }
}
