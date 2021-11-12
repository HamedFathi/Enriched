using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Enriched.EnumExtended
{
    public static class EnumExtensions
    {
        public static bool ContainsFlagValue(this Enum e, string flagValue)
        {
            var enumType = e.GetType();

            if (Enum.IsDefined(enumType, flagValue))
            {
                var intEnumValue = Convert.ToInt32(e);
                var intFlagValue = (int)Enum.Parse(enumType, flagValue);

                return (intFlagValue & intEnumValue) == intFlagValue;
            }
            else
            {
                return false;
            }
        }

        public static bool ContainsFlagValue(this Enum e, Enum flagValue)
        {
            if (Enum.IsDefined(e.GetType(), flagValue))
            {
                var intFlagValue = Convert.ToInt32(flagValue);

                return (intFlagValue & Convert.ToInt32(e)) == intFlagValue;
            }
            else
            {
                return false;
            }
        }

        public static string GetDescription(this Enum @enum, bool replaceNullWithEnumName = false)
        {
            return
                @enum
                    .GetType()
                    .GetMember(@enum.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description
                ?? (replaceNullWithEnumName ? null : @enum.ToString());
        }

        public static string GetName(this Enum value)
        {
            return value.ToString();
        }

        public static int GetValue(this Enum value)
        {
            return Convert.ToInt32(value);
        }

        public static bool HasFlags<TEnum>(this TEnum @this, params TEnum[] flags)
                            where TEnum : Enum
        {
            foreach (var flag in flags)
            {
                if (!Enum.IsDefined(typeof(TEnum), flag))
                    return false;

                var numFlag = Convert.ToUInt64(flag);
                if ((Convert.ToUInt64(@this) & numFlag) != numFlag)
                    return false;
            }

            return true;
        }

        public static bool In(this Enum @this, params Enum[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }

        public static bool NotIn(this Enum @this, params Enum[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
    }
}