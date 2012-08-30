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
    /// Helperclass. Provides methods to read Pixel-Colors out of an Image
    /// </summary>
    public class ImageReader
    {
        BitmapSource src;
        IInputElement sender;

        UIElement[] elements;

        /// <summary>
        /// Creates a new ImageReader by an existing image
        /// </summary>
        /// <param name="src">Souce-Bitmap</param>
        /// <param name="sender">Owner of the bitmap</param>
        public ImageReader(BitmapSource src, IInputElement sender)
        {
            this.src = src;
            this.sender = sender;
        }

        /// <summary>
        /// Width of the Image (1st element)
        /// </summary>
        public double RenderWidth
        {
            get
            {
                if (elements != null && elements.Length > 0)
                {
                    return elements[0].RenderSize.Width;
                }
                return double.NaN;
            }
        }
        /// <summary>
        /// Height of the Image (1st element)
        /// </summary>
        public double RenderHeight
        {
            get
            {
                if (elements != null && elements.Length > 0)
                {
                    return elements[0].RenderSize.Height;
                }
                return double.NaN;
            }
        }

        /// <summary>
        /// Creates a new ImageReader by UIElements
        /// </summary>
        /// <param name="elements">The elements of the UI that have to be rendered to the Bitmap.</param>
        /// <param name="sender">Owner of the Visuals</param>
        public ImageReader(UIElement[] elements, IInputElement sender)
        {
            this.elements = elements;
            this.sender = sender;
        }

        /// <summary>
        /// Get the Pixel-Color of the desired position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="renderSizeX">Aktuelle horizontale Größe des Steuerelements</param>
        /// <param name="renderSizeY">Aktuelle vertikale Größe des Steuerelements</param>
        /// <returns></returns>
        public System.Windows.Media.Color? GetPixel(double x, double y)
        {
            if (src == null)
            {
                RenderTargetBitmap bmp = new RenderTargetBitmap(
                (int)RenderWidth, (int)RenderHeight,     // Dimensions in physical pixels
                96, 96,     // Pixel resolution (dpi)
                PixelFormats.Pbgra32);

                foreach (System.Windows.Media.Visual v in elements)
                    bmp.Render(v);

                src = bmp;
            }
            byte[] pixels = new byte[4];
            double[] pos = { x, y};
            double[] fac = { src.PixelWidth / RenderWidth, src.PixelHeight / RenderHeight };

            pos[0] *= fac[0];
            pos[1] *= fac[1];

            try
            {
                CroppedBitmap cb = new CroppedBitmap(src, new Int32Rect((int)pos[0], (int)pos[1], 1, 1));
                cb.CopyPixels(pixels, 4, 0);
                return System.Windows.Media.Color.FromArgb(pixels[3], pixels[2], pixels[1], pixels[0]);
            }
            catch
            {
                return null;
            }

        }
    }
}
