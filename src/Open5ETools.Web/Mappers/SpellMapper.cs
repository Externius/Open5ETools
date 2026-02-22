using Open5ETools.Core.Common.Models.SM;
using Open5ETools.Web.Models.Spell;

namespace Open5ETools.Web.Mappers;

public static class SpellMapper
{
    public static SpellViewModel ToModel(this SpellModel spellModel)
    {
        return new SpellViewModel
        {
            Name = spellModel.Name,
            Desc = spellModel.Desc,
            HigherLevel = spellModel.HigherLevel,
            Page = spellModel.Page,
            Range = spellModel.Range,
            Components = spellModel.Components,
            Material = spellModel.Material,
            Ritual = spellModel.Ritual,
            Duration = spellModel.Duration,
            Concentration = spellModel.Concentration,
            CastingTime = spellModel.CastingTime,
            Level = spellModel.Level,
            School = spellModel.School,
            Class = spellModel.Class,
            Archetype = spellModel.Archetype,
            Circles = spellModel.Circles,
            Domains = spellModel.Domains,
            Oaths = spellModel.Oaths,
            Patrons = spellModel.Patrons,
            Id = spellModel.Id,
            Timestamp = spellModel.Timestamp,
            CreatedBy = spellModel.CreatedBy,
            Created = spellModel.Created,
            LastModifiedBy = spellModel.LastModifiedBy,
            LastModified = spellModel.LastModified
        };
    }
}