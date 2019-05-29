using System;
using System.Collections.Generic;
using System.Linq;

namespace Fuzzy.Quality
{
    public class DegreeOfAppropriateness : IDegree
    {
        public double T3 { get; set; }
        public Summarizer.Summarizer Summarizer1 { get; set; }
        public Summarizer.Summarizer Summarizer2 { get; set; }
        public List<int> ValuesForSummarizer1 { get; set; }
        public List<int> ValuesForSummarizer2 { get; set; }
        public DegreeOfAppropriateness() { }
        public double Call()
        {
            double r = 1.0;
            if (Summarizer2 == null)
            {
                r *= R(Summarizer1, ValuesForSummarizer1);
            }
            else
            {
                r *= R(Summarizer1, ValuesForSummarizer1);
                r *= R(Summarizer2, ValuesForSummarizer2);
            }
            return Math.Abs(r - T3);
        }

        private double R(Summarizer.Summarizer summarizer, List<int> values)
        {
            List<int> result = new List<int>();
            foreach (int i in values)
            {
                result.Add(summarizer.FuzzySet.Membership(i) > 0 ? 1 : 0);
            }

            return result.Sum() / values.Count;
        }
    }
}