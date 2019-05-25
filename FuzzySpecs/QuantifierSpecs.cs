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
            quantifier = new Quantifier();
        }
        [TestMethod]
        public void AddLinguisticValueToSummarizer()
        {
            quantifier.AddLinguisticValue("ALMOST NONE", fuzzySet);
            Assert.AreEqual(quantifier.FuzzySets["ALMOST NONE"], fuzzySet);
        }
        [TestMethod]
        public void RemoveLinguisticVariableFromSummarizer()
        {
            quantifier.AddLinguisticValue("ALMOST NONE", fuzzySet);
            quantifier.RemoveLinguisticValue("ALMOST NONE");
            CollectionAssert.DoesNotContain(quantifier.FuzzySets, fuzzySet);
        }
    }
}
