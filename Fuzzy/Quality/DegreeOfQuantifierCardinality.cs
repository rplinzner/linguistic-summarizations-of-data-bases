using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuzzy.Summarizer;

namespace Fuzzy.Quality
{
    public class DegreeOfQuantifierCardinality : IDegree
    {
        public Quantifier Quantifier { get; set; }
        public DegreeOfQuantifierCardinality(Quantifier quantifier)
        {
            Quantifier = quantifier;
        }
        public double Call()
        {
            return 1.0 - (Quantifier.FuzzySet.MembershipFunction.Cardinality() /
                          Quantifier.FuzzySet.MembershipFunction.DomainCardinality());
        }
    }
}
