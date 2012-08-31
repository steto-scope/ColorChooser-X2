using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using ColorChooserX2.Util;
using ColorChooserX2.Extensions;

namespace ColorChooserX2.ViewModels
{
    public class HSVFieldViewModel : ViewModelBase, IColorChooser
    {
        public double Width
        {
            get;
            set;
        }
        public double Height
        {
            get;
            set;
        }

        private HSVColor hovercolor;

        public Color HoverColor
        {
            get { return hovercolor.ToColor(); }
            set { hovercolor = value.ToHSV();  RaisePropertyChanged("HoverColor"); }
        }

        public HSVColor HSV
        {
            get { return selectedcolor; }
            set { selectedcolor = value; RaisePropertyChanged("SelectedColor"); }
        }

        private HSVColor selectedcolor;

        public Color SelectedColor
        {
            get { return selectedcolor.ToColor(); }
            set { selectedcolor = value.ToHSV(); RaisePropertyChanged("SelectedColor"); RaisePropertyChanged("NormalizedHue"); RaisePropertyChanged("Saturation"); RaisePropertyChanged("CrosshairPosition"); }
        }


        public double Saturation
        {
            get { return HSV.Saturation; }
            set { HSV.Saturation = 1-value; RaisePropertyChanged("Saturation"); RaisePropertyChanged("NormalizedHue");  RaisePropertyChanged("SelectedColor");RaisePropertyChanged("CrosshairPosition"); }
        }

        public double NormalizedHue
        {
            get { return HSV.NormalizedHue; }
            set { HSV.NormalizedHue = value; RaisePropertyChanged("NormalizedHue");  RaisePropertyChanged("SelectedColor"); RaisePropertyChanged("CrosshairPosition"); }
        }

        public double Value
        {
            get { return HSV.Value; }
            set { HSV.Value = value; RaisePropertyChanged("Value");  RaisePropertyChanged("SelectedColor"); RaisePropertyChanged("CrosshairPosition"); }
        }


        public Point CrosshairPosition
        {
            get 
            {
                var x = HSV.NormalizedHue * Width;
                var y = HSV.Value * Height;
                return new Point(x-5, Height-y-5);
            }
        }

        private ICommand hovercolorchangedcommand;
        public System.Windows.Input.ICommand HoverColorChangedCommand
        {
            get
            {
                if (hovercolorchangedcommand == null)
                    hovercolorchangedcommand = new RelayCommand(p => OnHoverColorChanged(p), p=>true);
                return hovercolorchangedcommand;
            }
        }

        private void OnHoverColorChanged(object parameter)
        {
            if (parameter is Point)
            {
                Point p = (Point)parameter;
                Color old = HoverColor;

                hovercolor.NormalizedHue = p.X / Width;
                hovercolor.Value = 1 - p.Y / Height;
                hovercolor.Saturation = Saturation;

                RaisePropertyChanged("HoverColor");
                if (HoverColorChanged != null)
                    HoverColorChanged(this, new ColorChangedEventArgs(old, SelectedColor));
                
            }
        }

        public HSVFieldViewModel()
        {
            HSV = new HSVColor(0, 1, 1, 255);
            hovercolor = new HSVColor(0, 1, 1, 255);
        }

        private void OnSelectedColorChanged(object parameter)
        {
            if (parameter is Point)
            {
                Point p = (Point)parameter;
                Color old = SelectedColor;

                var hue = p.X / Width;
                var light = p.Y / Height;

                NormalizedHue = hue;
                Value = 1 - light;

                if (SelectedColorChanged != null)
                    SelectedColorChanged(this, new ColorChangedEventArgs(old, SelectedColor));

            }
        }

        private ICommand selectedcolorchangedcommand;
        public System.Windows.Input.ICommand SelectedColorChangedCommand
        {
            get
            {
                if (selectedcolorchangedcommand == null)
                    selectedcolorchangedcommand = new RelayCommand(p => OnSelectedColorChanged(p), p=>true);
                return selectedcolorchangedcommand;
            }
        }


        public event EventHandler<ColorChangedEventArgs> HoverColorChanged;

        public event EventHandler<ColorChangedEventArgs> SelectedColorChanged;
    }
}
