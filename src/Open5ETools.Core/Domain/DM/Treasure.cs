using Open5ETools.Core.Common.Models.DM.Generator;

namespace Open5ETools.Core.Domain.DM;

public class Treasure : AuditableEntity
{
    public TreasureDescription TreasureDescription { get; set; } = null!;
}