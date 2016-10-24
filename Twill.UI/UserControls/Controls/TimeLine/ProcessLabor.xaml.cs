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


using model = Twill.UI.Core.Models.Controls.TimeLine.ProcessLaborModel;

namespace Twill.UI.UserControls.Controls.TimeLine
{
    public partial class ProcessLabor : UserControl
    {
        public ProcessLabor()
        {
            InitializeComponent();
        }


        private static Type thisType = typeof(ProcessLabor);

        public static readonly DependencyProperty ProcessLaborModelProperty =
            DependencyProperty.Register(nameof(ProcessLaborModel), typeof(model), thisType);
        public model ProcessLaborModel
        {
            get { return (model)GetValue(ProcessLaborModelProperty); }
            set { SetValue(ProcessLaborModelProperty, value); }
        }

    }
}
