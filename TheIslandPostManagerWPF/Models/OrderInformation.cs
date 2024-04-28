using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TheIslandPostManagerWPF.Enumerator;

namespace TheIslandPostManagerWPF.Models;
public partial class OrderInformation : ObservableObject
{
    [ObservableProperty] private string downloadURL;

    [Display(Name = "Customer ID", AutoGenerateField = false)]
    public string CustomerID { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Date/Time", AutoGenerateField = false)]
    public DateTime DateTime { get; set; }

    [Display(AutoGenerateField = false)]
    public List<string> ApprovedImages { get; set; } = new();
    
    public Dictionary<string, int> ApprovedPrints { get; set; } = new();

    [ObservableProperty] private bool isFinalized;
}
