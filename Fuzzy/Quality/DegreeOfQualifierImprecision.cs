using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuzzy.Summarizer;

namespace Fuzzy.Quality
{
    public class DegreeOfQualifierImprecision : IDegree
    {
        public List<Qualifier> Qualifiers { get; set; } = new List<Qualifier>();
        public DegreeOfQualifierImprecision() { }
        public DegreeOfQualifierImprecision(List<Qualifier> qualifiers)
        {
            Qualifiers = qualifiers;
        }
        public double Call()
        {
            if (Qualifiers.Count == 0) return 1;
            double result = 1;
            foreach (Qualifier qualifier in Qualifiers)
            {
                result *= qualifier.FuzzySet.DegreeOfFuzziness();
            }

            result = Math.Pow(result, 1.0 / Qualifiers.Count);
            return 1.0 - result;
        }
    }
}
