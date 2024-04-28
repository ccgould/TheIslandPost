using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Xml;
using TheIslandPostPhotoManager.Models;
using TheIslandPostPhotoManager.Services;

namespace TheIslandPostPhotoManager.ViewModels;
public partial class OrderCompletionPageViewModel : ObservableObject
{
    [ObservableProperty] private OrderInformation order;
    private ImageService imageService;

    public ObservableCollection<ImageObj> OrderImages { get; set; } = new();

    public OrderCompletionPageViewModel(ImageService imageService)
    {
        order = new OrderInformation();

        foreach (var item in imageService.Images)
        {
            if(item.IsSelected)
            {
                OrderImages.Add(item);
            }
        }

        this.imageService = imageService;
    }

    [RelayCommand]
    private async Task Done()
    {
        foreach (var item in OrderImages)
        {
            Order.ApprovedImages.Add(item.Name);
            AddWaterMark(item.ImageUrl);
        }

        

        SaveRecords();

        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }

    private void SaveRecords()
    {
        imageService.FinishOrder(Order);
    }

    private void AddWaterMark(string currentImage)
    {
        //Image image = new Image { Source = currentImage };
        //Image watermarkImage = new Image { Source = @"C:\Users\Public\Pictures\Sample Pictures\watermark.png" };

        //using (Graphics imageGraphics = Graphics.FromImage(image))
        //using (TextureBrush watermarkBrush = new TextureBrush(watermarkImage))
        //{
        //    int x = (image.Width / 2 - watermarkImage.Width / 2);
        //    int y = (image.Height / 2 - watermarkImage.Height / 2);
        //    watermarkBrush.TranslateTransform(x, y);
        //    imageGraphics.FillRectangle(watermarkBrush, new Rectangle(new Point(x, y), new Size(watermarkImage.Width + 1, watermarkImage.Height)));
        //    image.Save(@"C:\Users\Public\Pictures\Sample Pictures\Desert_watermark.jpg");
        //}
    }
}
