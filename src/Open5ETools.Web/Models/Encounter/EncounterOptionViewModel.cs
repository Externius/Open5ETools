using Microsoft.AspNetCore.Mvc.Rendering;
using Open5ETools.Core.Common.Enums.EG;
using System.ComponentModel.DataAnnotations;
namespace Open5ETools.Web.Models.Encounter;

public class EncounterOptionViewModel : EditViewModel
{
    [Required]
    public int PartyLevel { get; set; }
    [Display(ResourceType = typeof(Resources.Common), Name = "PartyLevel")]
    public IEnumerable<SelectListItem> PartyLevels { get; set; } = [];
    [Required]
    public int PartySize { get; set; }
    [Display(ResourceType = typeof(Resources.Common), Name = "PartySize")]
    public IEnumerable<SelectListItem> PartySizes { get; set; } = [];
    public Difficulty? Difficulty { get; set; }
    [Display(ResourceType = typeof(Resources.Common), Name = "Difficulty")]
    public IEnumerable<SelectListItem> Difficulties { get; set; } = [];
    public List<MonsterType> SelectedMonsterTypes { get; set; } = [];
    [Display(ResourceType = typeof(Resources.Encounter), Name = "MonsterType")]
    public IEnumerable<SelectListItem> MonsterTypes { get; set; } = [];
    public List<Size> SelectedSizes { get; set; } = [];
    [Display(ResourceType = typeof(Resources.Common), Name = "Size")]
    public IEnumerable<SelectListItem> Sizes { get; set; } = [];
    public int Count { get; set; } = 9;
}