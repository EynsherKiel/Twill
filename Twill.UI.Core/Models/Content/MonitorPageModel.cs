using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Twill.Processes.Tracking;
using Twill.Processes.Windows;
using Twill.UI.Core.Data;
using Twill.UI.Core.Models.Controls.TimeLine;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Twill.Tools.WPF;

namespace Twill.UI.Core.Models.Content
{
    public class MonitorPageModel : ViewModelBase
    {
        public MonitorPageModel()
        {
            Investigator = new Investigator();

            Investigator.NewMilieu += GetMilieu;
            Investigator.ChangeWorkingApplication += GetChangeWorkingApplication;

            AddNewRecordCommand = new RelayCommand<List<object>>(AddNewRecordMethod);
            RemoveTopElementCommand = new RelayCommand<ReportModel>(RemoveTopElementMethod);
            RemoveBottomElementCommand = new RelayCommand<ReportModel>(RemoveBottomElementMethod);

            ProcessDayLabors = Controller.GetLabor<SortableObservableCollection<ProcessDayLaborModel>>(DateTime.Now);
        }

        public MonitorPageModel(DateTime time)
        {
            ProcessDayLabors = Controller.GetLabor<SortableObservableCollection<ProcessDayLaborModel>>(time);

            AddNewRecordCommand = new RelayCommand<List<object>>(AddNewRecordMethod);
            RemoveTopElementCommand = new RelayCommand<ReportModel>(RemoveTopElementMethod);
            RemoveBottomElementCommand = new RelayCommand<ReportModel>(RemoveBottomElementMethod);
        }

        public Investigator Investigator = null;

        private object SyncRoot = new object();

        public Storage.Barrier.Controller Controller = new Storage.Barrier.Controller();


        public ICommand AddNewRecordCommand { get; }
        public ICommand RemoveTopElementCommand { get; }
        public ICommand RemoveBottomElementCommand { get; }


        private SortableObservableCollection<ProcessDayLaborModel> processDayLabors = new SortableObservableCollection<ProcessDayLaborModel>();
        public SortableObservableCollection<ProcessDayLaborModel> ProcessDayLabors
        {
            get { return processDayLabors; }
            set { Set(nameof(ProcessDayLabors), ref processDayLabors, value); }
        }

        private ObservableCollection<ReportModel> reports = new ObservableCollection<ReportModel>() { new ReportModel() };
        public ObservableCollection<ReportModel> Reports
        {
            get { return reports; }
            set { Set(nameof(Reports), ref reports, value); }
        }


        private void RemoveTopElementMethod(ReportModel report)
        {
            if (report == null)
                return;

            var findIndexReport = Reports.IndexOf(report);

            if (findIndexReport < 1)
                return;

            report.Start = Reports[findIndexReport - 1].Start;
            report.End = report.End;
            Reports.RemoveAt(findIndexReport - 1);

        }

        private void RemoveBottomElementMethod(ReportModel report)
        {
            if (report == null)
                return;

            var findIndexReport = Reports.IndexOf(report);

            if (findIndexReport == Reports.Count - 1)
                return;

            report.End = Reports[findIndexReport + 1].End;
            Reports.RemoveAt(findIndexReport + 1);

        }

        private void AddNewRecordMethod(List<object> values)
        {
            if (values == null)
                return;

            if (values.Count < 2)
                return;

            if (values[0] is double == false)
                return;
            var height = (double)values[0];

            if (values[1] is double == false)
                return;
            var mouseY = (double)values[1];

            var minutesInPixel = Labor.MinetsInDay / height;

            var choisenMinute = mouseY * minutesInPixel;

            var choisenDateTime = DateTime.Now.Date.AddMinutes(choisenMinute);

            var findRepor = Reports.FirstOrDefault(rep => rep.Start <= choisenDateTime && rep.End >= choisenDateTime);

            if (findRepor == null)
                return;

            var findIndexReport = Reports.IndexOf(findRepor);

            Reports.Insert(findIndexReport + 1, new ReportModel() { Start = choisenDateTime, End = findRepor.End } );
            findRepor.End = choisenDateTime;
        }

        private void GetMilieu(Milieu milieu) => AsyncAction(GetMilieuInCurrentContext, milieu.Applications);
        private void GetChangeWorkingApplication(Application obj) => AsyncAction(GetChangeWorkingApplicationInCurrentContext, obj);


        private void GetChangeWorkingApplicationInCurrentContext(Application application)
        {
            var procdaylab = ProcessDayLabors.FirstOrDefault(pdl => pdl.ProcessName == application.Name);
            if (procdaylab == null)
                return;

            procdaylab.ProcessLabors.Last().Headers.Add(new ReportModel() { Text = application.LastName, Start = DateTime.Now, End = DateTime.Now });

            if (!IsInDesignMode)
                Controller.SaveLabor(ProcessDayLabors, DateTime.Now);

            RaisePropertyChanged(nameof(ProcessDayLabors));
        } 

        private void GetMilieuInCurrentContext(List<Application> applications)
        {
            //return;
            lock (SyncRoot)
            {

                foreach (var application in applications.ToList())
                {
                    var pdl = ProcessDayLabors.FirstOrDefault(daylabors => daylabors.ProcessName == application.Name);

                    if (pdl == null)
                    {
                        ProcessDayLabors.Add(new ProcessDayLaborModel()
                        {
                            ProcessName = application.Name,
                            ProcessLabors = new ObservableCollection<ProcessLaborModel>()
                            {
                                 new ProcessLaborModel()
                                 {
                                     Start = application.StartWork ?? DateTime.Now,
                                     End = DateTime.Now
                                 }
                            }
                        });
                        continue;
                    }


                    ProcessLaborModel processLaborModel = null;

                    if (pdl.ProcessLabors.Last().Start == application.StartWork)
                        processLaborModel = pdl.ProcessLabors.Last();
                    else
                    {
                        processLaborModel = new ProcessLaborModel() { Start = application.StartWork ?? DateTime.Now, End = DateTime.Now };
                        pdl.ProcessLabors.Add(processLaborModel);
                    }

                    processLaborModel.End = DateTime.Now;
                }

                ProcessDayLabors.Sort(procdaylabor => procdaylabor.ProcessLabors.Sum(processLabors => processLabors.Headers.Count), System.ComponentModel.ListSortDirection.Descending);
            }
            if (!IsInDesignMode)
                Controller.SaveLabor(ProcessDayLabors, DateTime.Now);
        }

        private SynchronizationContext Context = SynchronizationContext.Current;

        protected void AsyncAction<T>(Action<T> action, T data) where T : class
        {
            if (SynchronizationContext.Current == Context)
            {
                action(data);
            }
            else
            {
                Context.Post((e) =>
                {
                    var asyncData = e as T;

                    if (asyncData == null)
                        return;

                    action?.Invoke(asyncData); ;
                }, data);
            }
        }


        ~MonitorPageModel()
        {
            if (Investigator != null)
            {
                Investigator.NewMilieu -= GetMilieu;
                Investigator.ChangeWorkingApplication -= GetChangeWorkingApplication;
            }
        }
    }
}
