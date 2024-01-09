using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace Open5ETools.Core.Common.Extensions;
public static class SelectListExtensions
{
    public static IEnumerable<SelectListItem> AddFirstItem(this IEnumerable<SelectListItem> selectList,
    string? value = null,
    string? text = null,
    bool selected = false)
    {
        if (string.IsNullOrEmpty(text))
            text = ((char)160).ToString(CultureInfo.InvariantCulture);

        var emptyItem = new[] {new SelectListItem
        {
            Text = text,
            Value = value,
            Selected = selected
        }};

        return emptyItem.Concat(selectList);
    }
}
