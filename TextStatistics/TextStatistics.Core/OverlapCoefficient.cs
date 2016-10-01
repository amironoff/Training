using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TextStatistics.Core
{
    /// <summary>
    /// Provides extension methods for generating overlap coefficient
    /// for two strings. This method performs better on substrings.
    /// </summary>
    public static class OverlapCoefficient
    {
        public static double GetOverlapCoefficientWith<T>(this ISet<T> source, ISet<T> shingles)
        {
            int minLength = Math.Min(source.Count, shingles.Count);
            IEnumerable<T> setIntersection = source.Intersect(shingles);
            int intersectionLength = setIntersection.Count();

            if (intersectionLength.Equals(0) || minLength.Equals(0))
                return 0;

            return intersectionLength / (double)minLength;
        }

    }
}
