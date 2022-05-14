using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Enriched.UriExtensions
{
    public static class UriExtensions
    {
        public static string ReadAsString(this Uri uri)
        {
            var httpClient = new HttpClient();

            var response = httpClient.GetAsync(uri).Result;
            var content = response.Content.ReadAsStream();
            var reader = new StreamReader(content);
            var text = reader.ReadToEnd();

            return text;
        }

        public static Stream ReadAsStream(this Uri uri)
        {
            var httpClient = new HttpClient();

            var response = httpClient.GetAsync(uri).Result;
            var content = response.Content.ReadAsStream();

            return content;
        }

        public static byte[] ReadAsByteArray(this Uri uri)
        {
            var httpClient = new HttpClient();

            var response = httpClient.GetAsync(uri).Result;
            var content = response.Content.ReadAsStream();

            using var memoryStream = new MemoryStream();
            content.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }

        public static async Task<byte[]> ReadAsByteArrayAsync(this Uri uri, CancellationToken cancellationToken = default)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(uri, cancellationToken);
            var content = await response.Content.ReadAsByteArrayAsync(cancellationToken);

            return content;
        }

        public static async Task<Stream> ReadAsStreamAsync(this Uri uri, CancellationToken cancellationToken = default)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(uri, cancellationToken);
            var content = await response.Content.ReadAsStreamAsync(cancellationToken);

            return content;
        }

        public static async Task<string> ReadAsStringAsync(this Uri uri, CancellationToken cancellationToken = default)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(uri, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            return content;
        }
    }
}