using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Helper
{
    internal static class ImageExtender
    {
        /// <summary>
        /// Converts the given bitmap to an grayscale-bitmap
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        internal static Bitmap ToGrayScale(Bitmap bmp)
        {
            Bitmap grayBmp = new Bitmap(bmp);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int x = 0; x < bmp.Height; x++)
                {
                    Color oc = bmp.GetPixel(i, x);
                    int grayScale = (int)((oc.R * 0.3) + (oc.G * 0.59) + (oc.B * 0.11));
                    Color nc = Color.FromArgb(oc.A, grayScale, grayScale, grayScale);
                    grayBmp.SetPixel(i, x, nc);
                }
            }

            return grayBmp;
        }
    }
}
