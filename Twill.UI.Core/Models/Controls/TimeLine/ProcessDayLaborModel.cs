﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Media;

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

        public Brush Brush { get; set; } = Twill.Tools.WPF.UniqueColorGenerator.Next();

        private string processName = string.Empty;
        public string ProcessName
        {
            get { return processName; }
            set { Set(nameof(ProcessName), ref processName, value); }
        }

        private ObservableCollection<ProcessLaborModel> processLabors = new ObservableCollection<ProcessLaborModel>();
        public ObservableCollection<ProcessLaborModel> ProcessLabors
        {
            get { return processLabors; }
            set { Set(nameof(ProcessLabors), ref processLabors, value); }
        }
    }
}