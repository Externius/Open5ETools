using System.ComponentModel.DataAnnotations;

namespace Open5ETools.Core.Domain.DM;

public class DungeonOption : AuditableEntity
{
    [StringLength(short.MaxValue)] public required string DungeonName { get; set; }
    public int DungeonSize { get; set; }
    public int DungeonDifficulty { get; set; }
    public int PartyLevel { get; set; }
    public int PartySize { get; set; }
    public double TreasureValue { get; set; }
    public int ItemsRarity { get; set; }
    public int RoomDensity { get; set; }
    public int RoomSize { get; set; }
    [StringLength(50)] public required string MonsterType { get; set; }
    public int TrapPercent { get; set; }
    public bool DeadEnd { get; set; }
    public bool Corridor { get; set; }
    public int RoamingPercent { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public List<Dungeon> Dungeons { get; set; } = [];
}