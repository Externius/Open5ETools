using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Open5ETools.Core.Common.Exceptions;
using Open5ETools.Core.Common.Interfaces.Data;
using Open5ETools.Core.Common.Interfaces.Services.DM;
using Open5ETools.Core.Common.Interfaces.Services.DM.Generator;
using Open5ETools.Core.Common.Models.DM.Services;
using Open5ETools.Core.Domain.DM;
using Open5ETools.Resources;

namespace Open5ETools.Core.Services.DM;

public class DungeonService(
    IMapper mapper,
    IAppDbContext context,
    IDungeon dungeon,
    IDungeonNoCorridor dungeonNcDungeon,
    ILogger<DungeonService> logger) : IDungeonService
{
    private readonly IAppDbContext _context = context;
    private readonly IDungeon _dungeon = dungeon;
    private readonly IDungeonNoCorridor _dungeonNcDungeon = dungeonNcDungeon;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async Task<int> CreateDungeonOptionAsync(DungeonOptionModel dungeonOption,
        CancellationToken cancellationToken)
    {
        try
        {
            ValidateModel(dungeonOption);
            var option = _mapper.Map<DungeonOption>(dungeonOption);
            _context.DungeonOptions.Add(option);
            await _context.SaveChangesAsync(cancellationToken);
            return option.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Create dungeon option failed.");
            throw;
        }
    }

    private static void ValidateModel(DungeonOptionModel model)
    {
        var errors = new List<ServiceException>();
        if (string.IsNullOrWhiteSpace(model.DungeonName))
            errors.Add(new ServiceException(string.Format(Error.RequiredValidation, nameof(model.DungeonName))));
        if (model.UserId == 0)
            errors.Add(new ServiceException(string.Format(Error.RequiredValidation, nameof(model.UserId))));
        if (errors.Count != 0)
            throw new ServiceAggregateException(errors);
    }

    public async Task<int> AddDungeonAsync(DungeonModel savedDungeon, CancellationToken cancellationToken)
    {
        try
        {
            var dungeonEntity = _mapper.Map<Domain.DM.Dungeon>(savedDungeon);
            _context.Dungeons.Add(dungeonEntity);
            await _context.SaveChangesAsync(cancellationToken);
            return dungeonEntity.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Add dungeon failed.");
            throw;
        }
    }

    public async Task<DungeonModel> CreateOrUpdateDungeonAsync(DungeonOptionModel optionModel, bool addDungeon,
        int level, CancellationToken cancellationToken)
    {
        try
        {
            ValidateModel(optionModel);
            if (addDungeon)
            {
                return await AddDungeonToExistingOptionAsync(optionModel, level, cancellationToken);
            }

            var existingDungeonOption =
                await GetDungeonOptionByNameAsync(optionModel.DungeonName, optionModel.UserId, cancellationToken);
            if (existingDungeonOption is null)
            {
                return await CreateOptionAndAddDungeonToItAsync(optionModel, cancellationToken);
            }

            // regenerate
            var existingDungeons =
                await ListUserDungeonsByNameAsync(optionModel.DungeonName, optionModel.UserId, cancellationToken);
            var oldDungeon = existingDungeons.FirstOrDefault();
            if (oldDungeon is not null)
            {
                return await UpdateExistingDungeonAsync(optionModel, existingDungeonOption, oldDungeon,
                    cancellationToken);
            }

            optionModel.Id = existingDungeonOption.Id;
            return await AddDungeonToExistingOptionAsync(optionModel, level, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating dungeon.");
            throw;
        }
    }

    private async Task<DungeonModel> UpdateExistingDungeonAsync(DungeonOptionModel optionModel,
        DungeonOptionModel existingDungeonOption, DungeonModel oldDungeon, CancellationToken cancellationToken)
    {
        var dungeonModel = await GenerateDungeonAsync(optionModel, existingDungeonOption.Id);
        dungeonModel.Id = oldDungeon.Id;
        dungeonModel.Level = oldDungeon.Level;
        await UpdateDungeonAsync(dungeonModel, cancellationToken);
        return dungeonModel;
    }

    private async Task<DungeonModel> CreateOptionAndAddDungeonToItAsync(DungeonOptionModel optionModel,
        CancellationToken cancellationToken)
    {
        var dungeonOptionId = await CreateDungeonOptionAsync(optionModel, cancellationToken);
        var dungeonModel = await GenerateDungeonAsync(optionModel, dungeonOptionId);
        dungeonModel.Level = 1;
        var id = await AddDungeonAsync(dungeonModel, cancellationToken);
        dungeonModel.Id = id;
        return dungeonModel;
    }

    private async Task<DungeonModel> AddDungeonToExistingOptionAsync(DungeonOptionModel optionModel, int level,
        CancellationToken cancellationToken)
    {
        var existingDungeons =
            (await ListUserDungeonsByNameAsync(optionModel.DungeonName, optionModel.UserId, cancellationToken))
            .ToList();
        var dungeonModel = await GenerateDungeonAsync(optionModel, optionModel.Id);
        dungeonModel.Level = level;
        if (existingDungeons.Exists(d => d.Level == level))
        {
            dungeonModel.Id = existingDungeons.First(dm => dm.Level == level).Id;
            await UpdateDungeonAsync(dungeonModel, cancellationToken);
        }
        else
        {
            dungeonModel.Id = await AddDungeonAsync(dungeonModel, cancellationToken);
        }

        return dungeonModel;
    }

    private async Task<DungeonModel> GenerateDungeonAsync(DungeonOptionModel optionModel, int optionId)
    {
        var dungeonModel = await GenerateDungeonAsync(optionModel);
        dungeonModel.DungeonOptionId = optionId;
        return dungeonModel;
    }

    public async Task<DungeonModel> GenerateDungeonAsync(DungeonOptionModel model)
    {
        try
        {
            ValidateModel(model);
            if (model.Corridor)
                return await Task.FromResult(_dungeon.Generate(model));
            return await Task.FromResult(_dungeonNcDungeon.Generate(model));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {message}", ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<DungeonOptionModel>> GetAllDungeonOptionsAsync(CancellationToken cancellationToken)
    {
        try
        {
            var options = await _context.DungeonOptions
                .Include(d => d.Dungeons)
                .AsNoTracking()
                .OrderBy(d => d.Created)
                .ToListAsync(cancellationToken);

            return options.Select(_mapper.Map<DungeonOptionModel>);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get all dungeon options failed.");
            throw;
        }
    }

    public async Task<IEnumerable<DungeonOptionModel>> GetAllDungeonOptionsForUserAsync(int userId,
        CancellationToken cancellationToken)
    {
        try
        {
            var options = await _context.DungeonOptions
                .AsNoTracking()
                .Include(d => d.Dungeons)
                .Where(d => d.UserId == userId)
                .OrderBy(d => d.Created)
                .ToListAsync(cancellationToken);

            return options.Select(_mapper.Map<DungeonOptionModel>);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get all dungeon options for user failed.");
            throw;
        }
    }

    public async Task<DungeonOptionModel?> GetDungeonOptionByNameAsync(string dungeonName, int userId,
        CancellationToken cancellationToken)
    {
        try
        {
            return _mapper.Map<DungeonOptionModel>(await _context.DungeonOptions
                .Include(d => d.Dungeons)
                .AsNoTracking()
                .Where(d => d.DungeonName.Equals(dungeonName) && d.UserId == userId)
                .FirstOrDefaultAsync(cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get dungeon option by name failed.");
            throw;
        }
    }

    public async Task<bool> DeleteDungeonOptionAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _context.DungeonOptions
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            if (entity is not null)
            {
                var dungeons = await _context.Dungeons.Where(sd => sd.DungeonOptionId == entity.Id)
                    .ToListAsync(cancellationToken);
                _context.Dungeons.RemoveRange(dungeons);
                _context.DungeonOptions.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }

            _logger.LogError(" Entity not found (Option# {id})", id);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete dungeon option failed.");
            throw;
        }
    }

    public async Task<bool> DeleteDungeonAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _context.Dungeons
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            if (entity is not null)
            {
                _context.Dungeons.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                await DeleteDungeonOptionIfNeededAsync(entity.DungeonOptionId, cancellationToken);
                return true;
            }

            _logger.LogError(" Entity not found (SavedDungeon# {id})", id);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete dungeon failed.");
            throw;
        }
    }

    private async Task DeleteDungeonOptionIfNeededAsync(int dungeonOptionId, CancellationToken cancellationToken)
    {
        var entity = await _context.DungeonOptions
            .Include(d => d.Dungeons)
            .FirstOrDefaultAsync(d => d.Id == dungeonOptionId, cancellationToken);
        if (entity is not null && !entity.Dungeons.Any())
        {
            _context.DungeonOptions.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<IEnumerable<DungeonModel>> ListUserDungeonsAsync(int userId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _context.DungeonOptions
                .AsNoTracking()
                .Include(d => d.Dungeons)
                .Where(d => d.UserId == userId)
                .OrderBy(d => d.Created)
                .SelectMany(d => d.Dungeons)
                .ToListAsync(cancellationToken);
            return result.Select(_mapper.Map<DungeonModel>);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "List dungeons for user failed.");
            throw;
        }
    }

    public async Task<IEnumerable<DungeonModel>> ListUserDungeonsByNameAsync(string dungeonName, int userId,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _context.DungeonOptions
                .AsNoTracking()
                .Include(d => d.Dungeons)
                .Where(d => d.DungeonName.Equals(dungeonName) && d.UserId == userId)
                .OrderBy(d => d.Created)
                .SelectMany(d => d.Dungeons)
                .ToListAsync(cancellationToken);
            return result.Select(_mapper.Map<DungeonModel>);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "List dungeons by name failed.");
            throw;
        }
    }

    public async Task<DungeonModel> GetDungeonAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            return _mapper.Map<DungeonModel>(await _context.Dungeons
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get dungeon failed.");
            throw;
        }
    }

    public async Task UpdateDungeonAsync(DungeonModel model, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _context.Dungeons
                .FirstOrDefaultAsync(d => d.Id == model.Id, cancellationToken);
            if (entity is not null)
            {
                _mapper.Map(model, entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Update dungeon failed.");
            throw;
        }
    }

    public async Task<DungeonOptionModel> GetDungeonOptionAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            return _mapper.Map<DungeonOptionModel>(
                await _context.DungeonOptions.FirstOrDefaultAsync(d => d.Id == id, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get dungeon option failed.");
            throw;
        }
    }

    public async Task RenameDungeonAsync(int optionId, int userId, string newName, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _context.DungeonOptions.FirstOrDefaultAsync(d => d.Id == optionId && d.UserId == userId,
                cancellationToken);
            if (entity is not null)
            {
                entity.DungeonName = newName;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Rename dungeon failed.");
            throw;
        }
    }

    public async Task<string> ExportToJsonAsync(int dungeonId, CancellationToken cancellationToken)
    {
        try
        {
            var dungeon = _mapper.Map<DungeonModel>(await _context.Dungeons
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == dungeonId, cancellationToken));

            return dungeon is not null ? JsonSerializer.Serialize(dungeon) : string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ExportToJson failed.");
            throw;
        }
    }
}