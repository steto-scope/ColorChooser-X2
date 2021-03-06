﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using ColorChooserX2.Util;

namespace ColorChooserX2.Views.Converter
{
    class HueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            HSVColor hsv = (HSVColor)value;
            return new HSVColor(hsv.NormalizedHue, 1, hsv.Value, 255).ToColor();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
