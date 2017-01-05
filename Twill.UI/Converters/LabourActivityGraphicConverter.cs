using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Twill.UI.Core.Data;
using Twill.UI.Core.Models.Controls.TimeLine;

namespace Twill.UI.Converters
{
    public class LabourActivityGraphicConverter : IMultiValueConverter
    {
        private const int MaxWeight = 10;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return null;

            //var lists = new List<List<Point>>();

            //if (values == null)
            //    return lists;

            //if (values.Length < 2)
            //    return lists;

            //var processDayLaborModels = values[0] as IEnumerable<object>;
            //if (processDayLaborModels == null)
            //    return lists;

            //if (!(values[1] is double))
            //    return lists;
            //var width = (double)values[1];

            //if (!(values[2] is double))
            //    return lists;
            //var height = (double)values[2];


            //var pixelsInMinute = height / Tools.Math.Position.MinutesInDay;

            //var processes = processDayLaborModels.ToDictionary(
            //    processDayLaborModel => processDayLaborModel.ProcessName,
            //    processDayLaborModel => new Tuple<List<int>, List<int>, Brush>(
            //        new List<int>() { 0 }, 
            //        processDayLaborModel.ProcessLabors.SelectMany(processLabor => processLabor.Headers.Select(header => (int)Math.Round(header.Start.TimeOfDay.TotalMinutes))).ToList(),
            //        processDayLaborModel.Brush));

            //lists.AddRange(processes.Select(process => new List<Point>()));



            //for (int i = 1; i < Tools.Math.Position.MinutesInDay; i++)
            //{ 
            //    foreach (var process in processes)
            //    {
            //        var lastItem = process.Value.Item1.Last();
            //        if (lastItem != 0)
            //            lastItem -= 1;
            //        process.Value.Item1.Add(lastItem);
            //    }

            //    foreach (var process in processes.Where(process => process.Value.Item2.Contains(i)).ToList())
            //    { 
            //        process.Value.Item1[process.Value.Item1.Count - 1] = process.Value.Item1.Last() + 2;
                     
            //    }
            //}
             

            //var widthpixelscoefficient = (int)Math.Round(width / (double)MaxWeight);

            //for (int i = 0, length = lists.Count; i < Tools.Math.Position.MinutesInDay; i++)
            //{
            //    var currentMinute = i * pixelsInMinute;

            //    for (int j = 0; j < length; j++)
            //    {
            //        var x = processes.Values.ElementAt(j).Item1[i];

            //        lists[j].Add(new Point((x > MaxWeight ? MaxWeight : x) * widthpixelscoefficient, currentMinute));
            //    }

            //}



            //return lists.Zip(processes, (list, process) => new Tuple<Brush, PointCollection>(process.Value.Item3, new PointCollection(list))).ToList();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
