using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Processes.Tools
{
    internal static class Singleton<T> where T : class, IDisposable, new()
    {
        private static volatile T instance;
        private static object syncRoot = new object();

        public static T Instance
        {
            get
            {
                if (instance == null)
                    lock (syncRoot)
                        if (instance == null)
                            instance = new T();

                return instance;
            }
        }

        #region IDisposable Support
        private static bool disposedValue = false;

        private static void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (instance == null)
                        lock (syncRoot)
                            instance?.Dispose();
                }

                disposedValue = true;
            }
        }

        public static void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
