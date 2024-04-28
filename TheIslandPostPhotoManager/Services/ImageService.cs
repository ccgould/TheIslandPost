using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheIslandPostPhotoManager.Models;
using TheIslandPostPhotoManager.ViewModels;

namespace TheIslandPostPhotoManager.Services;
public partial class ImageService : ObservableObject
{

    public ObservableCollection<ImageObj> Images { get; set; } = new();

    [ObservableProperty] private ImageObj selectedImage;
    private int currentIndex = 0;
    public ObservableCollection<OrderInformation> CurrentTransaction = new();

    public ImageService()
    {
        var files = Directory.GetFiles("C:\\Users\\SPV_s\\OneDrive\\Desktop\\", "*.JPG", SearchOption.TopDirectoryOnly);

        foreach (var file in files)
        {
            Images.Add(new ImageObj
            {
                ImageUrl = file,
                Name = Path.GetFileName(file),
                Width = 300,
                Height = 450,
            });
        }

        
    }

    internal void ChangeSelectedImage(bool getNextImage)
    {
        if(SelectedImage is not null)
        {
            var index = Images.IndexOf(SelectedImage);

            if (getNextImage)
            {
                if (index + 1 > Images.Count - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
            else
            {
                if (index - 1 == -1)
                {
                    index = Images.Count - 1;
                }
                else
                {
                    index--;
                }
            }

            currentIndex = index;
            SelectedImage = Images.ElementAt(index);


        }
    }

    internal List<ImageObj> GetImages()
    {
        throw new NotImplementedException();
    }

    internal void FinishOrder(OrderInformation order)
    {
        CurrentTransaction.Add(order);
        SaveRecords();
        
    }

    private void SaveRecords()
    {
        string output = JsonConvert.SerializeObject(new DataSave
        {
            CurrentTransaction = CurrentTransaction
        }, Formatting.Indented);

        var fileName = $"IslandPostSave_{DateTime.Now.ToString("MM_dd_yyyy")}.json";
        string mainDir = FileSystem.Current.AppDataDirectory;
        var rootFolder = Path.GetDirectoryName(mainDir);
        var savesDirectory = Path.Combine(rootFolder, "Data", "Saves");
        var path = Path.Combine(savesDirectory, fileName);

        if (!Directory.Exists(savesDirectory))
        {
            Directory.CreateDirectory(savesDirectory);
        }

        File.WriteAllText(path, output);

    }
}
