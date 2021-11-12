using Enriched.StreamExtended;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Enriched.ByteArrayExtended
{
    public static class ByteArrayExtensions
    {
        public static T ConvertTo<T>(this byte[] data)
        {
            if (data == null)
                return default;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                object obj = bf.Deserialize(ms);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
                return (T)obj;
            }
        }

        public static T Deserialize<T>(this byte[] data) where T : class
        {
            var formatter = new BinaryFormatter();
            using (var ms = new MemoryStream(data))
            {
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                return (T)formatter.Deserialize(ms);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
            }
        }

        public static int FindArrayInArray(this byte[] array1, byte[] array2)
        {
            if (array2 == null)
                throw new ArgumentNullException(nameof(array2));

            if (array1 == null)
                throw new ArgumentNullException(nameof(array1));

            if (array2.Length == 0)
                return 0;

            var j = -1;
            var end = array1.Length - array2.Length;
            while ((j = Array.IndexOf(array1, array2[0], j + 1)) <= end && j != -1)
            {
                var i = 1;
                while (array1[j + i] == array2[i])
                {
                    if (++i == array2.Length)
                        return j;
                }
            }

            return -1;
        }

        public static byte[] Resize(this byte[] @this, int newSize)
        {
            Array.Resize(ref @this, newSize);
            return @this;
        }

        public static int ToBase64CharArray(this byte[] inArray, int offsetIn, int length, char[] outArray,
                                    int offsetOut)
        {
            return Convert.ToBase64CharArray(inArray, offsetIn, length, outArray, offsetOut);
        }

        public static int ToBase64CharArray(this byte[] inArray, int offsetIn, int length, char[] outArray,
                                    int offsetOut, Base64FormattingOptions options)
        {
            return Convert.ToBase64CharArray(inArray, offsetIn, length, outArray, offsetOut, options);
        }

        public static string ToBase64String(this byte[] inArray)
        {
            return Convert.ToBase64String(inArray);
        }

        public static string ToBase64String(this byte[] inArray, Base64FormattingOptions options)
        {
            return Convert.ToBase64String(inArray, options);
        }

        public static string ToBase64String(this byte[] inArray, int offset, int length)
        {
            return Convert.ToBase64String(inArray, offset, length);
        }

        public static string ToBase64String(this byte[] inArray, int offset, int length,
                                    Base64FormattingOptions options)
        {
            return Convert.ToBase64String(inArray, offset, length, options);
        }

        public static FileStream ToFileStream(this byte[] @this, string filePath, FileMode fileMode = FileMode.Create,
                                    FileAccess fileAccess = FileAccess.ReadWrite)
        {
            return @this.ToStream().ToFileStream(filePath, fileMode, fileAccess);
        }

        public static string ToHexString(this byte[] byteArray)
        {
            string result = string.Empty;
            foreach (byte outputByte in byteArray)
            {
                result += outputByte.ToString("x2");
            }

            return result;
        }

        public static MemoryStream ToMemoryStream(this byte[] @this)
        {
            return new MemoryStream(@this);
        }

        public static Stream ToStream(this byte[] @this)
        {
            return new MemoryStream(@this);
        }

        public static string ToText(this byte[] bytes, EncodingType encodingType = EncodingType.UTF8)
        {
            return encodingType switch
            {
#pragma warning disable SYSLIB0001 // Type or member is obsolete
                EncodingType.UTF7 => Encoding.UTF7.GetString(bytes),
#pragma warning restore SYSLIB0001 // Type or member is obsolete
                EncodingType.BigEndianUnicode => Encoding.BigEndianUnicode.GetString(bytes),
                EncodingType.Unicode => Encoding.Unicode.GetString(bytes),
                EncodingType.ASCII => Encoding.ASCII.GetString(bytes),
                EncodingType.UTF8 => Encoding.UTF8.GetString(bytes),
                EncodingType.UTF32 => Encoding.UTF32.GetString(bytes),
                EncodingType.Default => Encoding.Default.GetString(bytes),
                _ => Encoding.Default.GetString(bytes),
            };
        }

        public static string ToText(this byte[] bytes, int index,
                            int count, EncodingType encodingType = EncodingType.UTF8)
        {
            return encodingType switch
            {
#pragma warning disable SYSLIB0001 // Type or member is obsolete
                EncodingType.UTF7 => Encoding.UTF7.GetString(bytes, index, count),
#pragma warning restore SYSLIB0001 // Type or member is obsolete
                EncodingType.BigEndianUnicode => Encoding.BigEndianUnicode.GetString(bytes, index, count),
                EncodingType.Unicode => Encoding.Unicode.GetString(bytes, index, count),
                EncodingType.ASCII => Encoding.ASCII.GetString(bytes, index, count),
                EncodingType.UTF8 => Encoding.UTF8.GetString(bytes, index, count),
                EncodingType.UTF32 => Encoding.UTF32.GetString(bytes, index, count),
                EncodingType.Default => Encoding.Default.GetString(bytes, index, count),
                _ => Encoding.Default.GetString(bytes, index, count),
            };
        }
    }
}