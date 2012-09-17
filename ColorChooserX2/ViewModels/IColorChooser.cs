using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using ColorChooserX2.Util;

namespace ColorChooserX2.ViewModels
{
    /// <summary>
    /// Color Chooser View Model Interface. Basic Members supported by all Choosers
    /// </summary>
    public interface IColorChooser
    {
        Color HoverColor { get; }
        Color SelectedColor { get; set; }
        Point CrosshairPosition { get;  }
        ICommand HoverColorChangedCommand { get;  }
        ICommand SelectedColorChangedCommand { get; }
        event EventHandler<ColorChangedEventArgs> HoverColorChanged;
        event EventHandler<ColorChangedEventArgs> SelectedColorChanged;
    }
}
