using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces;
using Twill.Processes.Interfaces.WMI;

namespace Twill.Processes.WMI
{
    internal class Manager
    {
        public const string CIMV2 = "root\\CIMV2";
        public const string ClassName = "Win32_Process";

        private IEnumerable<ManagementObject> ExecuteResearch(IntPtr handle)
        {
            using (var searcher = new ManagementObjectSearcher(CIMV2, $"SELECT * FROM {ClassName} WHERE ProcessId = '{handle}'"))
            {
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    yield return queryObj;
                }
            }
        }
         
        private DateTime GetDate(ManagementObject obj, string fieldName)
        { 
            var creationtime = obj[fieldName] as string;
            if (creationtime == null)
                return default(DateTime); 

            return ManagementDateTimeConverter.ToDateTime(creationtime);
        }

        internal void UpDateProcess(IProcess wmiprocess, IntPtr handle)
        {
            var obj = ExecuteResearch(handle).FirstOrDefault();
            if (obj == null)
                return;

            wmiprocess.Name = obj[nameof(IProcess.Name)] as string ?? string.Empty;
            wmiprocess.CreationDate = GetDate(obj, nameof(IProcess.CreationDate));
        }
    }
}
