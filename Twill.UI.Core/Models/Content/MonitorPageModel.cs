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
using Twill.UI.Core.Tools;

namespace Twill.UI.Core.Models.Content
{
    public class MonitorPageModel : Notify
    {
        public MonitorPageModel()
        {
            Investigator.NewMilieu += GetMilieu;


            AddNewRecordCommand = new RelayCommand<List<object>>(AddNewRecordMethod);

        }


        public Investigator Investigator = new Investigator();

        public ICommand AddNewRecordCommand { get; }
        

        private ObservableCollection<string> texts = new ObservableCollection<string>();
        public ObservableCollection<string> Texts
        {
            get { return texts; }
            set
            {
                texts = value;
                RaisePropertyChanged(nameof(Texts));
            }
        }

        private ObservableCollection<Report> reports = new ObservableCollection<Report>() { new Report() { Text = "Hello!" } };
        public ObservableCollection<Report> Reports
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

            var minutesInPixel = Report.MinetsInDay / height;

            var choisenMinute = mouseY * minutesInPixel;

            var choisenDateTime = DateTime.Now.Date.AddMinutes(choisenMinute);

            var findRepor = Reports.FirstOrDefault(rep => rep.Start <= choisenDateTime && rep.End >= choisenDateTime);

            if (findRepor == null)
                return;

            var findIndexReport = Reports.IndexOf(findRepor);

            Reports.Insert(findIndexReport + 1, new Report() { Start = choisenDateTime, End = findRepor.End } );
            findRepor.End = choisenDateTime;
        }

        private void GetMilieu(Milieu milieu) => AsyncAction(AddApplications, milieu.Applications);


        private void AddApplications(List<Application> applications)
        {
            var appNames = applications.Select(application => application.Name).ToList();

            foreach (var appName in appNames)
            {
                if (!Texts.Contains(appName))
                    Texts.Add(appName);
            }

            foreach (var deletext in Texts.Where(text => !appNames.Contains(text)).ToList())
            {
                Texts.Remove(deletext);
            }
        }


        ~MonitorPageModel()
        {
            Investigator.NewMilieu -= GetMilieu;
        }
    }
}
