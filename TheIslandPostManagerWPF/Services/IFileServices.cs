
using TheIslandPostManagerWPF.Models;

namespace TheIslandPostManagerWPF.Services;

public interface IFileServices
{
    Task Copy(List<Tuple<string, string>> file, bool moveToPrinter = true, Action<int, string> callback = null);
    Task CreateBackup(List<string> files, bool moveToPrinter = true, Action<int, string> callback = null);
    Task<bool> CreatePendingOrder(PendingOrder pendingOrder);
    void Load();
    string CreateOrderDirectory(string name);
    string GetTempDir();

    Action OnPendingUpdate { get; set; }

}