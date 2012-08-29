using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;

namespace ColorChooserX2.Util
{
    /// <summary>
    /// Hilfsklasse, die es ermöglicht die Farbe eines bestimmten Pixels auf einem Bild auszulesen.
    /// </summary>
    class ImageReader
    {
        BitmapSource src;
        IInputElement sender;

        Visual[] elements;

        /// <summary>
        /// Erzeugt ein neues Objekt unter Verwendung einer BitmapSource. 
        /// Dieser Konstruktor wird klassischerweise verwendet, wenn ein Image vorliegt, das aus einer Bitmap (Datei) geladen wurde
        /// </summary>
        /// <param name="src">Quelle (normalerweise Image.Source)</param>
        /// <param name="sender">Auslösendes Objekt</param>
        public ImageReader(BitmapSource src, IInputElement sender)
        {
            this.src = src;
            this.sender = sender;
        }

        /// <summary>
        /// Erzeugt ein neues Objekt unter Angabe einer Liste von UI-Elementen. 
        /// Dieser Konstruktor kann dazu verwendet werden eine Bitmap zu erzeugen, in die WPF-Steuerelemente (z.B. Rectangle) gerendert werden
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="sender"></param>
        public ImageReader(System.Windows.Media.Visual[] elements, IInputElement sender)
        {
            this.elements = elements;
            this.sender = sender;
        }

        /// <summary>
        /// Liest die Farbe eines Pixels an der angegebenen Stelle aus
        /// </summary>
        /// <param name="mousePosX">X-Mausposition relativ zum Bild/Steuerelement (übergeben im Konstruktor)</param>
        /// <param name="mousePosY">Y-Mausposition relativ zum Bild/Steuerelement (übergeben im Konstruktor)</param>
        /// <param name="renderSizeX">Aktuelle horizontale Größe des Steuerelements</param>
        /// <param name="renderSizeY">Aktuelle vertikale Größe des Steuerelements</param>
        /// <returns></returns>
        public System.Windows.Media.Color GetPixel(double mousePosX, double mousePosY, double renderSizeX, double renderSizeY)
        {
            if (src == null)
            {
                RenderTargetBitmap bmp = new RenderTargetBitmap(
                (int)renderSizeX, (int)renderSizeY,     // Dimensions in physical pixels
                96, 96,     // Pixel resolution (dpi)
                PixelFormats.Pbgra32);

                foreach (System.Windows.Media.Visual v in elements)
                    bmp.Render(v);

                src = bmp;
            }
            byte[] pixels = new byte[4];
            double[] pos = { (double)Mouse.GetPosition(sender).X, (double)Mouse.GetPosition(sender).Y };
            double[] fac = { src.PixelWidth / renderSizeX, src.PixelHeight / renderSizeY };

            pos[0] *= fac[0];
            pos[1] *= fac[1];

            CroppedBitmap cb = new CroppedBitmap(src, new Int32Rect((int)pos[0], (int)pos[1], 1, 1));
            cb.CopyPixels(pixels, 4, 0);
            return System.Windows.Media.Color.FromArgb(pixels[3], pixels[2], pixels[1], pixels[0]);

        }
    }
}
