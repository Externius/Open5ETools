namespace Open5ETools.Core.Common.Helpers;

public static class AbilityHelper
{
    public static string CalcMod(int ability)
    {
        var result = -5 + Math.Floor((decimal)(ability / 2));
        if (result > 0)
        {
            return '+' + result.ToString();
        }
        return result.ToString();
    }
}
