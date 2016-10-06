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
            DependencyProperty.Register(nameof(TrayToolTipModel), typeof(Twill.UI.Core.Models.Tray.TrayToolTipModel), thisType);
        public Twill.UI.Core.Models.Tray.TrayToolTipModel TrayToolTipModel
        {
            get { return (Twill.UI.Core.Models.Tray.TrayToolTipModel)GetValue(TrayToolTipModelProperty); }
            set { SetValue(TrayToolTipModelProperty, value); }
        }

    }
}
