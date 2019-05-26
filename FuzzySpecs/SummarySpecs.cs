using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fuzzy.Summarizer;
using Fuzzy.Function;
using Fuzzy.Quality;
using Fuzzy.Set;

namespace FuzzySpecs
{
    [TestClass]
    public class SummarySpecs
    {
        Quantifier quantifier;
        Summarizer summarizer;
        IFunction quantifierTrapezoidalFunction;
        IFunction summarizerTrapezoidalFunction;
        FuzzySet quantifierFuzzySet;
        FuzzySet summarizerFuzzySet;

        [TestInitialize]
        public void Setup()
        {
            quantifierTrapezoidalFunction = new TrapezoidalFunction(0.12, 0.44, 0.36, 0.16);
            summarizerTrapezoidalFunction = new TrapezoidalFunction(0.0, 40.0, 30.0, 0);
            quantifierFuzzySet = new FuzzySet(quantifierTrapezoidalFunction);
            summarizerFuzzySet = new FuzzySet(summarizerTrapezoidalFunction);
            quantifier = new Quantifier("SOME", quantifierFuzzySet);
            summarizer = new Summarizer("YOUNG", summarizerFuzzySet);
        }

        [TestMethod]
        public void SampleSummary()
        {
            List<int> ages = new List<int>()
            {
                38,31,36,24,30,24,45,38,28,40,45,50
            };
            string p = "People";
            string summary = $"{quantifier.Label} {p} ARE/HAVE {summarizer.Label}";
            DegreeOfTruth degreeOfTruth = new DegreeOfTruth()
            {

                Summarizer1 = summarizer,
                Quantifier = quantifier,
                ValuesForSummarizer1 = ages
            };
            double result = degreeOfTruth.Call();
            summary += $"[{result}]";
            Assert.AreEqual(summary, "");
        }
    }
}
