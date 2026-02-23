using Open5ETools.Core.Common.Models.Services;

namespace Open5ETools.Core.Common.Models.DM.Services;

public record DungeonOptionModel(
    string DungeonName,
    int UserId,
    int DungeonSize,
    int DungeonDifficulty,
    int PartyLevel,
    int PartySize,
    double TreasureValue,
    int ItemsRarity,
    int RoomDensity,
    int RoomSize,
    string MonsterType,
    int TrapPercent,
    bool DeadEnd,
    bool Corridor,
    int RoamingPercent,
    DungeonModel[] Dungeons,
    int Id,
    byte[] Timestamp,
    string CreatedBy,
    DateTime Created,
    string LastModifiedBy,
    DateTime LastModified,
    int Width = 800,
    int Height = 800
) : EditModel(Id, Timestamp, CreatedBy, Created, LastModifiedBy, LastModified);