using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheIslandPost.Models;
using TheIslandPost.Views;

namespace TheIslandPost.ViewModels;
public partial class HomePageViewModel : ObservableObject
{
    public ObservableCollection<SystemDrive> SystemDrives { get; set; }

    public HomePageViewModel()
    {
        this.SystemDrives = new ObservableCollection<SystemDrive>();
        this.SystemDrives.Add(new SystemDrive() { Name = "Facebook", ID = 0 });
        this.SystemDrives.Add(new SystemDrive() { Name = "Google Plus", ID = 1 });
        this.SystemDrives.Add(new SystemDrive() { Name = "Instagram", ID = 2 });
        this.SystemDrives.Add(new SystemDrive() { Name = "LinkedIn", ID = 3 });
        this.SystemDrives.Add(new SystemDrive() { Name = "Skype", ID = 4 });
        this.SystemDrives.Add(new SystemDrive() { Name = "Telegram", ID = 5 });
        this.SystemDrives.Add(new SystemDrive() { Name = "Televzr", ID = 6 });
        this.SystemDrives.Add(new SystemDrive() { Name = "Tik Tok", ID = 7 });
        this.SystemDrives.Add(new SystemDrive() { Name = "Tout", ID = 8 });
        this.SystemDrives.Add(new SystemDrive() { Name = "Tumblr", ID = 9 });
        this.SystemDrives.Add(new SystemDrive() { Name = "Twitter", ID = 10 });
        this.SystemDrives.Add(new SystemDrive() { Name = "Vimeo", ID = 11 });
        this.SystemDrives.Add(new SystemDrive() { Name = "WhatsApp", ID = 12 });
        this.SystemDrives.Add(new SystemDrive() { Name = "YouTube", ID = 13 });
    }


    [RelayCommand] 
    private async Task GoToPassengerInfo()
    {
        await Shell.Current.GoToAsync(nameof(PassenegerInfoPageView));
    }

    [RelayCommand]
    private void DeselectAll()
    {

    }
}
