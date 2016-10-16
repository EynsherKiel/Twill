using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Twill.UI.Core.Tools
{
    public class Notify : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        private SynchronizationContext Context = SynchronizationContext.Current;

        protected void AsyncAction<T>(Action<T> action, T data) where T : class
        {
            if (SynchronizationContext.Current == Context)
            {
                action(data);
            }
            else
            {
                Context.Post((e) =>
                {
                    var asyncData = e as T;

                    if (asyncData == null)
                        return;

                    action?.Invoke(asyncData); ;
                }, data);
            }
        }
    }
}
