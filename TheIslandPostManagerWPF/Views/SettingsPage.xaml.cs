using System.Windows.Controls;
using TheIslandPostManagerWPF.ViewModels;

namespace TheIslandPostManagerWPF.Views;
/// <summary>
/// Interaction logic for SettingsPage.xaml
/// </summary>
public partial class SettingsPage : Page
{
    public SettingsPage(SettingsPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
