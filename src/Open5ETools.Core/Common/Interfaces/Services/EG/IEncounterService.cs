using Open5ETools.Core.Common.Models.EG;

namespace Open5ETools.Core.Common.Interfaces.Services.EG;

public interface IEncounterService
{
    Task<MonsterModel> GetMonsterByIdAsync(int id);
    Task<EncounterModel> GenerateAsync(EncounterOption option);
    Task<KeyValuePair<string, int>[]> GetEnumListAsync<T>() where T : struct;
}