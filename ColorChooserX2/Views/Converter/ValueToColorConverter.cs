using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using ColorChooserX2.Util;
using System.Windows.Media;

namespace ColorChooserX2.Views.Converter
{
    class ValueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Color.FromRgb((byte)(((double)value)*255),(byte)(((double)value)*255), (byte)(((double)value)*255));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
