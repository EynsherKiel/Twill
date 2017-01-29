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

namespace Twill.UI.UserControls.Controls.TextBlocks
{
    public partial class DotTextBlock : UserControl
    {
        public DotTextBlock()
        {
            InitializeComponent();
        }

        private static Type thisType = typeof(DotTextBlock);


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), thisType);

        public Brush DotBrush
        {
            get { return (Brush)GetValue(DotBrushProperty); }
            set { SetValue(DotBrushProperty, value); }
        }
         
        public static readonly DependencyProperty DotBrushProperty = DependencyProperty.Register(nameof(DotBrush), typeof(Brush), thisType);
    }
}
