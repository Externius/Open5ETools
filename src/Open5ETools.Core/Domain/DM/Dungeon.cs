using System.ComponentModel.DataAnnotations;

namespace Open5ETools.Core.Domain.DM;

public class Dungeon : AuditableEntity
{
    [StringLength(int.MaxValue)] public required string DungeonTiles { get; set; }
    [StringLength(short.MaxValue)] public required string RoomDescription { get; set; }
    [StringLength(short.MaxValue)] public required string TrapDescription { get; set; }
    [StringLength(short.MaxValue)] public string? RoamingMonsterDescription { get; set; }
    public int Level { get; set; }
    public int DungeonOptionId { get; set; }
    public DungeonOption? DungeonOption { get; set; }
}