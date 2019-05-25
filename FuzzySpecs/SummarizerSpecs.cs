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
            summarizer = new Summarizer();
        }
        [TestMethod]
        public void AddLinguisticValueToSummarizer()
        {
            summarizer.AddLinguisticValue("YOUNG", fuzzySet);
            Assert.AreEqual(summarizer.FuzzySets["YOUNG"], fuzzySet);
        }
        [TestMethod]
        public void RemoveLinguisticVariableFromSummarizer()
        {
            summarizer.AddLinguisticValue("YOUNG", fuzzySet);
            summarizer.RemoveLinguisticValue("YOUNG");
            CollectionAssert.DoesNotContain(summarizer.FuzzySets, fuzzySet);
        }
    }
}
