using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucid.Common;
using Lucid.Controls.DataClasses.Badge;
using Lucid.Theming;

namespace Lucid.Renderers
{
    /// <summary>
    /// This class is for rendering badges
    /// </summary>
    internal static class BadgeRenderer
    {
        private static Font _BadgeFontGridColumn;
        private static Font _BadgeFontStandard;
        private static int _BadgeSpacing;

        static BadgeRenderer()
        {
            _BadgeFontGridColumn = new Font("Tahoma Sans Serif", 10f, FontStyle.Bold);
            _BadgeFontStandard = new Font("Tahoma Sans Serif", 7f, FontStyle.Bold);
            _BadgeSpacing = 5;
        }

        public static void RenderForGridColumn(Graphics g, Rectangle bounds, BadgeCollection collection)
        {
            var currentXPosition = bounds.X;

            using (SaveableGraphicsState gs = new SaveableGraphicsState(g))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;

                foreach (var badge in collection.Badges)
                {
                    if (!badge.Visible || string.IsNullOrEmpty(badge.Value))  // We do not need draw that kind of badges
                        return;

                    // Retrieve the correct colors or use the default ones
                    var badgeBackColor = collection.ColorCollection.BadgeColors.FirstOrDefault(u => u.ColorId == badge.BadgeColorId)?.BackColor ?? ColorTranslator.FromHtml("#5c6bc0");
                    var badgeForeColor = collection.ColorCollection.BadgeColors.FirstOrDefault(u => u.ColorId == badge.BadgeColorId)?.ForeColor ?? ColorTranslator.FromHtml("#ffffff");

                    using (var p = new Pen(ThemeProvider.Theme.Colors.LightText))
                    using (var b = new SolidBrush(badgeBackColor))
                    using (var bF = new SolidBrush(badgeForeColor))
                    {
                        var xCord = Math.Max(currentXPosition, bounds.X);  // We need the X Cordinate that is bigger than the X bounds of the drawing 

                        // Now we measure the text in order to determine the badge width
                        var textSize = g.MeasureString(badge.Value, _BadgeFontGridColumn);
                        textSize.Width = textSize.Width + 10;

                        // Calculate the size of the badge
                        var badgeRect = new Rectangle(xCord, bounds.Y, (int)textSize.Width, 18);


                        if (badgeRect.Width > bounds.Width) // Does the badge fit in the drawing area?
                            continue;

                        // ######## Drawing ##########

                        var badgePath = Helper.RoundedRectangleHelper.CreateRoundedRectanglePath(badgeRect, 12);  // Create the badge rectangle path

                        DrawBadge(g, badgePath, badgeBackColor, badgeForeColor, badge.Value, new Point(badgeRect.X + 5, badgeRect.Y + 2), _BadgeFontGridColumn);

                        // ######## Drawing ##########

                        // Update the X-Position with the current badge width and spacing
                        currentXPosition = currentXPosition + badgeRect.Width + _BadgeSpacing;
                    }
                }
            }
        }

        /// <summary>
        /// Draws an badge with the given parameters
        /// </summary>
        /// <param name="g"></param>
        /// <param name="badgePath"></param>
        /// <param name="backColor"></param>
        /// <param name="foreColor"></param>
        /// <param name="text"></param>
        /// <param name="startingPoint"></param>
        private static void DrawBadge(Graphics g, GraphicsPath badgePath, Color backColor, Color foreColor, string text, Point startingPoint, Font font)
        {
            // Draw the badge
            using (var backBrush = new SolidBrush(backColor))
            using (var foreBrush = new SolidBrush(foreColor))
            using (var borderPen = new Pen(ThemeProvider.Theme.Colors.LightText))
            {
                g.FillPath(backBrush, badgePath);
                g.DrawPath(borderPen, badgePath);
                g.DrawString(text, font, foreBrush, startingPoint);
            }
        }

        #region OLD DRAWING CODE - REPLACE IF NO LONGER NEEDED

