using System;
using System.Linq;
using System.Linq.Expressions;

namespace CasSys.Domain.EntityFrameworkCore.Collections
{
    public static class IPagedListExtensions
    {
        public static PagedList<TResult> ToPagedList<T, TResult>(this IPagedList<T> source, Expression<Func<T, TResult>> converter)
        {
            var items = source.Items.Select(x => converter.Compile().Invoke(x)).ToList();

            return new PagedList<TResult>
            {
                PageIndex = source.PageIndex,
                PageSize = source.PageSize,
                TotalCount = source.TotalCount,
                TotalPages = source.TotalPages,
                Items = items
            };
        }
    }
}