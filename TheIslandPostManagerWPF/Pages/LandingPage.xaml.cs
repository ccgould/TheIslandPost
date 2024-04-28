using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TheIslandPostManagerWPF.CustomControl;
using Wpf.Ui.Controls;

namespace TheIslandPostManagerWPF.Pages;

/// <summary>
/// Interaction logic for LandingPage.xaml
/// </summary>
public partial class LandingPage : Page
{
    public LandingPage()
    {
        InitializeComponent();
    }

    private void Grid_Click(object sender, RoutedEventArgs e)
    {
        var navBTN = e.OriginalSource as NavButton;

        if(navBTN is not null)
        {
            NavigationService.Navigate(navBTN.NavUri);
        }
    }
}
