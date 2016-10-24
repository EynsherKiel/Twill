using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;


namespace Twill.UI.Core.Models.Controls.TimeLine
{
    public class ProcessDayLaborModel : ViewModelBase
    {
        public ProcessDayLaborModel()
        {
            if (IsInDesignMode)
            {
                ProcessLabors = new ObservableCollection<ProcessLaborModel>() { new ProcessLaborModel(), new ProcessLaborModel() };
                ProcessName = "Twill";
            }
        }

        private string processName = string.Empty;
        public string ProcessName
        {
            get { return processName; }
            set
            {
                processName = value;
                RaisePropertyChanged(nameof(ProcessName));
            }
        }

        private ObservableCollection<ProcessLaborModel> processLabors = new ObservableCollection<ProcessLaborModel>();
        public ObservableCollection<ProcessLaborModel> ProcessLabors
        {
            get { return processLabors; }
            set
            {
                processLabors = value;
                RaisePropertyChanged(nameof(ProcessLabors));
            }
        }
    }
}
