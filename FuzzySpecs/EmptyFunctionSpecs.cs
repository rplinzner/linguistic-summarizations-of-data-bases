using Fuzzy.Function;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FuzzySpecs
{
    [TestClass]
    public class EmptyFunctionSpecs
    {
        IFunction func;

        [TestInitialize]
        public void Setup()
        {
            func = new EmptyFunction();
        }

        [TestMethod]
        public void RangeReturns0()
        {
            Assert.AreEqual(func.Range(), 0);
        }

        [TestMethod]
        public void ValueReturns0()
        {
            Assert.AreEqual(func.Value(23456789.0), 0);
        }

        [TestMethod]
        public void HeightReturns0()
        {
            Assert.AreEqual(func.GetHeight(), 0);
        }
    }
}
