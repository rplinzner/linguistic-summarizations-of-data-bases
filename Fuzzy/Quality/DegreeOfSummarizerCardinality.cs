using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuzzy.Quality
{
    public class DegreeOfSummarizerCardinality : IDegree
    {
        public List<Summarizer.Summarizer> Summarizers { get; set; }
        public DegreeOfSummarizerCardinality(List<Summarizer.Summarizer> summarizers)
        {
            Summarizers = Summarizers;
        }
        public double Call()
        {
            double result = 1.0;
            foreach (Summarizer.Summarizer summarizer in Summarizers)
            {
                result *= (summarizer.FuzzySet.MembershipFunction.Cardinality() /
                          summarizer.FuzzySet.MembershipFunction.DomainCardinality());
            }
            result = Math.Pow(result, Summarizers.Count);
            return 1.0 - result;
        }
    }
}
