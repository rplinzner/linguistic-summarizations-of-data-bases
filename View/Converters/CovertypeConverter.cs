using System;
using System.Globalization;
using System.Windows.Data;

namespace View.Converters
{
    public class CovertypeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temp = (int)value;
            return temp == 1 ? "Spruce/Fir" :
                temp == 2 ? "Lodgepole Pine" :
                temp == 3 ? "Ponderosa Pine" :
                temp == 4 ? "Cottonwood/Willow" :
                temp == 5 ? "Aspen" :
                temp == 6 ? "Douglas-fir" : "Krummholz";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var temp = (string) value;
            return temp == "Spruce/Fir" ? 1 :
                temp == "Lodgepole Pine" ? 2 :
                temp == "Ponderosa Pine" ? 3 :
                temp == "Cottonwood/Willow" ? 4 :
                temp == "Aspen" ? 5 :
                temp == "Douglas-fir" ? 6 : 7;
        }
    }
}