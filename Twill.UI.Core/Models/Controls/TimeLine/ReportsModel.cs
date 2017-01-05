using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Twill.UI.Core.Models.Controls.TimeLine
{
    public class ReportsModel : ViewModelBase
    {


        public ICommand AddRepordCommand => new RelayCommand<System.Collections.IList>(AddRepordMethod);
        public ICommand RemoveTopElementCommand => new RelayCommand<ReportModel>(RemoveTopElementMethod);
        public ICommand RemoveBottomElementCommand => new RelayCommand<ReportModel>(RemoveBottomElementMethod);

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

            var choisenDateTime = DateTime.Now.Date.AddMinutes(Tools.Math.Position.ChoisenMinute(mouseY, height));

            var findRepor = Reports.FirstOrDefault(rep => rep.Start <= choisenDateTime && rep.End >= choisenDateTime);

            if (findRepor == null)
                return;

            var findIndexReport = Reports.IndexOf(findRepor);

            Reports.Insert(findIndexReport + 1, new ReportModel() { Start = choisenDateTime, End = findRepor.End });
            findRepor.End = choisenDateTime;
        }


        private ObservableCollection<ReportModel> reports = new ObservableCollection<ReportModel>() { new ReportModel() };
        public ObservableCollection<ReportModel> Reports
        {
            get { return reports; }
            set { Set(ref reports, value); }
        }
    }
}
