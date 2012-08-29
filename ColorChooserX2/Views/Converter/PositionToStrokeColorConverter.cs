using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace ColorChooserX2.Views.Converter
{
    class PositionToStrokeColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                double height = (double)values[0];
                Point pos = (Point)values[1];

                if (pos.Y > 0 && height > 0)
                {
                    return pos.Y > height / 2   ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Black);
                }
            }
            catch{}
            return new SolidColorBrush(Colors.Black);
        }
        
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
