using Microsoft.AspNetCore.Mvc.Rendering;

namespace Open5ETools.Core.Common.Helpers;

public static class SelectListHelper
{
    public static SelectListItem[] GetBool()
    {
        return
        [
            new SelectListItem { Text = Resources.Common.Yes, Value = "true", Selected = true },
            new SelectListItem { Text = Resources.Common.No, Value = "false" }
        ];
    }

    public static SelectListItem[] GenerateIntSelectList(int from, int to)
    {
        var ints = Enumerable.Range(from, to - from + 1).ToArray();
        var selectList = new SelectListItem[ints.Length];
        for (var i = 0; i <= ints.Length - 1; i++)
        {
            selectList[i] = new SelectListItem { Text = ints[i].ToString(), Value = ints[i].ToString() };
        }

        return selectList;
    }
}