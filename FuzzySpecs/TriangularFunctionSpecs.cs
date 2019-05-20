using Fuzzy.Function;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FuzzySpecs
{
    [TestClass]
    public class TriangularFunctionSpecs
    {
        [TestMethod]
        public void RangeWhenAandBArePostive()
        {
            TriangularFunction func = new TriangularFunction(1.0, 2.0);
            Assert.AreEqual(func.Range(), 1.0);
        }

        [TestMethod]
        public void RangeWhenAandBAreNegative()
        {
            TriangularFunction func = new TriangularFunction(-1.0, -2.0);
            Assert.AreEqual(func.Range(), 1.0);
        }

        [TestMethod]
        public void RangeWhenAandBAreOfDifferentSigns()
        {
            TriangularFunction func = new TriangularFunction(-1.0, 2.0);
            Assert.AreEqual(func.Range(), 3.0);
        }

        [TestMethod]
        public void ValueWhenXIsOutOfRange()
        {
            TriangularFunction func = new TriangularFunction(1.0, 2.0);
            Assert.AreEqual(func.Value(0.8), 0);
            Assert.AreEqual(func.Value(2.1), 0);
        }

        [TestMethod]
        public void ValueWhenXIsOnTheEdgeOfTheFunction()
        {
            TriangularFunction func = new TriangularFunction(1.0, 2.0);
            Assert.AreEqual(func.Value(2.0), 0);
        }

        [TestMethod]
        public void ValueWhenXIsIsInTheMiddleOfTheTriangle()
        {
            TriangularFunction func = new TriangularFunction(1.0, 2.0);
            Assert.AreEqual(func.Value(1.5), 1);
        }

        [TestMethod]
        public void ValueWhenXIsIsBetweenAancC()
        {
            TriangularFunction func = new TriangularFunction(0.0, 4.0);
            Assert.AreEqual(func.Value(1.0), 0.5);
        }

        [TestMethod]
        public void ValueWhenXIsIsBetweenCandB()
        {
            TriangularFunction func = new TriangularFunction(0.0, 4.0);
            Assert.AreEqual(func.Value(3.0), 0.5);
        }

        [TestMethod]
        public void HeightDefaultsTo1()
        {
            TriangularFunction func = new TriangularFunction(0.0, 4.0);
            Assert.AreEqual(func.GetHeight(), 1.0);
        }

        public void AllowsToChangeHeight()
        {
            TriangularFunction func = new TriangularFunction(0.0, 4.0, 0.5);
            Assert.AreEqual(func.GetHeight(), 0.5);
        }
    }
}
