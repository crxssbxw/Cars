using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Cars.Converters
{
    [ValueConversion(typeof(DateOnly),typeof(DateTime))]
    public class DateOnlyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateonly = (DateOnly)value;
            var time = new TimeOnly();
            return new DateTime(dateonly, time);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var datetime = (DateTime)value;
            return DateOnly.FromDateTime(datetime);
        }
    }
}
