using System;
using System.Collections.Generic;
using System.Linq;
namespace Data
{
    public class Cover
    {
        public int Id { get; set; }
        public int Elevation { get; set; }
        public int Slope { get; set; }
        public int HorizontalDistanceToHydrology { get; set; }
        public int VerticalDistanceToHydrology { get; set; }
        public int HorizontalDistanceToRoadways { get; set; }
        public int Hillshade9Am { get; set; }
        public int HillshadeNoon { get; set; }
        public int Hillshade3Pm { get; set; }
        public int HorizontalDistanceToFirePoints { get; set; }
        public int CoverType { get; set; }
        public int Aspect { get; set; }
    }
}
