namespace Open5ETools.Core.Common.Models.DM.Generator;

public class RoomDescription(string name, string treasure, string monster, string doors)
{
    public string Name { get; } = name;
    public string Treasure { get; } = treasure;
    public string Monster { get; } = monster;
    public string Doors { get; } = doors;
}