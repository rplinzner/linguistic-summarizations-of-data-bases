using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuzzy.Quality
{
    public class LengthOfSummary : IDegree
    {
        public List<Summarizer.Summarizer> Summarizers { get; set; }
        public LengthOfSummary(List<Summarizer.Summarizer> summarizers)
        {
            Summarizers = summarizers;
        }

        public double Call()
        {
            return 2.0 * Math.Pow(0.5, Summarizers.Count);
        }
    }
}
