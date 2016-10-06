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
    public partial class Popup : UserControl
    {
        public Popup()
        {
            InitializeComponent();
        }

        private static Type thisType = typeof(Popup);

        public static readonly DependencyProperty TrayPopupModelProperty =
            DependencyProperty.Register(nameof(TrayPopupModel), typeof(Twill.UI.Core.Models.Tray.TrayPopupModel), thisType);
        public Twill.UI.Core.Models.Tray.TrayPopupModel TrayPopupModel
        {
            get { return (Twill.UI.Core.Models.Tray.TrayPopupModel)GetValue(TrayPopupModelProperty); }
            set { SetValue(TrayPopupModelProperty, value); }
        }

    }
}
