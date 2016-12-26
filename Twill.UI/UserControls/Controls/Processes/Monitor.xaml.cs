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

using model = Twill.UI.Core.Models.Controls.Processes.Monitor;

namespace Twill.UI.UserControls.Controls.Processes
{
    public partial class Monitor : UserControl
    {
        public Monitor()
        {
            InitializeComponent();
        }

        private static Type thisType = typeof(Monitor);

        public static readonly DependencyProperty MonitorModelProperty =
            DependencyProperty.Register(nameof(MonitorModel), typeof(model), thisType);
        public model MonitorModel
        {
            get { return (model)GetValue(MonitorModelProperty); }
            set { SetValue(MonitorModelProperty, value); }
        }

    }
}
