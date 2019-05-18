using System.Collections.Generic;
using Fuzzy.Set;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FuzzySpecs
{
    [TestClass]
    public class ClassicSetSpecs
    {
        [TestMethod]
        public void SumOfClassicSets()
        {
            ClassicSet set1 = new ClassicSet(new List<int>()
            {
                1,
                2,
            });

            ClassicSet set2 = new ClassicSet(new List<int>()
            {
                1,
                4,
                5
            });
            
            CollectionAssert.AreEquivalent(set1.Sum(set2).Values, new List<int>() { 1, 2, 4, 5 });
            CollectionAssert.AreEquivalent(set2.Sum(set1).Values, new List<int>() { 1, 2, 4, 5 });
        }

        [TestMethod]
        public void MultiplicationOfClassicsSets()
        {
            ClassicSet set1 = new ClassicSet(new List<int>()
            {
                3,
                4,
                5
            });

            ClassicSet set2 = new ClassicSet(new List<int>()
            {
                4,
                5,
                6,
                7
            });
            CollectionAssert.AreEquivalent(set1.Multiplication(set2).Values, new List<int>() { 4, 5 });
            CollectionAssert.AreEquivalent(set2.Multiplication(set1).Values, new List<int>() { 4, 5 });
        }

        [TestMethod]
        public void EqualityOfClassicSets()
        {
            ClassicSet set1 = new ClassicSet(new List<int>()
            {
                3,
                4,
                5
            });

            ClassicSet set2 = new ClassicSet(new List<int>()
            {
                3,
                5,
                4
            });

            Assert.IsTrue(set1.Equals(set2));
            Assert.IsTrue(set2.Equals(set1));
        }

        [TestMethod]
        public void InequalityOfClassicSets()
        {
            ClassicSet set1 = new ClassicSet(new List<int>()
            {
                3,
                4,
                5
            });

            ClassicSet set2 = new ClassicSet(new List<int>()
            {
                3,
                2,
                1
            });

            Assert.IsFalse(set1.Equals(set2));
            Assert.IsFalse(set2.Equals(set1));
        }
    }
}
