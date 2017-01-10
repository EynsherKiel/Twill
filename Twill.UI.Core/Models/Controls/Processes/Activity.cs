using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;

namespace Twill.UI.Core.Models.Controls.Processes
{
    public class Activity : Interval, IActivity
    {
        private bool isAlive;
        public bool IsAlive
        {
            get { return isAlive; }
            set { Set(ref isAlive, value); }
        }
    }
}
