using Open5ETools.Core.Common.Models.EG;
using Open5ETools.Web.Models.Encounter;

namespace Open5ETools.Web.Mappers;

public static class EncounterMapper
{
    public static EncounterOption ToModel(this EncounterOptionViewModel model)
    {
        return new EncounterOption
        (
            model.PartyLevel,
            model.PartySize,
            model.Difficulty,
            model.SelectedMonsterTypes,
            model.SelectedSizes,
            model.Count
        );
    }

    public static EncounterViewModel ToViewModel(this EncounterModel model)
    {
        return new EncounterViewModel
        {
            Monsters = [.. model.Monsters.Select(m => m.ToViewModel())]
        };
    }

    public static MonsterViewModel ToViewModel(this MonsterModel monster)
    {
        return new MonsterViewModel
        {
            Monster = monster.JsonMonsterModel,
            Id = monster.Id,
            Timestamp = monster.Timestamp,
            Created = monster.Created,
            CreatedBy = monster.CreatedBy,
            LastModified = monster.LastModified,
            LastModifiedBy = monster.LastModifiedBy
        };
    }
}