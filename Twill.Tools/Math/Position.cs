using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Tools.Math
{
    public static class Position
    {
        public const double SecondsInMinute = 60.0;
        public const double MinutesInDay = 1440.0;


        public static double ChoisenPixel(DateTime time, double sideSize) => ChoisenPixel(time.TimeOfDay.TotalMinutes, sideSize);

        public static double ChoisenPixel(double totalMinutes, double sideSize) => totalMinutes * (sideSize / MinutesInDay);


        public static double ChoisenMinute(double pixels, double sideSize) => pixels * (MinutesInDay / sideSize);
    }
}
