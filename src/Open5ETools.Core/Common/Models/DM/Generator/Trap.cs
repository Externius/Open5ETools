using Open5ETools.Core.Common.Enums.DM;

namespace Open5ETools.Core.Common.Models.DM.Generator;

public record Trap(
    string Name,
    Save Save,
    int Spot,
    int Disable,
    DisableCheck DisableCheck,
    bool AttackMod,
    DamageType? DmgType,
    string Special
);