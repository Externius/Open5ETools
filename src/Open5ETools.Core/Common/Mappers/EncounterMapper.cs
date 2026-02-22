using Open5ETools.Core.Common.Enums.EG;
using Open5ETools.Core.Common.Extensions;
using Open5ETools.Core.Common.Models.EG;
using Open5ETools.Core.Domain.EG;

namespace Open5ETools.Core.Common.Mappers;

public static class EncounterMapper
{
    extension(Monster monster)
    {
        public MonsterModel ToModel(Difficulty difficulty, int allXp, int count)
        {
            return new MonsterModel(
                new JsonMonsterModel(
                    allXp,
                    count,
                    monster.JsonMonster.Name,
                    GetTranslation<MonsterType>(monster.JsonMonster.Type),
                    difficulty.GetName(Resources.Enum.ResourceManager),
                    monster.JsonMonster.ChallengeRating,
                    GetTranslation<Size>(monster.JsonMonster.Size),
                    monster.JsonMonster.Alignment ?? string.Empty,
                    monster.JsonMonster.HitPoints ?? 0,
                    monster.JsonMonster.ArmorClass ?? 0,
                    monster.JsonMonster.HitDice ?? string.Empty,
                    monster.JsonMonster.Speed ?? string.Empty,
                    monster.JsonMonster.Senses ?? string.Empty,
                    monster.JsonMonster.Languages ?? string.Empty,
                    monster.JsonMonster.Strength ?? 0,
                    monster.JsonMonster.Dexterity ?? 0,
                    monster.JsonMonster.Constitution ?? 0,
                    monster.JsonMonster.Intelligence ?? 0,
                    monster.JsonMonster.Wisdom ?? 0,
                    monster.JsonMonster.Charisma ?? 0,
                    monster.JsonMonster.StrengthSave ?? 0,
                    monster.JsonMonster.DexteritySave ?? 0,
                    monster.JsonMonster.ConstitutionSave ?? 0,
                    monster.JsonMonster.IntelligenceSave ?? 0,
                    monster.JsonMonster.WisdomSave ?? 0,
                    monster.JsonMonster.CharismaSave ?? 0,
                    monster.JsonMonster.History ?? 0,
                    monster.JsonMonster.Perception ?? 0,
                    monster.JsonMonster.DamageVulnerabilities,
                    monster.JsonMonster.DamageResistances,
                    monster.JsonMonster.DamageImmunities,
                    monster.JsonMonster.ConditionImmunities,
                    monster.JsonMonster.SpecialAbilities?.ToArray() ?? [],
                    monster.JsonMonster.Actions?.ToArray() ?? [],
                    monster.JsonMonster.LegendaryActions?.ToArray() ?? [],
                    monster.JsonMonster.Reactions?.ToArray() ?? []
                ),
                monster.Id,
                monster.Timestamp,
                monster.CreatedBy,
                monster.Created,
                monster.LastModifiedBy,
                monster.LastModified
            );
        }

        public MonsterModel ToModel()
        {
            return new MonsterModel(
                new JsonMonsterModel(
                    0,
                    0,
                    monster.JsonMonster.Name,
                    GetTranslation<MonsterType>(monster.JsonMonster.Type),
                    string.Empty,
                    monster.JsonMonster.ChallengeRating,
                    GetTranslation<Size>(monster.JsonMonster.Size),
                    monster.JsonMonster.Alignment ?? string.Empty,
                    monster.JsonMonster.HitPoints ?? 0,
                    monster.JsonMonster.ArmorClass ?? 0,
                    monster.JsonMonster.HitDice ?? string.Empty,
                    monster.JsonMonster.Speed ?? string.Empty,
                    monster.JsonMonster.Senses ?? string.Empty,
                    monster.JsonMonster.Languages ?? string.Empty,
                    monster.JsonMonster.Strength ?? 0,
                    monster.JsonMonster.Dexterity ?? 0,
                    monster.JsonMonster.Constitution ?? 0,
                    monster.JsonMonster.Intelligence ?? 0,
                    monster.JsonMonster.Wisdom ?? 0,
                    monster.JsonMonster.Charisma ?? 0,
                    monster.JsonMonster.StrengthSave ?? 0,
                    monster.JsonMonster.DexteritySave ?? 0,
                    monster.JsonMonster.ConstitutionSave ?? 0,
                    monster.JsonMonster.IntelligenceSave ?? 0,
                    monster.JsonMonster.WisdomSave ?? 0,
                    monster.JsonMonster.CharismaSave ?? 0,
                    monster.JsonMonster.History ?? 0,
                    monster.JsonMonster.Perception ?? 0,
                    monster.JsonMonster.DamageVulnerabilities,
                    monster.JsonMonster.DamageResistances,
                    monster.JsonMonster.DamageImmunities,
                    monster.JsonMonster.ConditionImmunities,
                    monster.JsonMonster.SpecialAbilities?.ToArray() ?? [],
                    monster.JsonMonster.Actions?.ToArray() ?? [],
                    monster.JsonMonster.LegendaryActions?.ToArray() ?? [],
                    monster.JsonMonster.Reactions?.ToArray() ?? []
                ),
                monster.Id,
                monster.Timestamp,
                monster.CreatedBy,
                monster.Created,
                monster.LastModifiedBy,
                monster.LastModified
            );
        }
    }

    private static string GetTranslation<T>(string original) where T : struct, Enum
    {
        string? translation = null;
        if (Enum.TryParse(original, out T type))
            translation = type.GetName(Resources.Enum.ResourceManager);
        return translation ?? original;
    }
}