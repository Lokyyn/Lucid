using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Helper
{
    public static class RoundedRectangleHelper
    {
        // Make this internal (public just for testing)
        public static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, radius, radius, 180f, 90f);
            path.AddArc((rect.Right - radius), rect.Y, radius, radius, 270f, 90f);
            path.AddArc((rect.Right - radius), (rect.Bottom - radius), radius, radius, 0f, 90f);
            path.AddArc(rect.X, (rect.Bottom - radius), radius, radius, 90f, 90f);
            path.CloseFigure();


            return path;
        }
    }
}
