using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ColorChooserX2.ViewModels;

namespace ColorChooserX2.Views.Components
{
    /// <summary>
    /// Interaktionslogik für SpectrumSlider.xaml
    /// </summary>
    public partial class SpectrumSlider : UserControl
    {
        public SpectrumSlider()
        {
            InitializeComponent();
            DataContext = this;
           // DataContext = new SpectrumSliderViewModel(new LinearGradientBrush(new GradientStopCollection(new GradientStop[]{ new GradientStop(Colors.White,0), new GradientStop(Colors.Blue,1)})));
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = Mouse.GetPosition(bar);

            OnHoverChanged(p);
            if (e.LeftButton == MouseButtonState.Pressed)
                OnSelectionChanged(p);
            
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point p = Mouse.GetPosition(bar);
            OnSelectionChanged(p);
            
        }

        

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty = 
            DependencyProperty.Register("Value", typeof(double), typeof(SpectrumSlider), new FrameworkPropertyMetadata(0.0,FrameworkPropertyMetadataOptions.AffectsMeasure,new PropertyChangedCallback(OnValueChanged),null));

        

        public double HoverValue
        {
            get { return (double)GetValue(HoverProperty); }
            set { SetValue(HoverProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Hover.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverProperty = 
            DependencyProperty.Register("HoverValue", typeof(double), typeof(SpectrumSlider), new UIPropertyMetadata(0.0));


        

        public Point CrosshairPosition
        {
            get { return (Point)GetValue(CrosshairPositionProperty); }
            set { SetValue(CrosshairPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CrosshairPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CrosshairPositionProperty = 
            DependencyProperty.Register("CrosshairPosition", typeof(Point), typeof(SpectrumSlider), new UIPropertyMetadata(new Point(0,0)));


        

        public LinearGradientBrush BarGradient
        {
            get { return (LinearGradientBrush)GetValue(BarGradientProperty); }
            set { SetValue(BarGradientProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BarGradient.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BarGradientProperty = 
            DependencyProperty.Register("BarGradient", typeof(LinearGradientBrush), typeof(SpectrumSlider), new UIPropertyMetadata(new LinearGradientBrush()));


        static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SpectrumSlider sl = (SpectrumSlider)d;
            //sl.CrosshairPosition = new Point(0, (double)e.NewValue * sl.ActualHeight);
            
        }


        private void OnSelectionChanged(Point p)
        {
            CrosshairPosition = new Point(0,p.Y);
            if(bar.ActualHeight!=0)
                Value = (p.Y / bar.ActualHeight);
            Console.WriteLine(Value + " | " + bar.ActualHeight + " "+p.Y);
        }


        private void OnHoverChanged(Point p)
        {
            if(bar.ActualHeight!=0)
            HoverValue = (p.Y / bar.ActualHeight);
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                if (e.NewSize.Width != double.NaN && e.NewSize.Height != double.NaN && e.PreviousSize.Height > 0 && e.PreviousSize.Width > 0)
                {
                        double posx = CrosshairPosition.X / e.PreviousSize.Width;
                        double posy = CrosshairPosition.Y / e.PreviousSize.Height;

                        CrosshairPosition = new Point(e.NewSize.Width * posx, e.NewSize.Height * posy);
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
