﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Twill.UI.Core.Data;

namespace Twill.UI.Converters
{
    public class ReportsTimeIntervalToHeightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
                return 0;

            var report = values[0] as Labor;
            if (report == null)
                return 0;

            if (!(values[1] is double))
                return 0;
            var height = (double)values[1];

            var pixelsInMinute = height / Labor.MinetsInDay; 

            return report.TotalMinutesInterval * pixelsInMinute;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}