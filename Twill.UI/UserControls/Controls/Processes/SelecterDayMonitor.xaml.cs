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

using model = Twill.UI.Core.Models.Content.ReportsPageModel;

namespace Twill.UI.UserControls.Controls.Processes
{
    public partial class SelecterDayMonitor : UserControl
    {
        public SelecterDayMonitor()
        {
            InitializeComponent();
        }

        private static Type thisType = typeof(SelecterDayMonitor);

        public static readonly DependencyProperty ReportsPageModelProperty =
            DependencyProperty.Register(nameof(ReportsPageModel), typeof(model), thisType);
        public model ReportsPageModel
        {
            get { return (model)GetValue(ReportsPageModelProperty); }
            set { SetValue(ReportsPageModelProperty, value); }
        }
    }
}
