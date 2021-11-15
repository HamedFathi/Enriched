#if NET5_0_OR_GREATER
using Enriched.EnumerableExtended;
using Enriched.QueueExtended;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Enriched.TaskExtended
{
    public static partial class TaskExtensions
    {

        public static async Task<ImmutableArray<T>> ToImmutableArrayAsync<T>(this Task<IEnumerable<T>> @this)
        {
            var source = await @this.ConfigureAwait(false);
            return source.ToImmutableArray();
        }

        public static async Task<IImmutableDictionary<TKey, TSource>> ToImmutableDictionaryAsync<TSource, TKey>(this Task<IEnumerable<TSource>> @this, Func<TSource, TKey> keySelector)
                                            where TKey : notnull
        {
            var source = await @this.ConfigureAwait(false);
            return source.ToImmutableDictionary(keySelector);
        }

        public static async Task<IImmutableDictionary<TKey, TSource>> ToImmutableDictionaryAsync<TSource, TKey>(this Task<IEnumerable<TSource>> @this, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> keyComparer)
                                            where TKey : notnull
        {
            var source = await @this.ConfigureAwait(false);
            return source.ToImmutableDictionary(keySelector, keyComparer);
        }

        public static async Task<IImmutableDictionary<TKey, TElement>> ToImmutableDictionaryAsync<TSource, TKey, TElement>(this Task<IEnumerable<TSource>> @this, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
                                            where TKey : notnull
        {
            var source = await @this.ConfigureAwait(false);
            return source.ToImmutableDictionary(keySelector, elementSelector);
        }

        public static async Task<IImmutableDictionary<TKey, TElement>> ToImmutableDictionaryAsync<TSource, TKey, TElement>(this Task<IEnumerable<TSource>> @this, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> keyComparer)
                                            where TKey : notnull
        {
            var source = await @this.ConfigureAwait(false);
            return source.ToImmutableDictionary(keySelector, elementSelector, keyComparer);
        }

        public static async Task<IImmutableDictionary<TKey, TElement>> ToImmutableDictionaryAsync<TSource, TKey, TElement>(this Task<IEnumerable<TSource>> @this, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> keyComparer, IEqualityComparer<TElement> valueComparer)
                                            where TKey : notnull
        {
            var source = await @this.ConfigureAwait(false);
            return source.ToImmutableDictionary(keySelector, elementSelector, keyComparer, valueComparer);
        }

        public static async Task<IImmutableSet<TSource>> ToImmutableHashSetAsync<TSource>(this Task<IEnumerable<TSource>> @this)
        {
            var source = await @this.ConfigureAwait(false);
            return source.ToImmutableHashSet();
        }

        public static async Task<IImmutableSet<TSource>> ToImmutableHashSetAsync<TSource>(this Task<IEnumerable<TSource>> @this, IEqualityComparer<TSource> comparer)
        {
            var source = await @this.ConfigureAwait(false);
            return source.ToImmutableHashSet(comparer);
        }

        public static async Task<IImmutableList<T>> ToImmutableListAsync<T>(this Task<IEnumerable<T>> @this)
        {
            var source = await @this.ConfigureAwait(false);
            return source.ToImmutableList();
        }

        public static async Task<IImmutableDictionary<TKey, TElement>> ToImmutableSortedDictionaryAsync<TSource, TKey, TElement>(this Task<IEnumerable<TSource>> @this, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
                                            where TKey : notnull
        {
            var source = await @this.ConfigureAwait(false);
            return source.ToImmutableSortedDictionary(keySelector, elementSelector);
        }

        public static async Task<IImmutableDictionary<TKey, TElement>> ToImmutableSortedDictionaryAsync<TSource, TKey, TElement>(this Task<IEnumerable<TSource>> @this, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IComparer<TKey> keyComparer)
                                            where TKey : notnull
        {
            var source = await @this.ConfigureAwait(false);
            return source.ToImmutableSortedDictionary(keySelector, elementSelector, keyComparer);
        }

        public static async Task<IImmutableDictionary<TKey, TElement>> ToImmutableSortedDictionaryAsync<TSource, TKey, TElement>(this Task<IEnumerable<TSource>> @this, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IComparer<TKey> keyComparer, IEqualityComparer<TElement> valueComparer)
                                            where TKey : notnull
        {
            var source = await @this.ConfigureAwait(false);
            return source.ToImmutableSortedDictionary(keySelector, elementSelector, keyComparer, valueComparer);
        }

        public static async Task<IImmutableSet<TSource>> ToImmutableSortedSetAsync<TSource>(this Task<IEnumerable<TSource>> @this)
        {
            var source = await @this.ConfigureAwait(false);
            return source.ToImmutableSortedSet();
        }

        public static async Task<IImmutableSet<TSource>> ToImmutableSortedSetAsync<TSource>(this Task<IEnumerable<TSource>> @this, IComparer<TSource> comparer)
        {
            var source = await @this.ConfigureAwait(false);
            return source.ToImmutableSortedSet(comparer);
        }
    }
}
#endif