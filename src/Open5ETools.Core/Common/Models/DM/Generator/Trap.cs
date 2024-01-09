using Open5ETools.Core.Common.Enums.DM;

namespace Open5ETools.Core.Common.Models.DM.Generator;

public class Trap(string name, Save save, int spot, int disable, DisableCheck disableCheck, bool attackMod, DamageType? dmgType, string special)
{
    public string Name { get; } = name;
    public Save Save { get; } = save;
    public int Spot { get; } = spot;
    public int Disable { get; } = disable;
    public DisableCheck DisableCheck { get; } = disableCheck;
    public bool AttackMod { get; } = attackMod;
    public DamageType? DmgType { get; } = dmgType;
    public string Special { get; } = special;
}