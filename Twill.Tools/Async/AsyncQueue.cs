using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Twill.Tools.Async
{
    public class AsyncQueue : IDisposable
    {
        public AsyncQueue()
        {
            Task.Factory.StartNew(() => RunConsumer());
        }

        private BlockingCollection<Tuple<Action, ManualResetEvent>> queue = new BlockingCollection<Tuple<Action, ManualResetEvent>>();

        public Action MayDispose = null;

        private object SyncRoot = new object();

        public int? Count => queue?.Count;

        private void RunConsumer()
        {
            using (queue)
            {
                foreach (var tuple in queue.GetConsumingEnumerable())
                {
                    tuple.Item1?.Invoke();
                    tuple.Item2?.Set();

                    lock (SyncRoot)
                        if (queue.Count == 0)
                            MayDispose?.Invoke();
                }
            }
        }

        public void AsyncAdd(Action action)
        {
            lock (SyncRoot)
                queue?.Add(new Tuple<Action, ManualResetEvent>(action, null));
        }

        public void Add(Action action)
        {
            using (var token = new ManualResetEvent(false))
            {
                lock (SyncRoot)
                    queue?.Add(new Tuple<Action, ManualResetEvent>(action, token));
                token.WaitOne();

            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    queue?.CompleteAdding();
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
