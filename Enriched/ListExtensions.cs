using Enriched.EnumerableExtended;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace Enriched.ListExtended
{
    public static class ListExtensions
    {
        private static readonly Random Rnd = new Random(Guid.NewGuid().GetHashCode());

        public static void AddRange<T>(this IList<T> container, IEnumerable<T> rangeToAdd)
        {
            if ((container == null) || (rangeToAdd == null))
            {
                return;
            }
            foreach (T toAdd in rangeToAdd)
            {
                container.Add(toAdd);
            }
        }

        public static void AddRangeUnique<T>(this IList<T> list, T[] items) where T : class
        {
            foreach (var item in items)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
        }

        public static void AddRangeUnique<T>(this IList<T> list, IEnumerable<T> items) where T : class
        {
            foreach (var item in items)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
        }

        public static void AddRangeUnique<T>(this List<T> list, T[] items) where T : class
        {
            foreach (var item in items)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
        }

        public static void AddRangeUnique<T>(this List<T> list, IEnumerable<T> items) where T : class
        {
            foreach (var item in items)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
        }

        public static void AddToFront<T>(this IList<T> list, T item)
        {
            list.Insert(0, item);
        }

        public static void AddUnique<T>(this IList<T> list, T item) where T : class
        {
            if (!list.Contains(item))
            {
                list.Add(item);
            }
        }

        public static void AddUnique<T>(this List<T> list, T item) where T : class
        {
            if (!list.Contains(item))
            {
                list.Add(item);
            }
        }

        public static bool All<T>(this IList<T> list, Func<T, bool> predicate)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (!predicate(list[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Any<T>(this IList<T> list, Func<T, bool> predicate)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool AnyOrNotNull(this List<string> source)
        {
            var hasData = source.Aggregate((a, b) => a + b).Any();
            if (source != null && source.Any() && hasData)
                return true;
            else
                return false;
        }

        public static int BinarySearch<T>(this IList sortedList, T element, IComparer<T> comparer)
        {
            if (sortedList is null)
                throw new ArgumentNullException(nameof(sortedList));

            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            if (sortedList.Count <= 0)
                return -1;

            int left = 0;
            int right = sortedList.Count - 1;
            while (left <= right)
            {
                // determine the index in the list to compare with. This is the middle of the segment we're searching in.
                int index = left + (right - left) / 2;
                int compareResult = comparer.Compare((T)sortedList[index], element);
                if (compareResult == 0)
                {
                    // found it, done. Return the index
                    return index;
                }
                if (compareResult < 0)
                {
                    // element is bigger than the element at index, so we can skip all elements at the left of index including the element at index.
                    left = index + 1;
                }
                else
                {
                    // element is smaller than the element at index, so we can skip all elements at the right of index including the element at index.
                    right = index - 1;
                }
            }
            return ~left;
        }

        public static List<T> Cast<T>(this IList source)
        {
            var list = new List<T>();
            list.AddRange(source.OfType<T>());
            return list;
        }

        public static T First<T>(this IList<T> list)
        {
            if (list.Count == 0)
            {
                return default;
            }
            else
            {
                return list[0];
            }
        }

        public static T GetRandomItem<T>(this List<T> list)
        {
            var count = list.Count;
            var i = Rnd.Next(0, count);
            return list[i];
        }

        public static IEnumerable<T> GetRandomItems<T>(this List<T> list, int count, bool uniqueItems = true)
        {
            var c = list.Count;
            var l = new List<T>();
            while (true)
            {
                var i = Rnd.Next(0, c);
                if (!l.Contains(list[i]) && uniqueItems)
                {
                    l.Add(list[i]);
                }
                if (!uniqueItems)
                {
                    l.Add(list[i]);
                }
                if (l.Count == count)
                    break;
            }
            return l;
        }

        public static bool HasDuplicates<T>(this IList<T> list)
        {
            var hs = new HashSet<T>();
            for (var i = 0; i < list.Count(); ++i)
            {
                if (!hs.Add(list[i])) return true;
            }
            return false;
        }

        public static int IndexOf<T>(this IList<T> list, Func<T, bool> comparison)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (comparison(list[i]))
                    return i;
            }
            return -1;
        }

        public static int InsertRangeUnique<T>(this IList<T> list, int startIndex, IEnumerable<T> items)
        {
            var index = startIndex + items.Reverse().Count(item => list.InsertUnique(startIndex, item));
            return (index - startIndex);
        }

        public static bool InsertUnique<T>(this IList<T> list, int index, T item)
        {
            if (!list.Contains(item))
            {
                list.Insert(index, item);
                return true;
            }
            return false;
        }

        public static bool IsEmpty(this IList collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            return collection.Count == 0;
        }

        public static bool IsEmpty<T>(this IList<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            return collection.Count == 0;
        }

        public static bool IsFirst<T>(this IList<T> list, T element)
        {
            return list.IndexOf(element) == 0;
        }

        public static bool IsLast<T>(this IList<T> list, T element)
        {
            return list.IndexOf(element) == list.Count - 1;
        }

        public static bool IsNullOrEmpty<T>(this IList<T> toCheck)
        {
            return (toCheck == null) || (toCheck.Count <= 0);
        }

        public static string Join<T>(this IList<T> list, string joinString)
        {
            if (list == null || !list.Any())
                return string.Empty;
            StringBuilder result = new StringBuilder();
            int listCount = list.Count;
            int listCountMinusOne = listCount - 1;
            if (listCount > 1)
            {
                for (var i = 0; i < listCount; i++)
                {
                    if (i != listCountMinusOne)
                    {
                        result.Append(list[i]);
                        result.Append(joinString);
                    }
                    else
                        result.Append(list[i]);
                }
            }
            else
                result.Append(list[0]);
            return result.ToString();
        }

        public static T Last<T>(this IList<T> list)
        {
            if (list.Count == 0)
            {
                return default;
            }
            else
            {
                return list[list.Count - 1];
            }
        }

        public static IList<K> Map<T, K>(this IList<T> list, Func<T, K> function)
        {
            var newList = new List<K>(list.Count);
            for (var i = 0; i < list.Count; ++i)
            {
                newList.Add(function(list[i]));
            }
            return newList;
        }

        public static List<T> Match<T>(this IList<T> list, string searchString, int top, params Expression<Func<T, object>>[] args)
        {
            // Create a new list of results and matches;
            var results = new List<T>();
            var matches = new Dictionary<T, int>();
            var maxMatch = 0;
            list.ForEach(s =>
            {
                // Generate the expression string from the argument.
                var regExp = string.Empty;
                if (args != null)
                {
                    foreach (var arg in args)
                    {
                        var property = arg.Compile();
                        // Attach the new property to the expression string
                        regExp += (string.IsNullOrEmpty(regExp) ? "(?:" : "|(?:") + property(s) + ")+?";
                    }
                }
                // Get the matches
                var match = Regex.Matches(searchString, regExp, RegexOptions.IgnoreCase);
                // If there are more than one match
                if (match.Count > 0)
                {
                    // Add it to the match dictionary, including the match count.
                    matches.Add(s, match.Count);
                }
                // Get the highest max matching
                maxMatch = match.Count > maxMatch ? match.Count : maxMatch;
            });
            // Convert the match dictionary into a list
            var matchList = matches.ToList();
            // Sort the list by decending match counts
            // matchList.Sort((s1, s2) => s2.Value.CompareTo(s1.Value));
            // Remove all matches that is less than the best match.
            matchList.RemoveAll(s => s.Value < maxMatch);
            // If the top value is set and is less than the number of matches
            var getTop = top > 0 && top < matchList.Count ? top : matchList.Count;
            // Add the maches into the result list.
            for (var i = 0; i < getTop; i++)
                results.Add(matchList[i].Key);
            return results;
        }

        public static List<T> Merge<T>(params List<T>[] lists)
        {
            var merged = new List<T>();
            foreach (var list in lists) merged.Merge(list);
            return merged;
        }

        public static List<T> Merge<T>(Expression<Func<T, object>> match, params List<T>[] lists)
        {
            var merged = new List<T>();
            foreach (var list in lists) merged.Merge(list, match);
            return merged;
        }

        public static List<T> Merge<T>(this List<T> list1, List<T> list2, Expression<Func<T, object>> match)
        {
            if (list1 != null && list2 != null && match != null)
            {
                var matchFunc = match.Compile();
                foreach (var item in list2)
                {
                    var key = matchFunc(item);
                    if (!list1.Exists(i => matchFunc(i).Equals(key))) list1.Add(item);
                }
            }
            return list1;
        }

        public static List<T> Merge<T>(this List<T> list1, List<T> list2)
        {
            if (list1 != null && list2 != null) foreach (var item in list2.Where(item => !list1.Contains(item))) list1.Add(item);
            return list1;
        }

        public static T Next<T>(this IList<T> list, ref int Index)
        {
            Index = ++Index >= 0 && Index < list.Count ? Index : 0;
            return list[Index];
        }

        public static T PickOneOf<T>(this IList<T> list)
        {
            var rng = new Random();
            return list[rng.Next(list.Count)];
        }

        public static T Previous<T>(this IList<T> list, ref int Index)
        {
            Index = --Index >= 0 && Index < list.Count ? Index : list.Count - 1;
            return list[Index];
        }

        public static void RemoveFirst<T>(this IList<T> list)
        {
            if (list.Count > 0)
            {
                list.RemoveAt(0);
            }
        }

        public static void RemoveLast<T>(this IList<T> source, int n = 1)
        {
            for (int i = 0; i < n; i++)
            {
                source.RemoveAt(source.Count - 1);
            }
        }

        public static void RemoveLast<T>(this IList<T> list)
        {
            if (list.Count > 0)
            {
                list.RemoveAt(list.Count - 1);
            }
        }

        public static void Replace<T>(this IList<T> @this, T oldValue, T newValue)
        {
            var oldIndex = @this.IndexOf(oldValue);
            while (oldIndex > 0)
            {
                @this.RemoveAt(oldIndex);
                @this.Insert(oldIndex, newValue);
                oldIndex = @this.IndexOf(oldValue);
            }
        }

        public static bool Replace<T>(this IList<T> thisList, int position, T item)
        {
            if (position > thisList.Count - 1)
                return false;
            thisList.RemoveAt(position);
            thisList.Insert(position, item);
            return true;
        }

        public static IEnumerable<T> Replace<T>(this IEnumerable<T> source, T oldValue, T newValue)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            return source.Select(x => EqualityComparer<T>.Default.Equals(x, oldValue) ? newValue : x);
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            var rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this IList<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value));
        }

        public static void Swap<T>(this IList<T> source, int indexA, int indexB)
        {
            if (indexA < 0 || indexA >= source.Count)
            {
                throw new IndexOutOfRangeException("indexA is out of range");
            }
            if (indexB < 0 || indexB >= source.Count)
            {
                throw new IndexOutOfRangeException("indexB is out of range");
            }

            if (indexA == indexB)
            {
                return;
            }

            var tempValue = source[indexA];
            source[indexA] = source[indexB];
            source[indexB] = tempValue;
        }

        public static void SwapValues<T>(this IList<T> source, int indexA, int indexB)
        {
            if ((indexA < 0) || (indexA >= source.Count))
            {
                throw new IndexOutOfRangeException("indexA is out of range");
            }
            if ((indexB < 0) || (indexB >= source.Count))
            {
                throw new IndexOutOfRangeException("indexB is out of range");
            }

            if (indexA == indexB)
            {
                return;
            }

            T tempValue = source[indexA];
            source[indexA] = source[indexB];
            source[indexB] = tempValue;
        }

        public static IEnumerable<T> TakeLast<T>(this IList<T> list, int n)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (list.Count - n < 0)
            {
                n = list.Count;
            }

            for (var i = list.Count - n; i < list.Count; i++)
            {
                yield return list[i];
            }
        }
    }
}