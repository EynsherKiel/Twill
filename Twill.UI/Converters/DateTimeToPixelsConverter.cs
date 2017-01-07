using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Twill.UI.Converters
{
    public class DateTimeToPixelsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return GetPosition(values)/*.ToString()*/;
        }

        private object GetPosition(object[] values)
        {
            if (values.Length < 2)
                return 0.0;

            if (values[1] is double == false)
                return 0.0;
            var height = (double)values[1];


            if (values[0] is DateTime)
                return Tools.Math.Position.ChoisenPixel((DateTime)values[0], height);

            if (values[0] is double)
                return Tools.Math.Position.ChoisenPixel((double)values[0], height);


            return 0.0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
