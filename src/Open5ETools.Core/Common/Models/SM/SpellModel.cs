using Open5ETools.Core.Common.Enums.SM;
using Open5ETools.Core.Common.Models.Services;

namespace Open5ETools.Core.Common.Models.SM;

public record SpellModel(
    string Name,
    string Desc,
    string? HigherLevel,
    string Page,
    string Range,
    string Components,
    string? Material,
    bool Ritual,
    string? Duration,
    bool Concentration,
    string CastingTime,
    string Level,
    School School,
    string Class,
    string? Archetype,
    string? Circles,
    string? Domains,
    string? Oaths,
    string? Patrons,
    int Id,
    byte[] Timestamp,
    string CreatedBy,
    DateTime Created,
    string LastModifiedBy,
    DateTime LastModified
) : EditModel(Id, Timestamp, CreatedBy, Created, LastModifiedBy, LastModified);