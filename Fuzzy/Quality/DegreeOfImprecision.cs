using System;
using System.Collections.Generic;
namespace Fuzzy.Quality
{
    public class DegreeOfImprecision : IDegree
    {
        public List<Summarizer.Summarizer> Summarizers { get; set; }

        public DegreeOfImprecision(List<Summarizer.Summarizer> summarizers)
        {
            Summarizers = summarizers;
        }
        public double Call()
        {
            double result = 1;
            foreach (Summarizer.Summarizer summarizer in Summarizers)
            {
                result *= summarizer.FuzzySet.DegreeOfFuzziness();
            }

            result = Math.Pow(result, 1.0 / Summarizers.Count);
            return 1.0 - result;
        }
    }
}
