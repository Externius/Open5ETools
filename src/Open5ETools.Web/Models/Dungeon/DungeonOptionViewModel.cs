using System.ComponentModel.DataAnnotations;

namespace Open5ETools.Web.Models.Dungeon;

public class DungeonOptionViewModel : EditViewModel
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string DungeonName { get; set; }

    [Required] public required int DungeonSize { get; set; }
    [Required] public required int DungeonDifficulty { get; set; }
    [Required] public required int PartyLevel { get; set; }
    [Required] public required int PartySize { get; set; }
    [Required] public required double TreasureValue { get; set; }
    [Required] public required int ItemsRarity { get; set; }
    [Required] public required int RoomDensity { get; set; }
    [Required] public required int RoomSize { get; set; }
    [Required] public required string MonsterType { get; set; }
    [Required] public required int TrapPercent { get; set; }
    [Required] public required bool DeadEnd { get; set; }
    [Required] public required bool Corridor { get; set; }
    [Required] public required int RoamingPercent { get; set; }
    public required DungeonViewModel[] Dungeons { get; set; }
}