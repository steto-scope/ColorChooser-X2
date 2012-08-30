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
    public class RGBFieldViewModel : ViewModelBase, IColorChooser
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

        private Color selectedcolor;

        public Color SelectedColor
        {
            get { return selectedcolor; }
            set { selectedcolor = value; bg.Color = value; RaisePropertyChanged("SelectedColor");  RaisePropertyChanged("SelectedColorBrush"); }
        }

        private SolidColorBrush bg;
        public SolidColorBrush SelectedColorBrush
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

        private void OnSelectedColorChanged(object parameter)
        {
            if (parameter is Point && _ir!=null)
            {
                Point p = (Point)parameter;
                Color old = SelectedColor;

                Color? newColor = _ir.GetPixel(p.X, p.Y);
                if (newColor.HasValue)
                {
                    SelectedColor = newColor.Value;
                    CrosshairPosition = new Point(p.X - 5, p.Y - 5);

                    if (SelectedColorChanged != null)
                        SelectedColorChanged(this, new ColorChangedEventArgs(old, SelectedColor));
                }
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
