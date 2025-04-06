namespace Open5ETools.Core.Common.Models.DM.Generator;

public record TreasureDescription(
    string Name,
    int Cost,
    int Rarity,
    bool Magical,
    IReadOnlyCollection<string> Types
);