using System;
using System.Linq;
using System.Collections.Generic;

namespace CasSys.Domain.EntityFrameworkCore.Collections
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IPagedList{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of the data to page</typeparam>
    public class PagedList<T> : IPagedList<T>
    {
        /// <summary>
        /// Gets or sets the index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>The total count.</value>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the total pages.
        /// </summary>
        /// <value>The total pages.</value>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public IList<T> Items { get; set; }

        /// <summary>
        /// Gets the has previous page.
        /// </summary>
        /// <value>The has previous page.</value>
        public bool HasPreviousPage => (PageIndex > 1);

        /// <summary>
        /// Gets the has next page.
        /// </summary>
        /// <value>The has next page.</value>
        public bool HasNextPage => (PageIndex < TotalPages);

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}" /> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="pageIndex">The index of the page.</param>
        /// <param name="pageSize">The size of the page.</param>
        internal PagedList(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;

            if (source is IQueryable<T> queryable)
            {
                TotalCount = queryable.Count();
                Items = queryable.Skip((PageIndex - 1) * PageIndex).Take(PageSize).ToList();
            }
            else
            {
                TotalCount = source.Count();
                Items = source.Skip((PageIndex - 1) * PageIndex).Take(PageSize).ToList();
            }

            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}" /> class.
        /// </summary>
        internal PagedList() => Items = new T[0];
    }
}