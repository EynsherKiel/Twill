using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Twill.UI.Core.Models.Controls.TimeLine; 

namespace Twill.UI.Converters
{
    public class HeadersFilterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var reports = value as IEnumerable<ReportModel>;
            if (reports == null)
                return value;

            var newReports = new List<ReportModel>();

            if (reports.Count() > 0)
            {
                newReports.Add(reports.First());
            }

            foreach (var report in reports.Skip(1))
            {
                var last = newReports.Last();

                if (last?.Text == report.Text)
                    continue;

                newReports.Add(report);
            }

            return newReports;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
