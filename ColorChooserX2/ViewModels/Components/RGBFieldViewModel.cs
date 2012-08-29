using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace ColorChooserX2.ViewModels.Components
{
    public class RGBFieldViewModel : ViewModelBase
    {
        private Color hovercolor;

        public Color HoverColor
        {
            get { return hovercolor; }
            set { hovercolor = value; bg.Color = value; RaisePropertyChanged("HoverColor"); RaisePropertyChanged("HoverColorBrush"); }
        }

        private Color selectedcolor;

        public Color SelectedColor
        {
            get { return selectedcolor; }
            set { selectedcolor = value;  RaisePropertyChanged("SelectedColor");  }
        }

        private SolidColorBrush bg;
        public SolidColorBrush HoverColorBrush
        {
            get
            {
                if (bg == null)
                    bg = new SolidColorBrush(Colors.Transparent);
                return bg;
            }
        }

        private SolidColorBrush chstroke;
        public SolidColorBrush CrosshairStrokeBrush
        {
            get
            {
                if (bg == null)
                    bg = new SolidColorBrush(Colors.Black);
                return chstroke;
            }
        }


        private Point crosshairposition;

        public Point CrosshairPosition
        {
            get { return crosshairposition; }
            set { crosshairposition = value; RaisePropertyChanged("CrosshairPosition"); }
        }


    }
}
