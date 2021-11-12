using System;
using System.Collections.Generic;

namespace Enriched.DbTypeExtended
{
    public static class DbTypeExtensions
    {
        public static Type ToType(this System.Data.DbType dbType)
        {
            var typeMap = new Dictionary<System.Data.DbType, Type>
            {
                [System.Data.DbType.Byte] = typeof(byte),
                [System.Data.DbType.SByte] = typeof(sbyte),
                [System.Data.DbType.Int16] = typeof(short),
                [System.Data.DbType.UInt16] = typeof(ushort),
                [System.Data.DbType.Int32] = typeof(int),
                [System.Data.DbType.UInt32] = typeof(uint),
                [System.Data.DbType.Int64] = typeof(long),
                [System.Data.DbType.UInt64] = typeof(ulong),
                [System.Data.DbType.Single] = typeof(float),
                [System.Data.DbType.Double] = typeof(double),
                [System.Data.DbType.Decimal] = typeof(decimal),
                [System.Data.DbType.Boolean] = typeof(bool),
                [System.Data.DbType.String] = typeof(string),
                [System.Data.DbType.StringFixedLength] = typeof(char),
                [System.Data.DbType.Guid] = typeof(Guid),
                [System.Data.DbType.DateTime] = typeof(DateTime),
                [System.Data.DbType.DateTimeOffset] = typeof(DateTimeOffset),
                [System.Data.DbType.Binary] = typeof(byte[]),
                [System.Data.DbType.Byte] = typeof(byte?),
                [System.Data.DbType.SByte] = typeof(sbyte?),
                [System.Data.DbType.Int16] = typeof(short?),
                [System.Data.DbType.UInt16] = typeof(ushort?),
                [System.Data.DbType.Int32] = typeof(int?),
                [System.Data.DbType.UInt32] = typeof(uint?),
                [System.Data.DbType.Int64] = typeof(long?),
                [System.Data.DbType.UInt64] = typeof(ulong?),
                [System.Data.DbType.Single] = typeof(float?),
                [System.Data.DbType.Double] = typeof(double?),
                [System.Data.DbType.Decimal] = typeof(decimal?),
                [System.Data.DbType.Boolean] = typeof(bool?),
                [System.Data.DbType.StringFixedLength] = typeof(char?),
                [System.Data.DbType.Guid] = typeof(Guid?),
                [System.Data.DbType.DateTime] = typeof(DateTime?),
                [System.Data.DbType.DateTimeOffset] = typeof(DateTimeOffset?)
                // ,[System.Data.DbType.Binary] = typeof(System.Data.Linq.Binary)
            };
            return typeMap[dbType];
        }
    }
}