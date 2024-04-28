using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TheIslandPostManagerWPF.Models;

public partial class ImageObj : ObservableObject
{
    [ObservableProperty] private string imageUrl;
    [ObservableProperty] private string name;
    [ObservableProperty] private int height;
    [ObservableProperty] private int width;
    [ObservableProperty] private BitmapImage image;
    [ObservableProperty] private BitmapImage hDImage;

    [ObservableProperty] private int printAmount = 1;
    [ObservableProperty] private bool isSelected;
    [ObservableProperty] private bool isPending;
    [ObservableProperty] private bool isPrintable;
    [ObservableProperty] private int index;
    private bool isDirty;

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (isDirty) return;

        if(e.PropertyName.Equals(nameof(IsSelected)))
        {
            if(!IsSelected)
            {
                isDirty = true;
                IsPrintable = false;
            }
        }

        if (e.PropertyName.Equals(nameof(IsPending)))
        {
            if (IsSelected)
            {
                isDirty = true;
                IsSelected = false;
            }
        }

        if (e.PropertyName.Equals(nameof(IsSelected)))
        {
            if (IsPending)
            {
                isDirty = true;

                IsPending = false;
            }
        }

        isDirty = false;
    }
}