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
using TheIslandPostManagerWPF.ViewModels;

namespace TheIslandPostManagerWPF.Views;
/// <summary>
/// Interaction logic for LogsPageView.xaml
/// </summary>
public partial class LogsPageView : Page
{

    public LogsPageView(LogsPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;

        
    }

    private void trv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        var f = e.NewValue as CategorizedLogsViewModel;

        var vm = (LogsPageViewModel)DataContext;

        vm.IsSelected = f;

    }
}
