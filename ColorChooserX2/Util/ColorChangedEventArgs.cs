using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ColorChooserX2.Util
{
    /// <summary>
    /// EventArgs for the Color-Events
    /// </summary>
    public class ColorChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Last Color
        /// </summary>
        public Color OldColor { get; set; }
        /// <summary>
        /// New Color
        /// </summary>
        public Color NewColor { get; set; }

        /// <summary>
        /// Creates a new Object
        /// </summary>
        /// <param name="oldColor"></param>
        /// <param name="newColor"></param>
        public ColorChangedEventArgs(Color oldColor, Color newColor)
        {
            OldColor = oldColor;
            NewColor = newColor;
        }
    }
}
