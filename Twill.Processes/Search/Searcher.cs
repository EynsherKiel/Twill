using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Models;
//using Twill.Processes.Windows;

namespace Twill.Processes.Search
{
    public class Searcher
    {

        public TypeSearch TypeSerach = TypeSearch.Desktop;

        private readonly DesktopSearcher DesktopSearcher = new DesktopSearcher();

        public Tuple<BaseProcess, List<IntPtr>> FindAllProcess()
        {
            var handleList = new List<IntPtr>();

            if (TypeSerach.HasFlag(TypeSearch.Desktop))
            {
                handleList.AddRange(DesktopSearcher.FindDesktopProcess());
            }

            return new Tuple<BaseProcess, List<IntPtr>>(DesktopSearcher.GetActiveWindow(), handleList);
        }
        
    }
}
