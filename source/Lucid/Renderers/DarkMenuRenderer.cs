using Lucid.Theming;
using Lucid.Controls;
using Lucid.Icons;
using System;
using System.Drawing;
using System.Windows.Forms;
using Lucid.Common;

namespace Lucid.Renderers
{
    public class DarkMenuRenderer : ToolStripRenderer
    {
        #region Initialisation Region

        protected override void Initialize(ToolStrip toolStrip)
        {
            base.Initialize(toolStrip);

            toolStrip.BackColor = ThemeProvider.Theme.Colors.MainBackgroundColor;
            toolStrip.ForeColor = Helper.ColorExtender.GetContrastColor(toolStrip.BackColor);
        }

        protected override void InitializeItem(ToolStripItem item)
        {
            base.InitializeItem(item);

            item.BackColor = ThemeProvider.Theme.Colors.MainBackgroundColor;
            item.ForeColor = Helper.ColorExtender.GetContrastColor(item.BackColor);

            if (item.GetType() == typeof(ToolStripSeparator))
            {
                item.Margin = new Padding(0, 0, 0, 1);
            }
        }

        #endregion

        #region Render Region

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            var g = e.Graphics;
            using (var b = new SolidBrush(ThemeProvider.Theme.Colors.MainBackgroundColor))
            {
                g.FillRectangle(b, e.AffectedBounds);
            }
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            var g = e.Graphics;

            var rect = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);

            using (var p = new Pen(ThemeProvider.Theme.Colors.LightBorder))
            {
                g.DrawRectangle(p, rect);
            }
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            var g = e.Graphics;

            var rect = new Rectangle(e.ImageRectangle.Left - 2, e.ImageRectangle.Top - 2,
                                         e.ImageRectangle.Width + 4, e.ImageRectangle.Height + 4);

            using (var b = new SolidBrush(ThemeProvider.Theme.Colors.LightBorder))
            {
                g.FillRectangle(b, rect);
            }

            using (var p = new Pen(ThemeProvider.Theme.Colors.ControlHighlight))
            {
                var modRect = new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);
                g.DrawRectangle(p, modRect);
            }

            if (e.Item.ImageIndex == -1 && String.IsNullOrEmpty(e.Item.ImageKey) && e.Item.Image == null)
            {
                g.DrawImageUnscaled(MenuIcons.TickBlack, new Point(e.ImageRectangle.Left, e.ImageRectangle.Top));
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            var g = e.Graphics;

            var rect = new Rectangle(1, 3, e.Item.Width, 1);

            using (var b = new SolidBrush(ThemeProvider.Theme.Colors.LightBorder))
            {
                g.FillRectangle(b, rect);
            }
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = ThemeProvider.Theme.Colors.LightText;
            e.ArrowRectangle = new Rectangle(new Point(e.ArrowRectangle.Left, e.ArrowRectangle.Top - 1), e.ArrowRectangle.Size);

            base.OnRenderArrow(e);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            var g = e.Graphics;

            e.Item.ForeColor = e.Item.Enabled ? ThemeProvider.Theme.Colors.LightText : ThemeProvider.Theme.Colors.DisabledText;

            if (e.Item.Enabled)
            {

                //var bgColor = e.Item.Selected ? ThemeProvider.Theme.Colors.GreyHighlight : e.Item.BackColor;
                var bgColor = e.Item.Selected ? ThemeProvider.Theme.Colors.MainAccent : ThemeProvider.Theme.Colors.MainBackgroundColor;

                // Normal item
                var rect = new Rectangle(2, 0, e.Item.Width - 3, e.Item.Height - 2);

                using (var b = new SolidBrush(bgColor))
                using (var p = new Pen(bgColor))
                using (var sgs = new SaveableGraphicsState(g))
                {
                    var path = Helper.RoundedRectangleHelper.CreateRoundedRectanglePath(rect, 6);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    g.FillPath(b, path);
                }

                // Header item on open menu
                if (e.Item.GetType() == typeof(ToolStripMenuItem))
                {
                    if (((ToolStripMenuItem)e.Item).DropDown.Visible && e.Item.IsOnDropDown == false)
                    {
                        using (var b = new SolidBrush(ThemeProvider.Theme.Colors.MainAccent))
                        using (var sgs = new SaveableGraphicsState(g))
                        {
                            var path = Helper.RoundedRectangleHelper.CreateRoundedRectanglePath(rect, 6);
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            g.FillPath(b, path);

                            // Handle this as it has the main accent so when this item does not have focus shows the correct fore color
                            e.Item.ForeColor = Helper.ColorExtender.GetContrastColor(ThemeProvider.Theme.Colors.MainAccent);
                        }
                    }
                }

                if (e.Item.Selected)
                    e.Item.ForeColor = Helper.ColorExtender.GetContrastColor(bgColor);
            }
        }

        #endregion
    }
}
