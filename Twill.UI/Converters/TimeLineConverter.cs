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
    public class TimeLineConverter : IValueConverter
    {
        private const double DateTimeHeight = 12.0; 

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is double == false)
                return null;
            var height = (double)value;

            var pixelsInMinute = height / Report.MinetsInDay;

            var forconstr = height / DateTimeHeight;

            var a = Report.MinetsInDay / forconstr;

            var list = new List<object>();

            var now = DateTime.Now.Date;

            for (double i = 0.0; i < Report.MinetsInDay; i+= (a * 5))
            {
                list.Add(now.AddMinutes(i));

                list.Add(false);
                list.Add(false);
                list.Add(false);
                list.Add(false);
            }

            return list;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}