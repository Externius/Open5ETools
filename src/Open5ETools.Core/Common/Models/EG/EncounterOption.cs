using Open5ETools.Core.Common.Enums.EG;

namespace Open5ETools.Core.Common.Models.EG;

public record EncounterOption(
    int PartyLevel,
    int PartySize,
    Difficulty? Difficulty,
    MonsterType[] MonsterTypes,
    Size[] Sizes,
    int Count = 10
);