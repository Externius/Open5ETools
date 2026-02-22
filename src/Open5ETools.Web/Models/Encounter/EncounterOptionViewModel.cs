using Microsoft.AspNetCore.Mvc.Rendering;
using Open5ETools.Core.Common.Enums.EG;
using System.ComponentModel.DataAnnotations;

namespace Open5ETools.Web.Models.Encounter;

public class EncounterOptionViewModel : EditViewModel
{
    [Required] public int PartyLevel { get; set; }

    [Display(ResourceType = typeof(Resources.Common), Name = "PartyLevel")]
    public SelectListItem[] PartyLevels { get; init; } = [];

    [Required] public int PartySize { get; set; }

    [Display(ResourceType = typeof(Resources.Common), Name = "PartySize")]
    public SelectListItem[] PartySizes { get; init; } = [];

    public Difficulty? Difficulty { get; set; }

    [Display(ResourceType = typeof(Resources.Common), Name = "Difficulty")]
    public SelectListItem[] Difficulties { get; init; } = [];

    public MonsterType[] SelectedMonsterTypes { get; set; } = [];

    [Display(ResourceType = typeof(Resources.Encounter), Name = "MonsterType")]
    public SelectListItem[] MonsterTypes { get; init; } = [];

    public Size[] SelectedSizes { get; set; } = [];

    [Display(ResourceType = typeof(Resources.Common), Name = "Size")]
    public SelectListItem[] Sizes { get; init; } = [];

    public int Count { get; set; } = 9;
}