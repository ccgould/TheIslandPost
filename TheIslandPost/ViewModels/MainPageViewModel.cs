using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheIslandPost.Views;

namespace TheIslandPost.ViewModels;
public partial class MainPageViewModel : ObservableObject
{
    public MainPageViewModel()
    {
        
    }

    [RelayCommand]
    private async Task GoHome()
    {
        await Shell.Current.GoToAsync(nameof(HomePageView));
    }
}
