using Microsoft.AspNetCore.Mvc.Rendering;

namespace Open5ETools.Core.Common.Helpers;
public static class SelectListHelper
{
    public static List<SelectListItem> GetBool()
    {
        return
        [
            new() { Text = Resources.Common.Yes, Value = "true", Selected = true },
            new() { Text = Resources.Common.No, Value = "false" }
        ];
    }

    public static List<SelectListItem> GenerateIntSelectList(int from, int to)
    {
        var list = new List<SelectListItem>();
        for (var i = from; i <= to; i++)
        {
            list.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
        }
        return list;
    }
}