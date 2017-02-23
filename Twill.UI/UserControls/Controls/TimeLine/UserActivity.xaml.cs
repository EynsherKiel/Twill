using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using model = Twill.UI.Core.Models.Controls.Processes.ProcessMonitor;

namespace Twill.UI.UserControls.Controls.TimeLine
{
    public partial class UserActivity : UserControl
    {
        public UserActivity()
        {
            InitializeComponent();
        }

        private static Type thisType = typeof(UserActivity);

        public static readonly DependencyProperty ProcessMonitorModelProperty =
            DependencyProperty.Register(nameof(ProcessMonitorModel), typeof(model), thisType);
        public model ProcessMonitorModel
        {
            get { return (model)GetValue(ProcessMonitorModelProperty); }
            set { SetValue(ProcessMonitorModelProperty, value); }
        }

        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(nameof(Click), RoutingStrategy.Bubble, typeof(RoutedEventHandler), thisType);

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        private void UserActivityLogClickMethod(object sender, RoutedEventArgs e)
        { 
            RaiseEvent(new TextChangedEventArgs(ClickEvent, UndoAction.None));
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            var service =  tooltipservice.ToolTip as ToolTip;

            service.DataContext = (sender as FrameworkElement).DataContext;

            service.IsOpen = true;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            var service =  tooltipservice.ToolTip as ToolTip;

            service.IsOpen = false;
        }
    }
}
