using System;
using System.Linq;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class CoverRepositorySpecs
    {
        [TestMethod]
        public void AllSpec()
        {
            var temp = CoverRepository.All();
            Assert.AreEqual(temp.Count, 11001);
        }

        [TestMethod]
        public void Domains()
        {
            var objects = CoverRepository.All();
            Console.Out.WriteLine("Elevation: Min: {0}, Max {1}", objects.Select(c => c.Elevation).Min(), objects.Select(c => c.Elevation).Max());
            Console.Out.WriteLine("Aspect: Min: {0}, Max {1}", objects.Select(c => c.Aspect).Min(), objects.Select(c => c.Aspect).Max());
            Console.Out.WriteLine("slope: Min: {0}, Max {1}", objects.Select(c => c.Slope).Min(), objects.Select(c => c.Slope).Max());
            Console.Out.WriteLine("horizontal_distance_to_hydrology: Min: {0}, Max {1}", objects.Select(c => c.HorizontalDistanceToHydrology).Min(), objects.Select(c => c.HorizontalDistanceToHydrology).Max());
            Console.Out.WriteLine("vertical_distance_to_hydrology: Min: {0}, Max {1}", objects.Select(c => c.VerticalDistanceToHydrology).Min(), objects.Select(c => c.VerticalDistanceToHydrology).Max());
            Console.Out.WriteLine("horizontal_distance_to_roadways: Min: {0}, Max {1}", objects.Select(c => c.HorizontalDistanceToRoadways).Min(), objects.Select(c => c.HorizontalDistanceToRoadways).Max());
            Console.Out.WriteLine("hillshade_9am: Min: {0}, Max {1}", objects.Select(c => c.Hillshade9Am).Min(), objects.Select(c => c.Hillshade9Am).Max());
            Console.Out.WriteLine("hillshade_noon: Min: {0}, Max {1}", objects.Select(c => c.HillshadeNoon).Min(), objects.Select(c => c.HillshadeNoon).Max());
            Console.Out.WriteLine("hillshade_3pm: Min: {0}, Max {1}", objects.Select(c => c.Hillshade3Pm).Min(), objects.Select(c => c.Hillshade3Pm).Max());
            Console.Out.WriteLine("horizontal_distance_to_fire_points: Min: {0}, Max {1}", objects.Select(c => c.HorizontalDistanceToFirePoints).Min(), objects.Select(c => c.HorizontalDistanceToFirePoints).Max());
            Console.Out.WriteLine("cover_type: Min: {0}, Max {1}", objects.Select(c => c.CoverType).Min(), objects.Select(c => c.CoverType).Max());
        }
    }
}
