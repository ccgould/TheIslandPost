using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;
using TheIslandPostManagerWPF.Views;
using Wpf.Ui;
using TheIslandPostManagerWPF.Services;

namespace TheIslandPostManagerWPF.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private bool _isInitialized = false;
    [ObservableProperty] private ImageService service;


    [ObservableProperty]
    private string _applicationTitle = String.Empty;

    [ObservableProperty]
    private ObservableCollection<object> _navigationItems = new();

    [ObservableProperty]
    private ObservableCollection<object> _navigationFooter = new();

    [ObservableProperty]
    private ObservableCollection<MenuItem> _trayMenuItems = new();

    public MainWindowViewModel(ImageService service)
    {
        if (!_isInitialized)
            Service = service;
            InitializeViewModel();

        service.OnImageCountUpdate += Update;
    }


    private void InitializeViewModel()
    {
        ApplicationTitle = "The Island Post - Photography Manager";



        NavigationItems = new ObservableCollection<object>
        {
            new NavigationViewItem()
            {
                Content = "Home",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(DashboardPage)
            },
            new NavigationViewItem()
            {
                Content = "Images",
                Icon = new SymbolIcon { Symbol = SymbolRegular.PictureInPictureEnter24 },
                TargetPageType = typeof(DataPage),
                InfoBadge = new InfoBadge
                {
                    Visibility = Service.Images.Any() ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed ,
                    Severity = InfoBadgeSeverity.Attention,
                    Value = Service.Images.Count.ToString()
                }
            },
            new NavigationViewItem()
            {
                Content = "Logs",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Note24 },
                TargetPageType = typeof(LogsPageView)
            },            
            new NavigationViewItem()
            {
                Content = "Orders",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Send24 },
                TargetPageType = typeof(OrdersPage),
                InfoBadge = new InfoBadge
                {
                    Visibility = Service.CurrentTransaction.Any() ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed ,
                    Severity = InfoBadgeSeverity.Attention,
                    Value = Service.CurrentTransaction.Count.ToString()
                }
            }
        };

        NavigationFooter = new ObservableCollection<object>
        {
            new NavigationViewItem()
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(SettingsPage)
            }
        };

        TrayMenuItems = new ObservableCollection<MenuItem>
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };

        _isInitialized = true;
    }

    private void Update()
    {
        var item = _navigationItems[1] as NavigationViewItem;
        item.InfoBadge.Visibility = Service.Images.Any() ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        item.InfoBadge.Severity = InfoBadgeSeverity.Attention;
        item.InfoBadge.Value = Service.Images.Count.ToString();
    }
}
