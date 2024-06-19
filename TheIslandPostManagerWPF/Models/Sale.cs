using CommunityToolkit.Mvvm.ComponentModel;

namespace TheIslandPostManagerWPF.Models;

public partial class Sale : ObservableObject
{
    [ObservableProperty] private decimal amount;
    [ObservableProperty] private DateTime date;
}