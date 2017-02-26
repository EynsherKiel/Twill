using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Twill.Units.Implementation
{
    [TestClass]
    public class ActionPlanner
    {
        [TestMethod]
        public void PerfomanceTest()
        {
            using (var token = new ManualResetEvent(false))
            {
                var planner = new Tools.Async.ActionPlanner();

                planner.Start();

                planner.Add(DateTime.Now.DayOfWeek, DateTime.Now.TimeOfDay.Add(TimeSpan.FromSeconds(2)));
             
                planner.Event += (obj, e) =>
                { 
                    token.Set();
                };

                planner.Add(DateTime.Now.DayOfWeek, DateTime.Now.TimeOfDay.Add(TimeSpan.FromSeconds(5)));

                token.WaitOne();
            }
        }

        [TestMethod]
        public void AddRemoveTest()
        {
            var now = DateTime.Now;

            var planner = new Tools.Async.ActionPlanner();

            planner.Start();

            planner.Add(now.DayOfWeek, now.TimeOfDay.Add(TimeSpan.FromSeconds(10)));

            planner.Remove(now.DayOfWeek, now.TimeOfDay.Add(TimeSpan.FromSeconds(10)));
        }
    }
}
