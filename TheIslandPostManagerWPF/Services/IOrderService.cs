using System.Collections.ObjectModel;
using TheIslandPostManagerWPF.Models;
namespace TheIslandPostManagerWPF.Services;

public interface IOrderService
{
    void CreateNewOrder();
    void AddCurrentOrder();
    void DeleteOrder(OrderInformation order);
    void AddApprovedPhoto(OrderImageSelectionData orderImageSelectionData);
    void RemoveApprovedPhoto(OrderImageSelectionData orderImageSelectionData);
    Task FinalizeOrder(bool bypassDigitals);

    ObservableCollection<OrderInformation> Orders { get; set; }
}
