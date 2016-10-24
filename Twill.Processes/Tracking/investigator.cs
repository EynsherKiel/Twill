using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Windows;
using Twill.Tools.Architecture;

namespace Twill.Processes.Tracking
{
    // Needed only for create Singleton instance Monitor
    public class Investigator
    {
        public Investigator()
        {
            Monitor.NewMilieu += GettedData;
        }

        private Monitor Monitor = Singleton<Monitor>.Instance;


        public Action<Milieu> NewMilieu = delegate { };

        private void GettedData(Milieu obj) => NewMilieu(obj); 

        //public void Stop() => Singleton<Monitor>.Dispose();

        ~Investigator()
        {
            Monitor.NewMilieu -= GettedData;
        }
    }
}
