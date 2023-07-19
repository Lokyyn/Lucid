using Lucid.Theming;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using Lucid.Common;
using Lucid.Helper;

namespace Lucid.Controls
{
    public class DarkSectionPanel : Panel
    {
        #region Field Region

        private string _sectionHeader;

        #endregion

        #region Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding
        {
            get { return base.Padding; }
        }

        [Category("Appearance")]
        [Description("The section header text associated with this control.")]
        public string SectionHeader
        {
            get { return _sectionHeader; }
            set
            {
                _sectionHeader = value;
                Invalidate();
            }
        }

        #endregion

        #region Constructor Region

        public DarkSectionPanel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            base.Padding = new Padding(1, 25, 1, 1);
        }

        #endregion

        #region Event Handler Region

        protected override void OnEnter(System.EventArgs e)
        {
            base.OnEnter(e);

            Invalidate();
        }

        protected override void OnLeave(System.EventArgs e)
        {
            base.OnLeave(e);

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (Controls.Count > 0)
                Controls[0].Focus();
        }

        #endregion

        #region Paint Region

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            var rect = ClientRectangle;

            var modRect = new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);

            var completePath = new GraphicsPath();
            completePath.AddRectangle(modRect);
            var roundPath = RoundedRectangleHelper.CreateRoundedRectanglePath(modRect, 16);

            // Fill body
            using (var b = new SolidBrush(ThemeProvider.Theme.Colors.MainBackgroundColor))
            {
                g.FillRectangle(b, rect);
            }

            // Draw header
            var bgColor = ContainsFocus ? ThemeProvider.Theme.Colors.BlueBackground : ThemeProvider.Theme.Colors.HeaderBackground;
            var darkColor = ContainsFocus ? ThemeProvider.Theme.Colors.DarkBlueBorder : ThemeProvider.Theme.Colors.DarkBorder;
            var lightColor = ContainsFocus ? ThemeProvider.Theme.Colors.LightBlueBorder : ThemeProvider.Theme.Colors.LightBorder;

            using (var b = new SolidBrush(bgColor))
            {
                var bgRect = new Rectangle(0, 0, rect.Width, 25);
                g.FillRectangle(b, bgRect);
            }

            using (var p = new Pen(darkColor))
            {
                g.DrawLine(p, rect.Left, 0, rect.Right, 0);
                g.DrawLine(p, rect.Left, 25 - 1, rect.Right, 25 - 1);
            }

            using (var p = new Pen(lightColor))
            {
                g.DrawLine(p, rect.Left, 1, rect.Right, 1);
            }

            var xOffset = 3;

            using (var b = new SolidBrush(ThemeProvider.Theme.Colors.LightText))
            {
                var textRect = new Rectangle(xOffset, 0, rect.Width - 4 - xOffset, 25);

                var format = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.NoWrap,
                    Trimming = StringTrimming.EllipsisCharacter
                };

                g.DrawString(SectionHeader, Font, b, textRect, format);
            }

            // Draw border
            using (var p = new Pen(ThemeProvider.Theme.Colors.DarkBorder, 1))
            using (var state = new SaveableGraphicsState(e.Graphics))
            {
                completePath.AddPath(roundPath, true);

                // Draw the corners so the round effect is hown correctly
                using (var b = new SolidBrush(ThemeProvider.Theme.Colors.MainBackgroundColor))
                    g.FillPath(b, completePath);

                // Draw a rectangle so the wrong pixels outside the path are painted with the back color
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                g.DrawRectangle(new Pen(ThemeProvider.Theme.Colors.MainBackgroundColor, 1), new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1));
                g.DrawPath(p, roundPath);
            }

        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Absorb event
        }

        #endregion
    }
}
