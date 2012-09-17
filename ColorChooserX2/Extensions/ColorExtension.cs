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
        /// <summary>
        /// Converts a Color into HSV-Space
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static HSVColor ToHSV(this Color color)
        {
            return HSVColor.FromColor(color);
        }

        /// <summary>
        /// Gets the hexadecimal representation of the color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToHex(this Color color)
        {
            return null;
        }
        /// <summary>
        /// Gets the name of the color if available (i.e. Salmon or Aqua)
        /// </summary>
        /// <param name="color"></param>
        /// <returns>CSS-Name or null</returns>
        public static string ToCSSName(this Color color)
        {
            return null;
        }

        /// <summary>
        /// Tries to get the name of the color. If no name is available the hexadecimal representation will be used
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToCSSNameOrHex(this Color color)
        {
            return ToCSSName(color) ?? ToHex(color);
        }
    }
}
