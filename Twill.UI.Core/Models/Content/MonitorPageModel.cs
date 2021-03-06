﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Linq;
using System.Windows.Input;
using Twill.Storage.Files.Reports;
using Twill.Tools.Async;
using Twill.UI.Core.Models.Content.Settings;
using Twill.UI.Core.Models.Controls.Processes;
using Twill.UI.Core.Models.Controls.TimeLine;

namespace Twill.UI.Core.Models.Content
{
    public class MonitorPageModel : ViewModelBase
    {
        public MonitorPageModel()
        {
            if (IsInDesignMode)
            {
                GeneralPageModel = new GeneralPageModel();
            }
            else
            {
                GeneralPageModel = StorageHelperManager.Load<GeneralPageModel>();
            }
        }

        private StorageHelper.Manager StorageHelperManager = new StorageHelper.Manager(); 

        public ICommand SaveReportsCommand => new RelayCommand(SaveReportsMethod);

        private void SaveReportsMethod()
        {
            var dayreport = DayMonitor.GetDayReport();

            if (dayreport.Reports.Count == 0)
                return;

            var path = DayMonitor.ReportsModel.ReportsRegulator.Save(dayreport, GeneralPageModel.ToType);

            System.Windows.MessageBox.Show(path);

        }

        private DayMonitor dayMonitor = new DayMonitor();
        public DayMonitor DayMonitor
        {
            get { return dayMonitor; }
            set { Set(ref dayMonitor, value); }
        }

        private GeneralPageModel generalPageModel;
        public GeneralPageModel GeneralPageModel
        {
            get { return generalPageModel; }
            set { Set(ref generalPageModel, value); }
        }
    }
}
