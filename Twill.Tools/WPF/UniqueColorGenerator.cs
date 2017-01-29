using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Twill.Tools.WPF
{
    public static class UniqueColorGenerator
    {
        private static List<int> BrushesIndexes = new List<int>();
        private static Type BrushesType = typeof(Brushes);

        private static Random Random = new Random();
         

        private static Brush GetUniqueBrush()
        {
            var result = System.Windows.Media.Brushes.Transparent;

            var properties = BrushesType.GetProperties();

            if (BrushesIndexes.Count == properties.Length)
            {
                Console.WriteLine("UniqueColorGenerator is full");
                BrushesIndexes.Clear();
            }

            int random = Random.Next(properties.Length);
            while (BrushesIndexes.Contains(random))
                random = Random.Next(properties.Length);
             
            return (Brush)properties[random].GetValue(null, null);
        }

        public static Brush Next() => GetUniqueBrush();
    }
}
