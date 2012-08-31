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
        private ImageReader _ir;

        public ImageReader ImageReader
        {
            get { return _ir; }
            set { _ir = value; RaisePropertyChanged("ImageReader"); }
        }

        private Color hovercolor;

        public Color HoverColor
        {
            get { return hovercolor; }
            set { hovercolor = value;  RaisePropertyChanged("HoverColor"); }
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



        private SolidColorBrush chstroke;
        public SolidColorBrush CrosshairStrokeBrush
        {
            get
            {
                if (chstroke == null)
                    chstroke = new SolidColorBrush(Colors.Black);
                return chstroke;
            }
        }


        public Point CrosshairPosition
        {
            get 
            {
                var x = HSV.NormalizedHue * _ir.RenderWidth;
                var y = HSV.Value * _ir.RenderHeight;
                return new Point(x-5, _ir.RenderHeight-y-5);
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
            if (parameter is Point && _ir!=null)
            {
                Point p = (Point)parameter;
                Color old = HoverColor;

                Color? newColor = _ir.GetPixel(p.X, p.Y);
                if (newColor.HasValue)
                {
                    HoverColor = newColor.Value;

                    if (HoverColorChanged != null)
                        HoverColorChanged(this, new ColorChangedEventArgs(old, SelectedColor));
                }
            }
        }

        public HSVFieldViewModel()
        {
            HSV = new HSVColor(0, 1, 1, 255);
        }

        private void OnSelectedColorChanged(object parameter)
        {
            if (parameter is Point && _ir!=null)
            {
                Point p = (Point)parameter;
                Color old = SelectedColor;

                var hue = p.X / _ir.RenderWidth;
                var light = p.Y / _ir.RenderHeight;

                NormalizedHue = hue;
                Value = 1 - light;

                //HSVColor hsl = new HSVColor(hue,selectedcolor.Saturation,1-light,selectedcolor.Alpha);
                //Color newColor = hsl.ToColor();

                //SelectedColor = newColor;
                
                if (SelectedColorChanged != null)
                    SelectedColorChanged(this, new ColorChangedEventArgs(old, SelectedColor));

                /*
                Color? newColor = _ir.GetPixel(p.X, p.Y);
                if (newColor.HasValue)
                {
                    SelectedColor = newColor.Value;
                    CrosshairPosition = new Point(p.X - 5, p.Y - 5);

                    if (SelectedColorChanged != null)
                        SelectedColorChanged(this, new ColorChangedEventArgs(old, SelectedColor));
                }*/
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
