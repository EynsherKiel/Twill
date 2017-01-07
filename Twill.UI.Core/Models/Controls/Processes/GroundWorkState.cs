using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;

namespace Twill.UI.Core.Models.Controls.Processes
{
    public class GroundWorkState : Activity, IGroundWorkState
    {
        private string title;
        public string Title
        {
            get { return title; }
            set { Set(ref title, value); }
        }
    }
}
