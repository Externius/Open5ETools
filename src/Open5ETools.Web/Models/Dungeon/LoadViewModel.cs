using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Open5ETools.Web.Models.Dungeon;

public class LoadViewModel
{
    public required DungeonOptionViewModel Option { get; set; }
    public required string Theme { get; set; }

    [Display(ResourceType = typeof(Resources.Dungeon), Name = "Theme")]
    public SelectListItem[] Themes { get; init; } = [];

    [Display(ResourceType = typeof(Resources.Dungeon), Name = "GeneratePlainMap")]
    public bool GeneratePlainMap { get; set; }
}