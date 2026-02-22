using System.Globalization;
using Open5ETools.Core.Common.Models.DM.Services;
using Open5ETools.Web.Models.Dungeon;

namespace Open5ETools.Web.Mappers;

public static class DungeonMapper
{
    public static DungeonOptionViewModel ToViewModel(this DungeonOptionModel model)
    {
        return new DungeonOptionViewModel
        {
            DungeonName = model.DungeonName,
            DungeonSize = model.DungeonSize,
            DungeonDifficulty = model.DungeonDifficulty,
            PartyLevel = model.PartyLevel,
            PartySize = model.PartySize,
            Corridor = model.Corridor,
            DeadEnd = model.DeadEnd,
            Dungeons = [.. model.Dungeons.Select(ToViewModel)],
            ItemsRarity = model.ItemsRarity,
            MonsterType = model.MonsterType,
            RoamingPercent = model.RoamingPercent,
            RoomDensity = model.RoomDensity,
            RoomSize = model.RoomSize,
            TrapPercent = model.TrapPercent,
            TreasureValue = model.TreasureValue,
            Id = model.Id,
            Created = model.Created,
            CreatedBy = model.CreatedBy,
            LastModified = model.LastModified,
            LastModifiedBy = model.LastModifiedBy,
            Timestamp = model.Timestamp
        };
    }

    private static DungeonViewModel ToViewModel(this DungeonModel model)
    {
        return new DungeonViewModel
        {
            DungeonTiles = model.DungeonTiles,
            RoomDescription = model.RoomDescription,
            TrapDescription = model.TrapDescription,
            RoamingMonsterDescription = model.RoamingMonsterDescription ?? string.Empty,
            DungeonOptionId = model.DungeonOptionId,
            Level = model.Level,
            Id = model.Id,
            Created = model.Created,
            CreatedBy = model.CreatedBy,
            LastModified = model.LastModified,
            LastModifiedBy = model.LastModifiedBy,
            Timestamp = model.Timestamp
        };
    }

    public static DungeonOptionCreateViewModel CreateDefaultDungeonOptionCreateViewModel(int userId)
    {
        return new DungeonOptionCreateViewModel
        {
            DungeonName = string.Empty,
            DungeonSize = 0,
            DungeonDifficulty = 0,
            PartyLevel = 1,
            PartySize = 4,
            Corridor = true,
            DeadEnd = true,
            ItemsRarity = 1,
            MonsterType = null,
            RoamingPercent = 0,
            RoomDensity = 0,
            RoomSize = 0,
            TrapPercent = 15,
            TreasureValue = string.Empty,
            Id = 0,
            Created = DateTime.UtcNow,
            CreatedBy = string.Empty,
            LastModified = DateTime.UtcNow,
            LastModifiedBy = string.Empty,
            UserId = userId
        };
    }

    public static DungeonOptionCreateViewModel ToCreateViewModel(this DungeonOptionModel model)
    {
        return new DungeonOptionCreateViewModel
        {
            DungeonName = model.DungeonName,
            DungeonSize = model.DungeonSize,
            DungeonDifficulty = model.DungeonDifficulty,
            PartyLevel = model.PartyLevel,
            PartySize = model.PartySize,
            Corridor = model.Corridor,
            DeadEnd = model.DeadEnd,
            ItemsRarity = model.ItemsRarity,
            MonsterType = model.MonsterType.Split(','),
            RoamingPercent = model.RoamingPercent,
            RoomDensity = model.RoomDensity,
            RoomSize = model.RoomSize,
            TrapPercent = model.TrapPercent,
            TreasureValue = model.TreasureValue.ToString(CultureInfo.InvariantCulture),
            Id = model.Id,
            Created = model.Created,
            CreatedBy = model.CreatedBy,
            LastModified = model.LastModified,
            LastModifiedBy = model.LastModifiedBy,
            Timestamp = model.Timestamp,
            UserId = model.UserId
        };
    }

    public static DungeonOptionModel ToModel(this DungeonOptionCreateViewModel viewModel)
    {
        var monsterType = string.Empty;
        if (viewModel.MonsterType is not null)
            monsterType = string.Join(",", viewModel.MonsterType);
        return new DungeonOptionModel
        (
            viewModel.DungeonName,
            viewModel.UserId,
            viewModel.DungeonSize,
            viewModel.DungeonDifficulty,
            viewModel.PartyLevel,
            viewModel.PartySize,
            Convert.ToDouble(viewModel.TreasureValue, CultureInfo.InvariantCulture),
            viewModel.ItemsRarity,
            viewModel.RoomDensity,
            viewModel.RoomSize,
            monsterType,
            viewModel.TrapPercent,
            viewModel.DeadEnd,
            viewModel.Corridor,
            viewModel.RoamingPercent,
            [],
            viewModel.Id,
            viewModel.Timestamp,
            viewModel.CreatedBy,
            viewModel.Created,
            viewModel.LastModifiedBy,
            viewModel.LastModified
        );
    }
}