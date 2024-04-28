using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace TheIslandPostPhotoManager.ViewModels;

public partial class ImageObj : ObservableObject
{
    public string ImageUrl { get; set; }
    public string Name { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    [ObservableProperty] private bool isSelected;
    [ObservableProperty] private bool isPending;
    [ObservableProperty] private bool isPrintable;

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if(e.PropertyName.Equals(nameof(IsSelected)))
        {
            if(!IsSelected)
            {
                IsPrintable = false;
            }
        }
    }
}