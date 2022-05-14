using Enriched.ByteArrayExtended;
using Enriched.StreamExtended;
using Enriched.StringExtended;
using System.IO;
using System.Security.Cryptography;

namespace Enriched.Cryptography
{
    public static partial class CryptoExtensions
    {
        public static byte[] ToPBKDF2(this string data, string salt = "", HashAlgorithm hashAlgorithm = HashAlgorithm.SHA256, int hashSize = 24, int iterations = 10000)
        {
            byte[] _salt;
            if (string.IsNullOrEmpty(salt))
            {
                var provider = new RNGCryptoServiceProvider();
                _salt = new byte[24];
                provider.GetBytes(_salt);
            }
            else
            {
                _salt = salt.ToByteArray();
            }

            var hashAlg = hashAlgorithm switch
            {
                HashAlgorithm.MD5 => HashAlgorithmName.MD5,
                HashAlgorithm.SHA1 => HashAlgorithmName.SHA1,
                HashAlgorithm.SHA256 => HashAlgorithmName.SHA256,
                HashAlgorithm.SHA384 => HashAlgorithmName.SHA384,
                HashAlgorithm.SHA512 => HashAlgorithmName.SHA512,
                _ => HashAlgorithmName.SHA256,
            };
            using var deriveBytes = new Rfc2898DeriveBytes(data, _salt, iterations, hashAlg);
            return deriveBytes.GetBytes(hashSize);
        }

        public static byte[] ToPBKDF2(this Stream data, string salt = "", HashAlgorithm hashAlgorithm = HashAlgorithm.SHA256, int hashSize = 24, int iterations = 10000)
        {
            return ToPBKDF2(data.ToText(), salt, hashAlgorithm, hashSize, iterations);
        }

        public static byte[] ToPBKDF2(this byte[] data, string salt = "", HashAlgorithm hashAlgorithm = HashAlgorithm.SHA256, int hashSize = 24, int iterations = 10000)
        {
            return ToPBKDF2(data.ToText(), salt, hashAlgorithm, hashSize, iterations);
        }

    }
}