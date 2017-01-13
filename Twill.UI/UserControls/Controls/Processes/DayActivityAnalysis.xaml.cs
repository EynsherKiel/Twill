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

using model = Twill.UI.Core.Models.Controls.Processes.DayActivityAnalysis;


namespace Twill.UI.UserControls.Controls.Processes
{
    public partial class DayActivityAnalysis : UserControl
    {
        public DayActivityAnalysis()
        {
            InitializeComponent();
        }

        private static Type thisType = typeof(DayActivityAnalysis);

        public static readonly DependencyProperty DayActivityAnalysisModelProperty =
            DependencyProperty.Register(nameof(DayActivityAnalysisModel), typeof(model), thisType, new PropertyMetadata(null, DayActivityAnalysisModelPropertyChangedCallBack));

        private static void DayActivityAnalysisModelPropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as DayActivityAnalysis;
            if (obj == null)
                return;

            var model = e.NewValue as model;
            if (model == null)
                return;

            model.ContentHeight = obj.ContentHeight;
            model.SegmentMinHeight = obj.SegmentMinHeight;
        }

        public model DayActivityAnalysisModel
        {
            get { return (model)GetValue(DayActivityAnalysisModelProperty); }
            set { SetValue(DayActivityAnalysisModelProperty, value); }
        }

        public static readonly DependencyProperty ContentHeightProperty =
            DependencyProperty.Register(nameof(ContentHeight), typeof(double), thisType, new PropertyMetadata(model.ContentHeightConstant, ContentHeightPropertyChangedCallBack));

        private static void ContentHeightPropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = (d as DayActivityAnalysis)?.DayActivityAnalysisModel;
            if (model == null)
                return;

            model.ContentHeight = (double)e.NewValue;
        }

        public double ContentHeight
        {
            get { return (double)GetValue(ContentHeightProperty); }
            set { SetValue(ContentHeightProperty, value); }
        }

        public static readonly DependencyProperty SegmentMinHeightProperty =
            DependencyProperty.Register(nameof(SegmentMinHeight), typeof(double), thisType, new PropertyMetadata(model.SegmentMinHeightConstant, SegmentMinHeightPropertyChangedCallBack));
        public double SegmentMinHeight
        {
            get { return (double)GetValue(SegmentMinHeightProperty); }
            set { SetValue(SegmentMinHeightProperty, value); }
        }

        private static void SegmentMinHeightPropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = (d as DayActivityAnalysis)?.DayActivityAnalysisModel;
            if (model == null)
                return;

            model.SegmentMinHeight = (double)e.NewValue;
        }

    }
}
