using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Twill.UI.Core.Data;

namespace Twill.UI.Converters
{
    public class DateTimeToPixelsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
                return 0;

            var totalMinutes = 0.0;

            if (values[0] is DateTime)
                totalMinutes = ((DateTime)values[0]).TimeOfDay.TotalMinutes;
            else
                if (values[0] is double)
                totalMinutes = (double)values[0];
            else
                return 0;


            if (values[1] is double == false)
                return 0;
            var height = (double)values[1];

            var pixelsInMinute = height / Labor.MinetsInDay;

            return totalMinutes * pixelsInMinute;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
