using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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
             

            var forconstr = height / DateTimeHeight;

            var additinaltime = Tools.Math.Position.MinutesInDay / forconstr;

            var now = DateTime.Now.Date;

            var list = new List<object>()
            {
                now
            };

            double time = additinaltime;
             
            for (int i = 1, len = (int)forconstr; i < len; time += additinaltime, i++)
            {
                if (i % 5 == 0)
                {
                    list.Add(now.AddMinutes(time));
                }
                else
                {
                    list.Add(default(bool));
                }
            }

            return list;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}