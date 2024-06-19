
using System.Collections.ObjectModel;
using TheIslandPostManagerWPF.Models;

namespace TheIslandPostManagerWPF.Services;

public interface IFTPService
{
    void AssignImagesToCustomer(string guid, ImageObj images);
    Task Connect();
    Task Disconnect();
    Task InsertPendingCustomer(string guid, string name);
    Task<ObservableCollection<PendingOrder>> LoadPendings();
    Task UploadFile(string source, string dest);
}