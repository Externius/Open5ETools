using Open5ETools.Core.Common.Models.Services;

namespace Open5ETools.Core.Common.Models.EG;

public record MonsterModel
(
    JsonMonsterModel JsonMonsterModel,
    int Id,
    byte[] Timestamp,
    string CreatedBy,
    DateTime Created,
    string LastModifiedBy,
    DateTime LastModified
) : EditModel(Id, Timestamp, CreatedBy, Created, LastModifiedBy, LastModified);