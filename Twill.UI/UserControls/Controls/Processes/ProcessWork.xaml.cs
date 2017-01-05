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

using model = Twill.UI.Core.Models.Controls.Processes.ProcessWork;

namespace Twill.UI.UserControls.Controls.Processes
{ 
    public partial class ProcessWork : UserControl
    {
        public ProcessWork()
        {
            InitializeComponent();
        }

        private static Type thisType = typeof(ProcessWork);

        public static readonly DependencyProperty ProcessWorkModelProperty =
            DependencyProperty.Register(nameof(ProcessWorkModel), typeof(model), thisType);
        public model ProcessWorkModel
        {
            get { return (model)GetValue(ProcessWorkModelProperty); }
            set { SetValue(ProcessWorkModelProperty, value); }
        }
    }
}
