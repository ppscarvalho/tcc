#nullable disable

using JetBrains.Annotations;
using System.Collections;
using Loja.Inspiracao.RabbitMQ.Extensions;

namespace Loja.Inspiracao.RabbitMQ.Extensions
{
    public static class EnumerableExtensions
    {
        #region Common

        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "The source collection cannot be null.");

            foreach (TSource item in source)
                action(item);
        }

        /// <summary>
        /// Indicates whether the collection is null or has no elements.
        /// </summary>
        /// <param name="source">The collection to test.</param>
        /// <returns>Returns true if the collection is null or empty; false otherwise.</returns>
        [ContractAnnotation("source:null => true")]
        public static bool IsNullOrEmpty(this IEnumerable source)
        {
            var collection = source as ICollection;
            return collection == null || collection.Count == 0;
        }

        /// <summary>
        /// Indicates whether the generic collection is null or has no elements.
        /// </summary>
        /// <typeparam name="TSource">The type to consume the method.</typeparam>
        /// <param name="source">The generic collection to check for.</param>
        /// <returns>Returns true if the collection is null or empty; false otherwise.</returns>
        [ContractAnnotation("source:null => true")]
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source == null || !Enumerable.Any(source);
        }

        #endregion

        #region Convertions

        /// <summary>
        /// Converts a Enumerable collection to a List
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the list.</typeparam>
        /// <param name="source">The collection to be converted.</param>
        /// <returns>An instance of a concrete implementation of System.Collections.Generic.IList`1</returns>
        public static IList<TSource> ToList<TSource>(this IEnumerable source)
        {
            var convertedList = new List<TSource>();
            var collection = source as ICollection;

            if (collection == null)
                throw new ArgumentNullException(nameof(source), "The source collection cannot be null.");

            convertedList.AddRange(
                collection
                    .Cast<object>()
                    .AsQueryable()
                    .Select(item => (TSource)Convert.ChangeType(item, typeof(TSource))));

            return convertedList;
        }

        #endregion

        #region Dynamic LINQ

        /// <summary>
        /// Sorts the elements of a sequence in a particular direction (ascending, descending), according to string expression.
        /// </summary>
        /// <remarks>Dynamic LINQ Implementation</remarks>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence to be ordered.</param>
        /// <param name="orderByExpression">The column to be ordered by.</param>
        /// <param name="isAscending">Optional Parameter: indicates whether the order by direction will be Ascending (low to high) or Descending (high to low).</param>
        /// <returns>An ordered copy of the source sequence.</returns>
        public static IEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> source, string orderByExpression, bool isAscending = true)
        {
            var orderByList = source.ToList();

            if (orderByList.IsNullOrEmpty())
                return orderByList;

            if (orderByExpression == null)
                throw new ArgumentNullException(nameof(orderByExpression), "The order by expression cannot be null");

            IEnumerable<TSource> orderedEnumerable = orderByList.OrderBy($"{orderByExpression} {(isAscending ? "asc" : "desc")}");

            return orderedEnumerable;
        }

        /// <summary>
        /// Takes a page of elements from a sorted sequence in a particular direction, according to a key selector.
        /// </summary>
        /// <remarks>Dynamic LINQ Implementation</remarks>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence to be ordered.</param>
        /// <param name="orderByExpression">The column to be ordered by.</param>
        /// <param name="page">The page number to be retrieved.</param>
        /// <param name="pageSize">The number of elements of the page.</param>
        /// <param name="isAscending">Optional Parameter: indicates whether the order by direction will be Ascending (low to high) or Descending (high to low).</param>
        /// <returns></returns>
        public static IEnumerable<TSource> PageBy<TSource>(this IEnumerable<TSource> source, string orderByExpression,
            int page, int pageSize, bool isAscending = true)
        {
            IEnumerable<TSource> pagedResults =
                source.OrderBy(orderByExpression, isAscending)
                    .Skip<TSource>((page - 1) * pageSize)
                    .Take<TSource>(pageSize);

            return pagedResults;
        }

        #endregion

        #region LINQ

        /// <summary>
        /// Returns all distinct elements of the given source.
        /// </summary>
        /// <remarks>
        /// This operator uses deferred execution and streams the results, although
        /// a set of already-seen keys is retained. If a key is seen multiple times,
        /// only the first element with that key is returned. 
        /// The default equality comparer for <c>TSource</c> is used
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence.</typeparam>
        /// <typeparam name="TKey">Type of the projected element.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="keySelector">The projection to determine which selector should be treated as distinct.</param>
        /// <returns>A sequence consisting of distinct elements from the source sequence,
        /// comparing them by the specified key projection.</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "The sequence cannot be null.");

            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector), "The key selector cannot be null");

            var seenKeys = new HashSet<TKey>();

            foreach (var element in source.ToList().Where(element => seenKeys.Add(keySelector(element))))
                yield return element;
        }

        /// <summary>
        /// Sorts the elements of a sequence in a particular direction (ascending, descending), according to a key selector.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the key used to order elements.</typeparam>
        /// <param name="source">The source sequence to be ordered.</param>
        /// <param name="keySelector">The key selector function that will order the elements of the sequence.</param>
        /// <param name="ascending">Optional Parameter: Indicates whether the order by direction will be Ascending (low to high) or Descending (high to low).</param>
        /// <returns>An ordered copy of the source sequence.</returns>
        public static IEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, bool ascending = true)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "The source collection cannot be null.");

            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector), "The key selector function cannot be null.");

            IEnumerable<TSource> orderedEnumerable =
                ascending
                    ? source.OrderBy(keySelector, null)
                    : source.OrderByDescending(keySelector);

            return orderedEnumerable;
        }

        /// <summary>
        /// Takes a page of elements from a sorted sequence in a particular direction, according to a key selector.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the key used to order elements.</typeparam>
        /// <param name="source">The source sequence to be ordered.</param>
        /// <param name="keySelector">The key selector function that will order the elements of the sequence.</param>
        /// <param name="page">The page number to be retrieved.</param>
        /// <param name="pageSize">The number of elements of the page.</param>
        /// <param name="ascending">Optional Parameter: indicates whether the order by direction will be Ascending (low to high) or Descending (high to low).</param>
        /// <returns></returns>
        public static IEnumerable<TSource> PageBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
            int page, int pageSize, bool ascending = true)
        {
            IEnumerable<TSource> pagedResults =
                source
                    .OrderBy(keySelector, ascending)
                    .Skip<TSource>((page - 1) * pageSize)
                    .Take<TSource>(pageSize);

            return pagedResults;
        }

        #endregion
    }
}
