using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Tools;

namespace Twill.Processes.Tracking
{
    public class Monitor
    {
        public Monitor()
        {
            Tracker.Data += GettedData;
        }

        private Tracker Tracker = Singleton<Tracker>.Instance;


        public Action<string> Data = delegate { };

        private void GettedData(string data) => Data(data);


        public void Stop() => Singleton<Tracker>.Dispose();

        ~Monitor()
        {
            Tracker.Data -= GettedData;
        }
    }
}
