using Newtonsoft.Json;
using System.Windows.Media.Imaging;

namespace TheIslandPostManagerWPF.Models;
public class PendingOrder
{
    public string Name { get; set; }

    public string Guid { get; set; }
    [JsonIgnore] public BitmapImage Thumbnail { get; set; }
    public List<ImageObj> PendingImages { get; set; }
    public DateTime Date { get; set; }

    public PendingOrder()
    {
        PendingImages = new();
        Date = DateTime.Now;
    }
}
