using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Search;
using Twill.Processes.Windows;
using System.Threading;
using System;

namespace Twill.Units.Implementation
{
    [TestClass]
    public class Processes
    {

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void FindAllWindows()
        {
           
            // desktop is default. 
            var searcher = new Searcher();

            var results = searcher.FindAllProcess();

            var selectedprocess = results.Item1;
            var handles = results.Item2;

            Assert.AreNotEqual(handles.Count, 0, "vs - is not desktop process ? >_<");

            if(selectedprocess != null)
            {
                TestContext.WriteLine($"{selectedprocess.Handle} - {selectedprocess.Title}");

                Assert.IsTrue(handles.Contains(selectedprocess.Handle), "title was not foundid how desktop process");

                handles.Remove(selectedprocess.Handle);
            }

            foreach (var handle in handles)
            {
                TestContext.WriteLine(handle.ToString());
            }

            var firstHandle = handles.FirstOrDefault();
            if (firstHandle == null)
                return;


        }

        [TestMethod]
        public void EnvironTest()
        {
            using (var token = new ManualResetEvent(false))
            {
                var environ = new Environ();

                environ.UpdateEvent += @this =>
                {
                    // cool 

                    Assert.AreNotEqual(@this.Processes.Count, 0, "vs - is not desktop process ? >_<");

                    TestContextWriteLineEnviron(@this);

                    token.Set();
                };

                token.WaitOne();
            }
        }

        private void TestContextWriteLineEnviron(Environ environ)
        {
            if (environ.Lead != null)
            {
                TestContext.WriteLine("Lead process : ");
                TestContext.WriteLine("     {0}", environ.Lead.ToString());
            }

            TestContext.WriteLine("All process :");

            foreach (var process in environ.Processes)
            {
                TestContext.WriteLine("     {0}", process.ToString());
            }
        }

        [TestMethod]
        public void MonitorSimpleWork()
        {
            const int wait = 10;

            var monitor = new Twill.Processes.Tracking.Monitor();

            var time = monitor.TimeUpdate;
            var upDateCount = 0;
            var processesCount = new List<int>();

            monitor.UpDateEvent += @this =>
            {
                upDateCount++;
                processesCount.Add(@this.ProcessMonitor.Processes?.Count ?? 0);
                TestContext.WriteLine(@this.ProcessMonitor.Lead?.Name ?? string.Empty);
            };

            Thread.Sleep(TimeSpan.FromSeconds(time.Seconds * wait));

            Assert.IsTrue(upDateCount >= wait, "Not updates");
            Assert.IsTrue(processesCount.Sum() != 0, "Not work");
        }


        [TestMethod]
        public void MonitorFilterTest()
        {
            using (var token = new ManualResetEvent(false))
            {
                var monitor = new Twill.Processes.Tracking.Monitor();

                monitor.FilterProcessNames.Clear();

                var processNames = new List<string>();

                monitor.UpDateEvent += @this =>
                {
                    if(processNames.Count == 0)
                        processNames.AddRange(@this.FilterProcessMonitor.Processes.Select(p => p.Name));

                    TestContext.WriteLine("");
                    TestContext.WriteLine("--------start list-------");

                    @this.FilterProcessMonitor.Processes.Select(p => p.Name).ToList().ForEach(p => TestContext.WriteLine(p ?? string.Empty));

                    TestContext.WriteLine("--------end list-------");
                    TestContext.WriteLine("");

                    token.Set();
                };

                token.WaitOne();

                Assert.IsTrue(processNames.Count != 0, "No desktop process");

                var deleteName = processNames.First();
                monitor.FilterProcessNames.Add(deleteName);
                TestContext.WriteLine($"Add to filter {deleteName}");
                processNames.Clear();

                token.Reset();
                token.WaitOne();

                Assert.IsTrue(processNames.Count != 0, "CollectionClear. Isn't updated.");
                Assert.IsFalse(processNames.Contains(deleteName), "Not delete name");

            }

        }
         
    }
}
