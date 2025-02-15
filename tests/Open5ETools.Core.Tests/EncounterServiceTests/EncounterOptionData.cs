using Open5ETools.Core.Common.Enums.EG;
using Open5ETools.Core.Common.Models.EG;

namespace Open5ETools.Core.Tests.EncounterServiceTests;

public static class EncounterOptionData
{
    public static TheoryData<EncounterOption> Data =>
    [
        new EncounterOption
        {
            PartyLevel = 4,
            PartySize = 3,
            MonsterTypes = [MonsterType.Aberration],
            Sizes = [Size.Small, Size.Medium],
            Count = 1
        },
        new EncounterOption
        {
            PartyLevel = 3,
            PartySize = 4,
            MonsterTypes = [MonsterType.Ooze, MonsterType.Fey],
            Sizes = [Size.Tiny, Size.Small, Size.Medium],
            Count = 6
        },
        new EncounterOption
        {
            PartyLevel = 4,
            PartySize = 5,
            MonsterTypes = [MonsterType.Elemental, MonsterType.Giant, MonsterType.Fiend],
            Sizes = [Size.Small, Size.Medium, Size.Large],
            Count = 3
        },
        new EncounterOption
        {
            PartyLevel = 8,
            PartySize = 4,
            MonsterTypes = [MonsterType.Beast, MonsterType.Humanoid, MonsterType.Celestial, MonsterType.Dragon],
            Sizes = [Size.Medium, Size.Large, Size.Huge],
            Count = 5
        },
        new EncounterOption
        {
            PartyLevel = 12,
            PartySize = 3,
            MonsterTypes =
            [
                MonsterType.SwarmOfTinyBeasts, MonsterType.Undead, MonsterType.Construct,
                MonsterType.Plant, MonsterType.Monstrosity
            ],
            Sizes = [Size.Medium, Size.Large, Size.Huge, Size.Gargantuan],
            Count = 2
        }
    ];

    public static TheoryData<Difficulty, int, int> FilterWithDifficultyData =>
        new()
        {
            { Difficulty.Easy, 1, 4 },
            { Difficulty.Medium, 2, 3 },
            { Difficulty.Hard, 4, 5 },
            { Difficulty.Deadly, 7, 3 },
        };
}