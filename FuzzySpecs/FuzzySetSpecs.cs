using System.Collections.Generic;
using Fuzzy.Function;
using Fuzzy.Set;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FuzzySpecs
{
    [TestClass]
    public class FuzzySetSpecs
    {
        [TestMethod]
        public void SumOfFuzzySets()
        {
            TriangularFunction func1 = new TriangularFunction(1.0, 2.0);
            TrapezoidalFunction func2 = new TrapezoidalFunction(1.0, 2.0, 1.8, 1.5);
            FuzzySet set1 = new FuzzySet(func1);
            FuzzySet set2 = new FuzzySet(func2);

            Assert.AreEqual(set1.Union(set2, 1.8), 1);
        }

        [TestMethod]
        public void IntersectionfFuzzySets()
        {
            TriangularFunction func1 = new TriangularFunction(1.0, 2.0);
            TrapezoidalFunction func2 = new TrapezoidalFunction(1.0, 2.0, 1.8, 1.5);
            FuzzySet set1 = new FuzzySet(func1);
            FuzzySet set2 = new FuzzySet(func2);

            Assert.AreEqual(set1.Intersection(set2, 1.0), 0);
        }

        [TestMethod]
        public void EqualityOfFuzzySets()
        {
            TriangularFunction func1 = new TriangularFunction(1.0, 2.0);
            TrapezoidalFunction func2 = new TrapezoidalFunction(1.0, 2.0, 1.8, 1.5);
            FuzzySet set1 = new FuzzySet(func1);
            FuzzySet set2 = new FuzzySet(func2);

            Assert.IsTrue(set1.Equals(set2, 1.5));
        }

        [TestMethod]
        public void InequalityOfFuzzySets()
        {
            TriangularFunction func1 = new TriangularFunction(1.0, 2.0);
            TrapezoidalFunction func2 = new TrapezoidalFunction(1.0, 2.0, 1.8, 1.5);
            FuzzySet set1 = new FuzzySet(func1);
            FuzzySet set2 = new FuzzySet(func2);

            Assert.IsFalse(set1.Equals(set2, 1.8));
        }
    }
}
