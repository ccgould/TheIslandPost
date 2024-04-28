using System.Windows.Controls;
using TheIslandPostManagerWPF.ViewModels;

namespace TheIslandPostManagerWPF.Views;

/// <summary>
/// Interaction logic for DashboardPage.xaml
/// </summary>
public partial class DashboardPage : Page
{
    public DashboardPage(DashBoardViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
