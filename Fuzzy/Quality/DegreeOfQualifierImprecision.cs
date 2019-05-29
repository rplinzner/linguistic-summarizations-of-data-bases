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
        public List<Base> Qualifiers { get; set; } = new List<Base>();
        public DegreeOfQualifierImprecision() { }
        public DegreeOfQualifierImprecision(List<Summarizer.Summarizer> qualifiers)
        {
            foreach (var summarizer in qualifiers)
            {
                Qualifiers.Add(summarizer);
            }
        }
        public double Call()
        {
            if (Qualifiers.Count == 0) return 1;
            double result = 1;
            foreach (Base qualifier in Qualifiers)
            {
                result *= qualifier.FuzzySet.DegreeOfFuzziness();
            }

            result = Math.Pow(result, 1.0 / Qualifiers.Count);
            return 1.0 - result;
        }
    }
}
