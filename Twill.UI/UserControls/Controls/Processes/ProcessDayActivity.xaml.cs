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

using model = Twill.UI.Core.Models.Controls.Processes.ProcessDayActivity;

namespace Twill.UI.UserControls.Controls.Processes
{ 
    public partial class ProcessDayActivity : UserControl
    {
        public ProcessDayActivity()
        {
            InitializeComponent();
        }

        private static Type thisType = typeof(ProcessDayActivity);

        public static readonly DependencyProperty ProcessDayActivityModelProperty =
            DependencyProperty.Register(nameof(ProcessDayActivityModel), typeof(model), thisType);
        public model ProcessDayActivityModel
        {
            get { return (model)GetValue(ProcessDayActivityModelProperty); }
            set { SetValue(ProcessDayActivityModelProperty, value); }
        }
    }
}
