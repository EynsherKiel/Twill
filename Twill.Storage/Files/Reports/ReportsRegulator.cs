using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Storage.Enums;
using Twill.Tools.Architecture;

namespace Twill.Storage.Files.Reports
{
    public class ReportsRegulator
    {
        public ReportsRegulator()
        {

        }

        public const string FileName = "dayreport";

        private ToType DefaultType = ToType.JSON;
        private Barrier.Manager BarrierManager = new Barrier.Manager();

        public DayReport<T> Load<T>(DateTime date) where T : Interfaces.Reports.IReport
        {
            var formatter = DefineReportFormat<T>(DefaultType);
            var requested = GetPath(DefaultType, date);

            if (!requested.IsFileExist())
                return null;

            var text = requested.Load();

            if (string.IsNullOrEmpty(text))
                return null;

            return formatter.Deserialize(text);
        }

        public string Save<T>(DayReport<T> report, ToType toType) where T : Interfaces.Reports.IReport
        {
            if(toType != DefaultType)
            {
                // need for report
                ImplementSave(report, DefaultType);
            }

            return ImplementSave(report, toType);
        }

        private string ImplementSave<T>(DayReport<T> report, ToType toType) where T : Interfaces.Reports.IReport
        {
            var formatter = DefineReportFormat<T>(toType);

            formatter.Add(report);

            var requested = GetPath(toType, report.Date); 

            BarrierManager.Save(requested, formatter.Serialize()); 

            return requested.Path;
        }

        private Interfaces.Files.IInteraction GetPath(ToType toType, DateTime time)
        { 
            switch (toType)
            {
                case ToType.ToDoList:
                case ToType.JSON:
                case ToType.XML:
                default:
                    return new Files.Ordinary(Settings.Default.GetLaborFullPath(time, $"{FileName}.{toType.GetAttributeDescription()}"));
            }
        }

        private Interfaces.Reports.IReportFormatter<T> DefineReportFormat<T>(ToType toType) where T : Interfaces.Reports.IReport
        {
            switch (toType)
            {
                case ToType.ToDoList:
                    return new To.TodoList<T>();
                case ToType.XML:
                    return new To.XMLReport<T>();
                case ToType.JSON:
                    return new To.JsonReport<T>();
                default:
                    return new To.TodoList<T>();
            }
        }
    }
}
