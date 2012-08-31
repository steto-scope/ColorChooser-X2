using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.ComponentModel;

namespace ColorChooserX2.Util
{
    public class HSVColor : INotifyPropertyChanged
    {
        private double a;
        private double h;
        private double s;
        private double v;
        /// <summary>
        /// Hue [0.0,1.0]
        /// </summary>
        public double NormalizedHue { get { return h; } set { if (h >= 0 && h <= 1) h = value; else { if (h < 0) h = 0; else h = 1; } } }
        /// <summary>
        /// Hue [0.0,360.0]
        /// </summary>
        public double Hue { get { return h * 360; } set { NormalizedHue = (value % 360) / 360.0; RaisePropertyChanged("Hue"); } }
        /// <summary>
        /// Sättigungswert {0.0,1.0}
        /// </summary>
        public double Saturation { get { return s; } set { if (s >= 0 && s <= 1) s = value; else { if (s < 0) s = 0; else s = 1; } RaisePropertyChanged("Saturation"); } }
        /// <summary>
        /// Hellwert [0.0,1.0]
        /// </summary>
        public double Value { get { return v; } 
            set { if (v >= 0 && v <= 1) v = value; else { if (v < 0) v = 0; else v = 1; } RaisePropertyChanged("Value"); } }
        /// <summary>
        /// Alpha [0.0,1.0]
        /// </summary>
        public double NormalizedAlpha { get { return a; } set { a = Math.Max(0, Math.Min(1, value)); } }
        /// <summary>
        /// Alpha [0.0,255.0]
        /// </summary>
        public double Alpha { get { return a * 255; } set { a = Math.Max(0, Math.Min(255, value)) / 255.0; RaisePropertyChanged("Alpha"); } }

        struct ColorPair
        {
            public double val;
            public string channel;
        }

        public HSVColor(double normalizedHue, double saturation, double lightness, double alpha)
        {
            NormalizedHue = normalizedHue;
            Saturation = saturation;
            Value = lightness;
            Alpha = alpha;
        }

        /// <summary>
        /// Konvertiert eine (RGB)Farbe System.Windows.Media.Color in den HSL-Farbraum. Alphakanal geht verloren
        /// </summary>
        /// <param name="rgb"></param>
        /// <returns></returns>
        public static HSVColor FromColor(Color rgb)
        {
            HSVColor hsv = new HSVColor(0, 0, 0, rgb.A);

            double deg1 = 1.0 / 360.0;
            double h = 0.0;

            ColorPair r = new ColorPair() { val = (double)rgb.R / 255.0, channel = "R" };
            ColorPair g = new ColorPair() { val = (double)rgb.G / 255.0, channel = "G" };
            ColorPair b = new ColorPair() { val = (double)rgb.B / 255.0, channel = "B" };
            ColorPair[] values = new ColorPair[] { r, g, b };

            ColorPair max = values.OrderByDescending(v => v.val).First();
            ColorPair min = values.OrderBy(v => v.val).First();

            if (min.channel == max.channel)
                h = 0;
            else
                switch (max.channel)
                {
                    case "R":
                        h = 60 * deg1 * (0 + (g.val - b.val) / (max.val - min.val));
                        break;
                    case "G":
                        h = 60 * deg1 * (2 + (b.val - r.val) / (max.val - min.val));
                        break;
                    case "B":
                        h = 60 * deg1 * (4 + (r.val - g.val) / (max.val - min.val));
                        break;
                    default:
                        h = 0;
                        break;
                }
            if (h < 0)
                h += 1;
            hsv.NormalizedHue = h;

            if (max.val == 0)
                hsv.Saturation = 0;
            else
                hsv.Saturation = (max.val - min.val) / max.val;

            hsv.Value = max.val;





            return hsv;

        }

        /// <summary>
        /// Generiert aus dem HSL-Farb-Objekt wieder ein (RGB)Color-Objekt
        /// </summary>
        /// <returns></returns>
        public Color ToColor()
        {
            Color rgb = new Color();


            int r = (int)Math.Truncate((NormalizedHue / (1.0 / 6.0)));
            double f = (NormalizedHue / (1.0 / 6.0)) - r;
            double p = Value * (1 - Saturation);
            double q = Value * (1 - Saturation * f);
            double t = Value * (1 - Saturation * (1 - f));

            switch (r)
            {
                case 0:
                case 6:
                default:
                    rgb = new Color() { R = (byte)Math.Round(Value * 255), G = (byte)Math.Round(t * 255), B = (byte)Math.Round(p * 255), A = (byte)Alpha };
                    break;
                case 1:
                    rgb = new Color() { R = (byte)Math.Round(q * 255), G = (byte)Math.Round(Value * 255), B = (byte)Math.Round(p * 255), A = (byte)Alpha };
                    break;
                case 2:
                    rgb = new Color() { R = (byte)Math.Round(p * 255), G = (byte)Math.Round(Value * 255), B = (byte)Math.Round(t * 255), A = (byte)Alpha };
                    break;
                case 3:
                    rgb = new Color() { R = (byte)Math.Round(p * 255), G = (byte)Math.Round(q * 255), B = (byte)Math.Round(Value * 255), A = (byte)Alpha };
                    break;
                case 4:
                    rgb = new Color() { R = (byte)Math.Round(t * 255), G = (byte)Math.Round(p * 255), B = (byte)Math.Round(Value * 255), A = (byte)Alpha };
                    break;
                case 5:
                    rgb = new Color() { R = (byte)Math.Round(Value * 255), G = (byte)Math.Round(p * 255), B = (byte)Math.Round(q * 255), A = (byte)Alpha };
                    break;
            }

            return rgb;
        }

        /// <summary>
        /// Gibt den RGB-Farbwert zurück
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToColor().ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
