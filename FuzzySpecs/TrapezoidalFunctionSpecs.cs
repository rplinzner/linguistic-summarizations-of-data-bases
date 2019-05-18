using Fuzzy.Function;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FuzzySpecs
{
    [TestClass]
    public class TrapezoidalFunctionSpecs
    {
        [TestMethod]
        public void RangeWhenAandBArePostive()
        {
            TrapezoidalFunction func = new TrapezoidalFunction(1.0, 2.0, 1.5, 1.8);
            Assert.AreEqual(func.Range(), 1.0);
        }

        [TestMethod]
        public void RangeWhenAandBAreNegative()
        {
            TrapezoidalFunction func = new TrapezoidalFunction(-1.0, -2.0, -1.8, -1.2);
            Assert.AreEqual(func.Range(), 1.0);
        }

        [TestMethod]
        public void RangeWhenAandBAreOfDifferentSigns()
        {
            TrapezoidalFunction func = new TrapezoidalFunction(-1.0, 2.0, 1.8, -0.8);
            Assert.AreEqual(func.Range(), 3.0);
        }

        [TestMethod]
        public void ValueWhenXisOutsideOfRange()
        {
            TrapezoidalFunction func = new TrapezoidalFunction(1.0, 2.0, 1.5, 1.8);
            Assert.AreEqual(func.Value(0.8), 0);
            Assert.AreEqual(func.Value(2.1), 0);
        }

        [TestMethod]
        public void ValueWhenXBetweenCAndD()
        {
            TrapezoidalFunction func = new TrapezoidalFunction(1.0, 2.0, 1.8, 1.5);
            Assert.AreEqual(func.Value(1.5), 1);
            Assert.AreEqual(func.Value(1.8), 1);
            Assert.AreEqual(func.Value(1.6), 1);
        }

        [TestMethod]
        public void ValueWhenXIsOnTheEdges()
        {
            TrapezoidalFunction func = new TrapezoidalFunction(1.0, 2.0, 1.8, 1.5);
            Assert.AreEqual(func.Value(1.0), 0);
            Assert.AreEqual(func.Value(2.0), 0);
        }

        [TestMethod]
        public void ValueWhenXIsBetweenAandD()
        {
            TrapezoidalFunction func = new TrapezoidalFunction(1.0, 2.0, 1.8, 1.4);
            Assert.AreEqual(func.Value(1.2), 0.5);

        }

        [TestMethod]
        public void ValueWhenXIsBetweenCandB()
        {
            TrapezoidalFunction func = new TrapezoidalFunction(1.0, 2.0, 1.6, 1.5);
            Assert.AreEqual(func.Value(1.8), 0.5);
        }
    }
}
