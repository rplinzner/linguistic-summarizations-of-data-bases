using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuzzy.Summarizer;

namespace Fuzzy.Quality
{
    public class LengthOfQualifier : IDegree
    {
        public List<Qualifier> Qualifiers { get; set; } = new List<Qualifier>();

        public LengthOfQualifier()
        {

        }
        public LengthOfQualifier(List<Qualifier> qualifiers)
        {
            Qualifiers = qualifiers;
        }

        public double Call()
        {
            if (Qualifiers.Count == 0) return 1.0;
            return 2.0 * Math.Pow(0.5, Qualifiers.Count);
        }
    }
}
