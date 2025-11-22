using Open5ETools.Core.Common.Models.DM.Services;

namespace Open5ETools.Core.Common.Interfaces.Services.DM;

public interface IDungeonService
{
    Task<DungeonOptionModel[]> GetAllDungeonOptionsAsync(CancellationToken cancellationToken);
    Task<DungeonOptionModel[]> GetAllDungeonOptionsForUserAsync(int userId, CancellationToken cancellationToken);
    Task<DungeonOptionModel> GetDungeonOptionAsync(int id, CancellationToken cancellationToken);
    Task<DungeonOptionModel?> GetDungeonOptionByNameAsync(string dungeonName, int userId, CancellationToken cancellationToken);
    Task<DungeonModel> GetDungeonAsync(int id, CancellationToken cancellationToken);
    Task<DungeonModel> CreateOrUpdateDungeonAsync(DungeonOptionModel optionModel, bool addDungeon, int level, CancellationToken cancellationToken);
    Task UpdateDungeonAsync(DungeonModel model, CancellationToken cancellationToken);
    Task<int> CreateDungeonOptionAsync(DungeonOptionModel dungeonOption, CancellationToken cancellationToken);
    Task<DungeonModel[]> ListUserDungeonsAsync(int userId, CancellationToken cancellationToken);
    Task<DungeonModel[]> ListUserDungeonsByNameAsync(string dungeonName, int userId, CancellationToken cancellationToken);
    Task<int> AddDungeonAsync(DungeonModel savedDungeon, CancellationToken cancellationToken);
    Task<bool> DeleteDungeonOptionAsync(int id, CancellationToken cancellationToken);
    Task<bool> DeleteDungeonAsync(int id, CancellationToken cancellationToken);
    Task<DungeonModel> GenerateDungeonAsync(DungeonOptionModel model);
    Task RenameDungeonAsync(int optionId, int userId, string newName, CancellationToken cancellationToken);
    Task<string> ExportToJsonAsync(int dungeonId, CancellationToken cancellationToken);
}