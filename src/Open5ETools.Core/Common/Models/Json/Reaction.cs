using System.Text.Json.Serialization;

namespace Open5ETools.Core.Common.Models.Json;

public record Reaction(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("desc")] string? Desc,
    [property: JsonPropertyName("attack_bonus")]
    int? AttackBonus
);