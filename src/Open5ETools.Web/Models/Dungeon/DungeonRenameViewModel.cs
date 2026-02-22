using System.ComponentModel.DataAnnotations;

namespace Open5ETools.Web.Models.Dungeon;

public class DungeonRenameViewModel : EditViewModel
{
    public int UserId { get; init; }
    [Required]
    [StringLength(50, MinimumLength = 3)]
    [Display(ResourceType = typeof(Resources.Dungeon), Name = "Name")]
    public required string DungeonName { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 3)]
    [Display(ResourceType = typeof(Resources.Dungeon), Name = "NewName")]
    public required string NewDungeonName { get; set; }
}