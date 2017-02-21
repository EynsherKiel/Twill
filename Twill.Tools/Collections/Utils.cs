using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Tools.Collections
{
    public static class Utils
    {
        public static TCollection CreateCollection<TCollection, TData>(IEnumerable<TData> enumerable) where TCollection : ICollection<TData>, new()
        {
            var list = new TCollection();

            foreach (var item in enumerable)
                list.Add(item);

            return list;
        }
    }
}
