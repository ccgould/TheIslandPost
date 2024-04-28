using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheIslandPostManagerWPF.Models;
using Wpf.Ui.Controls;

namespace TheIslandPostManagerWPF.Views;
/// <summary>
/// Interaction logic for ImageViewerContentDialogPage.xaml
/// </summary>
public partial class ImageViewerContentDialogPage : ContentDialog
{
    public ImageViewerContentDialogPage(ContentPresenter contentPresenter) : base(contentPresenter)
    {
        InitializeComponent();
    }

    public ImageObj ImageObj { get; internal set; }

    protected override void OnButtonClick(ContentDialogButton button)
    {
        base.OnButtonClick(button);
    }

    protected override void OnContextMenuOpening(ContextMenuEventArgs e)
    {
        base.OnContextMenuOpening(e);

        ImageViewer.Source = ImageObj.HDImage;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        
    }
}
