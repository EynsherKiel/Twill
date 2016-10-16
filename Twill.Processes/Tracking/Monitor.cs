using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Search;
using Twill.Processes.Tools;
using Twill.Processes.Windows;

namespace Twill.Processes.Tracking
{
    public class Monitor : IDisposable  
    {
        public Monitor()
        {
            Timer = new Timer(UpDate);
        }

        public readonly Timer Timer;

        public readonly Milieu Milieu = new Milieu();

        public readonly Searcher Seracher = new Searcher();


        public Action<Milieu> NewMilieu = delegate { };


        private void UpDate(object state)
        {
            Seracher.UpDateMilieu(Milieu);
            NewMilieu?.Invoke(Milieu);
        }


        #region IDisposable Support
        private bool disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Timer?.Dispose();
                    Milieu?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
