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

using model = Twill.UI.Core.Models.Controls.TimeLine.ReportModel;


namespace Twill.UI.UserControls.Controls.TimeLine
{
    public partial class Report : UserControl
    {
        public Report()
        {
            InitializeComponent();
        }

        private static Type thisType = typeof(Report);

        public static readonly DependencyProperty ReportModelProperty =
            DependencyProperty.Register(nameof(ReportModel), typeof(model), thisType);
        public model ReportModel
        {
            get { return (model)GetValue(ReportModelProperty); }
            set { SetValue(ReportModelProperty, value); }
        }

    }
}

