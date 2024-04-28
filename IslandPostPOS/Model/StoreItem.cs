using IslandPostPOS.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandPostPOS.Model;
public class StoreItem
{
    public int ID { get; set; }
    public string Name { get; set; }
    public MediaType MediaType { get; set; }
    public int Multiplyer { get; set; } = 1;
    public int Cost { get; set; } = 5;
}
