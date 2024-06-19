using System.Windows.Controls;
using TheIslandPostManagerWPF.ViewModels;

namespace TheIslandPostManagerWPF.Views;
/// <summary>
/// Interaction logic for BackupPageView.xaml
/// </summary>
public partial class BackupPageView : Page
{
    public BackupPageView(BackupPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
