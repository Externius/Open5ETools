using System.ComponentModel.DataAnnotations;

namespace Open5ETools.Web.Models.Dungeon;

public class DungeonViewModel : EditViewModel
{
    [Required]
    public required string DungeonTiles { get; init; }
    [Required]
    public required string RoomDescription { get; init; }
    public required string TrapDescription { get; init; }
    public required string RoamingMonsterDescription { get; init; } 
    public int DungeonOptionId { get; set; }
    public int Level { get; init; }
}