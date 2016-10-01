using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TextStatistics.Core
{
    /// <summary>
    /// Provides extension methods for generating the Tversky Index.
    /// Tversky index is a generalization of Jaccard index and Dice coefficient.
    /// When hyperparameters equal '1', it returns Jaccard coefficient.
    /// When hyperparameters equal '0.5', it returns dice coefficient.
    /// </summary>
    public static class TverskyIndex
    {
        public static double GetTverskyIndexFor<T>(this ISet<T> source, ISet<T> shingles, double alpha = 0.5, double beta = 0.55)
        {
            int setIntersectionSize = source.Intersect(shingles).Count();
            int leftComplement = source.Except(shingles).Count();
            int rightComplement = shingles.Except(source).Count();

            double divideBy = setIntersectionSize + alpha*leftComplement + beta*rightComplement;
            if (divideBy.Equals(0.0))
                return 0;

            return setIntersectionSize/divideBy;
        }

    }
}
