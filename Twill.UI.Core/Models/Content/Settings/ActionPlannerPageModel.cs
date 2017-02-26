using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Twill.Storage.Interfaces.Settings;

namespace Twill.UI.Core.Models.Content.Settings
{
    public class ActionPlannerPageModel : ObservableObject, IWeakEventListener, IActionPlanner
    {
        public ActionPlannerPageModel()
        {
            ActionPlanner.SubscribeUpDateEvent(this);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                ActionPlanner.Add(DayOfWeek.Monday, TimeSpan.FromHours(5));
                ActionPlanner.Add(DayOfWeek.Thursday, TimeSpan.FromHours(15));
            }

            if (Tasks.Count == 7)
            {
                foreach (var task in Tasks)
                {
                    foreach (var time in task.Value)
                    {
                        ActionPlanner.Add(task.Key, time);
                    }
                }
            }
            else
            {
                Tasks = ActionPlanner.GetPlanner();
            }

            ActionPlanner.Start();
        }

        private Tools.Async.ActionPlanner ActionPlanner = new Tools.Async.ActionPlanner();
        private StorageHelper.Manager StorageHelperManager = new StorageHelper.Manager();

        public ICommand AppendAction => new RelayCommand<IList>(AppendActionMethod);
        public ICommand DeleteAction => new RelayCommand<IList>(DeleteActionMethod);

        private void DeleteActionMethod(IList list)
        {
            if (ActionPlanner == null)
                return;

            if (list.Count < 2)
                return;

            if (list[0] is TimeSpan == false)
                return;
              
            int data;
            if (!int.TryParse(list[1] as string, out data))
                return;

            if (data > 6)
                return;

            var time = (TimeSpan)list[0];
            var day = (DayOfWeek)data;

            if (!ActionPlanner.Remove(day, time))
                return;

            Tasks = ActionPlanner.GetPlanner();

            StorageHelperManager.Save(this);
        }

        private void AppendActionMethod(IList list)
        {
            if (ActionPlanner == null)
                return;

            if (list.Count < 2)
                return;

            if (list[0] is DayOfWeek == false)
                return;

            if (list[1] is DateTime == false)
                return;

            var day = (DayOfWeek)list[0];
            var date = (DateTime)list[1];

            if (!ActionPlanner.Add(day, date.TimeOfDay))
                return;

            Tasks = ActionPlanner.GetPlanner();

            StorageHelperManager.Save(this);
        }


        private Dictionary<DayOfWeek, ObservableCollection<TimeSpan>> tasks = new Dictionary<DayOfWeek, ObservableCollection<TimeSpan>>();
        public Dictionary<DayOfWeek, ObservableCollection<TimeSpan>> Tasks
        {
            get { return tasks; }
            set { Set(ref tasks, value); }
        }

        private string message = "You need fill report";
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                RaisePropertyChanged(nameof(Message));
            }
        }

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {

            if (sender is Tools.Async.ActionPlanner)
            {
                Message = DateTime.Now.ToString();
            }

            return true;
        }
    }
}
