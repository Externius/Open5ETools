using Open5ETools.Core.Common.Models.DM.Services;
using Open5ETools.Core.Domain.DM;

namespace Open5ETools.Core.Common.Mappers;

public static class DungeonMapper
{
    public static DungeonOption ToEntity(this DungeonOptionModel dungeonOption)
    {
        return new DungeonOption
        {
            DungeonName = dungeonOption.DungeonName,
            DungeonSize = dungeonOption.DungeonSize,
            DungeonDifficulty = dungeonOption.DungeonDifficulty,
            PartyLevel = dungeonOption.PartyLevel,
            PartySize = dungeonOption.PartySize,
            TreasureValue = dungeonOption.TreasureValue,
            ItemsRarity = dungeonOption.ItemsRarity,
            RoomDensity = dungeonOption.RoomDensity,
            RoomSize = dungeonOption.RoomSize,
            MonsterType = dungeonOption.MonsterType,
            TrapPercent = dungeonOption.TrapPercent,
            DeadEnd = dungeonOption.DeadEnd,
            Corridor = dungeonOption.Corridor,
            RoamingPercent = dungeonOption.RoamingPercent,
            UserId = dungeonOption.UserId,
            CreatedBy = string.Empty,
            LastModifiedBy = string.Empty,
            Dungeons =
            [
                .. dungeonOption.Dungeons.Select(d => d.ToEntity())
            ]
        };
    }

    public static DungeonOptionModel ToModel(this DungeonOption dungeonOption)
    {
        return new DungeonOptionModel
        (
            dungeonOption.DungeonName,
            dungeonOption.UserId,
            dungeonOption.DungeonSize,
            dungeonOption.DungeonDifficulty,
            dungeonOption.PartyLevel,
            dungeonOption.PartySize,
            dungeonOption.TreasureValue,
            dungeonOption.ItemsRarity,
            dungeonOption.RoomDensity,
            dungeonOption.RoomSize,
            dungeonOption.MonsterType,
            dungeonOption.TrapPercent,
            dungeonOption.DeadEnd,
            dungeonOption.Corridor,
            dungeonOption.RoamingPercent,
            [.. dungeonOption.Dungeons.Select(d => d.ToModel())],
            dungeonOption.Id,
            dungeonOption.Timestamp,
            dungeonOption.CreatedBy,
            dungeonOption.Created,
            dungeonOption.LastModifiedBy,
            dungeonOption.LastModified
        );
    }

    public static void Map(Dungeon dungeon, DungeonModel model)
    {
        dungeon.DungeonTiles = model.DungeonTiles;
        dungeon.DungeonTiles = model.DungeonTiles;
        dungeon.RoomDescription = model.RoomDescription;
        dungeon.TrapDescription = model.TrapDescription;
        dungeon.RoamingMonsterDescription = model.RoamingMonsterDescription;
        dungeon.Level = model.Level;
        dungeon.DungeonOptionId = model.DungeonOptionId;
    }

    public static Dungeon ToEntity(this DungeonModel model)
    {
        return new Dungeon
        {
            DungeonTiles = model.DungeonTiles,
            RoomDescription = model.RoomDescription,
            TrapDescription = model.TrapDescription,
            RoamingMonsterDescription = model.RoamingMonsterDescription,
            Level = model.Level,
            DungeonOptionId = model.DungeonOptionId,
            Id = model.Id,
            Timestamp = model.Timestamp,
            CreatedBy = model.CreatedBy,
            Created = model.Created,
            LastModifiedBy = model.LastModifiedBy,
            LastModified = model.LastModified
        };
    }

    public static DungeonModel ToModel(this Dungeon model)
    {
        return new DungeonModel
        (
            model.DungeonTiles,
            model.RoomDescription,
            model.TrapDescription,
            model.RoamingMonsterDescription,
            model.DungeonOptionId,
            model.Level,
            model.Id,
            model.Timestamp,
            model.CreatedBy,
            model.Created,
            model.LastModifiedBy,
            model.LastModified
        );
    }
}