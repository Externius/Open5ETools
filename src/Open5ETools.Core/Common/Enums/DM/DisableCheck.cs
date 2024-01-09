using System.ComponentModel;

namespace Open5ETools.Core.Common.Enums.DM;

public enum DisableCheck
{
    Strength,
    Dexterity,
    Intelligence,
    [Description("Dispel Magic")]
    DispelMagic
}