using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fuzzy.Summarizer;
using Fuzzy.Set;
using Fuzzy.Function;

namespace FuzzySpecs
{
    [TestClass]
    public class SummarizerSpecs
    {
        IFunction function;
        FuzzySet fuzzySet;
        Summarizer summarizer;

        [TestInitialize]
        public void Setup()
        {
            function = new TriangularFunction(0.0, 0.2);
            fuzzySet = new FuzzySet(function);
            summarizer = new Summarizer("YOUNG", fuzzySet);
        }
        [TestMethod]
        public void AddLinguisticValueToSummarizer()
        {
            Assert.AreEqual(summarizer.FuzzySet, fuzzySet);
            Assert.AreEqual(summarizer.Label, "YOUNG");
        }
    }
}
