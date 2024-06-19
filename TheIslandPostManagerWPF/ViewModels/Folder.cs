using CommunityToolkit.Mvvm.ComponentModel;

namespace TheIslandPostManagerWPF.ViewModels;

public partial class Folder : ObservableObject
{
    [ObservableProperty] private string name;
    [ObservableProperty] private string fullPath;
}