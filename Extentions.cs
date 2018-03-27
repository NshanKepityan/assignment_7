using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Extentions
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extentions
    {
        /// <summary>
        /// specifies the type and shape of the elements in the returned sequence
        /// </summary>
        /// <typeparam name="TSource">the type of the given collection</typeparam>
        /// <typeparam name="TResult">the type that is required to be returned</typeparam>
        /// <param name="source">the given collection</param>
        /// <param name="selector">a lambda expression wich should be applied to the elements of the collection</param>
        /// <returns>a sequence of elements of Tresult type</returns>
        public static IEnumerable<TResult> ExtensionSelect<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            if (source == null || selector == null)
                throw new ArgumentNullException();

            foreach (var sourceElem in source)
            {
                yield return selector(sourceElem);
            }
        }

        /// <summary>
        ///  filters elements from a collectionbased on a predicate
        /// </summary>
        /// <typeparam name="TSource">the type of the given collection</typeparam>
        /// <param name="source">the given collection</param>
        /// <param name="predicate">condition to wich should satisfy the element</param>
        /// <returns>the elements wich satisfy to preticat</returns>
        public static IEnumerable<TSource> ExtensionWhere<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {

            if (source == null || predicate == null)
                throw new ArgumentNullException();

            

            foreach (var sourceElem in source)
            {
                if (predicate(sourceElem))
                {
                    yield return sourceElem;
                }

            }
        }

        /// <summary>
        /// groups the elements of a sequence according to a specified key selector function
        /// </summary>
        /// <typeparam name="TSource">the type of the given collection</typeparam>
        /// <typeparam name="TKey">the type of keys</typeparam>
        /// <param name="source">the given collection</param>
        /// <param name="keySelector">A function wich gives a key for each element</param>
        /// <returns>a grouped sequences of elements</returns>
        public static IEnumerable<IGrouping<TKey, TSource>> ExtensionGroupBy<TSource, TKey>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {

            if (source == null || keySelector == null)
                throw new ArgumentNullException();

            Dictionary<TKey,List<TSource>> dictionary = new Dictionary<TKey, List<TSource>>();
            foreach (var sourceElem in source)
            {
                if(keySelector(sourceElem) == null)
                    throw new ArgumentNullException();
                List<TSource> srcList = new List<TSource>();
                if (!dictionary.TryGetValue(keySelector(sourceElem), out srcList))
                {
                    dictionary.Add(keySelector(sourceElem),new List<TSource>());
                }

                srcList?.Add(sourceElem);
            }

            foreach (var elem in dictionary)
            {
                yield return new Grouped<TKey, TSource>(elem.Key, elem.Value);
            }
        }

        /// <summary>
        /// returns a collection as a List
        /// </summary>
        /// <typeparam name="TSource">the type of the given collection</typeparam>
        /// <param name="source">the given collection</param>
        /// <returns>returns a list</returns>
        public static List<TSource> ExtensionToList<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException();
            List<TSource> toList = new List<TSource>();
            foreach (var sourceElem in source)
                toList.Add(sourceElem);
            return toList;
        }

        /// <summary>
        /// sorts the elements of a collection in ascending according order
        /// </summary>
        /// <typeparam name="TSource">the type of the given collection</typeparam>
        /// <typeparam name="TKey">the type of keys</typeparam>
        /// <param name="source">the given collection</param>
        /// <param name="keySelector">A function wich gives a key for each element</param>
        /// <returns>collection ordered in ascending</returns>
        public static IOrderedEnumerable<TSource> ExtensionOrderBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            if (keySelector == null || source == null)
                throw new ArgumentNullException();
            
            var orderedCollection = new MyOrderedEnumerable<TSource>();

            foreach (var sourceElem in source)
            {
                orderedCollection.Add(sourceElem);
            }

            return orderedCollection;
        }

        /// <summary>
        /// sorts the elements of a collection in descending order
        /// </summary>
        /// <typeparam name="TSource">the type of the given collection</typeparam>
        /// <typeparam name="TKey">the type of keys</typeparam>
        /// <param name="source">the given collection</param>
        /// <param name="keySelector">A function wich gives a key for each element</param>
        /// <returns>collection ordered in descending</returns>
        public static IOrderedEnumerable<TSource> ExtensionOrderByDescending<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            if (keySelector == null || source == null)
                throw new ArgumentNullException();

            var orderedCollection = new MyOrderedEnumerable<TSource>();

            foreach (var sourceElem in source)
            {
                orderedCollection.AddDescending(sourceElem);
            }

            return orderedCollection;
        }
        /// <summary>
        /// converts a collection into a dictionary according to a specified key selector function
        /// </summary>
        /// <typeparam name="TSource">the type of the given collection</typeparam>
        /// <typeparam name="TKey">the type of keys</typeparam>
        /// <param name="source">the given collection</param>
        /// <param name="keySelector">A function wich gives a key for each element</param>
        /// <returns>returns a dictionary</returns>
        public static Dictionary<TKey, TSource> ExtensionToDictionary<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            if (source == null || keySelector == null)
                throw new ArgumentNullException();
            Dictionary<TKey,TSource> dictionary = new Dictionary<TKey, TSource>();
            List<TKey> keys = new List<TKey>();
            foreach (var src in source)
            {
                if (!keys.Contains(keySelector(src)) && keySelector(src) != null)
                    {
                        dictionary.Add(keySelector(src), src);
                        keys.Add(keySelector(src));   
                    }
                else if (keySelector(src) == null)
                        throw new ArgumentNullException();
                    else throw new DuplicateNameException();
            }

            return dictionary;
        }

    }
}