        /*
        
        [Obsolete]
        /// <summary>
        /// Renders the given collection
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="collection"></param>
        public static void Render(Graphics g, Rectangle bounds, BadgeCollection collection)
        {
            var newXPosition = 0;

            var comQuality = g.CompositingQuality;
            var intMode = g.InterpolationMode;
            var smoMode = g.SmoothingMode;


            // Iterate through all badges
            foreach (var badge in collection.Badges)
            {
                if (!badge.Visible || string.IsNullOrEmpty(badge.Value)) // We do not need draw that kind of badges
                    return;

                // Retrieve the correct colors or use the default ones
                var badgeBackColor = collection.ColorCollection.BadgeColors.FirstOrDefault(u => u.ColorId == badge.BadgeColorId)?.BackColor ?? ColorTranslator.FromHtml("#5c6bc0");
                var badgeForeColor = collection.ColorCollection.BadgeColors.FirstOrDefault(u => u.ColorId == badge.BadgeColorId)?.ForeColor ?? ColorTranslator.FromHtml("#ffffff");

                using (var p = new Pen(ThemeProvider.Theme.Colors.LightText))
                using (var pr = new Pen(Color.Red))
                using (var b = new SolidBrush(badgeBackColor))
                using (var bF = new SolidBrush(badgeForeColor))
                {
                    var xCord = newXPosition == 0 ? bounds.X : newXPosition;
                    var yCord = bounds.Y;

                    var measuredTextSize = g.MeasureString(badge.Value, _BadgeFont);
                    measuredTextSize.Width = measuredTextSize.Width - 10; // Correcting the measurement

                    var badgeSize = BadgeSize.Small;

                    var tuple = BadgePath(measuredTextSize, xCord, yCord, badgeSize);

                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.FillPath(b, tuple.Item1);
                    g.DrawPath(p, tuple.Item1);

                    if (badgeSize == BadgeSize.Medium)
                        g.DrawString(badge.Value, new Font("Tahoma Sans Serif", 10f, FontStyle.Bold), bF, xCord - 4, yCord - 3);
                    else
                        g.DrawString(badge.Value, _BadgeFont, bF, xCord - 4, yCord - 3);


                    // Shows the BadgeArea-Bounds (Debug only)
                    //g.DrawRectangle(pr, node.BadgeArea);

                    // Calculate next X Position
                    if (newXPosition == 0)
                        newXPosition = bounds.X + tuple.Item2.Width + _BadgeSpacing;
                    else
                        newXPosition += tuple.Item2.Width + _BadgeSpacing;
                }
            }

            g.CompositingQuality = comQuality;
            g.InterpolationMode = intMode;
            g.SmoothingMode = smoMode;
        }

        [Obsolete]
        private static Tuple<GraphicsPath, Rectangle> BadgePath(SizeF badgeSize, int xCoordinate, int yCoordinate, BadgeSize size = BadgeSize.Small)
        {
            //GraphicsPath path = new GraphicsPath();


            // New RoundedRectangleCode to improve Badges
            //return Helper.RoundedRectangleHelper.CreateRoundedRectanglePath(new Rectangle(xCoordinate - (int)badgeSize.Height + 10, yCoordinate - 4, (int)badgeSize.Width + 20, yCoordinate + 10), 12);

            if (size == BadgeSize.Small)
            {
                var rect = new Rectangle(xCoordinate - (int)badgeSize.Height + 3, yCoordinate - 4, (int)badgeSize.Width + 20, yCoordinate + 3);

                return Tuple.Create(Helper.RoundedRectangleHelper.CreateRoundedRectanglePath(rect, 10), rect);
            }
            else if (size == BadgeSize.Medium)
            {
                var rect = new Rectangle(xCoordinate - (int)badgeSize.Height + 10, yCoordinate - 4, (int)badgeSize.Width + 20, yCoordinate + 10);

                return Tuple.Create(Helper.RoundedRectangleHelper.CreateRoundedRectanglePath(rect, 12), rect);
            }

            return null;


            //// Top
            //path.AddLine(xCoordinate + 3, yCoordinate, xCoordinate + 15 + badgeSize.Width, yCoordinate);

            //// Right
            //path.AddLine(xCoordinate + 18 + badgeSize.Width, yCoordinate + 3, xCoordinate + 18 + badgeSize.Width, yCoordinate + 9);

            //// Bottom
            //path.AddLine(xCoordinate + 15 + badgeSize.Width, yCoordinate + 12, xCoordinate + 3, yCoordinate + 12);

            //// Left
            //path.AddLine(xCoordinate, yCoordinate + 9, xCoordinate, yCoordinate + 3);

            //path.CloseFigure();
            //return path;
        }

        */
        #endregion
    }

    public enum BadgeSize
    {
        Small,
        Medium
    }
}
