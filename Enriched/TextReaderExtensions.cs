using System;
using System.Collections.Generic;
using System.IO;

namespace Enriched.TextReaderExtended
{
    public static class TextReaderExtensions
    {
        public static IEnumerable<string> ReadLines(this TextReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
                yield return line;
        }

        public static void ReadLines(this TextReader reader, Action<string> action)
        {
            foreach (var line in reader.ReadLines())
                action(line);
        }
    }
}