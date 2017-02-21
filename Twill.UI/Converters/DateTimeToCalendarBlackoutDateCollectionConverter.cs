using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Twill.UI.Converters
{
    public class DateTimeToCalendarBlackoutDateCollectionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            if (values == null || values.Length < 2)
                return null;

            var calendar = values[1] as System.Windows.Controls.Calendar;
            if (calendar == null)
                return null;
            
            var blackdates = new ObservableCollection<CalendarDateRange>() { new CalendarDateRange() { Start = DateTime.MinValue, End = DateTime.MaxValue } };

            var list = values[0] as IList;
            if (list == null)
                return blackdates;


            foreach (var calendarDateRange in list.OfType<DateTime>().Select(el => new CalendarDateRange() { Start = el, End = el }))
            {
                Add(blackdates, calendarDateRange);
            }

            foreach (var item in blackdates.ToList().Where(el => el.Start == el.End))
            {
                blackdates.Remove(item);
            }

            var first = blackdates.First();

            foreach (var second in blackdates.Skip(1).ToList())
            {
                if (first.End.Date == second.Start.Date)
                {
                    first.End = first.End.AddDays(-1);
                    second.Start = second.Start.AddDays(1);
                }

                first = second;
            }

            foreach (var item in blackdates.ToList().Where(el => el.Start == el.End))
            {
                blackdates.Remove(item);
            }

            return blackdates;
        }


        private void Add(IList<CalendarDateRange> dates, CalendarDateRange date)
        {
            var userlog = dates.FirstOrDefault(rep => rep.Start <= date.Start && rep.End >= date.Start);

            var findIndexReport = dates.IndexOf(userlog);
            var end = userlog.End;

            if (userlog.End.Date == date.Start.Date)
            {
                dates[findIndexReport + 1].Start = date.End;
                dates.Insert(findIndexReport + 1, date);
            }
            else
            {
                var tprocessActivity = new CalendarDateRange();

                tprocessActivity.Start = date.End;
                tprocessActivity.End = userlog.End;
                userlog.End = date.Start;

                dates.Insert(findIndexReport + 1, date);
                dates.Insert(findIndexReport + 2, tprocessActivity);
            }
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
