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

using model = Twill.UI.Core.Models.Controls.TimeLine.ProcessDayLaborModel;

namespace Twill.UI.UserControls.Controls.TimeLine
{
    public partial class ProcessDayLabor : UserControl
    {
        public ProcessDayLabor()
        {
            InitializeComponent();
        }
        private static Type thisType = typeof(ProcessDayLabor);

        public static readonly DependencyProperty ProcessDayLaborModelProperty =
            DependencyProperty.Register(nameof(ProcessDayLaborModel), typeof(model), thisType);
        public model ProcessDayLaborModel
        {
            get { return (model)GetValue(ProcessDayLaborModelProperty); }
            set { SetValue(ProcessDayLaborModelProperty, value); }
        }

    }
}

