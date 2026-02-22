using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Open5ETools.Web.Models.Dungeon;

public class DungeonOptionCreateViewModel : EditViewModel
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    [Display(ResourceType = typeof(Resources.Dungeon), Name = "Name")]
    public required string DungeonName { get; set; }

    [Required] public required int DungeonSize { get; set; }
    [Required] public required int DungeonDifficulty { get; set; }
    [Required] public required int PartyLevel { get; set; }
    [Required] public required int PartySize { get; set; }
    [Required] public required string TreasureValue { get; set; }
    [Required] public required int ItemsRarity { get; set; }
    [Required] public required int RoomDensity { get; set; }
    [Required] public required int RoomSize { get; set; }
    [Required] public required string[]? MonsterType { get; set; }
    [Required] public required int TrapPercent { get; set; }
    [Required] public required bool DeadEnd { get; set; }
    [Required] public required bool Corridor { get; set; }
    [Required] public required int RoamingPercent { get; set; }
    public int Theme { get; set; }

    [Display(ResourceType = typeof(Resources.Dungeon), Name = "DungeonSize")]
    public SelectListItem[] DungeonSizes { get; set; } = [];

    [Display(ResourceType = typeof(Resources.Dungeon), Name = "DungeonDifficulty")]
    public SelectListItem[] DungeonDifficulties { get; set; } = [];

    [Display(ResourceType = typeof(Resources.Common), Name = "PartyLevel")]
    public SelectListItem[] PartyLevels { get; set; } = [];

    [Display(ResourceType = typeof(Resources.Common), Name = "PartySize")]
    public SelectListItem[] PartySizes { get; set; } = [];

    [Display(ResourceType = typeof(Resources.Dungeon), Name = "TreasureValue")]
    public SelectListItem[] TreasureValues { get; set; } = [];

    [Display(ResourceType = typeof(Resources.Dungeon), Name = "ItemsRarity")]
    public SelectListItem[] ItemsRarities { get; set; } = [];

    [Display(ResourceType = typeof(Resources.Dungeon), Name = "RoomDensity")]
    public SelectListItem[] RoomDensities { get; set; } = [];

    [Display(ResourceType = typeof(Resources.Dungeon), Name = "RoomSize")]
    public SelectListItem[] RoomSizes { get; set; } = [];

    [Display(ResourceType = typeof(Resources.Dungeon), Name = "MonsterType")]
    public SelectListItem[] MonsterTypes { get; set; } = [];

    [Display(ResourceType = typeof(Resources.Dungeon), Name = "TrapPercent")]
    public SelectListItem[] TrapPercents { get; set; } = [];

    [Display(ResourceType = typeof(Resources.Dungeon), Name = "DeadEnd")]
    public SelectListItem[] DeadEnds { get; set; } = [];

    [Display(ResourceType = typeof(Resources.Dungeon), Name = "Corridor")]
    public SelectListItem[] Corridors { get; set; } = [];

    [Display(ResourceType = typeof(Resources.Dungeon), Name = "RoamingPercent")]
    public SelectListItem[] RoamingPercents { get; set; } = [];

    [Display(ResourceType = typeof(Resources.Dungeon), Name = "Theme")]
    public SelectListItem[] Themes { get; set; } = [];

    public int UserId { get; init; }
    public bool AddDungeon { get; set; }
    public int Level { get; set; }
}