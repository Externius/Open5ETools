using Open5ETools.Core.Common.Extensions;
using X.PagedList;
using X.PagedList.Extensions;

namespace Open5ETools.Web.Models;

public class XPagedListViewModel<T>(IEnumerable<T> items,
    int pageIndex,
    int pageSize,
    string? search,
    string? sortColumn,
    bool ascending
    )
{
    public IPagedList<T> Items { get; set; } = items.Sort(sortColumn, ascending).ToPagedList(pageIndex, pageSize);
    public int PageIndex { get; set; } = pageIndex;
    public string? Search { get; set; } = search;
    public string? SortColumn { get; set; } = sortColumn;
    public bool Ascending { get; set; } = ascending;

    public bool ChangeOrder()
    {
        return !Ascending;
    }
}