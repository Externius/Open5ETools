using Open5ETools.Core.Common.Enums.SM;
using Open5ETools.Core.Common.Models.SM;
using Open5ETools.Core.Domain.SM;

namespace Open5ETools.Core.Common.Mappers;

public static class SpellMapper
{
    public static SpellModel ToModel(this Spell spell)
    {
        return new SpellModel
        (
            spell.Name,
            spell.Desc,
            spell.HigherLevel,
            spell.Page,
            spell.Range,
            spell.Components,
            spell.Material,
            spell.Ritual,
            spell.Duration,
            spell.Concentration,
            spell.CastingTime,
            spell.Level,
            spell.School,
            spell.Class,
            spell.Archetype,
            spell.Circles,
            spell.Domains,
            spell.Oaths,
            spell.Patrons,
            spell.Id,
            spell.Timestamp,
            spell.CreatedBy,
            spell.Created,
            spell.LastModifiedBy,
            spell.LastModified
        );
    }

    public static Spell ToEntity(this Open5ETools.Core.Common.Models.Json.Spell spell)
    {
        return new Spell
        {
            Name = spell.Name,
            Desc = spell.Desc,
            HigherLevel = spell.HigherLevel,
            Page = spell.Page,
            Range = spell.Range ?? string.Empty,
            Components = spell.Components ?? string.Empty,
            Material = spell.Material,
            Ritual = ConvertToBool(spell.Ritual),
            Duration = spell.Duration,
            Concentration = ConvertToBool(spell.Concentration),
            CastingTime = spell.CastingTime ?? string.Empty,
            Level = spell.Level,
            School = Enum.Parse<School>(spell.School ?? string.Empty),
            Class = spell.Class ?? string.Empty,
            Archetype = spell.Archetype,
            Circles = spell.Circles,
            Domains = spell.Domains,
            Oaths = spell.Oaths,
            Patrons = spell.Patrons,
            CreatedBy = string.Empty,
            LastModifiedBy = string.Empty
        };
    }

    private static bool ConvertToBool(string? property)
    {
        return string.IsNullOrWhiteSpace(property) ||
               !property.Equals("no", StringComparison.InvariantCultureIgnoreCase);
    }
}