using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Twill.Processes.Search;
using Twill.Processes.Windows;

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

                environ.UpDateEvent += obj =>
                {
                    // cool 

                    Assert.AreNotEqual(obj.Processes.Count, 0, "vs - is not desktop process ? >_<");

                    foreach (var process in obj.Processes)
                    {
                        Console.WriteLine($"{process.Name} - {process.Title} - {process.CreationDate} - {process.WorkTime}");
                    }

                    token.Set();
                };

                token.WaitOne();
            }
        }
         
    }
}
