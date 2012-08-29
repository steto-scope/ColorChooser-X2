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
using ColorChooserX2.Util;
using ColorChooserX2.ViewModels.Components;

namespace ColorChooserX2.Views.Components
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class RGBField : UserControl
    {
        private ImageReader _ir;

        public RGBField()
        {
            InitializeComponent();
            _ir = new ImageReader(new Visual[] { rgbRect, saturationRect }, saturationRect);
            DataContext = new RGBFieldViewModel();
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition((IInputElement)sender);
            RGBFieldViewModel model = DataContext as RGBFieldViewModel;
            if (model != null)
            {
                model.HoverColor = _ir.GetPixel(p.X, p.Y, saturationRect.RenderSize.Width, saturationRect.RenderSize.Height);
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    model.SelectedColor = _ir.GetPixel(p.X, p.Y, saturationRect.RenderSize.Width, saturationRect.RenderSize.Height);
                    model.CrosshairPosition = new Point(p.X - 5, p.Y - 5);
                }
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition((IInputElement)sender);
            RGBFieldViewModel model = DataContext as RGBFieldViewModel;
            if (model != null)
            {
                model.SelectedColor = _ir.GetPixel(p.X, p.Y, saturationRect.RenderSize.Width, saturationRect.RenderSize.Height);
                model.CrosshairPosition = new Point(p.X - 5, p.Y - 5);
            }
        }


        
    }
}
