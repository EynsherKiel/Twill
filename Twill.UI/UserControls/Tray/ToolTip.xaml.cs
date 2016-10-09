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

using model = Twill.UI.Core.Models.Tray.TrayToolTipModel;

namespace Twill.UI.UserControls.Tray
{
    public partial class ToolTip : UserControl
    {
        public ToolTip()
        {
            InitializeComponent(); 
        }
        
        private static Type thisType = typeof(ToolTip);

        public static readonly DependencyProperty TrayToolTipModelProperty =
            DependencyProperty.Register(nameof(TrayToolTipModel), typeof(model), thisType);
        public model TrayToolTipModel
        {
            get { return (model)GetValue(TrayToolTipModelProperty); }
            set { SetValue(TrayToolTipModelProperty, value); }
        }

    }
}
