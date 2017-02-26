using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using planertype = System.Collections.Generic.Dictionary<System.DayOfWeek, System.Collections.Generic.List<System.TimeSpan>>;

namespace Twill.Tools.Async
{
    public class ActionPlanner : Events.ISmartWeakEventManager
    {
        public ActionPlanner()
        {
        }

        private Task AsyncTask = null;

        private object SyncRoot = new object();

        public event EventHandler<EventArgs> Event;

        private planertype Planners = new planertype()
        {
            {  DayOfWeek.Monday , new List<TimeSpan>() },
            {  DayOfWeek.Tuesday , new List<TimeSpan>() },
            {  DayOfWeek.Wednesday , new List<TimeSpan>() },
            {  DayOfWeek.Thursday , new List<TimeSpan>() },
            {  DayOfWeek.Friday , new List<TimeSpan>() },
            {  DayOfWeek.Saturday , new List<TimeSpan>() },
            {  DayOfWeek.Sunday , new List<TimeSpan>() }
        };

        public void Start()
        {
            if (AsyncTask != null)
                return;

            AsyncTask = Task.Factory.StartNew(Work);
        }

        public void Stop()
        {
            if (AsyncTask == null)
                return;

            throw new NotImplementedException();
        }

        public Dictionary<DayOfWeek, ObservableCollection<TimeSpan>> GetPlanner()
            => Planners.ToDictionary(el => el.Key, val => new ObservableCollection<TimeSpan>(val.Value));

        public void Add(DayOfWeek dayofweek, TimeSpan time)
        {
            if (time.TotalMinutes > Math.Position.MinutesInDay - 1.0)
                return;

            if (Planners[dayofweek].Contains(time))
                return;

            Planners[dayofweek].Add(time);
            Planners[dayofweek].Sort();

            Pulse();
        }

        public void Remove(DayOfWeek dayofweek, TimeSpan time)
        {
            if (!Planners[dayofweek].Contains(time))
                return;

            Planners[dayofweek].Remove(time);

            Pulse();
        }

        private void Work()
        {
            lock (SyncRoot)
            {
                while (true)
                {
                    var next = Next();

                    if (next == null)
                        Monitor.Wait(SyncRoot);
                    else
                    {
                        var mill = (int)GetMillisecondsTimeWait(next.Item1, next.Item2);

                        var isgoodwait = !Monitor.Wait(SyncRoot, mill);

                        var checknext = Next();

                        if (Planners[next.Item1].Contains(next.Item2))
                        { 
                            if (isgoodwait)
                            {
                                Event(this, new EventArgs()); 
                            }
                        }
                    }
                }
            }
        }

        private void Pulse()
        {
            if (AsyncTask != null)
            {
                lock (SyncRoot)
                { 
                    Monitor.Pulse(SyncRoot);
                }
            }
        }

        private double GetMillisecondsTimeWait(DayOfWeek dayofweek, TimeSpan time)
        {
            var now = DateTime.Now;
            var nowdaytime = now.TimeOfDay;
            var nowdayofweek = now.DayOfWeek;
             
            int nextday = ((int)dayofweek - (int)nowdayofweek + 7) % 7;
            DateTime next = now.Date;
            
            next = next.AddMilliseconds(time.TotalMilliseconds);

            if (nextday > 0)
            {
                next = next.AddDays(nextday);
            }
            else
            {
                if (now.TimeOfDay.TotalMilliseconds > time.TotalMilliseconds)
                {
                    next = next.AddDays(7);
                }
            }

            return (next - now).TotalMilliseconds;
        }

        private Tuple<DayOfWeek, TimeSpan> Next()
        {
            if (!Planners.Any(el => el.Value.Count == 0))
                return null;

            var now = DateTime.Now;
            var daytime = now.TimeOfDay;
            var dayofweek = now.DayOfWeek;

            for (int i = 0; i < 8; i++)
            {
                var dayindex = (DayOfWeek)(((int)dayofweek + i) % 7);
                IEnumerable<TimeSpan> spans;

                if (i == 0)
                    spans = Planners[dayindex].Where(el => el > daytime);
                else
                if (i == 7)
                    spans = Planners[dayindex].Where(el => el < daytime);
                else
                    spans = Planners[dayindex];

                var first = spans.FirstOrDefault();

                if (first != TimeSpan.Zero)
                    return new Tuple<DayOfWeek, TimeSpan>(dayindex, first);
            }

            return null;
        }

        public void SubscribeUpDateEvent(IWeakEventListener obj) =>
            Events.SmartWeakEventManager<ActionPlanner>.AddListener(this, obj);

        public void UnSubscribeUpDateEvent(IWeakEventListener obj) =>
            Events.SmartWeakEventManager<ActionPlanner>.RemoveListener(this, obj);

    }
}
