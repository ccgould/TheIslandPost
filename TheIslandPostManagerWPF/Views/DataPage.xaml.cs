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
/// Interaction logic for DataPage.xaml
/// </summary>
public partial class DataPage : Page
{
    public DataPageViewModel ViewModel { get; }

    public DataPage(DataPageViewModel vm)
    {
        ViewModel = vm;
        DataContext = this;
        InitializeComponent();
    }
}
