using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using TheIslandPostManagerWPF.Services;


namespace TheIslandPostManagerWPF.Models;
public partial class ApplicationSaveData : ObservableObject
{
    [ObservableProperty] private string inputLocation;
    [ObservableProperty] private string outputLocation;
    [ObservableProperty] private string printerLocation;
    [ObservableProperty] private string watermarkLocation;
    [ObservableProperty] private long imageQuality = 90;
    [ObservableProperty] private int videoCount = 0;
    [ObservableProperty] private int imageCount = 0;
    [ObservableProperty] private int printCount = 0;
    [ObservableProperty] private bool addWaterMarkToImage = true;

    [ObservableProperty] private int imageWidth = 0;
    [ObservableProperty] private int imageHeight = 0;
    [ObservableProperty] private int watermarkPosition = 4;
}


public partial class ApplicationSaveService : ObservableObject
{
    [ObservableProperty] private ApplicationSaveData applicationSaveData;
    private readonly string _appDataLocation;
    private readonly string _appTempLocation;
    private readonly string _appSaveDataLocation;
    private readonly string _appRecordsLocation;

    public ApplicationSaveService()
    {
        _appDataLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        _appTempLocation = Path.Combine(_appDataLocation, "Temp");
        _appSaveDataLocation = Path.Combine(_appDataLocation, "AppSaveData.json");
        _appRecordsLocation = Path.Combine(_appDataLocation, "Records");
    }

    public bool Load()
    {
        try
        {
            if (!File.Exists(_appSaveDataLocation))
            {
                ApplicationSaveData = new ApplicationSaveData();
                SaveData();
            }
            else
            {
                // deserialize JSON directly from a file
                using (StreamReader file = File.OpenText(_appSaveDataLocation))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    ApplicationSaveData = (ApplicationSaveData)serializer.Deserialize(file, typeof(ApplicationSaveData));
                }
            }
         }
        catch (Exception ex) 
        {
            return false;
        }

        return true;
    }

    internal ObservableCollection<OrderInformation> LoadRecords()
    {
        var fileName = $"{DateTime.Now.ToString("MM_dd_yyyy")}.json";
        var path = Path.Combine(_appRecordsLocation, fileName);

        if (!File.Exists(path))
        {
            return new();
        }
        else
        {
            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (ObservableCollection<OrderInformation>)serializer.Deserialize(file, typeof(ObservableCollection<OrderInformation>));
            }
        }
    }

    internal Dictionary<string, List<OrderInformation>> LoadAllRecords()
    {
        var result = new Dictionary<string, List<OrderInformation>>();

        if (!Directory.Exists(_appRecordsLocation))
        {
            Directory.CreateDirectory(_appRecordsLocation);
        }
        else
        {
            var dirInfo =  Directory.GetFiles(_appRecordsLocation, "*.json", SearchOption.TopDirectoryOnly);

            foreach (string logLocation in dirInfo)
            {
                // deserialize JSON directly from a file
                using (StreamReader file = File.OpenText(logLocation))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    var record =  (List<OrderInformation>)serializer.Deserialize(file, typeof(List<OrderInformation>));
                    result.Add(Path.GetFileNameWithoutExtension(logLocation), record);
                }
            }

            return result;
        }

        return new();
    }

    public void SaveData()
{
        if (!File.Exists(_appSaveDataLocation))
        {
            if(!Directory.Exists(_appDataLocation))
            {
                Directory.CreateDirectory(_appDataLocation);
            }
        }

        // serialize JSON directly to a file
        using (StreamWriter file = File.CreateText(_appSaveDataLocation))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, ApplicationSaveData);
        }
    }

    internal void SaveRecords(ObservableCollection<OrderInformation> currentTransaction)
    {
        string output = JsonConvert.SerializeObject(currentTransaction, Formatting.Indented);

        var fileName = $"{DateTime.Now.ToString("MM_dd_yyyy")}.json";

        var path = Path.Combine(_appRecordsLocation, fileName);

        if (!Directory.Exists(_appRecordsLocation))
        {
            Directory.CreateDirectory(_appRecordsLocation);
        }

        File.WriteAllText(path, output);

    }

    internal void AddApprovedImage()
    {
        ApplicationSaveData.ImageCount += 1;
        SaveData();
    }

    internal void AddApprovedPrints(int printAmount)
    {
        ApplicationSaveData.PrintCount += printAmount;
        SaveData();
    }

    internal string? GetTempFolder()
    {
        return _appTempLocation;
    }
}
