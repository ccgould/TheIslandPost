using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic.Logging;
using System.Collections.ObjectModel;
using TheIslandPostManagerWPF.Models;
using TheIslandPostManagerWPF.Services;
using Wpf.Ui;

namespace TheIslandPostManagerWPF.ViewModels;
public partial class OrdersPageViewModel : ObservableObject
{
    private EmailService emailService;
    private readonly ImageService imageService;
    private ApplicationSaveService applicationSaveService;
    [ObservableProperty] private KeyValuePair<string, List<OrderInformation>>? selectedItem;
    [ObservableProperty] private ObservableCollection<KeyValuePair<string, List<OrderInformation>>> logs = new();

    public OrdersPageViewModel(ApplicationSaveService applicationSaveService, EmailService emailService,ImageService imageService)
    {
        this.emailService = emailService;
        this.imageService = imageService;
        this.applicationSaveService = applicationSaveService;
        foreach (var log in applicationSaveService.LoadAllRecords())
        {
            Logs.Add(log);
        }
    }

    [RelayCommand]
    private async Task SendEmail(OrderInformation order)
    {
        if(await emailService.SendEmail(order))
        {
            order.IsFinalized = true;
            imageService.FinalizeOrder(order);
            applicationSaveService.SaveRecords(imageService.CurrentTransaction);
        }
    }

    [RelayCommand]
    private void ResendEmail(OrderInformation order)
    {
        if (order.IsFinalized)
        {
            order.IsFinalized = false;
        }
    }

}
