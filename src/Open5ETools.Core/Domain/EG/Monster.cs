namespace Open5ETools.Core.Domain.EG;

public class Monster : AuditableEntity
{
    public Common.Models.Json.Monster JsonMonster { get; set; } = new();
}
