using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Twill.Storage.Files.Reports;

namespace Twill.UI.Core.Models.Controls.TimeLine
{
    public class ReportsModel : ViewModelBase
    {
        public ICommand AddRepordCommand => new RelayCommand<System.Collections.IList>(AddRepordMethod);
        public ICommand RemoveTopElementCommand => new RelayCommand<ReportModel>(RemoveTopElementMethod);
        public ICommand RemoveBottomElementCommand => new RelayCommand<ReportModel>(RemoveBottomElementMethod);

        public readonly ReportsRegulator ReportsRegulator = new ReportsRegulator();

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

        private void AddRepordMethod(System.Collections.IList values)
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

            var choisenDateTime = TimeSpan.FromMinutes(Tools.Math.Position.ChoisenMinute(mouseY, height));

            var findRepor = Reports.FirstOrDefault(rep => rep.Start <= choisenDateTime && rep.End >= choisenDateTime);

            if (findRepor == null)
                return;

            var findIndexReport = Reports.IndexOf(findRepor);

            Reports.Insert(findIndexReport + 1, new ReportModel() { Start = choisenDateTime, End = findRepor.End });
            findRepor.End = choisenDateTime;
        }

        public void UpDateReportModel(IList<ReportModel> list)
        {
            if (list == null)
                return;

            var reports = new ObservableCollection<ReportModel>() { new ReportModel() };

            foreach (var item in list)
            {
                Add(reports, item);
            }

            Reports = reports;
        }

        private void Add(IList<ReportModel> list, ReportModel report)
        {
            var usereport = list.FirstOrDefault(rep => rep.Start <= report.Start && rep.End >= report.Start);

            var reportindex = list.IndexOf(usereport);
            var end = usereport.End;

            if (usereport.End == report.Start)
            {
                list[reportindex + 1].Start = report.End;
                list.Insert(reportindex + 1, report);
            }
            else
            {
                var newreport = new ReportModel();

                newreport.Start = report.End;
                newreport.End = usereport.End;
                usereport.End = report.Start;

                list.Insert(reportindex + 1, report);
                list.Insert(reportindex + 2, newreport);
            }
        }

        private ObservableCollection<ReportModel> reports = new ObservableCollection<ReportModel>() { new ReportModel() };
        public ObservableCollection<ReportModel> Reports
        {
            get { return reports; }
            set { Set(ref reports, value); }
        }
    }
}
