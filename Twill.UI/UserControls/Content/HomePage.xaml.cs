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

namespace Twill.UI.UserControls.Content
{
    public partial class HomePage : UserControl
    {
        public HomePage()
        {
            InitializeComponent();
        }


        private static Type thisType = typeof(HomePage);


        public ICommand MonitorClickCommand
        {
            get { return (ICommand)GetValue(MonitorClickCommandProperty); }
            set { SetValue(MonitorClickCommandProperty, value); }
        }
         
        public static readonly DependencyProperty MonitorClickCommandProperty =
            DependencyProperty.Register(nameof(MonitorClickCommand), typeof(ICommand), thisType);



        public ICommand ReportsClickCommand
        {
            get { return (ICommand)GetValue(ReportsClickCommandProperty); }
            set { SetValue(ReportsClickCommandProperty, value); }
        }

        public static readonly DependencyProperty ReportsClickCommandProperty =
            DependencyProperty.Register(nameof(ReportsClickCommand), typeof(ICommand), thisType);



    }
}
