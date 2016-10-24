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

namespace Twill.UI.Core.Models.Content
{
    public class MonitorPageModel : ViewModelBase
    {
        public MonitorPageModel()
        {
            Investigator.NewMilieu += GetMilieu;

            AddNewRecordCommand = new RelayCommand<List<object>>(AddNewRecordMethod);


            ProcessDayLabors = Controller.GetLabor<ObservableCollection<ProcessDayLaborModel>>(DateTime.Now); 
        }
         

        public Storage.Barrier.Controller Controller = new Storage.Barrier.Controller();

        public Investigator Investigator = new Investigator();

        public ICommand AddNewRecordCommand { get; }
        

        private ObservableCollection<ProcessDayLaborModel> processDayLabors = new ObservableCollection<ProcessDayLaborModel>();
        public ObservableCollection<ProcessDayLaborModel> ProcessDayLabors
        {
            get { return processDayLabors; }
            set
            {
                processDayLabors = value;
                RaisePropertyChanged(nameof(ProcessDayLabors));
            }
        }

        private ObservableCollection<ReportModel> reports = new ObservableCollection<ReportModel>() { new ReportModel() { Text = "Hello!" } };
        public ObservableCollection<ReportModel> Reports
        {
            get { return reports; }
            set
            {
                reports = value;
                RaisePropertyChanged(nameof(Reports));
            }
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

        private void GetMilieu(Milieu milieu) => AsyncAction(AddApplications, milieu.Applications);

        private void AddApplications(List<Application> applications)
        {
            var appNames = applications.Select(application => application.Name).ToList();

            //foreach (var appName in appNames)
            //{
            //    if (!ProcessDayLabors.Contains(appName))
            //        ProcessDayLabors.Add(appName);
            //}

            //foreach (var deletext in ProcessDayLabors.Where(text => !appNames.Contains(text)).ToList())
            //{
            //    ProcessDayLabors.Remove(deletext);
            //}
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
            Investigator.NewMilieu -= GetMilieu;
        }
    }
}
