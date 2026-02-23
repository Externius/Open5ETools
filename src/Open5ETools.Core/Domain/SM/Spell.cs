using System.ComponentModel.DataAnnotations;
using Open5ETools.Core.Common.Enums.SM;

namespace Open5ETools.Core.Domain.SM;

public class Spell : AuditableEntity
{
    [StringLength(short.MaxValue)] public required string Name { get; set; }
    [StringLength(short.MaxValue)] public required string Desc { get; set; }
    [StringLength(short.MaxValue)] public required string Page { get; set; }
    [StringLength(short.MaxValue)] public required string Range { get; set; }
    [StringLength(short.MaxValue)] public required string Components { get; set; }
    [StringLength(short.MaxValue)] public required string CastingTime { get; set; }
    [StringLength(short.MaxValue)] public required string Level { get; set; }
    [StringLength(short.MaxValue)] public required string Class { get; set; }
    public School School { get; set; }
    public bool Ritual { get; set; }
    public bool Concentration { get; set; }
    [StringLength(short.MaxValue)] public string? HigherLevel { get; set; }
    [StringLength(short.MaxValue)] public string? Material { get; set; }
    [StringLength(short.MaxValue)] public string? Duration { get; set; }
    [StringLength(short.MaxValue)] public string? Archetype { get; set; }
    [StringLength(short.MaxValue)] public string? Circles { get; set; }
    [StringLength(short.MaxValue)] public string? Domains { get; set; }
    [StringLength(short.MaxValue)] public string? Oaths { get; set; }
    [StringLength(short.MaxValue)] public string? Patrons { get; set; }
}