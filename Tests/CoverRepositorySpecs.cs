using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class CoverRepositorySpecs
    {
        private List<Cover> objects;

        [TestInitialize]
        public void Setup()
        {
            objects = CoverRepository.All();
        }
        [TestMethod]
        public void AllSpec()
        {
            Assert.AreEqual(objects.Count, 11001);
        }

        [TestMethod]
        public void ElevationDomain()
        {
            Assert.AreEqual(objects.Select(c => c.Elevation).Min(), 1863);
            Assert.AreEqual(objects.Select(c => c.Elevation).Max(), 3849);
        }

        [TestMethod]
        public void SlopeDomain()
        {
            Assert.AreEqual(objects.Select(c => c.Slope).Min(), 0);
            Assert.AreEqual(objects.Select(c => c.Slope).Max(), 52);
        }

        [TestMethod]
        public void AspectDomain()
        {
            Assert.AreEqual(objects.Select(c => c.Aspect).Min(), 0);
            Assert.AreEqual(objects.Select(c => c.Aspect).Max(), 360);
        }
        [TestMethod]
        public void HorizontalDistanceToHydrologyDomain()
        {
            Assert.AreEqual(objects.Select(c => c.HorizontalDistanceToHydrology).Min(), 0);
            Assert.AreEqual(objects.Select(c => c.HorizontalDistanceToHydrology).Max(), 1343);
        }
        [TestMethod]
        public void VerticalDistanceToHydrologyDomain()
        {
            Assert.AreEqual(objects.Select(c => c.VerticalDistanceToHydrology).Min(), -146);
            Assert.AreEqual(objects.Select(c => c.VerticalDistanceToHydrology).Max(), 554);
        }
        [TestMethod]
        public void HorizontalDistanceToRoadwaysDomain()
        {
            Assert.AreEqual(objects.Select(c => c.HorizontalDistanceToRoadways).Min(), 0);
            Assert.AreEqual(objects.Select(c => c.HorizontalDistanceToRoadways).Max(), 6890);
        }
        [TestMethod]
        public void Hillshade9amDomain()
        {
            Assert.AreEqual(objects.Select(c => c.Hillshade9Am).Min(), 0);
            Assert.AreEqual(objects.Select(c => c.Hillshade9Am).Max(), 254);
        }
        [TestMethod]
        public void HillshadeNoonDomain()
        {
            Assert.AreEqual(objects.Select(c => c.HillshadeNoon).Min(), 99);
            Assert.AreEqual(objects.Select(c => c.HillshadeNoon).Max(), 254);
        }
        [TestMethod]
        public void Hillshade3pmDomain()
        {
            Assert.AreEqual(objects.Select(c => c.Hillshade3Pm).Min(), 0);
            Assert.AreEqual(objects.Select(c => c.Hillshade3Pm).Max(), 248);
        }
        [TestMethod]
        public void HorizontalDistanceToFirePointsDomain()
        {
            Assert.AreEqual(objects.Select(c => c.HorizontalDistanceToFirePoints).Min(), 30);
            Assert.AreEqual(objects.Select(c => c.HorizontalDistanceToFirePoints).Max(), 6853);
        }
        [TestMethod]
        public void CoverTypeDomain()
        {
            Assert.AreEqual(objects.Select(c => c.CoverType).Min(), 1);
            Assert.AreEqual(objects.Select(c => c.CoverType).Max(), 7);
        }
    }
}
