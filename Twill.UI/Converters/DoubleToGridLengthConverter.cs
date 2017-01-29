using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Twill.UI.Converters
{
    // https://social.msdn.microsoft.com/Forums/vstudio/en-US/79744e50-01b4-438f-b76e-9a4fbbebc248/is-it-possible-to-animate-a-rowdefinition?forum=wpf
    [ValueConversion(typeof(Double), typeof(GridLength))]
    public class DoubleToGridLength : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Console.WriteLine((double)value);
            return new GridLength((double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((GridLength)value).Value;
        }
    }
}
