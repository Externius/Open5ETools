namespace Open5ETools.Web.Models.Encounter;

public class EncounterViewModel
{
    public int SumXp { get; set; }
    public IEnumerable<MonsterViewModel> Details { get; set; } = [];
}