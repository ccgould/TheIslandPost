using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TheIslandPostManagerWPF.Models;

namespace TheIslandPostManagerWPF.Services;

public partial class WorkerService : ObservableObject
{
    [ObservableProperty] private ObservableCollection<Worker> workers = new();

    public WorkerService()
    {
        workers.Add(new Worker
        {
            Name = "Adewale"
        });

        workers.Add(new Worker
        {
            Name = "Ashton"
        });

        workers.Add(new Worker
        {
            Name = "Creswell"
        });

        workers.Add(new Worker
        {
            Name = "Sascha"
        });
    }
}
