using Open5ETools.Core.Common.Models.Services;

namespace Open5ETools.Core.Common.Models.DM.Services;

public record DungeonModel(
    string DungeonTiles,
    string RoomDescription,
    string TrapDescription,
    string? RoamingMonsterDescription,
    int DungeonOptionId,
    int Level,
    int Id,
    byte[] Timestamp,
    string CreatedBy,
    DateTime Created,
    string LastModifiedBy,
    DateTime LastModified
) : EditModel(Id, Timestamp, CreatedBy, Created, LastModifiedBy, LastModified);