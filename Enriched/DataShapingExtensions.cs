using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Enriched.DataShaping
{
    public static class DataShapingExtensions
    {
        private static IEnumerable<PropertyInfo> ExtractSelectedPropertiesInfo<T>(string fields, List<PropertyInfo> propertyInfoList, bool ignoreCase)
        {
            var fieldsAfterSplit = fields.Split(',');

            foreach (var propertyName in fieldsAfterSplit.Select(f => f.Trim()))
            {
                var propName = ignoreCase ? propertyName.ToLower() : propertyName;
                var propertyInfo = typeof(T).GetRuntimeProperties().FirstOrDefault(x => (ignoreCase ? x.Name.ToLower() : x.Name) == propName);

                if (propertyInfo == null)
                {
                    continue;
                }

                propertyInfoList.Add(propertyInfo);
            }

            return propertyInfoList;
        }

        private static void FillDictionary<T>(this IDictionary<string, object> dictionary, T source, IEnumerable<PropertyInfo> fields, Func<T, string, object, object> converter = null)
        {
            foreach (var propertyInfo in fields)
            {
                var propertyValue = propertyInfo.GetValue(source);

                var value = converter != null ? converter(source, propertyInfo.Name, propertyValue) : propertyValue;

                dictionary.Add(propertyInfo.Name, value);
            }
        }

        private static IEnumerable<PropertyInfo> GetPropertyInfos<T>(string fields, bool ignoreCase)
        {
            var propertyInfoList = new List<PropertyInfo>();

            if (!string.IsNullOrWhiteSpace(fields))
            {
                return ExtractSelectedPropertiesInfo<T>(fields, propertyInfoList, ignoreCase);
            }

            var propertyInfos = typeof(T).GetRuntimeProperties();
            propertyInfoList.AddRange(propertyInfos);
            return propertyInfoList;
        }

        // T, string, object, object
        // CurrentTypeInstance , CurrentPropertyName, CurrentPropertyValue, ConvertedValue
        public static IEnumerable<DataField> ToShapedData<T>(this T dataToShape, string fields, bool ignoreCase = true, Func<T, string, object, object> converter = null)
        {
            var result = new Dictionary<string, object>();
            if (dataToShape == null)
            {
                return null;
            }

            var propertyInfoList = GetPropertyInfos<T>(fields, ignoreCase);

            result.FillDictionary(dataToShape, propertyInfoList, converter);

            return result.Select(x => new DataField() { Key = x.Key, Value = x.Value });
        }

        // T, string, object, object
        // CurrentTypeInstance , CurrentPropertyName, CurrentPropertyValue, ConvertedValue
        public static ShapedData ToShapedData<T>(this IEnumerable<T> dataToShape, string fields, bool ignoreCase = true, Func<T, string, object, object> converter = null)
        {
            var result = new Dictionary<string, object>();
            if (dataToShape == null)
            {
                return null;
            }

            var propertyInfoList = GetPropertyInfos<T>(fields, ignoreCase);

            var list = dataToShape.Select(line =>
            {
                var data = new Dictionary<string, object>();
                data.FillDictionary(line, propertyInfoList, converter);
                return data;
            }).Where(d => d.Keys.Count > 0).ToList();

            var dsh = new ShapedData();

            var values = new List<List<DataField>>();
            foreach (var item in list)
            {
                var records = item.Select(x => new DataField() { Key = x.Key, Value = x.Value }).ToList();
                values.Add(records);
            }

            dsh.Values.AddRange(values);

            return dsh;
        }
    }
}