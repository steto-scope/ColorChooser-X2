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
using ColorChooserX2.Util;

namespace ColorChooserX2.Views.Components
{
    /// <summary>
    /// Interaktionslogik für RGBColorField.xaml
    /// </summary>
    public partial class HSVColorField : UserControl
    {
        public HSVColorField()
        {
            InitializeComponent();
            DataContextChanged += new DependencyPropertyChangedEventHandler(HSLColorField_DataContextChanged);
        }

        void HSLColorField_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            HSVFieldViewModel model = DataContext as HSVFieldViewModel;
            if (model != null)
                model.ImageReader = new ImageReader(new UIElement[] { rgbRect, saturationRect }, saturationRect);
        }

        

        protected void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(rgbRect);
            IColorChooser model = DataContext as IColorChooser;
            if (model != null)
            {
                model.HoverColorChangedCommand.Execute(p);
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    model.SelectedColorChangedCommand.Execute(p);
                }
            }
        }

        public IEnumerable<UIElement> Field { get; set; }


        protected void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
             Point p = e.GetPosition(rgbRect);
            IColorChooser model = DataContext as IColorChooser;
            if (model != null)
            {
                model.SelectedColorChangedCommand.Execute(p);
            }
        }

        protected void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                if (e.NewSize.Width != double.NaN && e.NewSize.Height != double.NaN && e.PreviousSize.Height > 0 && e.PreviousSize.Width > 0)
                {
                    HSVFieldViewModel model = DataContext as HSVFieldViewModel;
                    if (model != null)
                    {
                        double posx = model.CrosshairPosition.X / e.PreviousSize.Width;
                        double posy = model.CrosshairPosition.Y / e.PreviousSize.Height;

                        model.ImageReader = new ImageReader(new UIElement[] { rgbRect, saturationRect }, saturationRect);
                        model.CrosshairPosition = new Point(e.NewSize.Width * posx, e.NewSize.Height * posy);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
