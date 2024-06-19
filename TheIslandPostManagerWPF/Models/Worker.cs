using CommunityToolkit.Mvvm.ComponentModel;

namespace TheIslandPostManagerWPF.Models;
public partial class Worker : ObservableObject
{
    [ObservableProperty] private string name;
    [ObservableProperty] private int imageSales;
    [ObservableProperty] private int videoSales;
    [ObservableProperty] private double rating;
    [ObservableProperty] private List<string> comments;
    [ObservableProperty] private List<Sale> sales;

    internal void AppendEarnings(decimal split)
    {
        if(Sales is null)
        {
            Sales = new List<Sale>();
        }

        Sales.Add(new Sale
        {
            Date = DateTime.Now,
            Amount = split
        });
    }

    internal decimal GetTotalTransactions(DateTime dateTime)
    {
        if(Sales is null)
        {
            return 0;
        }
        return Sales.Where(x => x.Date == dateTime).Sum(x => x.Amount);
    }

    internal decimal GetTotalTransactions()
    {
        if (Sales is null)
        {
            return 0;
        }
        return Sales.Sum(x => x.Amount);
    }
}
