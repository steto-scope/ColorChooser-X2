using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace ColorChooserX2.Views.Converter
{
    class PointToMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is Point)
            {
                Point p = (Point)value;
                return new Thickness(p.X, p.Y, 0, 0);
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is Thickness)
            {
                Thickness m = (Thickness)value;
                return new Point(m.Top, m.Left);
            }
            return Binding.DoNothing;
        }
    }
}
