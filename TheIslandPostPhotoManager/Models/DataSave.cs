using System.Collections.ObjectModel;

namespace TheIslandPostPhotoManager.Models;
internal class DataSave
{
    //public ObservableCollection<StoreItem> CurrentPurchases { get; set; }
    public ObservableCollection<OrderInformation> CurrentTransaction { get; set; }
}
