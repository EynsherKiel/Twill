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
            Brush resultBrush = null;
            do
                resultBrush = new SolidColorBrush(Color.FromRgb((byte)Random.Next(0, 255), (byte)Random.Next(0, 255), (byte)Random.Next(0, 255)));
            while (Brushes.Contains(resultBrush));

            return resultBrush;
        }

        public static Brush Next() => GetUniqueBrush();
    }
}
