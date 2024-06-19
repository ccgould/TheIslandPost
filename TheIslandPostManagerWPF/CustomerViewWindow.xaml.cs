using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TheIslandPostManagerWPF.Models;
using TheIslandPostManagerWPF.Services;
using TheIslandPostManagerWPF.ViewModels;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace TheIslandPostManagerWPF;
/// <summary>
/// Interaction logic for CustomerViewWindow.xaml
/// </summary>
public partial class CustomerViewWindow
{
    public CustomerViewWindow(NotificationService notificationService)
    {
        InitializeComponent();
        zoomBorder.MouseEnter += (s, e) => Mouse.OverrideCursor = Cursors.Hand;
        zoomBorder.MouseLeave += (s, e) => Mouse.OverrideCursor = Cursors.Arrow;
        zoomBorder.MouseEnter += (s, e) => buttonView.Visibility = System.Windows.Visibility.Visible;
        buttonView.MouseEnter += (s, e) => buttonView.Visibility = System.Windows.Visibility.Visible;

        zoomBorder.MouseLeave += (s, e) => buttonView.Visibility = System.Windows.Visibility.Hidden;

        zoomBorder.Focusable = true;

        zoomBorder.Focus();

        //DataContext = vm;

        notificationService.OnDisplayCustomerInfoPanel += () =>
        {
            ((CustomerViewWindowViewModel)DataContext).OnDisplayCustomerInfoPanel();
        };
    }

    public void Reset()
    {
        zoomBorder.Reset();
    }

    private void Reset(object sender, RoutedEventArgs e)
    {
        zoomBorder.Reset();
    }

    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }


}
