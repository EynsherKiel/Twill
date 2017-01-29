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

using model = Twill.UI.Core.Models.Controls.Processes.DayActivityCommonAnalyse;

namespace Twill.UI.UserControls.Controls.Processes
{
    public partial class DayActivityCommonAnalyse : UserControl
    {
        public DayActivityCommonAnalyse()
        {
            InitializeComponent();
        }

        private static Type thisType = typeof(DayActivityCommonAnalyse);


        public model DayActivityCommonAnalyseModel
        {
            get { return (model)GetValue(DayActivityCommonAnalyseModelProperty); }
            set { SetValue(DayActivityCommonAnalyseModelProperty, value); }
        }

        public static readonly DependencyProperty DayActivityCommonAnalyseModelProperty = DependencyProperty.Register(nameof(DayActivityCommonAnalyseModel), typeof(model), thisType);

    }
}
