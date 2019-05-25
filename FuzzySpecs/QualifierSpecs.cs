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
            qualifier = new Qualifier();
        }
        [TestMethod]
        public void AddLinguisticValueToSummarizer()
        {
            qualifier.AddLinguisticValue("YOUNG", fuzzySet);
            Assert.AreEqual(qualifier.FuzzySets["YOUNG"], fuzzySet);
        }
        [TestMethod]
        public void RemoveLinguisticVariableFromSummarizer()
        {
            qualifier.AddLinguisticValue("YOUNG", fuzzySet);
            qualifier.RemoveLinguisticValue("YOUNG");
            CollectionAssert.DoesNotContain(qualifier.FuzzySets, fuzzySet);
        }
    }
}
