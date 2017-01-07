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

using model = System.Tuple<Twill.UI.Core.Models.Controls.Processes.ProcessDayActivity, Twill.UI.Core.Models.Controls.Processes.GroundWorkState>;

namespace Twill.UI.UserControls.Controls.TimeLine
{ 
    public partial class UserActivityLog : UserControl
    {
        public UserActivityLog()
        {
            InitializeComponent();
        }

        private static Type thisType = typeof(UserActivityLog);

        public static readonly DependencyProperty ActivityLogModelProperty =
            DependencyProperty.Register(nameof(ActivityLogModel), typeof(model), thisType);
        public model ActivityLogModel
        {
            get { return (model)GetValue(ActivityLogModelProperty); }
            set { SetValue(ActivityLogModelProperty, value); }
        } 
    }
}
