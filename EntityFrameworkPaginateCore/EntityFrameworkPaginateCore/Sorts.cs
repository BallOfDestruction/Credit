using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EntityFrameworkPaginateCore
{
    /// <summary>
    /// Stores and processes your conditional sorts.
    /// This sorting is mutually exclusive and the first sort satisfying the condition will be applied.
    /// </summary>
    /// <typeparam name="T">Type of Entity for which the sort is applicable.</typeparam>
    public class Sorts<T>
    {
        private readonly List<dynamic> _sortList;

        public Sorts()
        {
            _sortList = new List<dynamic>();
        }

        /// <summary>
        /// Adds a conditional sort for your query.
        /// </summary>
        /// <typeparam name="TKey">The data type for the sort object.</typeparam>
        /// <param name="condition">The condition on which the the given sort will run.</param>
        /// <param name="expression">Sort expression.</param>
        /// <param name="byDescending">True if sorting is to be done be descending. By default data gets sorted by ascending.</param>
        public void Add<TKey>(bool condition, Expression<Func<T, TKey>> expression, bool byDescending = false)
        {
            Append(condition, expression, byDescending);
        }

        private void Append<TKey>(bool condition, Expression<Func<T, TKey>> expression, bool byDescending = false)
        {
            _sortList.Add(new Sort<T, TKey>
            {
                Condition = condition,
                Expression = expression,
                ByDescending = byDescending
            });
        }

        internal bool IsValid()
        {
            return _sortList.Any(s => s.Condition);
        }

        internal dynamic Get() => _sortList.FirstOrDefault(s => s.Condition);

        public System.Collections.Generic.IEnumerable<dynamic> Power()
        {
            foreach (var o in _sortList.Where(w => w.Condition))
            {
                yield return o;
            }
        }

        internal IQueryable<T> ApplySort<TKey>(IQueryable<T> query, Sorts<T> sorts, Sort<T, TKey> sort)
        {
            IOrderedQueryable<T> nonFirstQuerable = null;
            var first = true;
            foreach (var c in Power())
            {
                if (first)
                {
                    nonFirstQuerable = ApplySortFisrt(query, c);
                    first = false;
                }
                else
                {
                    nonFirstQuerable = ApplySortNonFisrt(nonFirstQuerable, c);
                }
            }

            return nonFirstQuerable;
        }

        internal static IOrderedQueryable<T> ApplySortFisrt<TKey>(IQueryable<T> query, Sort<T, TKey> sort)
        {
            return sort.ByDescending
                ? query.OrderByDescending(sort.Expression)
                : query.OrderBy(sort.Expression);
        }

        internal static IOrderedQueryable<T> ApplySortNonFisrt<TKey>(IOrderedQueryable<T> query, Sort<T, TKey> sort)
        {
            return sort.ByDescending
                ? query.ThenByDescending(sort.Expression)
                : query.ThenBy(sort.Expression);
        }
    }
}