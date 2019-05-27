using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Data;

namespace View.ViewModel
{
    public class AtributesLoader
    {
        public static ObservableCollection<AttributesListVm> ConvertCoverToAtributesListVms(List<Cover> covers)
        {
            ObservableCollection<AttributesListVm> ret = new ObservableCollection<AttributesListVm>();
            ret.Add(new AttributesListVm()
            {
                Name = nameof(Cover.Elevation),
                Max = covers.Select(c=> c.Elevation).Max(),
                Min = covers.Select(c => c.Elevation).Min()
            });
            ret.Add(new AttributesListVm()
            {
                Name = nameof(Cover.Slope),
                Max = covers.Select(c => c.Slope).Max(),
                Min = covers.Select(c => c.Slope).Min()
            });
            ret.Add(new AttributesListVm()
            {
                Name = nameof(Cover.HorizontalDistanceToHydrology),
                Max = covers.Select(c => c.HorizontalDistanceToHydrology).Max(),
                Min = covers.Select(c => c.HorizontalDistanceToHydrology).Min()
            });
            ret.Add(new AttributesListVm()
            {
                Name = nameof(Cover.VerticalDistanceToHydrology),
                Max = covers.Select(c => c.VerticalDistanceToHydrology).Max(),
                Min = covers.Select(c => c.VerticalDistanceToHydrology).Min()
            });
            ret.Add(new AttributesListVm()
            {
                Name = nameof(Cover.HorizontalDistanceToRoadways),
                Max = covers.Select(c => c.HorizontalDistanceToRoadways).Max(),
                Min = covers.Select(c => c.HorizontalDistanceToRoadways).Min()
            });
            ret.Add(new AttributesListVm()
            {
                Name = nameof(Cover.Hillshade9Am),
                Max = covers.Select(c => c.Hillshade9Am).Max(),
                Min = covers.Select(c => c.Hillshade9Am).Min()
            });
            ret.Add(new AttributesListVm()
            {
                Name = nameof(Cover.HillshadeNoon),
                Max = covers.Select(c => c.HillshadeNoon).Max(),
                Min = covers.Select(c => c.HillshadeNoon).Min()
            });
            ret.Add(new AttributesListVm()
            {
                Name = nameof(Cover.Hillshade3Pm),
                Max = covers.Select(c => c.Hillshade3Pm).Max(),
                Min = covers.Select(c => c.Hillshade3Pm).Min()
            });
            ret.Add(new AttributesListVm()
            {
                Name = nameof(Cover.HorizontalDistanceToFirePoints),
                Max = covers.Select(c => c.HorizontalDistanceToFirePoints).Max(),
                Min = covers.Select(c => c.HorizontalDistanceToFirePoints).Min()
            });
            ret.Add(new AttributesListVm()
            {
                Name = nameof(Cover.Aspect),
                Max = covers.Select(c => c.Aspect).Max(),
                Min = covers.Select(c => c.Aspect).Min()
            });
            return ret;

        }
    }
}