using System.Collections.ObjectModel;

namespace IslandPostPOS.Model;
internal class DataSave
{
    public ObservableCollection<StoreItem> CurrentPurchases { get; set; }
    public ObservableCollection<Transaction> CurrentTransaction { get; set; }
}
