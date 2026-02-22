using System.Text.Json.Serialization;

namespace Open5ETools.Core.Common.Models.Json;

public record Spell(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("desc")] string Desc,
    [property: JsonPropertyName("higher_level")]
    string? HigherLevel,
    [property: JsonPropertyName("page")] string Page,
    [property: JsonPropertyName("range")] string? Range,
    [property: JsonPropertyName("components")]
    string? Components,
    [property: JsonPropertyName("material")]
    string? Material,
    [property: JsonPropertyName("ritual")] string? Ritual,
    [property: JsonPropertyName("duration")]
    string? Duration,
    [property: JsonPropertyName("concentration")]
    string? Concentration,
    [property: JsonPropertyName("casting_time")]
    string? CastingTime,
    [property: JsonPropertyName("level")] string Level,
    [property: JsonPropertyName("school")] string? School,
    [property: JsonPropertyName("class")] string? Class,
    [property: JsonPropertyName("archetype")]
    string? Archetype,
    [property: JsonPropertyName("circles")]
    string? Circles,
    [property: JsonPropertyName("domains")]
    string? Domains,
    [property: JsonPropertyName("oaths")] string? Oaths,
    [property: JsonPropertyName("patrons")]
    string? Patrons
);