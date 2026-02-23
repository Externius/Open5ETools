using Open5ETools.Core.Common.Models.EG;

namespace Open5ETools.Web.Models.Encounter;

public class MonsterViewModel : EditViewModel
{
    public required JsonMonsterModel Monster { get; init; }
}