using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;

namespace Twill.UI.Core.Models.Controls.Processes
{
    public class GroundWorkState : ViewModelBase, IGroundWorkState
    {

        private DateTime? endWork;
        public DateTime? EndWork
        {
            get { return endWork; }
            set
            {
                Set(ref endWork, value);
                RaisePropertyChanged(nameof(UsingTime));
            }
        }

        private DateTime? startWork;
        public DateTime? StartWork
        {
            get { return startWork; }
            set
            {
                Set(ref startWork, value);
                RaisePropertyChanged(nameof(UsingTime));
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { Set(ref title, value); }
        }

        public TimeSpan? UsingTime => StartWork == null || EndWork == null ? null : EndWork - StartWork;
    }
}
