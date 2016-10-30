using System;
using System.Collections.Generic;
using System.Diagnostics;
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


        internal Dictionary<string, string> GetActiveProcess()
        {
            var handle = DesktopSearcher.GetActiveHandle();

            string WindowTitleText;
            if (!DesktopSearcher.TryGetWindowText(handle, out WindowTitleText))
                return null;

            using (var process = DesktopSearcher.GetActiveProcess(handle))
                return  new Dictionary<string, string>()
                {
                     [nameof(process.ProcessName)] = process.ProcessName,
                     [nameof(WindowTitleText)] = WindowTitleText
                };
        }

        internal void UpDateMilieu(Milieu milieu)
        {
            if(TypeSerach.HasFlag(TypeSearch.Desktop))
            {
                var handleList = DesktopSearcher.FindDesktopProcess();

                foreach (var handle in handleList)
                {
                    milieu.Add(DesktopSearcher.GetActiveProcess(handle));
                }
            }
        }
    }
}
