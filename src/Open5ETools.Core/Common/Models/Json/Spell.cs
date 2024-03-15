using System.Text.Json.Serialization;

namespace Open5ETools.Core.Common.Models.Json;
public class Spell
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("desc")]
    public string Desc { get; set; } = string.Empty;

    [JsonPropertyName("higher_level")]
    public string? HigherLevel { get; set; }

    [JsonPropertyName("page")]
    public string Page { get; set; } = string.Empty;

    [JsonPropertyName("range")]
    public string? Range { get; set; }

    [JsonPropertyName("components")]
    public string? Components { get; set; }

    [JsonPropertyName("material")]
    public string? Material { get; set; }

    [JsonPropertyName("ritual")]
    public string? Ritual { get; set; }

    [JsonPropertyName("duration")]
    public string? Duration { get; set; }

    [JsonPropertyName("concentration")]
    public string? Concentration { get; set; }

    [JsonPropertyName("casting_time")]
    public string? CastingTime { get; set; }

    [JsonPropertyName("level")]
    public string Level { get; set; } = string.Empty;

    [JsonPropertyName("school")]
    public string? School { get; set; }

    [JsonPropertyName("class")]
    public string? Class { get; set; }

    [JsonPropertyName("archetype")]
    public string? Archetype { get; set; }

    [JsonPropertyName("circles")]
    public string? Circles { get; set; }

    [JsonPropertyName("domains")]
    public string? Domains { get; set; }

    [JsonPropertyName("oaths")]
    public string? Oaths { get; set; }

    [JsonPropertyName("patrons")]
    public string? Patrons { get; set; }
}