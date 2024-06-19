using Newtonsoft.Json;
using System.IO;

namespace TheIslandPostManagerWPF.Models;
public class OrderImageSelectionData
{
    public string Path { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }

    [JsonIgnore]
    public OrderImageSelectionData ParentImage { get; set; }
    public string Root => System.IO.Path.GetDirectoryName(Path);

    internal void CopyParentData()
    {
        Path = ParentImage.Path;
        Name = ParentImage.Name;
    }
}
