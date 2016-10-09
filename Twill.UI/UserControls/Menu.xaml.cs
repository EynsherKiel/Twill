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

using model = Twill.UI.Core.Models.MenuModel;

namespace Twill.UI.UserControls
{
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
        }

        private static Type thisType = typeof(Menu);

        public static readonly DependencyProperty MenuModelProperty =
            DependencyProperty.Register(nameof(MenuModel), typeof(model), thisType);
        public model MenuModel
        {
            get { return (model)GetValue(MenuModelProperty); }
            set { SetValue(MenuModelProperty, value); }
        }
    }
}
