using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Twill.Tools.Architecture;
using Twill.Tools.Async;

namespace Twill.Storage.Barrier
{
    internal class FileOperationController  
    {
        internal Dictionary<string, AsyncQueue> Operations = new Dictionary<string, AsyncQueue>();

        private void CheckPathWatcher(string path)
        {
            if (!Operations.ContainsKey(path))
                Operations.Add(path, new AsyncQueue() { MayDispose = () => { Operations[path].Dispose(); Operations.Remove(path); } });
        }

        public string Read(string path)
        {
            CheckPathWatcher(path);

            string data = string.Empty;

            Operations[path].Add(() =>
            {
                try
                {
                    data = System.IO.File.ReadAllText(path);
                }
                catch
                {
                    data = "Not reading file";
                }
            });

            return data;
        }

        public void Write(string path, string data)
        {
            CheckPathWatcher(path);

            Operations[path].AsyncAdd(() =>
            {
                try
                {
                    System.IO.File.WriteAllText(path, data);
                }
                catch
                {
                }
            });
        }


  
    }
}
