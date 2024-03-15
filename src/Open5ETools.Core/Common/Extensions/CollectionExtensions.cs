using System.Linq.Expressions;

namespace Open5ETools.Core.Common.Extensions;

public static class CollectionExtensions
{
    public static IEnumerable<T> Sort<T>(this IEnumerable<T> source, string? sort = null, bool ascending = true)
    {
        if (sort is null)
            return source;

        var param = Expression.Parameter(typeof(T), "item");

        var sortExpression = Expression.Lambda<Func<T, object>>
            (Expression.Convert(Expression.Property(param, sort), typeof(object)), param);

        return ascending ? source.AsQueryable().OrderBy(sortExpression) : source.AsQueryable().OrderByDescending(sortExpression);
    }
}
