using System.ComponentModel.DataAnnotations;
using TheIslandPostPhotoManager.Enumerator;

namespace TheIslandPostPhotoManager.Models;
public class OrderInformation
{

    [Display(Name = "Customer ID", AutoGenerateField = false)]
    public string CustomerID { get; set; }
    public string Name { get; set; }

    public string Email { get; set; }

    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Picture Count", AutoGenerateField = false)]
    public int PictureAmount { get; set; }


    [Display(Name = "Video Count", AutoGenerateField = false)]
    public int VideoAmount { get; set; }

    [Display(AutoGenerateField = false)]
    public decimal Tip { get; set; }


    [Display(Name = "Payment Type", AutoGenerateField = false)]
    public PaymentType PaymentType { get; set; }

    [Display(Name = "Cash Total", AutoGenerateField = false)]
    public string CashTotal { get; set; }


    [Display(Name = "Date/Time", AutoGenerateField = false)]
    public DateTime DateTime { get; set; }

    [Display(AutoGenerateField = false)]
    public string Notes { get; set; }

    [Display(AutoGenerateField = false)]
    public int CashTotalInt { get; set; }


    [Display(AutoGenerateField = false)]
    public List<string> ApprovedImages { get; set; } = new();}
