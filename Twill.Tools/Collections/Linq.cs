using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Tools.Collections
{
    public static class Linq
    {
        public static void UpdateLinksWithFilter<T, T2>(this IEnumerable<T> list, IEnumerable<T2> secondlist, Func<T, T2, bool> predicate, ref IList<T> changedList)
        {
            var elements = list.Where(x => secondlist.Count(el => predicate(x, el)) != 0).ToList();

            foreach (var item in list)
            {
                if (elements.Contains(item))
                {
                    changedList.Remove(item);
                    continue;
                }

                if (!changedList.Contains(item))
                {
                    changedList.Add(item);
                    continue;
                }
            }

            foreach (var item in changedList.Where(el => !list.Contains(el)).ToList())
            {
                changedList.Remove(item);
            }
        }
    }
}
