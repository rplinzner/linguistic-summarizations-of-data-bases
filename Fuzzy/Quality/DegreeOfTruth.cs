using Fuzzy.Summarizer;
using System;
using System.Collections.Generic;
using Fuzzy.Set;

namespace Fuzzy.Quality
{
    public class DegreeOfTruth : IDegree
    {
        public List<int> ValuesForSummarizer1 { get; set; }
        public List<int> ValuesForSummarizer2 { get; set; }
        public Quantifier Quantifier;
        public List<int> ValuesForQualifier { get; set; }
        public Summarizer.Summarizer Summarizer1 { get; set; }
        public Summarizer.Summarizer Summarizer2 { get; set; }
        public string Operation { get; set; } = "NONE";
        public Base Qualifier { get; set; }

        public double Call()
        {
            double r;
            if (Qualifier == null || string.IsNullOrEmpty(Qualifier.Label))
            {
                r = rWithoutQualifier();
            }
            else
            {
                r = this.r();
            }
            return Quantifier.FuzzySet.Membership(r / ValuesForSummarizer1.Count);
        }

        private double rWithoutQualifier()
        {
            double r = 0.0;
            if (Operation == "OR")
            {
                for (int i = 0; i < ValuesForSummarizer1.Count; i++)
                {
                    r += Summarizer1.FuzzySet.SNorm(Summarizer2.FuzzySet, ValuesForSummarizer1[i], ValuesForSummarizer2[i]);
                }
            }
            else if (Operation == "AND")
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

            return r;
        }

        private double r()
        {
            double result = 0.0;
            if (Operation == "OR")
            {
                for (int i = 0; i < ValuesForSummarizer1.Count; i++)
                {
                    result += Math.Min(Summarizer1.FuzzySet.SNorm(Summarizer1.FuzzySet, ValuesForSummarizer1[i],ValuesForSummarizer2[i]), MembershipToQualifier(i));
                }
            }
            else if (Operation == "AND")
            {
                for (int i = 0; i < ValuesForSummarizer1.Count; i++)
                {
                    result += Math.Min(Summarizer1.FuzzySet.TNorm(Summarizer1.FuzzySet, ValuesForSummarizer1[i], ValuesForSummarizer2[i]), MembershipToQualifier(i));
                }
            }
            else
            {
                for (int i = 0; i < ValuesForSummarizer1.Count; i++)
                {
                    result += Summarizer1.FuzzySet.TNorm(Qualifier.FuzzySet, ValuesForSummarizer1[i],
                        ValuesForSummarizer1[i]);
                }
            }
            double denominator = 0.0;
            foreach(int value in ValuesForQualifier)
            {
                denominator += Qualifier.FuzzySet.Membership(value);
            }
            return result / denominator;
        }
        private double MembershipToQualifier(int i) => Qualifier.FuzzySet.Membership(ValuesForQualifier[i]);
    }
}
