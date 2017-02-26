using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Twill.UI.Core.Models.Content.Settings
{
    public class ActionPlannerPageModel : ObservableObject, IWeakEventListener
    {
        public ActionPlannerPageModel()
        {
            ActionPlanner.SubscribeUpDateEvent(this);

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
                RaisePropertyChanged(nameof(Message));
            }

            return true;
        }
    }
}
