using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Tools;

namespace Twill.Processes.Tracking
{
    internal class Tracker : IDisposable
    {
        public Tracker()
        {
            Timer = new Timer(Callback, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        private Timer Timer;

        public Action<string> Data = delegate { };

        private void Callback(object state)
        {
            Data(DateTime.Now.ToString());
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
