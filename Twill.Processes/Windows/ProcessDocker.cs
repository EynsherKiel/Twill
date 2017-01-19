using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Models;

namespace Twill.Processes.Windows
{
    public class ProcessDocker
    {
        public ProcessDocker(string name, ICollection<Process> processes)
        {
            if (string.IsNullOrEmpty(name))
                return;

            Name = name;

            CtorUp(processes);
        }

        public string Name { get; }
        public Process Lead { get; private set; }
        public bool IsTerminated => Processes.Count == 0;

        private List<Process> Processes = new List<Process>();


        public void UpTitles() => Processes.ForEach(process => process.UpTitle());

        public string[] GetTitles()
        {
            UpTitles();
            return Processes.Select(process => process.Name).ToArray();
        }

        public bool Contain(BaseProcess baseProcess) => Processes.Contains(baseProcess);

        public Process FirstOrDefault(IntPtr ptr) => Processes.FirstOrDefault(process => process.Handle == ptr);

        // if no more elements - return false
        // IsTerminated == false
        // Please destroy obj this class
        public bool Up(ICollection<Process> processes) => !IsTerminated && CtorUp(processes);

        private bool CtorUp(ICollection<Process> processes)
        {
            if (processes.Count == 0)
                return false;

            var currprocesses = processes.Where(process => process.Name == Name).ToList();

            if (currprocesses.Count == 0)
                return false;

            Processes.AddRange(currprocesses.Where(process => !Contain(process)));

            Processes.RemoveAll(process => !currprocesses.Contains(process));

            return !IsTerminated;
        }

        public static ProcessDocker SetLead(ICollection<ProcessDocker> collection, BaseProcess lead)
        {
            if (collection == null)
                return null;

            foreach (var item in collection.Where(item => item != null))
                item.Lead = null;

            if (lead == null)
                return null;

            var leadProcessDocker = collection.FirstOrDefault(process => process.Contain(lead));
            if (leadProcessDocker != null)
            {
                leadProcessDocker.Lead = leadProcessDocker.FirstOrDefault(lead.Handle);
            }

            return leadProcessDocker;
        }
    }
}
