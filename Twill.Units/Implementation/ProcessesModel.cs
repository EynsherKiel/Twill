using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.UI.Core.Models.Controls.Processes;

namespace Twill.Units.Implementation
{
    [TestClass]
    public class ProcessesModel
    {
        [TestMethod]
        public void DayActivityAnalisysFullArray()
        {
            var dayActivityAnalysis = new DayActivityAnalysis(false);

            var denev = new ProcessDayActivity() { Name = "denev.exe" };
            var twill = new ProcessDayActivity() { Name = "twill.exe" };
            var chrome = new ProcessDayActivity() { Name = "chrome.exe" };

            var now = DateTime.Now.Date;


            ICollection<ProcessActivity> list = new List<ProcessActivity>()
            {
                new ProcessActivity()
                {
                    LinkProcess = denev,
                    GroundWorkStates = new ObservableCollection<GroundWorkState>() { new GroundWorkState() { Title = nameof(denev)} },
                    Start = now.AddHours(9).AddMinutes(15).TimeOfDay,
                    End = now.AddHours(9).AddMinutes(15).AddSeconds(27).TimeOfDay
                },

                new ProcessActivity()
                {
                    LinkProcess = twill,
                    GroundWorkStates = new ObservableCollection<GroundWorkState>() { new GroundWorkState() { Title = nameof(twill) } },
                    Start =  now.AddHours(9).AddMinutes(15).AddSeconds(27).TimeOfDay,
                    End = now.AddHours(9).AddMinutes(23).AddSeconds(27).TimeOfDay
                },

                new ProcessActivity()
                {
                    LinkProcess = denev,
                    GroundWorkStates = new ObservableCollection<GroundWorkState>() { new GroundWorkState() { Title = nameof(denev) } },
                    Start = now.AddHours(9).AddMinutes(25).AddSeconds(27).TimeOfDay,
                    End = now.AddHours(9).AddMinutes(45).AddSeconds(27).TimeOfDay
                },

                new ProcessActivity()
                {
                    LinkProcess = twill,
                    GroundWorkStates = new ObservableCollection<GroundWorkState>() { new GroundWorkState() { Title = nameof(twill) } },
                    Start =  now.AddHours(9).AddMinutes(45).AddSeconds(27).TimeOfDay,
                    End = now.AddHours(9).AddMinutes(46).AddSeconds(27).TimeOfDay
                },

                // sites

                new ProcessActivity()
                {
                    LinkProcess = chrome,
                    GroundWorkStates = new ObservableCollection<GroundWorkState>() { new GroundWorkState() { Title = $"{nameof(chrome)} site 1" } },
                    Start =  now.AddHours(9).AddMinutes(46).AddSeconds(27).TimeOfDay,
                    End = now.AddHours(9).AddMinutes(46).AddSeconds(28).TimeOfDay
                },

                new ProcessActivity()
                {
                    LinkProcess = chrome,
                    GroundWorkStates = new ObservableCollection<GroundWorkState>() { new GroundWorkState() { Title = $"{nameof(chrome)} site 2" } },
                    Start =  now.AddHours(9).AddMinutes(46).AddSeconds(32).TimeOfDay,
                    End = now.AddHours(9).AddMinutes(46).AddSeconds(37).TimeOfDay
                },

                new ProcessActivity()
                {
                    LinkProcess = chrome,
                    GroundWorkStates = new ObservableCollection<GroundWorkState>() { new GroundWorkState() { Title = $"{nameof(chrome)} site 3" } },
                    Start =  now.AddHours(9).AddMinutes(46).AddSeconds(37).TimeOfDay,
                    End = now.AddHours(9).AddMinutes(46).AddSeconds(42).TimeOfDay
                },

                new ProcessActivity()
                {
                    LinkProcess = chrome,
                    GroundWorkStates = new ObservableCollection<GroundWorkState>() { new GroundWorkState() { Title = $"{nameof(chrome)} site 4" } },
                    Start =  now.AddHours(9).AddMinutes(46).AddSeconds(42).TimeOfDay,
                    End = now.AddHours(9).AddMinutes(46).AddSeconds(47).TimeOfDay
                },
                new ProcessActivity()
                {
                    LinkProcess = chrome,
                    GroundWorkStates = new ObservableCollection<GroundWorkState>() { new GroundWorkState() { Title = $"{nameof(chrome)} site 4" } },
                    Start =  now.AddHours(9).AddMinutes(46).AddSeconds(47).TimeOfDay,
                    End = now.AddHours(10).AddMinutes(46).AddSeconds(47).TimeOfDay
                },
            };

            dayActivityAnalysis.Analyse(list);

        }
    }
}
