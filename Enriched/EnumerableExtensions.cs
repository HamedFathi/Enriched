using Enriched.StringExtended;
using Enriched.StringBuilderExtended;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;

namespace Enriched.EnumerableExtended
{
    public static class EnumerableExtensions
    {
        private static readonly Random Rnd = new Random(Guid.NewGuid().GetHashCode());

        public static int GetCount<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)
            => source?.Count(predicate!) ?? 0;

        public static long GetLongCount<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)
            => source?.LongCount(predicate!) ?? 0;
        public static IEnumerable<T> Traverse<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            foreach (T item in source)
            {
                yield return item;
                IEnumerable<T> children = selector(item);
                foreach (T child in children.Traverse(selector))
                {
                    yield return child;
                }
            }
        }
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> sequence, Func<T, IEnumerable<T>> childFetcher)
        {
            var itemsToYield = new Queue<T>(sequence);
            while (itemsToYield.Count > 0)
            {
                var item = itemsToYield.Dequeue();
                yield return item;

                var children = childFetcher(item);
                if (children != null)
                {
                    foreach (var child in children)
                    {
                        itemsToYield.Enqueue(child);
                    }
                }
            }
        }
        public static bool AllSafe<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable?.All(predicate) == true;
        }

        public static bool AnySafe<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable?.Any(predicate) == true;
        }

        public static bool AllSafe<T>(this IEnumerable<T> enumerable)
        {
            return enumerable?.All() == true;
        }

        public static bool AnySafe<T>(this IEnumerable<T> enumerable)
        {
            return enumerable?.Any() == true;
        }

        public static string Aggregate<T>(this IEnumerable<T> enumeration, Func<T, string> toString, string separator)
        {
            if (toString == null)
                throw new ArgumentNullException(nameof(toString));

            return Aggregate(enumeration.Select(toString), separator);
        }

        public static string Aggregate(this IEnumerable<string> enumeration, string separator)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));
            // Check to see that separator is not null
            if (separator == null)
                throw new ArgumentNullException(nameof(separator));
            string returnValue = string.Join(separator, enumeration.ToArray());
            if (returnValue.Length > separator.Length)
                return returnValue.Substring(separator.Length);
            return returnValue;
        }

        public static bool All<T>(this IEnumerable<T> @this, params T[] values)
        {
            T[] list = @this.ToArray();
            foreach (T value in values)
            {
                if (!list.Contains(value))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Any(this IEnumerable source)
        {
            return Enumerable.Any(source.Cast<object>());
        }

        public static bool AnySafe(this IEnumerable source)
        {
            return source != null && Enumerable.Any(source.Cast<object>());
        }

        public static bool Any<T>(this IEnumerable<T> @this, params T[] values)
        {
            T[] list = @this.ToArray();
            foreach (T value in values)
            {
                if (list.Contains(value))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var index = 0;
            foreach (var item in source)
            {
                if (predicate(item, index))
                    return true;

                index++;
            }

            return false;
        }

        public static bool AnyOfType<TSource>(this IEnumerable source)
        {
            return source.OfType<TSource>().Any();
        }

        public static bool AnyOrNotNull(this IEnumerable<string> source)
        {
            var hasData = source.Aggregate((a, b) => a + b).Any();
            if (source != null && source.Any() && hasData)
                return true;
            return false;
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, T element)
        {
            foreach (var item in source)
            {
                yield return item;
            }
            yield return element;
        }

        public static bool AreAllSame<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));

            using (var enumerator = enumerable.GetEnumerator())
            {
                var toCompare = default(T);
                if (enumerator.MoveNext())
                {
                    toCompare = enumerator.Current;
                }

                while (enumerator.MoveNext())
                {
                    if (toCompare != null && !toCompare.Equals(enumerator.Current))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool AreItemsUnique<T>(this IEnumerable<T> items)
        {
            return items.Count() == items.Distinct().Count();
        }

        public static IEnumerable<T?> AsNullable<T>(this IEnumerable<T> enumeration) where T : struct
        {
            return from item in enumeration
                   select new T?(item);
        }

        public static ReadOnlyCollection<T> AsReadOnlyCollection<T>(this IEnumerable<T> @this)
        {
            if (@this != null)
                return new ReadOnlyCollection<T>(new List<T>(@this));
            throw new ArgumentNullException(nameof(@this));
        }

        public static T At<T>(this IEnumerable<T> enumeration, int index)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));
            return enumeration.Skip(index).First();
        }

        public static IEnumerable<T> At<T>(this IEnumerable<T> enumeration, params int[] indices)
        {
            return At(enumeration, (IEnumerable<int>)indices);
        }

        public static IEnumerable<T> At<T>(this IEnumerable<T> enumeration, IEnumerable<int> indices)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));
            // Check to see that indices is not null
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));
            int currentIndex = 0;
            foreach (int index in indices.OrderBy(i => i))
            {
                while (currentIndex != index)
                {
                    enumeration = enumeration.Skip(1);
                    currentIndex++;
                }
                yield return enumeration.First();
            }
        }

        public static bool AtLeast<T>(this IEnumerable<T> enumeration, int count)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));

            return enumeration.Count() >= count;
        }

        public static bool AtLeast<T>(this IEnumerable<T> enumeration, Func<T, bool> predicate, int count)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));

            return enumeration.Count(predicate) >= count;
        }

        public static bool AtMost<T>(this IEnumerable<T> enumeration, int count)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));

            return enumeration.Count() <= count;
        }

        public static bool AtMost<T>(this IEnumerable<T> enumeration, Func<T, bool> predicate, int count)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));

            return enumeration.Count(predicate) <= count;
        }

        public static BigInteger BigIntCount<T>(this IEnumerable<T> source)
        {
            BigInteger count = 0;
            foreach (var item in source)
            {
                count++;
            }
            return count;
        }

        public static IEnumerable<(T1 left, T2 right)> Cartesian<T1, T2>(this (IEnumerable<T1>, IEnumerable<T2>) seqs)
        {
            foreach (var a in seqs.Item1)
                foreach (var b in seqs.Item2)
                    yield return (a, b);
        }

        public static IEnumerable<(T1 left, T2 right)> Cartesian<T1, T2>(this IEnumerable<T1> left, IEnumerable<T2> right)
            => (left, right).Cartesian();

        private static IEnumerable<int> InfiniteSequence(int start)
        {
            while (true)
                yield return start++;
        }

        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> source, int select, bool repetition = false)
        {
            if (source == null || @select < 0)
                throw new ArgumentNullException();
            return @select == 0
                ? new[] { new T[0] }
                : source.SelectMany((element, index) =>
                    source
                        .Skip(repetition ? index : index + 1)
                        .Combinations(@select - 1, repetition)
                        .Select(c => new[] { element }.Concat(c)));
        }

        public static IEnumerable<T> Concat<T>(IEnumerable<T> target, T element)
        {
            foreach (T e in target) yield return e;
            yield return element;
        }

        public static string Concatenate(this IEnumerable<string> @this)
        {
            var sb = new StringBuilder();
            foreach (var s in @this)
            {
                sb.Append(s);
            }
            return sb.ToString();
        }

        public static string Concatenate<T>(this IEnumerable<T> source, Func<T, string> func)
        {
            var sb = new StringBuilder();
            foreach (var item in source)
            {
                sb.Append(func(item));
            }
            return sb.ToString();
        }

        public static IDictionary<TValue, TKey> ConcatToDictionary<TValue, TKey>(this IEnumerable<KeyValuePair<TValue, TKey>> first, IEnumerable<KeyValuePair<TValue, TKey>> second)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            return first
                .Concat(second)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static IDictionary<TValue, TKey> ConcatToDictionarySafe<TValue, TKey>(this IEnumerable<KeyValuePair<TValue, TKey>> first, IEnumerable<KeyValuePair<TValue, TKey>> second)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            return first
                .Concat(second)
                .GroupBy(x => x.Key)
                .ToDictionary(x => x.Key,
                               x => x.First()
                                     .Value);
        }

        public static string ConcatWith<T>(this IEnumerable<T> items, string separator = ",", string formatString = "")
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (separator == null) throw new ArgumentNullException(nameof(separator));
            // shortcut for string enumerable
            if (typeof(T) == typeof(string))
            {
                return string.Join(separator, ((IEnumerable<string>)items).ToArray());
            }
            if (string.IsNullOrEmpty(formatString))
            {
                formatString = "{0}";
            }
            else
            {
                formatString = string.Format("{{0:{0}}}", formatString);
            }
            return string.Join(separator, items.Select(x => string.Format(formatString, x)).ToArray());
        }

        public static bool Contains<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            return collection.Count(predicate) > 0;
        }

        public static bool ContainsIgnoreCase(this IEnumerable<string> source, string str)
        {
            if (string.IsNullOrEmpty(str) || source == null || !source.Any())
                return false;
            foreach (var item in source)
            {
                return item.ContainsIgnoreCase(str);
            }
            return false;
        }

        public static IEnumerable<T> Cycle<T>(this IEnumerable<T> source)
        {
            while (true)
            {
                foreach (var item in source)
                {
                    yield return item;
                }
            }
        }

        public static void Delete(this IEnumerable<FileInfo> @this)
        {
            foreach (FileInfo t in @this)
            {
                t.Delete();
            }
        }

        public static void Delete(this IEnumerable<DirectoryInfo> @this)
        {
            foreach (DirectoryInfo t in @this)
            {
                t.Delete();
            }
        }

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> source, Func<T, T, bool> equalityComparer)
        {
            int sourceCount = source.Count();
            for (int i = 0; i < sourceCount; i++)
            {
                bool found = false;
                for (int j = 0; j < i; j++)
                    if (equalityComparer(source.ElementAt(i), source.ElementAt(j))) // In some cases, it's better to convert source in List<T> (before first for)
                    {
                        found = true;
                        break;
                    }
                if (!found)
                    yield return source.ElementAt(i);
            }
        }

        public static IEnumerable<TKey> Distinct<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            return source.GroupBy(selector).Select(x => x.Key);
        }

        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector, IEqualityComparer<TKey> keyComparer)
        {
            // Use a hashset to maintain uniqueness of keys
            var keyHashSet = new HashSet<TKey>(keyComparer);
            foreach (var element in source)
            {
                if (keyHashSet.Add(keySelector(element)))
                    yield return element;
            }
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }

        public static IEnumerable<T> DistinctBy<T>(this IEnumerable<T> list, Func<T, object> propertySelector)
        {
            return list.GroupBy(propertySelector).Select(x => x.First());
        }

        private static IEnumerable<T> ElementsNotNullFrom<T>(IEnumerable<T> source)
        {
            return source.Where(x => x != null);
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> items)
        {
            return items ?? Enumerable.Empty<T>();
        }

        public static IEnumerable<string> EnumNamesToList<T>(this IEnumerable<T> collection)
        {
            var cls = typeof(T);
            var enumArrayList = cls.GetInterfaces();
            return (from objType in enumArrayList where objType.IsEnum select objType.Name).ToList();
        }

        public static IEnumerable<T> EnumValuesToList<T>(this IEnumerable<T> collection)
        {
            var enumType = typeof(T);
            // Can't use generic type constraints on value types,
            // so have to do check like this
            // consider using - enumType.IsEnum()
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");
            var enumValArray = Enum.GetValues(enumType);
            var enumValList = new List<T>(enumValArray.Length);
            enumValList.AddRange(from int val in enumValArray select (T)Enum.Parse(enumType, val.ToString()));
            return enumValList;
        }

        public static bool Exactly<T>(this IEnumerable<T> source, int count)
        {
            return source.Count() == count;
        }

        public static bool Exactly<T>(this IEnumerable<T> source, Func<T, bool> query, int count)
        {
            return source.Count(query) == count;
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, T value, IEqualityComparer<T> comparer)
        {
            return source.Where(i => !comparer.Equals(i, value));
        }

        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> source, TSource item)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            foreach (var eachItem in source)
            {
                if (Equals(eachItem, item))
                    continue;

                yield return eachItem;
            }
        }

        public static IEnumerable<T> ExceptDefault<T>(this IEnumerable<T> source) => source.Except(default!);

        public static IEnumerable<string> ExceptNullOrWhiteSpace(this IEnumerable<string> source)
        {
            return source.Where(s => !s.IsNullOrWhiteSpace());
        }

        public static bool Exists<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            return list.Index(predicate) > -1;
        }

        public static List<T> FindAll<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            var found = new List<T>();
            var enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (predicate(enumerator.Current))
                {
                    found.Add(enumerator.Current);
                }
            }
            return found;
        }

        public static TSource FirstOfType<TSource>(this IEnumerable source)
        {
            return source.OfType<TSource>().First();
        }

        public static T FirstOr<T>(this IEnumerable<T> @this, Func<T, bool> predicate, Func<T> onOr)
        {
            T found = @this.FirstOrDefault(predicate);
            if (found.Equals(default(T)))
            {
                found = onOr();
            }
            return found;
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> source, T defaultValue)
        {
            return source.IsNotEmpty() ? source.First() : defaultValue;
        }

        public static TSource FirstOrDefaultOfType<TSource>(this IEnumerable source)
        {
            return source.OfType<TSource>().FirstOrDefault();
        }

        public static string Flatten(this IEnumerable<string> strings, string seperator, string head, string tail)
        {
            if (strings == null || strings.Count() == 0)
                return string.Empty;

            var flattenedString = new StringBuilder();

            flattenedString.Append(head);
            foreach (var s in strings)
                flattenedString.AppendFormat("{0}{1}", s, seperator);
            flattenedString.Remove(flattenedString.Length - seperator.Length, seperator.Length);
            flattenedString.Append(tail);

            return flattenedString.ToString();
        }

        public static string Flatten(this IEnumerable<string> strings, string prefix, string suffix, string head, string tail)
        {
            // Return the flattened string
            return strings
                .Select(s => string.Format("{0}{1}{2}", prefix, s, suffix))
                .Flatten(String.Empty, head, tail);
        }

        public static IEnumerable<FileInfo> ForEach(this IEnumerable<FileInfo> @this, Action<FileInfo> action)
        {
            foreach (FileInfo t in @this)
            {
                action(t);
            }
            return @this;
        }

        public static IEnumerable<DirectoryInfo> ForEach(this IEnumerable<DirectoryInfo> @this, Action<DirectoryInfo> action)
        {
            foreach (DirectoryInfo t in @this)
            {
                action(t);
            }
            return @this;
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action, Func<T, bool> predicate)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            foreach (var item in collection.Where(predicate))
            {
                action(item);
            }
        }

        public static void ForEach(this IEnumerable collection, Action<object> action)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<int, T> action)
        {
            if (items != null)
            {
                int i = 0;
                foreach (T item in items)
                {
                    action(i++, item);
                }
            }
        }

        public static void ForEach<T>(this IEnumerable<T> @this, Action<T, int> action)
        {
            var array = @this.ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                action(array[i], i);
            }
        }

        public static void ForEachReverse<T>(this IEnumerable<T> source, Action<T> action)
        {
            var items = source.ToList();
            for (int i = items.Count - 1; i >= 0; i--)
            {
                action(items[i]);
            }
        }

        public static void ForEachReverse<T>(this IEnumerable<T> source, Action<T> action, Func<T, bool> predicate)
        {
            var items = source.ToList();
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (predicate(items[i]))
                    action(items[i]);
            }
        }

        public static void ForEachReverse<T>(this IEnumerable<T> @this, Action<T, int> action)
        {
            var array = @this.ToArray();
            for (var i = array.Length - 1; i >= 0; i--)
            {
                action(array[i], i);
            }
        }

        public static IEnumerable<T> GetDuplicates<T>(this IEnumerable<T> @this)
        {
            HashSet<T> hashset = new HashSet<T>();
            return @this.Where(e => !hashset.Add(e));
        }

        public static IEnumerable<IEnumerable<T>> GetKCombinations<T>(this IEnumerable<T> list, int length) where T : IComparable
        {
            if (length == 1) return list.Select(t => new[] { t });
            return GetKCombinations(list, length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                    (t1, t2) => t1.Concat(new[] { t2 }));
        }

        public static IEnumerable<IEnumerable<T>> GetKCombinationsWithRepetition<T>(this IEnumerable<T> list, int length) where T : IComparable
        {
            if (length == 1) return list.Select(t => new[] { t });
            return GetKCombinationsWithRepetition(list, length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) >= 0),
                    (t1, t2) => t1.Concat(new[] { t2 }));
        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(o => !t.Contains(o)),
                    (t1, t2) => t1.Concat(new[] { t2 }));
        }

        public static IEnumerable<IEnumerable<T>> GetPermutationsWithRepetition<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new[] { t });
            return GetPermutationsWithRepetition(list, length - 1)
                .SelectMany(_ => list,
                    (t1, t2) => t1.Concat(new[] { t2 }));
        }

        public static IEnumerable<T[]> GroupEvery<T>(this IEnumerable<T> enumeration, int count)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            int current = 0;
            T[] array = new T[count];
            foreach (var item in enumeration)
            {
                array[current++] = item;
                if (current == count)
                {
                    yield return array;
                    current = 0;
                    array = new T[count];
                }
            }
            if (current != 0)
            {
                yield return array;
            }
        }

        public static IEnumerable<T> IgnoreNulls<T>(this IEnumerable<T> target)
        {
            if (ReferenceEquals(target, null))
                yield break;
            foreach (var item in target.Where(item => !ReferenceEquals(item, null)))
                yield return item;
        }

        public static int Index<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            var enumerator = list.GetEnumerator();
            for (int i = 0; enumerator.MoveNext(); ++i)
            {
                if (predicate(enumerator.Current))
                {
                    return i;
                }
            }
            return -1;
        }

        public static int IndexOf<T>(this IEnumerable<T> items, T item, IEqualityComparer<T> comparer)
        {
            return IndexOf(items, item, comparer.Equals);
        }

        public static int IndexOf<T>(this IEnumerable<T> items, T item, Func<T, T, bool> predicate)
        {
            int index = 0;
            foreach (T instance in items)
            {
                if (predicate(item, instance))
                {
                    return index;
                }
                ++index;
            }
            return -1;
        }

        public static IEnumerable<int> IndicesOf<T>(this IEnumerable<T> obj, T value)
        {
            return (from i in Enumerable.Range(0, obj.Count())
                    where obj.ElementAt(i).Equals(value)
                    select i);
        }

        public static IEnumerable<int> IndicesOf<T>(this IEnumerable<T> obj, IEnumerable<T> value)
        {
            return (from i in Enumerable.Range(0, obj.Count())
                    where value.Contains(obj.ElementAt(i))
                    select i);
        }

        public static IEnumerable<int> IndicesWhere<T>(this IEnumerable<T> enumeration, Func<T, bool> predicate)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));
            // Check to see that predicate is not null
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            int i = 0;
            foreach (T item in enumeration)
            {
                if (predicate(item))
                    yield return i;
                i++;
            }
        }

        public static IEnumerable<TSource> Intersperse<TSource>(this IEnumerable<TSource> source, TSource separator, int count = 1)
        {
            bool isFirst = true;

            foreach (TSource element in source)
            {
                if (!isFirst)
                {
                    for (int i = 0; i < count; i++)
                    {
                        yield return separator;
                    }
                }
                else
                {
                    isFirst = false;
                }

                yield return element;
            }
        }

        public static bool IsCountEqual<TSource>(this IEnumerable<TSource> source, int expectedCount)
        {
            return source.Take(expectedCount + 1).Count() == expectedCount;
        }

        public static bool IsCountEqual<TSource>(this IEnumerable<TSource> source, int expectedCount, Func<TSource, bool> predicate)
        {
            return source.Where(predicate).Take(expectedCount + 1).Count() == expectedCount;
        }

        public static bool IsCountGreater<TSource>(this IEnumerable<TSource> source, int comparisonCount)
        {
            return source.Skip(comparisonCount).Any();
        }

        public static bool IsCountGreater<TSource>(this IEnumerable<TSource> source, int comparisonCount, Func<TSource, bool> predicate)
        {
            return source.Where(predicate).Skip(comparisonCount).Any();
        }

        public static bool IsCountSmaller<TSource>(this IEnumerable<TSource> source, int comparisonCount)
        {
            return !source.Skip(comparisonCount - 1).Any();
        }

        public static bool IsCountSmaller<TSource>(this IEnumerable<TSource> source, int comparisonCount, Func<TSource, bool> predicate)
        {
            return !source.Where(predicate).Skip(comparisonCount - 1).Any();
        }

        public static bool IsEmpty(this IEnumerable collection)
        {
            foreach (var _ in collection)
            {
                return false;
            }
            return true;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> @this) => !@this.Any();

        public static bool IsEqual<T>(this IEnumerable<T> source, IEnumerable<T> toCompareWith)
        {
            if ((source == null) || (toCompareWith == null))
            {
                return false;
            }
            return source.IsEqual(toCompareWith, null);
        }

        public static bool IsEqual<T>(this IEnumerable<T> source, IEnumerable<T> toCompareWith, IEqualityComparer<T> comparer)
        {
            if ((source == null) || (toCompareWith == null))
            {
                return false;
            }
            var countSource = source.Count();
            var countToCompareWith = toCompareWith.Count();
            if (countSource != countToCompareWith)
            {
                return false;
            }
            if (countSource == 0)
            {
                return true;
            }

            var comparerToUse = comparer ?? EqualityComparer<T>.Default;
            // check whether the intersection of both sequences has the same number of elements
            return source.Intersect(toCompareWith, comparerToUse).Count() == countSource;
        }

        public static IEnumerable<string> IsLike(this IEnumerable<string> @this, string pattern)
        {
            return @this.Where(x => x.IsLike(pattern));
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> @this)
        {
            return @this.Any();
        }

        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> @this)
        {
            return @this != null && @this.Any();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> @this)
        {
            return @this == null || !@this.Any();
        }

        public static bool IsOneItem<T>(this IEnumerable<T> source)
        {
            return source.Count() == 1;
        }

        public static bool IsOneItem<T>(this IEnumerable<T> source, Func<T, bool> query)
        {
            return source.Count(query) == 1;
        }

        public static bool IsSingle<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            using (var enumerator = source.GetEnumerator())
                return enumerator.MoveNext() && !enumerator.MoveNext();
        }

        public static string Join<T>(this IEnumerable<T> collection, Func<T, string> func, string separator)
        {
            return string.Join(separator, collection.Select(func).ToArray());
        }

        public static int LastIndexOf<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            // Buffer all elements
            var asReadOnlyList = source as IReadOnlyList<T> ?? source.ToArray();

            // Loop in reverse
            for (var i = asReadOnlyList.Count - 1; i >= 0; i--)
            {
                // If matches - return index
                if (predicate(asReadOnlyList[i]))
                    return i;
            }

            // If nothing found - return -1
            return -1;
        }

        public static int LastIndexOf<T>(this IEnumerable<T> source, T element) => source.LastIndexOf(element, EqualityComparer<T>.Default);

        public static int LastIndexOf<T>(this IEnumerable<T> source, T element, IEqualityComparer<T> comparer)
        {
            return source.LastIndexOf(i => comparer.Equals(i, element));
        }

        public static IEnumerable<TResult> LeftOuterJoin<TLeft, TRight, TKey, TResult>(
                                                    this IEnumerable<TLeft> left,
                                                    IEnumerable<TRight> right,
                                                    Func<TLeft, TKey> leftKeySelector,
                                                    Func<TRight, TKey> rightKeySelector,
                                                    Func<JoinResult<TLeft, TRight>, TResult> resultSelector)
        {
            IEnumerable<GroupJoinResult<TLeft, TRight>> groupJoinResult = left
                .GroupJoin(
                    right,
                    leftKeySelector,
                    rightKeySelector,
                    (l, r) => new GroupJoinResult<TLeft, TRight>
                    {
                        Key = l,
                        Values = r
                    });

            IEnumerable<TResult> leftOuterJoinResults = groupJoinResult
                .SelectMany(
                    x => x.Values.Select(c => new JoinResult<TLeft, TRight>
                    {
                        Left = x.Key,
                        Right = c
                    }))
                .Select(resultSelector);

            return leftOuterJoinResults;
        }

        public static bool Many<T>(this IEnumerable<T> source)
        {
            return source.Count() > 1;
        }

        public static bool Many<T>(this IEnumerable<T> source, Func<T, bool> query)
        {
            return source.Count(query) > 1;
        }

        public static TSource Max<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.FirstOrDefault(c => selector(c).Equals(source.Max(selector)));
        }

        public static TItem MaxItem<TItem, TValue>(this IEnumerable<TItem> items, Func<TItem, TValue> selector, out TValue maxValue)
                                                                                                                                                                                            where TItem : class
                                                                                                                                                                                            where TValue : IComparable
        {
            TItem maxItem = null;
            maxValue = default(TValue);
            foreach (var item in items)
            {
                if (item == null)
                    continue;
                var itemValue = selector(item);
                if ((maxItem != null) && (itemValue.CompareTo(maxValue) <= 0))
                    continue;
                maxValue = itemValue;
                maxItem = item;
            }
            return maxItem;
        }

        public static TItem MaxItem<TItem, TValue>(this IEnumerable<TItem> items, Func<TItem, TValue> selector)
                                                                                                                                                                                            where TItem : class
                                                                                                                                                                                            where TValue : IComparable
        {
            TValue maxValue;
            return items.MaxItem(selector, out maxValue);
        }

        public static IEnumerable<TSource> Maxs<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.ToLookup(selector).Max(n => n.Key);
        }

        public static IEnumerable<T> MergeDistinctInnerEnumerable<T>(this IEnumerable<IEnumerable<T>> @this)
        {
            List<IEnumerable<T>> listItem = @this.ToList();

            var list = new List<T>();

            foreach (var item in listItem)
            {
                list = list.Union(item).ToList();
            }

            return list;
        }

        public static IEnumerable<T> MergeInnerEnumerable<T>(this IEnumerable<IEnumerable<T>> @this)
        {
            List<IEnumerable<T>> listItem = @this.ToList();

            var list = new List<T>();

            foreach (var item in listItem)
            {
                list.AddRange(item);
            }

            return list;
        }

        public static TSource Min<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.FirstOrDefault(c => selector(c).Equals(source.Min(selector)));
        }

        public static TItem MinItem<TItem, TValue>(this IEnumerable<TItem> items, Func<TItem, TValue> selector, out TValue minValue)
                                                                                                                                                                                            where TItem : class
                                                                                                                                                                                            where TValue : IComparable
        {
            TItem minItem = null;
            minValue = default(TValue);
            foreach (var item in items)
            {
                if (item == null)
                    continue;
                var itemValue = selector(item);
                if ((minItem != null) && (itemValue.CompareTo(minValue) >= 0))
                    continue;
                minValue = itemValue;
                minItem = item;
            }
            return minItem;
        }

        public static TItem MinItem<TItem, TValue>(this IEnumerable<TItem> items, Func<TItem, TValue> selector)
                                                                                                                                                                                                            where TItem : class
                                                                                                                                                                                                            where TValue : IComparable
        {
            TValue minValue;
            return items.MinItem(selector, out minValue);
        }

        public static IEnumerable<TSource> Mins<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.ToLookup(selector).Min(n => n.Key);
        }

        public static bool None<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }

        public static bool None<T>(this IEnumerable<T> source, Func<T, bool> query)
        {
            return !source.Any(query);
        }

        public static IEnumerable NotOfType<TSource>(this IEnumerable source)
        {
            return source.Cast<object>().Where(x => !(x is TSource));
        }

        public static bool OnlyOne<T>(this IEnumerable<T> source)
        {
            return source.Count() == 1;
        }

        public static bool OnlyOne<T>(this IEnumerable<T> source, Func<T, bool> condition = null)
        {
            return source.Count(condition ?? (t => true)) == 1;
        }

        public static PartitionedSequence<TSource> Partition<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var matches = new List<TSource>();
            var mismatches = new List<TSource>();

            foreach (TSource value in source)
            {
                if (predicate(value))
                {
                    matches.Add(value);
                }
                else
                {
                    mismatches.Add(value);
                }
            }

            return new PartitionedSequence<TSource>(matches, mismatches);
        }

        public static string PathCombine(this IEnumerable<string> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            return Path.Combine(enumerable.ToArray());
        }

        public static IEnumerable<T> Perform<T>(this IEnumerable<T> collection, Func<T, T> func)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            foreach (var item in collection)
            {
                yield return func(item);
            }
        }

        public static Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>> Pivot<TSource, TFirstKey, TSecondKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TFirstKey> firstKeySelector, Func<TSource, TSecondKey> secondKeySelector, Func<IEnumerable<TSource>, TValue> aggregate)
        {
            var retVal = new Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>>();

            var l = source.ToLookup(firstKeySelector);
            foreach (var item in l)
            {
                var dict = new Dictionary<TSecondKey, TValue>();
                retVal.Add(item.Key, dict);
                var subdict = item.ToLookup(secondKeySelector);
                foreach (var subitem in subdict)
                {
                    dict.Add(subitem.Key, aggregate(subitem));
                }
            }

            return retVal;
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> source, T element)
        {
            yield return element;
            foreach (var item in source)
            {
                yield return item;
            }
        }

        public static T Random<T>(this IEnumerable<T> source)
        {
            // Buffer all elements
            var asReadOnlyList = source as IReadOnlyList<T> ?? source.ToArray();

            // If there are no elements - throw
            if (!asReadOnlyList.Any())
                throw new InvalidOperationException("Sequence contains no elements.");

            return asReadOnlyList[Rnd.Next(0, asReadOnlyList.Count)];
        }

        public static T RandomOrDefault<T>(this IEnumerable<T> source)
        {
            // Buffer all elements
            var asReadOnlyList = source as IReadOnlyList<T> ?? source.ToArray();

            // If there are no elements - return default
            if (!asReadOnlyList.Any())
                return default!;

            return asReadOnlyList[Rnd.Next(0, asReadOnlyList.Count)];
        }

        public static IEnumerable<T> RandomSubset<T>(this IEnumerable<T> sequence, int subsetSize)
        {
            return RandomSubset(sequence, subsetSize, new Random());
        }

        public static IEnumerable<T> RandomSubset<T>(this IEnumerable<T> sequence, int subsetSize, Random rand)
        {
            if (rand == null) throw new ArgumentNullException(nameof(rand));
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (subsetSize < 0) throw new ArgumentOutOfRangeException(nameof(subsetSize));
            return RandomSubsetImpl(sequence, subsetSize, rand);
        }

        private static IEnumerable<T> RandomSubsetImpl<T>(IEnumerable<T> sequence, int subsetSize, Random rand)
        {
            // The simplest and most efficient way to return a random subet is to perform
            // an in-place, partial Fisher-Yates shuffle of the sequence. While we could do
            // a full shuffle, it would be wasteful in the cases where subsetSize is shorter
            // than the length of the sequence.
            // See: http://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
            var seqArray = sequence.ToArray();
            if (seqArray.Length < subsetSize)
                throw new ArgumentOutOfRangeException(nameof(subsetSize), "Subset size must be <= sequence.Count()");
            var m = 0;                // keeps track of count items shuffled
            var w = seqArray.Length;  // upper bound of shrinking swap range
            var g = w - 1;            // used to compute the second swap index
            // perform in-place, partial Fisher-Yates shuffle
            while (m < subsetSize)
            {
                var k = g - rand.Next(w);
                var tmp = seqArray[k];
                seqArray[k] = seqArray[m];
                seqArray[m] = tmp;
                ++m;
                --w;
            }
            // yield the random subet as a new sequence
            for (var i = 0; i < subsetSize; i++)
                yield return seqArray[i];
        }

        public static IEnumerable<T> RemoveDuplicates<T>(this IEnumerable<T> @this)
        {
            return new HashSet<T>(@this).ToList();
        }

        public static IEnumerable<T> RemoveDuplicates<T>(this IEnumerable<T> list, Func<T, int> predicate)
        {
            var dict = new Dictionary<int, T>();

            foreach (var item in list)
            {
                if (!dict.ContainsKey(predicate(item)))
                {
                    dict.Add(predicate(item), item);
                }
            }

            return dict.Values.AsEnumerable();
        }

        public static IEnumerable<string> RemoveEmptyElements(this IEnumerable<string> strings)
        {
            foreach (var s in strings)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    yield return s;
                }
            }
        }

        public static IEnumerable<T> RemoveWhere<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            if (source == null)
                yield break;
            foreach (var t in source)
                if (!predicate(t))
                    yield return t;
        }

        public static IEnumerable<TSource> Repeat<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (count == 0)
            {
                yield break;
            }

            var collection = source as ICollection<TSource>;

            var elementBuffer = collection == null
                ? new List<TSource>()
                : new List<TSource>(collection.Count);

            foreach (TSource element in source)
            {
                yield return element;

                // We add this element to a local element buffer so that
                // we don't have to enumerate the sequence multiple times
                elementBuffer.Add(element);
            }

            if (elementBuffer.IsEmpty())
            {
                // If the element buffer is empty, so was the source sequence.
                // In this case, we can stop here and simply return an empty sequence.
                yield break;
            }

            // We already returned each element of the sequence once,
            // so take that into account when returning the elements repeatedly
            for (int i = 0; i < count - 1; i++)
            {
                foreach (TSource element in elementBuffer)
                {
                    yield return element;
                }
            }
        }

        public static IEnumerable<T> ReplaceWhere<T>(this IEnumerable<T> enumerable, Predicate<T> predicate, Func<T> replacement)
        {
            if (enumerable == null)
            {
                yield break;
            }
            foreach (var item in enumerable)
            {
                if (predicate(item))
                {
                    yield return replacement();
                }
                else
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> ReplaceWhere<T>(this IEnumerable<T> enumerable, Predicate<T> predicate, T replacement)
        {
            if (enumerable == null)
            {
                yield break;
            }
            foreach (var item in enumerable)
            {
                if (predicate(item))
                {
                    yield return replacement;
                }
                else
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, bool allowNull = true)
        {
            foreach (var item in source)
            {
                var select = selector(item);
                if (allowNull || !Equals(@select, default(TSource)))
                    yield return @select;
            }
        }

        public static IEnumerable<T> SelectMany<T>(this IEnumerable<IEnumerable<T>> source)
        {
            // Check to see that source is not null
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            foreach (var enumeration in source)
            {
                foreach (var item in enumeration)
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> SelectMany<T>(this IEnumerable<T[]> source)
        {
            // Check to see that source is not null
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            foreach (var enumeration in source)
            {
                foreach (var item in enumeration)
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> SelectManyAllInclusive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            return source.Concat(source.SelectManyRecursive(selector));
        }

        public static IEnumerable<T> SelectManyRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            var result = source.SelectMany(selector).ToList();
            if (result.Count == 0)
            {
                return result;
            }
            return result.Concat(result.SelectManyRecursive(selector));
        }

        public static bool SequenceEqual<T1, T2>(this IEnumerable<T1> left, IEnumerable<T2> right, Func<T1, T2, bool> comparer)
        {
            using (IEnumerator<T1> leftE = left.GetEnumerator())
            {
                using (IEnumerator<T2> rightE = right.GetEnumerator())
                {
                    bool leftNext = leftE.MoveNext(), rightNext = rightE.MoveNext();

                    while (leftNext && rightNext)
                    {
                        // If one of the items isn't the same...
                        if (!comparer(leftE.Current, rightE.Current))
                            return false;

                        leftNext = leftE.MoveNext();
                        rightNext = rightE.MoveNext();
                    }

                    // If left or right is longer
                    if (leftNext || rightNext)
                        return false;
                }
            }

            return true;
        }

        public static bool SequenceSuperset<T>(this IEnumerable<T> enumeration, IEnumerable<T> subset)
        {
            return SequenceSuperset(enumeration, subset, EqualityComparer<T>.Default.Equals);
        }

        public static bool SequenceSuperset<T>(this IEnumerable<T> enumeration, IEnumerable<T> subset, Func<T, T, bool> equalityComparer)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));
            // Check to see that subset is not null
            if (subset == null)
                throw new ArgumentNullException(nameof(subset));
            // Check to see that comparer is not null
            if (equalityComparer == null)
                throw new ArgumentNullException("comparer");
            using (IEnumerator<T> big = enumeration.GetEnumerator(), small = subset.GetEnumerator())
            {
                big.Reset(); small.Reset();
                while (big.MoveNext())
                {
                    // End of subset, which means we've gone through it all and it's all equal.
                    if (!small.MoveNext())
                        return true;
                    if (!equalityComparer(big.Current, small.Current))
                    {
                        // Comparison failed. Let's try comparing with the first item.
                        small.Reset();
                        // There's more than one item in the small enumeration. Guess why I know this.
                        small.MoveNext();
                        // No go with the first item? Reset the collection and brace for the next iteration of the big loop.
                        if (!equalityComparer(big.Current, small.Current))
                            small.Reset();
                    }
                }
                // End of both, which means that the small is the end of the big.
                if (!small.MoveNext())
                    return true;
            }
            return false;
        }

        public static bool SetEqual<T>(this IEnumerable<T> source, IEnumerable<T> toCompareWith)
        {
            if ((source == null) || (toCompareWith == null))
            {
                return false;
            }
            return source.SetEqual(toCompareWith, null);
        }

        public static bool SetEqual<T>(this IEnumerable<T> source, IEnumerable<T> toCompareWith, IEqualityComparer<T> comparer)
        {
            if ((source == null) || (toCompareWith == null))
            {
                return false;
            }
            var countSource = source.Count();
            var countToCompareWith = toCompareWith.Count();
            if (countSource != countToCompareWith)
            {
                return false;
            }
            if (countSource == 0)
            {
                return true;
            }
            var comparerToUse = comparer ?? EqualityComparer<T>.Default;
            // check whether the intersection of both sequences has the same number of elements
            return source.Intersect(toCompareWith, comparerToUse).Count() == countSource;
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            return items.ShuffleIterator();
        }

        private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> items)
        {
            var buffer = items.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = Rnd.Next(i, buffer.Count);
                yield return buffer[j];
                buffer[j] = buffer[i];
            }
        }

        public static TSource Single<TSource, TException>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, Func<TException> exceptionToThrow)
                                            where TException : Exception
        {
            switch (source.Count(predicate))
            {
                case 1:
                    return source.Single(predicate);

                default:
                    throw exceptionToThrow();
            }
        }

        public static TSource SingleOfType<TSource>(this IEnumerable source)
        {
            return source.OfType<TSource>().Single();
        }

        public static TSource SingleOrDefaultOfType<TSource>(this IEnumerable source)
        {
            return source.OfType<TSource>().SingleOrDefault();
        }

        public static TSource SingleOrThrow<TSource, TException>(this IEnumerable<TSource> source, Func<TException> exceptionToThrow)
                                            where TException : Exception
        {
            switch (source.Take(2).Count())
            {
                case 1:
                    return source.First();

                default:
                    throw exceptionToThrow();
            }
        }

        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source, int count)
        {
            // If count is 0 - return source
            if (count == 0)
                return source;

            // Buffer all elements
            var asReadOnlyList = source as IReadOnlyList<T> ?? source.ToArray();

            // If count is greater than element count - return empty
            if (count >= asReadOnlyList.Count)
                return Enumerable.Empty<T>();

            // Otherwise - slice
            return asReadOnlyList.Slice(0, asReadOnlyList.Count - count);
        }

        public static IEnumerable<T> SkipLastWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return source.Reverse().SkipWhile(predicate).Reverse();
        }

        public static IEnumerable<T> Slice<T>(this IEnumerable<T> items, int start, int end)
        {
            int index = 0;
            int count = 0;
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (items is ICollection<T>)
                count = ((ICollection<T>)items).Count;
            else if (items is ICollection)
                count = ((ICollection)items).Count;
            else
                count = items.Count();
            if (start < 0)
                start += count;
            if (end < 0)
                end += count;
            foreach (var item in items)
            {
                if (index >= end)
                    yield break;
                if (index >= start)
                    yield return item;
                ++index;
            }
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int chunkSize)
        {
            var count = 0;
            var chunk = new List<T>(chunkSize);
            foreach (var item in source)
            {
                chunk.Add(item);
                count++;
                if (count == chunkSize)
                {
                    yield return chunk.AsEnumerable();
                    chunk = new List<T>(chunkSize);
                    count = 0;
                }
            }
            if (count > 0)
            {
                yield return chunk.AsEnumerable();
            }
        }

        public static double StdDev(this IEnumerable<int> source)
        {
            return StdDevLogic(source);
        }

        public static double StdDev(this IEnumerable<double> source)
        {
            return StdDevLogic(source);
        }

        public static float StdDev(this IEnumerable<float> source)
        {
            return StdDevLogic(source);
        }

        private static double StdDevLogic(this IEnumerable<double> source, int buffer = 1)
        {
            if (source == null)
            { throw new ArgumentNullException(nameof(source)); }

            var data = source.ToList();
            var average = data.Average();
            var differences = data.Select(u => Math.Pow(average - u, 2.0)).ToList();
            return Math.Sqrt(differences.Sum() / (differences.Count() - buffer));
        }

        private static double StdDevLogic(this IEnumerable<int> source, int buffer = 1)
        {
            return StdDevLogic(source.Select(x => (double)x), buffer);
        }

        private static float StdDevLogic(this IEnumerable<float> source, int buffer = 1)
        {
            if (source == null)
            { throw new ArgumentNullException(nameof(source)); }
            var data = source.ToList();
            var average = data.Average();
            var differences = data.Select(u => Math.Pow(average - u, 2.0)).ToList();
            return (float)Math.Sqrt(differences.Sum() / (differences.Count() - buffer));
        }

        public static double StdDevP(this IEnumerable<int> source)
        {
            return StdDevLogic(source, 0);
        }

        public static double StdDevP(this IEnumerable<double> source)
        {
            return StdDevLogic(source, 0);
        }

        public static double StdDevP(this IEnumerable<float> source)
        {
            return StdDevLogic(source, 0);
        }

        public static string StringJoin<T>(this IEnumerable<T> @this, string separator)
        {
            return string.Join(separator, @this);
        }

        public static string StringJoin<T>(this IEnumerable<T> @this, char separator)
        {
            return string.Join(separator.ToString(), @this);
        }

        public static uint Sum(this IEnumerable<uint> source)
        {
            return source.Aggregate(0U, (current, number) => current + number);
        }

        public static ulong Sum(this IEnumerable<ulong> source)
        {
            return source.Aggregate(0UL, (current, number) => current + number);
        }

        public static uint? Sum(this IEnumerable<uint?> source)
        {
            return source.Where(nullable => nullable.HasValue)
                .Aggregate(0U, (current, nullable) => current + nullable.GetValueOrDefault());
        }

        public static ulong? Sum(this IEnumerable<ulong?> source)
        {
            return source.Where(nullable => nullable.HasValue)
                .Aggregate(0UL, (current, nullable) => current + nullable.GetValueOrDefault());
        }

        public static uint Sum<T>(this IEnumerable<T> source, Func<T, uint> selection)
        {
            return ElementsNotNullFrom(source).Select(selection).Sum();
        }

        public static uint? Sum<T>(this IEnumerable<T> source, Func<T, uint?> selection)
        {
            return ElementsNotNullFrom(source).Select(selection).Sum();
        }

        public static ulong Sum<T>(this IEnumerable<T> source, Func<T, ulong> selector)
        {
            return ElementsNotNullFrom(source).Select(selector).Sum();
        }

        public static ulong? Sum<T>(this IEnumerable<T> source, Func<T, ulong?> selector)
        {
            return ElementsNotNullFrom(source).Select(selector).Sum();
        }

        public static IEnumerable<T> TakeEvery<T>(this IEnumerable<T> enumeration, int startAt, int hopLength)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));
            int first = 0;
            int count = 0;
            foreach (T item in enumeration)
            {
                if (first < startAt)
                {
                    first++;
                }
                else if (first == startAt)
                {
                    yield return item;
                    first++;
                }
                else
                {
                    count++;
                    if (count == hopLength)
                    {
                        yield return item;
                        count = 0;
                    }
                }
            }
        }

        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> list, int n)
        {
            return list.ToList().TakeLast(n);
        }

        public static IEnumerable<T> TakeLastWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return source.Reverse().TakeWhile(predicate).Reverse();
        }

        public static IEnumerable<TSource> TakeSkip<TSource>(this IEnumerable<TSource> source, int take, int skip)
        {
            var enumerator = source.GetEnumerator();

            while (true)
            {
                for (int i = 0; i < take; i++)
                {
                    if (!enumerator.MoveNext())
                        yield break;

                    yield return enumerator.Current;
                }

                for (int i = 0; i < skip; i++)
                {
                    if (!enumerator.MoveNext())
                        yield break;
                }
            }
        }

        public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> collection, Func<T, bool> endCondition)
        {
            return collection.TakeWhile(item => !endCondition(item));
        }

        public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> collection, Predicate<T> endCondition)
        {
            return collection.TakeWhile(item => !endCondition(item));
        }

        public static TResult[] ToArray<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.Select(selector).ToArray();
        }

        public static ICollection<T> ToCollection<T>(this IEnumerable<T> enumerable)
        {
            var collection = new Collection<T>();
            foreach (var i in enumerable)
                collection.Add(i);
            return collection;
        }

        public static string ToCsv<T>(this IEnumerable<T> items, char separator = ',', bool trim = true)
        {
            var list = trim ? items.Select(x => x.ToString().Trim()) : items.Select(x => x.ToString());
            return list.Aggregate((a, b) => a + separator + b);
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> varlist)
        {
            var dtReturn = new DataTable();
            PropertyInfo[] oProps = null;
            if (varlist == null) return dtReturn;
            foreach (T rec in varlist)
            {
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                            colType = colType.GetGenericArguments()[0];
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) ?? DBNull.Value;
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> groupings)
        {
            if (groupings == null)
                throw new ArgumentNullException(nameof(groupings));

            return groupings.ToDictionary(group => group.Key, group => group.ToList());
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> enumeration)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));

            return enumeration.ToDictionary(item => item.Key, item => item.Value);
        }

        public static IDictionary<TKey, TElement> ToDistinctDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            if (source == null) throw new NullReferenceException("The 'source' cannot be null.");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));

            var dictionary = new Dictionary<TKey, TElement>();
            foreach (TSource current in source)
            {
                dictionary[keySelector(current)] = elementSelector(current);
            }
            return dictionary;
        }

        public static Dictionary<TKey, IEnumerable<TElement>>
                                                                                                                                                                                                                                                            ToGroupedDictionary<TKey, TElement>(this IEnumerable<IGrouping<TKey, TElement>> items)
        {
            return items.ToDictionary<IGrouping<TKey, TElement>, TKey, IEnumerable<TElement>>(
                item => item.Key,
                item => item);
        }

        public static List<TResult> ToList<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.Select(selector).ToList();
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            return new ObservableCollection<T>(enumerable);
        }

        public static IEnumerable<T> ToPaged<T>(this IEnumerable<T> query, int pageIndex, int pageSize)
        {
            return query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                ;
        }

        public static IReadOnlyCollection<TDestination> ToReadOnlyCollection<TDestination>(this IEnumerable<TDestination> source)
        {
            var sourceAsDestination = new List<TDestination>();
            if (source != null)
            {
                foreach (TDestination toAdd in source)
                {
                    sourceAsDestination.Add(toAdd);
                }
            }
            return new ReadOnlyCollection<TDestination>(sourceAsDestination);
        }

        public static string ToText(this IEnumerable<string> strs)
        {
            return strs.ToStringBuilder().ToString();
        }

        public static StringBuilder ToStringBuilder(this IEnumerable<string> strs)
        {
            var sb = new StringBuilder();
            foreach (var str in strs)
            {
                sb.AppendLine(str);
            }
            return sb;
        }

        public static bool TryGetNonEnumeratedCount<T>(this IEnumerable<T> enumerable, out int count)
        {
            count = 0;
            if (enumerable is ICollection<T> collection)
            {
                count = collection.Count;
                return true;
            }
            if (enumerable is IReadOnlyCollection<T> roCollection)
            {
                count = roCollection.Count;
                return true;
            }
            return false;
        }

        public static IEnumerable<T> Union<T>(this IEnumerable<IEnumerable<T>> enumeration)
        {
            // Check to see that enumeration is not null
            if (enumeration == null)
                throw new ArgumentNullException(nameof(enumeration));

            IEnumerable<T> returnValue = null;

            foreach (var e in enumeration)
            {
                if (returnValue != null)
                    returnValue = e;
                else
                    returnValue = returnValue.Union(e);
            }

            return returnValue;
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        public static IEnumerable<TSource> WhereNot<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Where(element => !predicate(element));
        }

        public static IEnumerable<TSource> WhereNot<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            return source.Where((element, index) => !predicate(element, index));
        }

        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> source) where T : class
        {
            return source.Where(x => x != null);
        }

        public static TList With<TList, T>(this TList list, T item) where TList : IList<T>, new()
        {
            var l = new TList();

            foreach (T i in list)
            {
                l.Add(i);
            }
            l.Add(item);

            return l;
        }

        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
        {
            return source.Select((item, index) => (item, index));
        }

        public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source, params TSource[] elements)
        {
            return Without(source, (IEnumerable<TSource>)elements);
        }

        public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> elements)
        {
            return WithoutIterator(source, elements, EqualityComparer<TSource>.Default);
        }

        public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source,
                                                                                                                                                                                                            IEqualityComparer<TSource> equalityComparer, params TSource[] elements)
        {
            return Without(source, equalityComparer, (IEnumerable<TSource>)elements);
        }

        public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source,
                                                                                                                                                                                                            IEqualityComparer<TSource> equalityComparer, IEnumerable<TSource> elements)
        {
            return WithoutIterator(source, elements, equalityComparer);
        }

        public static TList Without<TList, T>(this TList list, T item) where TList : IList<T>, new()
        {
            var l = new TList();

            foreach (T i in list.Where(n => !n.Equals(item)))
            {
                l.Add(i);
            }

            return l;
        }

        private static IEnumerable<TSource> WithoutIterator<TSource>(IEnumerable<TSource> source, IEnumerable<TSource> elementsToRemove, IEqualityComparer<TSource> comparer)
        {
            HashSet<TSource> elementsToRemoveSet = new HashSet<TSource>(elementsToRemove, comparer);
            return source.Where(elem => !elementsToRemoveSet.Contains(elem));
        }

        public static IEnumerable<(T1 left, T2 right)> Zip<T1, T2>(this (IEnumerable<T1>, IEnumerable<T2>) seqs)
        {
            using var iterLeft = seqs.Item1.GetEnumerator();
            using var iterRight = seqs.Item2.GetEnumerator();
            bool leftAdv, rightAdv;
            while ((leftAdv = iterLeft.MoveNext()) & (rightAdv = iterRight.MoveNext()))
                yield return (iterLeft.Current, iterRight.Current);

            if (leftAdv != rightAdv)
                throw new InvalidOperationException("Collections should have the same size");
        }
    }
}
