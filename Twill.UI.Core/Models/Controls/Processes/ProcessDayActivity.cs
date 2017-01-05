using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Twill.UI.Core.Models.Controls.Processes
{
    public class ProcessDayActivity : ViewModelBase, IProcessDayActivity<ProcessWork, GroundWorkState>
    {
        public ProcessDayActivity()
        {
            if(IsInDesignMode)
            {
                Name = "For test";
                Activities = new ObservableCollection<ProcessWork>()
                {
                    new ProcessWork()
                    {
                         Start = DateTime.Now,
                         End = DateTime.Now.AddHours(15)
                    }
                };
            }
        }


        public Brush Brush { get; } = Tools.WPF.UniqueColorGenerator.Next();


        private ObservableCollection<ProcessWork> activities;
        public ObservableCollection<ProcessWork> Activities
        {
            get { return activities; }
            set { Set(ref activities, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { Set(ref name, value); }
        }
    }
}
