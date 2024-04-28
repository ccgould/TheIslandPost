using IslandPostPOS.Enumerators;
using System.ComponentModel.DataAnnotations;

namespace IslandPostPOS.Model;
public class Transaction
{
    [Display(Name = "Customer ID")]
    public string CustomerID { get; set; }
    public string Name { get; set; }

    public string Email { get; set; }

    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Picture Count")]
    public int PictureAmount { get; set; }


    [Display(Name = "Video Count")]
    public int VideoAmount { get; set; }

    public decimal Tip { get; set; }


    [Display(Name = "Payment Type")]
    public PaymentType PaymentType { get; set; }

    [Display(Name = "Cash Total")]
    public string CashTotal { get; set; }


    [Display(Name = "Date/Time")]
    public DateTime DateTime { get; set; }

    public string Notes { get; set; }

    [Display(AutoGenerateField = false)]
    public int CashTotalInt { get; set; }
}
