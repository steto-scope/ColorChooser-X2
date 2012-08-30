using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ColorChooserX2.Util
{
    public class ColorChangedEventArgs : EventArgs
    {
        public Color OldColor { get; set; }
        public Color NewColor { get; set; }

        public ColorChangedEventArgs(Color oldColor, Color newColor)
        {
            OldColor = oldColor;
            NewColor = newColor;
        }
    }
}
