using CommunityToolkit.Mvvm.ComponentModel;
using FluentEmail.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace TheIslandPostManagerWPF.Models;
public partial class OrderInformation : ObservableObject
{
    [ObservableProperty] private string downloadURL;

    [Display(Name = "Customer ID", AutoGenerateField = false)]
    public string CustomerID { get; set; } = Guid.NewGuid().ToString(); 

    public string Name { get; set; }

    public string Email { get; set; }

    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Date/Time", AutoGenerateField = false)]
    public DateTime DateTime { get; set; }
    public List<Address> CC { get; set; }

    [ObservableProperty] private List<OrderImageSelectionData> approvedImages = new();
    [ObservableProperty] private List<OrderImageSelectionData> approvedPrints = new();

    [ObservableProperty] private bool isFinalized;
}
