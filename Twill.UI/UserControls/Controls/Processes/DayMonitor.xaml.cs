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

using model = Twill.UI.Core.Models.Controls.Processes.DayMonitor;

namespace Twill.UI.UserControls.Controls.Processes
{ 
    public partial class DayMonitor : UserControl
    {
        public DayMonitor()
        {
            InitializeComponent();
        }

        private static Type thisType = typeof(DayMonitor);

        public static readonly DependencyProperty DayMonitorModelProperty =
            DependencyProperty.Register(nameof(DayMonitorModel), typeof(model), thisType);
        public model DayMonitorModel
        {
            get { return (model)GetValue(DayMonitorModelProperty); }
            set { SetValue(DayMonitorModelProperty, value); }
        }

        public static readonly DependencyProperty ContentHeightProperty =
            DependencyProperty.Register(nameof(ContentHeight), typeof(double), thisType, new PropertyMetadata(20000.0));
        public double ContentHeight
        {
            get { return (double)GetValue(ContentHeightProperty); }
            set { SetValue(ContentHeightProperty, value); }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            scrollviewer.ScrollToVerticalOffset(Tools.Math.Position.ChoisenPixel(DateTime.Now, ContentHeight) - 50.0);
        }
    }
}
