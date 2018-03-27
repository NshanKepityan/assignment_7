using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Extentions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey">the type of keys</typeparam>
    /// <typeparam name="TSource">The type of collection</typeparam>
    public class Grouped<TKey, TSource> : IGrouping<TKey, TSource>
    {
        /// <summary>
        /// the key of separate group
        /// </summary>
        private readonly TKey _key;
        /// <summary>
        /// the elements of group
        /// </summary>
        private readonly IEnumerable<TSource> _elements;

        /// <summary>
        /// creates a separate group
        /// </summary>
        /// <param name="key">the key of group</param>
        /// <param name="elements">the collection of elements of the group</param>
        public Grouped(TKey key, IEnumerable<TSource> elements)
        {
            this._key = key;
            this._elements = elements;
        }

        /// <summary>
        /// iterates through the collection
        /// </summary>
        /// <returns>returns an enumerator </returns>
        public IEnumerator<TSource> GetEnumerator()
        {
            return this._elements.GetEnumerator();
        }

        /// <summary>
        /// iterates through the collection
        /// </summary>
        /// <returns>returns an enumerator </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this._elements).GetEnumerator();
        }

        /// <summary>
        /// returns the key of the group
        /// </summary>
        public TKey Key
        {
            get { return this._key; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource">The type of the collection</typeparam>
    public class MyOrderedEnumerable<TSource> : IOrderedEnumerable<TSource>
    {
        /// <summary>
        /// list of the elements of the collection
        /// </summary>
        private readonly List<TSource> sourceCollection;

        /// <summary>
        /// creates an empty list for elements
        /// </summary>
        public MyOrderedEnumerable()
        {
            this.sourceCollection = new List<TSource>();
        }

        /// <summary>
        /// Adds item to the collection and sorts it in ascending order 
        /// </summary>
        /// <param name="element">element to be added</param>
        public void Add(TSource element)
        {
            this.sourceCollection.Add(element);
            this.sourceCollection.Sort();
        }


        /// <summary>
        /// Adds item to the collection and sorts it in descending order
        /// </summary>
        /// <param name="element">element to be added</param>
        public void AddDescending(TSource element)
        {
            this.sourceCollection.Add(element);
            this.sourceCollection.Sort();
            this.sourceCollection.Reverse();
        }

        /// <summary>
        /// performs a subsequent ordering on the elements according to a key.
        /// </summary>
        /// <typeparam name="TKey">the type of key.</typeparam>
        /// <param name="keySelector">a function to extract the key for each element.</param>
        /// <param name="comparer">compares keys for placement in the returned sequence.</param>
        /// <param name="descending">if is true elements must be sorted in descending order; otherwise in ascending order</param>
        /// <returns></returns>
        public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector,IComparer<TKey> comparer,bool descending)
        {
            if (descending)
                return this.sourceCollection.OrderByDescending(keySelector, comparer);
                return this.sourceCollection.OrderBy(keySelector, comparer);
        }

        /// <summary>
        /// iterates through the collection
        /// </summary>
        /// <returns>returns an enumerator</returns>
        public IEnumerator<TSource> GetEnumerator()
        {
            return ((IEnumerable<TSource>)this.sourceCollection).GetEnumerator();
        }

        /// <summary>
        /// iterates through the collection
        /// </summary>
        /// <returns>returns an enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.sourceCollection.GetEnumerator();
        }
    }
}
