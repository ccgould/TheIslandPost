using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheIslandPost.Views;

namespace TheIslandPost.ViewModels;
public partial class PassenegerInfoPageViewModel : ObservableObject
{
    public PassenegerInfoPageViewModel()
    {
        
    }

    [RelayCommand]
    private async Task GoToFinalizePage()
    {
        await Shell.Current.GoToAsync(nameof(FinalizePageView));
    }
}
