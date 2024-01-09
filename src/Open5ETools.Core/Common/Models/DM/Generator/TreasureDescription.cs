namespace Open5ETools.Core.Common.Models.DM.Generator;

public class TreasureDescription
{
    public string Name { get; set; } = string.Empty;
    public int Cost { get; set; }
    public int Rarity { get; set; }
    public bool Magical { get; set; }
    public List<string> Types { get; set; } = [];
}