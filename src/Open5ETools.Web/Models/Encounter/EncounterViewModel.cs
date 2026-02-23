namespace Open5ETools.Web.Models.Encounter;

public class EncounterViewModel
{
    public int SumXp => Monsters.Sum(m => m.Monster.Xp);
    public MonsterViewModel[] Monsters { get; init; } = [];
}