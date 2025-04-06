using System.Globalization;

namespace Open5ETools.Core.Common.Helpers;

public static class AbilityHelper
{
    public static string CalcMod(int ability)
    {
        var result = -5 + Math.Floor((decimal)(ability / 2.0));
        if (result > 0)
        {
            return '+' + result.ToString(CultureInfo.InvariantCulture);
        }

        return result.ToString(CultureInfo.InvariantCulture);
    }
}