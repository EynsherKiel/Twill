using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Twill.Tools.Events
{
    public interface ISmartWeakEventManager
    {
        event EventHandler<EventArgs> Event;

        void SubscribeUpDateEvent(IWeakEventListener obj);
        void UnSubscribeUpDateEvent(IWeakEventListener obj);
    }
}
