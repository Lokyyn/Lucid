using System.Drawing.Drawing2D;

namespace Lucid.Helper;

internal static class RoundedRectangleHelper
{
    internal static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
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
