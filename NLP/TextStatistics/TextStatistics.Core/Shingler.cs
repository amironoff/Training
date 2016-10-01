using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextStatistics.Core
{
    /// <summary>
    /// Provides extension methods for generating
    /// character and term shingles out of a source string.
    /// </summary>
    public static class Shingler
    {
        public static HashSet<string> ToCharacterShingles
            (this string source,
            int shingleSize = 2,
            int shingleOverlap = 1)
        {
            if (shingleOverlap >= shingleSize) 
                throw new ArgumentException("Shingle overlap cannot be bigger than the shingle size");
            var returnValue = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            if (!String.IsNullOrWhiteSpace(source))
            {
                int loopCount = source.Length - shingleSize + 1;
                for (int i = 0; i < loopCount; i = i + shingleSize - shingleOverlap)
                    returnValue.Add(source.Substring(i, shingleSize));
            }

            return returnValue;

        }

        public static HashSet<string> ToTermShingles
            (this string source,
            int shingleSize = 2,
            int shingleOverlap = 1)
        {
            if (shingleOverlap >= shingleSize)
                throw new ArgumentException("Shingle overlap cannot be bigger than the shingle size");
            var returnValue = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            if (!String.IsNullOrWhiteSpace(source))
            {
                // just for the demo purposes. This would require more comprehensive
                // and language-specific tokenization in the real world.
                char[] tokenMarkers = new char[] {'.', ',', ' ', '!', '?'};
                string[] sourceTokens = source.Split(tokenMarkers,
                    StringSplitOptions.RemoveEmptyEntries);

                int loopCount = sourceTokens.Length - shingleSize + 1;
                for (int i = 0; i < loopCount; i = i + shingleSize - shingleOverlap)
                    returnValue.Add(String.Join(" ", sourceTokens, i, shingleSize));
            }

            return returnValue;

        } 
    }
}
