using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Windows;

namespace Twill.Processes.Search
{
    public class Searcher
    {

        public TypeSearch TypeSerach = TypeSearch.Desktop;

        public readonly DesktopSearcher DesktopSearcher = new DesktopSearcher();


        internal void UpDateMilieu(Milieu milieu)
        {
            if(TypeSerach.HasFlag(TypeSearch.Desktop))
            {
                var handleList = DesktopSearcher.FindDesktopProcess();

                foreach (var handle in handleList)
                {
                    milieu.Add(handle);
                }
            }
        }
    }
}
