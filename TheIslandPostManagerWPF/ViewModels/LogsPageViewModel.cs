using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

using TheIslandPostManagerWPF.Models;
using TheIslandPostManagerWPF.Views;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace TheIslandPostManagerWPF.ViewModels;
public partial class LogsPageViewModel : ObservableObject
{

    [ObservableProperty] private ObservableCollection<CategorizedLogsViewModel> logs = new();

    [ObservableProperty] private CategorizedLogsViewModel isSelected;

    [ObservableProperty] private bool isReadOnly = true;
    private IContentDialogService contentDialogService;

    public LogsPageViewModel(ApplicationSaveService applicationSaveService, IContentDialogService contentDialogService)
    {
        this.contentDialogService = contentDialogService;
        foreach (var log in applicationSaveService.LoadAllRecords())
        {
            var item1 = new CategorizedLogsViewModel
            {
                Name = log.Key,
                Children = GetChildren(log.Value)
            };

            Logs.Add(item1);
        }
    }

    [RelayCommand]
    private async Task ToggleEditingMode()
    {
        if (IsReadOnly)
        {
            var imageDisplay = new WorkerValidationDialog(contentDialogService.GetDialogHost()!);
            ContentDialogResult result = await imageDisplay.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                IsReadOnly = !IsReadOnly;
            }
        }
        else
        {
            IsReadOnly = !IsReadOnly;
        }

    }

    private ObservableCollection<CategorizedLogsViewModel> GetChildren(List<OrderInformation> logs)
    {
        var result = new ObservableCollection<CategorizedLogsViewModel>();

        if(logs is null || !logs.Any())
        {
            return result;
        }

        foreach (var log in logs)
        {
             result.Add(new CategorizedLogsViewModel
            {
                 Name = log.Name,
                 OrderInformation = log

            });
        }

        return result;
    }
}
