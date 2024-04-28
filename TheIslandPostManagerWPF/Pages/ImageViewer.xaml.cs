using System.Windows.Controls;
using System.Windows.Input;
using TheIslandPostManagerWPF.Models;

namespace TheIslandPostManagerWPF.Pages;

/// <summary>
/// Interaction logic for ImageViewer.xaml
/// </summary>
public partial class ImageViewer : Page
{
    public ImageViewerViewModel ViewModel { get; set; }

    public ImageViewer(ImageViewerViewModel vm)
    {
        ViewModel = vm;
        
        DataContext = this;

        InitializeComponent();

        zoomBorder.MouseEnter += (s, e) => Mouse.OverrideCursor = Cursors.Hand;
        zoomBorder.MouseLeave += (s, e) => Mouse.OverrideCursor = Cursors.Arrow;
        
        zoomBorder.MouseEnter += (s, e) => buttonView.Visibility = System.Windows.Visibility.Visible;
        buttonView.MouseEnter += (s, e) => buttonView.Visibility = System.Windows.Visibility.Visible;
        
        zoomBorder.MouseLeave += (s, e) => buttonView.Visibility = System.Windows.Visibility.Hidden;
    }

    public void Reset()
    {
        zoomBorder.Reset();
    }

    private void Reset(object sender, System.Windows.RoutedEventArgs e)
    {
        zoomBorder.Reset();
    }
}
