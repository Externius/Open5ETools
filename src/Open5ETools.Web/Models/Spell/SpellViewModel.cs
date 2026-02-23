using Open5ETools.Core.Common.Enums.SM;

namespace Open5ETools.Web.Models.Spell;

public class SpellViewModel : EditViewModel
{
    public required string Name { get; init; }
    public required string Desc { get; init; }
    public required string CastingTime { get; init; }
    public required string Level { get; init; }
    public required string Class { get; init; }
    public required string Page { get; init; }
    public required string Range { get; init; }
    public required string Components { get; init; }
    public required School School { get; init; }
    public bool Ritual { get; init; }
    public bool Concentration { get; init; }
    public string? Duration { get; init; }
    public string? Material { get; init; }
    public string? HigherLevel { get; init; }
    public string? Archetype { get; init; }
    public string? Circles { get; init; }
    public string? Domains { get; init; }
    public string? Oaths { get; init; }
    public string? Patrons { get; init; }
}