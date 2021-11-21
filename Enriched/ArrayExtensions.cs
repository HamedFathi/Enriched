using Enriched.EnumerableExtended;
using Enriched.IntExtended;
using Enriched.ObjectExtended;
using Enriched.StringExtended;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Enriched.ArrayExtended
{
    public static class ArrayExtensions
    {
        public static bool All<T>(this T[] array, Func<T, bool> predicate)
        {
            foreach (var item in array)
            {
                if (!predicate(item))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Any<T>(this T[] array, Func<T, bool> predicate)
        {
            foreach (var item in array)
            {
                if (predicate(item))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool AllSafe<T>(this T[] array, Func<T, bool> predicate)
        {
            return array?.All(predicate) == true;
        }

        public static bool AnySafe<T>(this T[] array, Func<T, bool> predicate)
        {
            return array?.Any(predicate) == true;
        }

        public static bool AllSafe<T>(this T[] array)
        {
            return array?.All() == true;
        }

        public static bool AnySafe<T>(this T[] array)
        {
            return array?.Any() == true;
        }

        public static void BlockCopy(this Array src, int srcOffset, Array dst, int dstOffset, int count)
        {
            Buffer.BlockCopy(src, srcOffset, dst, dstOffset, count);
        }

        public static T[] BlockCopy<T>(this T[] array, int index, int length)
        {
            return BlockCopy(array, index, length, false);
        }

        public static T[] BlockCopy<T>(this T[] array, int index, int length, bool padToLength)
        {
            if (array == null) throw new NullReferenceException();
            int n = length;
            T[] b = null;
            if (array.Length < index + length)
            {
                n = array.Length - index;
                if (padToLength)
                {
                    b = new T[length];
                }
            }
            if (b == null) b = new T[n];
            Array.Copy(array, index, b, 0, n);
            return b;
        }

        public static IEnumerable<T[]> BlockCopy<T>(this T[] array, int count, bool padToLength = false)
        {
            for (int i = 0; i < array.Length; i += count)
                yield return array.BlockCopy(i, count, padToLength);
        }

        public static int ByteLength(this Array array)
        {
            return Buffer.ByteLength(array);
        }

        public static void Clear(this Array array, int index, int length)
        {
            Array.Clear(array, index, length);
        }

        public static T[] ClearAll<T>(this T[] arrayToClear)
        {
            if (arrayToClear != null)
                for (var i = arrayToClear.GetLowerBound(0); i <= arrayToClear.GetUpperBound(0); ++i)
                    arrayToClear[i] = default;
            return arrayToClear;
        }

        public static void ClearAll(this Array @this)
        {
            Array.Clear(@this, 0, @this.Length);
        }

        public static Array ClearAt(this Array arrayToClear, int at)
        {
            if (arrayToClear != null)
            {
                int arrayIndex = at.GetArrayIndex();
                if (arrayIndex.IsIndexInArray(arrayToClear))
                    Array.Clear(arrayToClear, arrayIndex, 1);
            }
            return arrayToClear;
        }

        public static T[] ClearAt<T>(this T[] arrayToClear, int at)
        {
            if (arrayToClear != null)
            {
                int arrayIndex = at.GetArrayIndex();
                if (arrayIndex.IsIndexInArray(arrayToClear))
                    arrayToClear[arrayIndex] = default;
            }
            return arrayToClear;
        }

        public static T[] CombineArray<T>(this T[] combineWith, T[] arrayToCombine)
        {
            if (combineWith != default(T[]) && arrayToCombine != default(T[]))
            {
                var initialSize = combineWith.Length;
                Array.Resize(ref combineWith, initialSize + arrayToCombine.Length);
                Array.Copy(arrayToCombine, arrayToCombine.GetLowerBound(0), combineWith, initialSize, arrayToCombine.Length);
            }
            return combineWith;
        }

        public static void ConstrainedCopy(this Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
        {
            Array.ConstrainedCopy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
        }

        public static bool ContainsIgnoreCase(this string[] source, string str)
        {
            if (string.IsNullOrEmpty(str) || source == null || source.Length == 0)
                return false;
            foreach (var item in source)
            {
                return item.ContainsIgnoreCase(str);
            }
            return false;
        }

        public static bool ContainsIndex<T>(this T[] @this, int index)
        {
            return index >= 0 && index < @this.Length;
        }

        public static bool ContainsIndex(this Array @this, int index)
        {
            return index >= 0 && index < @this.Length;
        }

        public static void Copy(this Array sourceArray, Array destinationArray, int length)
        {
            Array.Copy(sourceArray, destinationArray, length);
        }

        public static void Copy(this Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
        {
            Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
        }

        public static void Copy(this Array sourceArray, Array destinationArray, long length)
        {
            Array.Copy(sourceArray, destinationArray, length);
        }

        public static void Copy(this Array sourceArray, long sourceIndex, Array destinationArray, long destinationIndex, long length)
        {
            Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
        }

        public static bool Exactly<T>(this T[] source, int count)
        {
            return source.Count() == count;
        }

        public static bool Exactly<T>(this T[] source, Func<T, bool> query, int count)
        {
            return source.Count(query) == count;
        }

        public static bool Exists<T>(this T[] array, Predicate<T> match)
        {
            return Array.Exists(array, match);
        }

        public static T Find<T>(this T[] array, Predicate<T> match)
        {
            return Array.Find(array, match);
        }

        public static T[] FindAll<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindAll(array, match);
        }

        public static int FindIndex<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindIndex(array, match);
        }

        public static int FindIndex<T>(this T[] array, int startIndex, Predicate<T> match)
        {
            return Array.FindIndex(array, startIndex, match);
        }

        public static int FindIndex<T>(this T[] array, int startIndex, int count, Predicate<T> match)
        {
            return Array.FindIndex(array, startIndex, count, match);
        }

        public static T FindLast<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindLast(array, match);
        }

        public static int FindLastIndex<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindLastIndex(array, match);
        }

        public static int FindLastIndex<T>(this T[] array, int startIndex, Predicate<T> match)
        {
            return Array.FindLastIndex(array, startIndex, match);
        }

        public static int FindLastIndex<T>(this T[] array, int startIndex, int count, Predicate<T> match)
        {
            return Array.FindLastIndex(array, startIndex, count, match);
        }

        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            foreach (var item in array)
            {
                action(item);
            }
        }

        public static void ForEach<T>(this T[] array, Action<T> action, Func<T, bool> predicate)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            foreach (var item in array.Where(predicate))
                action(item);
        }

        public static void ForEachReverse<T>(this T[] source, Action<T> action)
        {
            var items = source.ToList();
            for (var i = items.Count - 1; i >= 0; i--)
            {
                action(items[i]);
            }
        }

        public static void ForEachReverse<T>(this T[] source, Action<T> action, Func<T, bool> predicate)
        {
            var items = source.ToList();
            for (var i = items.Count - 1; i >= 0; i--)
            {
                if (predicate(items[i]))
                    action(items[i]);
            }
        }

        public static byte GetByte(this Array array, int index)
        {
            return Buffer.GetByte(array, index);
        }

        public static T[] GetDuplicates<T>(this T[] @this)
        {
            var hashset = new HashSet<T>();
            var duplicates = @this.Where(e => !hashset.Add(e));
            return duplicates.ToArray();
        }

        public static int IndexOf(this Array array, object value)
        {
            return Array.IndexOf(array, value);
        }

        public static int IndexOf(this Array array, object value, int startIndex)
        {
            return Array.IndexOf(array, value, startIndex);
        }

        public static int IndexOf(this Array array, object value, int startIndex, int count)
        {
            return Array.IndexOf(array, value, startIndex, count);
        }

        public static bool IsEmpty(this Array array)
        {
            if (array.IsNull())
            {
                throw new ArgumentNullException(nameof(array));
            }
            return array.Length == 0;
        }

        public static bool IsNullOrEmpty(this Array source)
        {
            return source == null || source.Length == 0;
        }

        public static string JoinNotNullOrEmpty(this string[] objs, string separator)
        {
            var items = new List<string>();
            foreach (var s in objs)
            {
                if (!string.IsNullOrEmpty(s))
                    items.Add(s);
            }
            return string.Join(separator, items.ToArray());
        }

        public static int LastIndexOf(this Array array, object value)
        {
            return Array.LastIndexOf(array, value);
        }

        public static int LastIndexOf(this Array array, object value, int startIndex)
        {
            return Array.LastIndexOf(array, value, startIndex);
        }

        public static int LastIndexOf(this Array array, object value, int startIndex, int count)
        {
            return Array.LastIndexOf(array, value, startIndex, count);
        }

        public static IEnumerable<T> Perform<T>(this T[] array, Func<T, T> func)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            foreach (var item in array)
            {
                yield return func(item);
            }
        }

        public static T PickOneOf<T>(this T[] array)
        {
            var rng = new Random();
            return array[rng.Next(array.Length)];
        }

        public static T[] Remove<T>(this T[] source, T item)
        {
            var result = new T[source.Length - source.Count(s => s.Equals(item))];
            var x = 0;
            foreach (var i in source.Where(i => !Equals(i, item)))
            {
                result[x] = i;
                x++;
            }
            return result;
        }

        public static T[] Remove<T>(this T[] array, Func<T, bool> condition)
        {
            var list = new List<T>();
            foreach (var item in array)
            {
                if (!condition(item))
                {
                    list.Add(item);
                }
            }
            return list.ToArray();
        }

        public static T[] RemoveAll<T>(this T[] source, Predicate<T> predicate)
        {
            var result = new T[source.Length - source.Count(s => predicate(s))];
            var i = 0;
            foreach (var item in source.Where(item => !predicate(item)))
            {
                result[i] = item;
                i++;
            }
            return result;
        }

        public static T[] RemoveAt<T>(this T[] source, int index)
        {
            var result = new T[source.Length - 1];
            var x = 0;
            for (var i = 0; i < source.Length; i++)
            {
                if (i == index) continue;
                result[x] = source[i];
                x++;
            }
            return result;
        }

        public static T[] RemoveDuplicates<T>(this T[] @this)
        {
            return new HashSet<T>(@this).ToArray();
        }

        public static T[] RemoveDuplicates<T>(this T[] list, Func<T, int> predicate)
        {
            var dict = new Dictionary<int, T>();

            foreach (var item in list)
            {
                if (!dict.ContainsKey(predicate(item)))
                {
                    dict.Add(predicate(item), item);
                }
            }

            return dict.Values.ToArray();
        }

        public static string[] RemoveEmptyElements(this string[] array)
        {
            if (array == null)
                return null;
            var arr = array.Where(str => !string.IsNullOrEmpty(str)).ToArray();
            if (arr.Length == 0)
                return null;
            return arr;
        }

        public static void Reverse<T>(this T[] array)
        {
            Array.Reverse(array);
        }

        public static void Reverse(this Array array)
        {
            Array.Reverse(array);
        }

        public static void Reverse(this Array array, int index, int length)
        {
            Array.Reverse(array, index, length);
        }

        public static void Reverse<T>(this T[] array, int index, int length)
        {
            Array.Reverse(array, index, length);
        }

        public static void SetByte(this Array array, int index, byte value)
        {
            Buffer.SetByte(array, index, value);
        }

        public static void Sort<T>(this T[] array)
        {
            Array.Sort(array);
        }

        public static void Sort<T>(this T[] array, Comparison<T> comparison)
        {
            Array.Sort(array, comparison);
        }

        public static void Sort<T>(this T[] array, IComparer<T> comparer)
        {
            Array.Sort(array, comparer);
        }

        public static void Sort<T>(this T[] array, int index, int length)
        {
            Array.Sort(array, index, length);
        }

        public static void Sort<T>(this T[] array, int index, int length, IComparer<T> comparer)
        {
            Array.Sort(array, index, length, comparer);
        }

        public static void Sort(this Array array)
        {
            Array.Sort(array);
        }

        public static void Sort(this Array array, Array items)
        {
            Array.Sort(array, items);
        }

        public static void Sort(this Array array, int index, int length)
        {
            Array.Sort(array, index, length);
        }

        public static void Sort(this Array array, Array items, int index, int length)
        {
            Array.Sort(array, items, index, length);
        }

        public static void Sort(this Array array, IComparer comparer)
        {
            Array.Sort(array, comparer);
        }

        public static void Sort(this Array array, Array items, IComparer comparer)
        {
            Array.Sort(array, items, comparer);
        }

        public static void Sort(this Array array, int index, int length, IComparer comparer)
        {
            Array.Sort(array, index, length, comparer);
        }

        public static void Sort(this Array array, Array items, int index, int length, IComparer comparer)
        {
            Array.Sort(array, items, index, length, comparer);
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this T[] source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value));
        }

        public static double StdDev(this int[] source)
        {
            return source.AsEnumerable().StdDev();
        }

        public static double StdDev(this double[] source)
        {
            return source.AsEnumerable().StdDev();
        }

        public static float StdDev(this float[] source)
        {
            return source.AsEnumerable().StdDev();
        }

        public static double StdDevP(this int[] source)
        {
            return source.AsEnumerable().StdDevP();
        }

        public static double StdDevP(this double[] source)
        {
            return source.AsEnumerable().StdDevP();
        }

        public static double StdDevP(this float[] source)
        {
            return source.AsEnumerable().StdDevP();
        }

        public static DataTable ToDataTable<T>(this T[] @this)
        {
            var type = typeof(T);

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            var dt = new DataTable();

            foreach (var property in properties)
            {
                dt.Columns.Add(property.Name, property.PropertyType);
            }

            foreach (var field in fields)
            {
                dt.Columns.Add(field.Name, field.FieldType);
            }

            foreach (var item in @this)
            {
                var dr = dt.NewRow();

                foreach (var property in properties)
                {
                    dr[property.Name] = property.GetValue(item, null);
                }

                foreach (var field in fields)
                {
                    dr[field.Name] = field.GetValue(item);
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static List<T> ToList<T>(this Array items, Func<object, T> mapFunction)
        {
            if (items == null || mapFunction == null)
                return new List<T>();
            var col = new List<T>();
            for (int i = 0; i < items.Length; i++)
            {
                T val = mapFunction(items.GetValue(i));
                if (val != null)
                    col.Add(val);
            }
            return col;
        }

        public static List<T> ToList<T>(this Array items)
        {
            var list = new List<T>();
            for (int i = 0; i < items.Length; i++)
            {
                T val = (T)items.GetValue(i);
                if (val != null)
                    list.Add(val);
            }
            return list;
        }

        public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this T[] array)
        {
            return Array.AsReadOnly(array);
        }

        public static bool TrueForAll<T>(this T[] array, Predicate<T> match)
        {
            return Array.TrueForAll(array, match);
        }

        public static IEnumerable<(T item, int index)> WithIndex<T>(this T[] source)
        {
            return source.Select((item, index) => (item, index));
        }

        public static bool WithinIndex(this Array @this, int index)
        {
            return index >= 0 && index < @this.Length;
        }

        public static bool WithinIndex(this Array @this, int index, int dimension)
        {
            var d = 0;
            if (dimension > 0)
                d = dimension;
            return index >= @this.GetLowerBound(d) && index <= @this.GetUpperBound(d);
        }
    }
}