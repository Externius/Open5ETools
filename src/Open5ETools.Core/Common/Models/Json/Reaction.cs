using System.Text.Json.Serialization;

namespace Open5ETools.Core.Common.Models.Json;

public class Reaction
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("desc")]
    public string? Desc { get; set; }

    [JsonPropertyName("attack_bonus")]
    public int? AttackBonus { get; set; }
}