using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ColorChooserX2.Util;
using System.Windows.Media;

namespace ColorChooserX2.Extensions
{
    public static class ColorExtension
    {
        public static HSVColor ToHSV(this Color color)
        {
            return HSVColor.FromColor(color);
        }

        public static string ToHex(this Color color)
        {
            return null;
        }

        public static string ToCSSName(this Color color)
        {
            return null;
        }

        public static string ToCSSNameOrHex(this Color color)
        {
            return ToCSSName(color) ?? ToHex(color);
        }
    }
}
