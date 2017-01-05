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

using model = Twill.UI.Core.Models.Controls.TimeLine.ReportsModel;

namespace Twill.UI.UserControls.Controls.TimeLine
{ 
    public partial class Reports : UserControl
    {
        public Reports()
        {
            InitializeComponent();
        }

        private static Type thisType = typeof(Reports);

        public static readonly DependencyProperty ReportsModelProperty =
            DependencyProperty.Register(nameof(ReportsModel), typeof(model), thisType);
        public model ReportsModel
        {
            get { return (model)GetValue(ReportsModelProperty); }
            set { SetValue(ReportsModelProperty, value); }
        }
    }
}
