using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Twill.Tools.Async
{
    public class SyncContext
    {
        public SyncContext(bool isCheckContext = false)
        {
            if (isCheckContext && Context == null)
                throw new NullReferenceException("Context is null");
        }

        public readonly SynchronizationContext Context = SynchronizationContext.Current;

        public void Action<T>(Action<T> action, T data) where T : class
        {
            if (SynchronizationContext.Current == Context)
            {
                action(data);
            }
            else
            {
                using (var manualResetEvent = new ManualResetEvent(false))
                {
                    Context.Post(e =>
                    {
                        try
                        {
                            InMainContext(action, e);
                        }
                        finally
                        {
                            manualResetEvent.Set();
                        }
                    }, data);

                    manualResetEvent.WaitOne();
                }

            }
        }

        public void AsyncAction<T>(Action<T> action, T data) where T : class
        {
            if (SynchronizationContext.Current == Context)
            {
                action(data);
            }
            else
            {
                Context.Post(e => InMainContext(action, e), data);
            }
        }

        private void InMainContext<T>(Action<T> action, object e) where T : class
        {
            var asyncData = e as T;

            if (asyncData == null)
                return;

            action?.Invoke(asyncData);
        }
    }
}
