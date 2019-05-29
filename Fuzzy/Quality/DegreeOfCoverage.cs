using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Fuzzy.Summarizer;

namespace Fuzzy.Quality
{
    public class DegreeOfCoverage : IDegree
    {
        public Summarizer.Summarizer Summarizer1 { get; set; }
        public Summarizer.Summarizer Summarizer2 { get; set; }
        public List<int> ValuesForSummarizer1 { get; set; }
        public List<int> ValuesForSummarizer2 { get; set; }
        public Base Qualifier { get; set; }
        public string Operation { get; set; } = "NONE";
        public DegreeOfCoverage()
        {

        }

        public double Call()
        {
            if (Qualifier == null)
            {
                return (double)tWithoutQualifier() / hWithoutQualifier();
            }

            return (double)t() / h();
        }

        private int t()
        {
            List<int> results = new List<int>();
            if (Summarizer2 == null)
            {
                foreach (int x in ValuesForSummarizer1)
                {
                    results.Add(Summarizer1.FuzzySet.Membership(x) > 0 && Qualifier.FuzzySet.Membership(x) > 0 ? 1 : 0);
                }
            }
            else
            {
                if (Operation == "AND")
                {
                    for (int i = 0; i < ValuesForSummarizer1.Count; i++)
                    {
                        results.Add(Summarizer1.FuzzySet.TNorm(Summarizer2.FuzzySet, ValuesForSummarizer1[i], ValuesForSummarizer2[i]) > 0 && Qualifier.FuzzySet.Membership(ValuesForSummarizer1[i]) > 0 && Qualifier.FuzzySet.Membership(ValuesForSummarizer2[i]) > 0 ? 1 : 0);
                    }
                }
                else if (Operation == "OR")
                {
                    for (int i = 0; i < ValuesForSummarizer1.Count; i++)
                    {
                        results.Add(Summarizer1.FuzzySet.SNorm(Summarizer2.FuzzySet, ValuesForSummarizer1[i], ValuesForSummarizer2[i]) > 0 && Qualifier.FuzzySet.Membership(ValuesForSummarizer1[i]) > 0 && Qualifier.FuzzySet.Membership(ValuesForSummarizer2[i]) > 0 ? 1 : 0);
                    }
                }
            }

            return results.Sum();
        }

        private int h()
        {
            List<int> results = new List<int>();
            if (Summarizer2 == null)
            {
                foreach (int x in ValuesForSummarizer1)
                {
                    results.Add(Qualifier.FuzzySet.Membership(x) > 0 ? 1 : 0);
                }
            }
            else
            {
                for (int i = 0; i < ValuesForSummarizer1.Count; i++)
                {
                    results.Add(Qualifier.FuzzySet.Membership(ValuesForSummarizer1[i]) > 0 && Qualifier.FuzzySet.Membership(ValuesForSummarizer2[i]) > 0 ? 1 : 0);
                }
            }

            return results.Sum();
        }

        private int tWithoutQualifier()
        {
            List<int> results = new List<int>();
            if (Summarizer2 == null)
            {
                foreach (int x in ValuesForSummarizer1)
                {
                    results.Add(Summarizer1.FuzzySet.Membership(x) > 0 ? 1 : 0);
                }
            }
            else
            {
                if (Operation == "AND")
                {
                    for (int i = 0; i < ValuesForSummarizer1.Count; i++)
                    {
                        results.Add(Summarizer1.FuzzySet.TNorm(Summarizer2.FuzzySet, ValuesForSummarizer1[i], ValuesForSummarizer2[i]) > 0 ? 1 : 0);
                    }
                }
                else if (Operation == "OR")
                {
                    for (int i = 0; i < ValuesForSummarizer1.Count; i++)
                    {
                        results.Add(Summarizer1.FuzzySet.SNorm(Summarizer2.FuzzySet, ValuesForSummarizer1[i], ValuesForSummarizer2[i]) > 0 ? 1 : 0);
                    }
                }
            }
            return results.Sum();
        }

        private int hWithoutQualifier()
        {
            List<int> results = new List<int>();
            foreach (int _ in ValuesForSummarizer1)
            {
                results.Add(1);

            }

            return results.Sum();
        }
    }
}