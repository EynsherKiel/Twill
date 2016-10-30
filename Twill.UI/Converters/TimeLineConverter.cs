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

            var pixelsInMinute = (int)(height / Labor.MinetsInDay);

            var forconstr = height / DateTimeHeight;

            var additinaltime = Labor.MinetsInDay / forconstr;

            var list = new List<object>();

            var now = DateTime.Now.Date;

            double time = 0.0;
            list.Add(now.AddMinutes(time));
            time += additinaltime;

            int len = (int)forconstr;
            for (int i = 1; i < len; time += additinaltime, i++)
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