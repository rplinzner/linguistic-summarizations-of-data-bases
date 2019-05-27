using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuzzy.Summarizer;

namespace Fuzzy.Quality
{
    public class DegreeOfQualifierCardinality : IDegree
    {
        public Qualifier Qualifier { get; set; }

        public DegreeOfQualifierCardinality()
        {

        }
        public DegreeOfQualifierCardinality(Qualifier qualifier)
        {
            Qualifier = qualifier;
        }
        public double Call()
        {
            if (Qualifier == null) return 1;
            return 1.0 - (Qualifier.FuzzySet.MembershipFunction.Cardinality() / Qualifier.FuzzySet.MembershipFunction.DomainCardinality());
        }
    }
}
