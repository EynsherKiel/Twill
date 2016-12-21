using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;
using Twill.Processes.Search;
using Twill.Processes.Windows;
using System.Threading;

namespace Twill.Units.Implementation
{
    [TestClass]
    public class Processes
    {
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
                Console.WriteLine($"{selectedprocess.Handle} - {selectedprocess.Title}");

                Assert.IsTrue(handles.Contains(selectedprocess.Handle), "title was not foundid how desktop process");

                handles.Remove(selectedprocess.Handle);
            }

            foreach (var handle in handles)
            {
                Console.WriteLine(handle);
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

                environ.UpdateEvent += obj =>
                {
                    // cool 

                    Assert.AreNotEqual(obj.Processes.Count, 0, "vs - is not desktop process ? >_<");

                    if (obj.Lead != null)
                    {
                        Console.WriteLine("Lead process : ");
                        Console.WriteLine("     {0}", obj.Lead.ToString());
                    }

                    Console.WriteLine("All process :");

                    foreach (var process in obj.Processes)
                    {
                        Console.WriteLine("     {0}", process.ToString());
                    }

                    token.Set();
                };

                token.WaitOne();
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

            monitor.UpDateEvent += (_this) =>
            {
                upDateCount++;
                processesCount.Add(_this.ProcessMonitor.Processes?.Count ?? 0);
                Console.WriteLine(_this.ProcessMonitor.Lead?.ProcessName ?? string.Empty);
            };

            Thread.Sleep(TimeSpan.FromSeconds(time.Seconds * wait));

            Assert.IsTrue(upDateCount >= wait, "Not updates");
            Assert.IsTrue(processesCount.Sum() != 0, "Not work");
        }
         
    }
}
