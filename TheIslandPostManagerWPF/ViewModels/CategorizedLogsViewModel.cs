using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheIslandPostManagerWPF.Models;

namespace TheIslandPostManagerWPF.ViewModels;
public partial  class CategorizedLogsViewModel : ObservableObject
{
    [ObservableProperty] private string name;
    [ObservableProperty] private ObservableCollection<CategorizedLogsViewModel> children;
    [ObservableProperty] private OrderInformation orderInformation;
}
