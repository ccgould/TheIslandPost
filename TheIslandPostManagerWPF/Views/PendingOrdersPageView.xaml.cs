using System.Windows.Controls;
using TheIslandPostManagerWPF.ViewModels;

namespace TheIslandPostManagerWPF.Views;
/// <summary>
/// Interaction logic for PendingOrdersPageView.xaml
/// </summary>
public partial class PendingOrdersPageView : Page
{
    public PendingOrdersPageView(PendingOrdersPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
        ViewModel = vm;
    }

    public PendingOrdersPageViewModel ViewModel { get; set; }
}
