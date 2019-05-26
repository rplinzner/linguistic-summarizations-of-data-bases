using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fuzzy.Function;
using Fuzzy.Set;
using Fuzzy.Summarizer;

namespace FuzzySpecs
{
    [TestClass]
    public class QuantifierSpecs
    {
        IFunction function;
        FuzzySet fuzzySet;
        Quantifier quantifier;

        [TestInitialize]
        public void Setup()
        {
            function = new TriangularFunction(0.0, 0.2);
            fuzzySet = new FuzzySet(function);
            quantifier = new Quantifier("ALMOST NONE", fuzzySet);
        }
        [TestMethod]
        public void AddLinguisticValueToSummarizer()
        {
            Assert.AreEqual(quantifier.FuzzySet, fuzzySet);
            Assert.AreEqual(quantifier.Label, "ALMOST NONE");
        }
    }
}
