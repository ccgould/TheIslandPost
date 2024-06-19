using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;


namespace TheIslandPostManagerWPF.Models;
public partial class OrderLog : ObservableObject
{
    [ObservableProperty] private string name;
    [ObservableProperty] private ObservableCollection<OrderLog> child;
    [ObservableProperty] private OrderInformation log;
}
