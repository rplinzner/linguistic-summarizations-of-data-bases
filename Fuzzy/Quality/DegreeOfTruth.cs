using Fuzzy.Summarizer;
using System;
using System.Collections.Generic;

namespace Fuzzy.Quality
{
    public class DegreeOfTruth : IDegree
    {
        public List<double> ValuesForSummarizer1 { get; set; }
        public List<double> ValuesForSummarizer2 { get; set; }
        public Quantifier Quantifier;
        public Summarizer.Summarizer Summarizer1 { get; set; }
        public Summarizer.Summarizer Summarizer2 { get; set; }
        public string Operation { get; set; }

        public double Call()
        {
            double r = 0;
            if(Operation == "OR")
            {
                for(int i = 0; i < ValuesForSummarizer1.Count; i++)
                {
                    r += Summarizer1.FuzzySet.SNorm(Summarizer2.FuzzySet, ValuesForSummarizer1[i], ValuesForSummarizer2[i]);
                }
            } else if(Operation == "AND")
            {
                for (int i = 0; i < ValuesForSummarizer1.Count; i++)
                {
                    r += Summarizer1.FuzzySet.TNorm(Summarizer2.FuzzySet, ValuesForSummarizer1[i], ValuesForSummarizer2[i]);
                }
            }
            else
            {
                for (int i = 0; i < ValuesForSummarizer1.Count; i++)
                {
                    r += Summarizer1.FuzzySet.Membership(ValuesForSummarizer1[i]);
                }
            }
            return Quantifier.FuzzySet.Membership(r / ValuesForSummarizer1.Count);
        }
    }
}
