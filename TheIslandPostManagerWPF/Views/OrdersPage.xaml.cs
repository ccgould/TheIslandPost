using System.Windows.Controls;
using TheIslandPostManagerWPF.ViewModels;

namespace TheIslandPostManagerWPF.Views;
/// <summary>
/// Interaction logic for OrdersPage.xaml
/// </summary>
public partial class OrdersPage : Page
{
    public OrdersPage(OrdersPageViewModel vm)
    {
        InitializeComponent();
        DataContext=vm;
    }
}
