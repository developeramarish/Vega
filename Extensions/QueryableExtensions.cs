using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vega.Core.Models;

namespace Vega.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(
            this IQueryable<T> query,
            IQueryObject queryObj,
            Dictionary<string, Expression<Func<T, object>>> columnMap)
        {
            if (String.IsNullOrWhiteSpace(queryObj.SortBy) || !columnMap.ContainsKey(queryObj.SortBy))
                return query;

            if (queryObj.IsSortAsc)
                return query.OrderBy(columnMap[queryObj.SortBy]);
            else
                return query.OrderByDescending(columnMap[queryObj.SortBy]);
        }
    }
}