using Open5ETools.Core.Common.Enums.EG;
using Open5ETools.Core.Common.Models.Services;

namespace Open5ETools.Core.Common.Models.EG;

public class EncounterOption : EditModel
{
    public int PartyLevel { get; set; }
    public int PartySize { get; set; }
    public Difficulty? Difficulty { get; set; }
    public IEnumerable<MonsterType> MonsterTypes { get; set; } = [];
    public IEnumerable<Size> Sizes { get; set; } = [];
    public int Count { get; set; } = 10;
}