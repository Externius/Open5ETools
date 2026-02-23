namespace Open5ETools.Core.Domain.EG;

public class Monster : AuditableEntity
{
    public required Common.Models.Json.Monster JsonMonster { get; set; } 
}