using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TextStatistics.Core
{
    /// <summary>
    /// Provides extension methods for generating Sorensen-Dice coefficient.
    /// </summary>
    public static class SorensenDiceCoefficient
    {
        public static double GetSorensenDiceCoefficientFor<T>(this ISet<T> source, ISet<T> shingles)
        {
            int intersectionLength = source.Intersect(shingles).Count();
            int combinedSetSize = source.Count + shingles.Count;

            return (2*intersectionLength)/(double)combinedSetSize;
        }

    }
}
