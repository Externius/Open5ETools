using Open5ETools.Core.Common.Enums.SM;

namespace Open5ETools.Web.Models.Spell;

public class SpellViewModel : EditViewModel
{
    public string Name { get; set; } = string.Empty;

    public string Desc { get; set; } = string.Empty;

    public string? HigherLevel { get; set; }

    public string Page { get; set; } = string.Empty;

    public string Range { get; set; } = string.Empty;

    public string Components { get; set; } = string.Empty;

    public string? Material { get; set; }

    public bool Ritual { get; set; }

    public string? Duration { get; set; }

    public bool Concentration { get; set; }

    public string CastingTime { get; set; } = string.Empty;

    public string Level { get; set; } = string.Empty;

    public School School { get; set; }

    public string Class { get; set; } = string.Empty;

    public string? Archetype { get; set; }

    public string? Circles { get; set; }

    public string? Domains { get; set; }

    public string? Oaths { get; set; }

    public string? Patrons { get; set; }
}