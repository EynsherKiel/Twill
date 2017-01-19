using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Twill.Tools.WPF
{
    public static class UniqueColorGenerator
    {
        private static List<Brush> Brushes = new List<Brush>();

        private static Random Random = new Random();

        private static Brush GetUniqueBrush()
        {
            SolidColorBrush resultBrush = null;
            do
            {
                resultBrush = new SolidColorBrush(Color.FromRgb((byte)Random.Next(0, 255), (byte)Random.Next(0, 255), (byte)Random.Next(0, 255)));

            }
            while (Brushes.Cast<SolidColorBrush>().Any(brush => (System.Math.Abs(resultBrush.Color.R - brush.Color.R) + System.Math.Abs(resultBrush.Color.G - brush.Color.G) + System.Math.Abs(resultBrush.Color.B - brush.Color.B)) < 60.0));

            return resultBrush;
        }

        public static Brush Next() => GetUniqueBrush();
    }
}
