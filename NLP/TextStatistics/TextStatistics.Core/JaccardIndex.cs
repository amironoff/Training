using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextStatistics.Core
{
    /// <summary>
    /// Provides extension methods for generating Jaccard index and related metrics
    /// out of source strings.
    /// </summary>
    public static class JaccardIndex
    {
        public static double GetJaccardIndexFor<T>(this ISet<T> source, ISet<T> shingles)
        {
            int unionSize = source.Union(shingles).Count();
            if (unionSize.Equals(0) || unionSize.Equals(source.Count))
            {
                return 1.0;
            }

            int intersectSize = source.Intersect(shingles).Count();

            return intersectSize/(double) unionSize;
        }

        public static double GetJaccardDistanceFrom<T>(this ISet<T> source, ISet<T> shingles)
        {
            return 1.0 - source.GetJaccardIndexFor(shingles);
        }
    }
}
