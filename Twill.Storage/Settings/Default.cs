using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Storage.Settings
{
    public static class Default
    {

        public const string UserFolderName = nameof(Twill);

        public static string ReportsName = "Reports";
        public static string LaborsName = "Activities";
        public static string LaborJsonName = "activity.json";

        public static string UserFolderPath => System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), UserFolderName);

        public static string LaborsPath => System.IO.Path.Combine(UserFolderPath, LaborsName);
        public static string ReportsPath => System.IO.Path.Combine(UserFolderPath, ReportsName);

        public static string GetLaborFullPath(DateTime time) => System.IO.Path.Combine(LaborsPath, time.GetDateTimeFormats().First(), LaborJsonName);

    }
}
