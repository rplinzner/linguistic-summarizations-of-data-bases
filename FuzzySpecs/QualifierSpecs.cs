using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fuzzy.Function;
using Fuzzy.Summarizer;
using Fuzzy.Set;

namespace FuzzySpecs
{
    [TestClass]
    public class QualifierSpecs
    {
        IFunction function;
        FuzzySet fuzzySet;
        Qualifier qualifier;

        [TestInitialize]
        public void Setup()
        {
            function = new TriangularFunction(0.0, 0.2);
            fuzzySet = new FuzzySet(function);
            qualifier = new Qualifier("YOUNG", fuzzySet);
        }
        [TestMethod]
        public void AddLinguisticValueToSummarizer()
        {
            Assert.AreEqual(qualifier.FuzzySet, fuzzySet);
            Assert.AreEqual(qualifier.Label, "YOUNG");
        }
    }
}
