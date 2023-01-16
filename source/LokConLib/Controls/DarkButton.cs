using LCL.Theming;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using LCL.Helper;

namespace LCL.Controls
{
    [ToolboxBitmap(typeof(Button))]
    [DefaultEvent("Click")]
    public class DarkButton : Button
    {
        #region Field Region

        private DarkButtonStyle _style = DarkButtonStyle.Normal;
        private DarkControlState _buttonState = DarkControlState.Normal;

        private int _roundedCornerRadius;
        private RoundedCornerType _roundedCornerType;

        private bool _isDefault;
        private bool _spacePressed;

        private int _padding = ThemeProvider.Theme.Sizes.Padding / 2;
        private int _imagePadding = 5; // ThemeProvider.Theme.Sizes.Padding / 2

        #endregion

        #region Designer Property Region

        public new string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        public new bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                base.Enabled = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Determines the style of the button.")]
        [DefaultValue(DarkButtonStyle.Normal)]
        public DarkButtonStyle ButtonStyle
        {
            get { return _style; }
            set
            {
                _style = value;

                if (_style == DarkButtonStyle.Rounded)
                {
                    this.BackColor = Color.Transparent;
                }

                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Determines the radius of the rounded corners.")]
        public int RoundedCornerRadius
        {
            get { return _roundedCornerRadius; }
            set
            {
                _roundedCornerRadius = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Determines the type of the rounded corners.")]
        [DefaultValue(RoundedCornerType.Relative)]
        public RoundedCornerType RoundedCornerType
        {
            get { return _roundedCornerType; }
            set
            {
                _roundedCornerType = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Determines the amount of padding between the image and text.")]
        [DefaultValue(5)]
        public int ImagePadding
        {
            get { return _imagePadding; }
            set
            {
                _imagePadding = value;
                Invalidate();
            }
        }

        #endregion

        #region Code Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool AutoEllipsis
        {
            get { return false; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DarkControlState ButtonState
        {
            get { return _buttonState; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ContentAlignment ImageAlign
        {
            get { return base.ImageAlign; }
            set { base.ImageAlign = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool FlatAppearance
        {
            get { return false; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FlatStyle FlatStyle
        {
            get { return base.FlatStyle; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ContentAlignment TextAlign
        {
            get { return base.TextAlign; }
            set { base.TextAlign = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool UseCompatibleTextRendering
        {
            get { return false; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool UseVisualStyleBackColor
        {
            get { return false; }
        }

        #endregion

        #region Constructor Region

        public DarkButton()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            base.UseVisualStyleBackColor = false;
            base.UseCompatibleTextRendering = false;

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, false);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            SetButtonState(DarkControlState.Normal);
            RoundedCornerRadius = 16;
            RoundedCornerType = RoundedCornerType.Relative;

            Padding = new Padding(_padding);
        }

        #endregion

        #region Method Region

        private void SetButtonState(DarkControlState buttonState)
        {
            if (_buttonState != buttonState)
            {
                _buttonState = buttonState;
                Invalidate();
            }
        }

        #endregion

        #region Event Handler Region

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            var form = FindForm();
            if (form != null)
            {
                if (form.AcceptButton == this)
                    _isDefault = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_spacePressed)
                return;

            if (e.Button == MouseButtons.Left)
            {
                if (ClientRectangle.Contains(e.Location))
                    SetButtonState(DarkControlState.Pressed);
                else
                    SetButtonState(DarkControlState.Hover);
            }
            else
            {
                SetButtonState(DarkControlState.Hover);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!ClientRectangle.Contains(e.Location))
                return;

            SetButtonState(DarkControlState.Pressed);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (_spacePressed)
                return;

            SetButtonState(DarkControlState.Normal);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (_spacePressed)
                return;

            SetButtonState(DarkControlState.Normal);
        }

        protected override void OnMouseCaptureChanged(EventArgs e)
        {
            base.OnMouseCaptureChanged(e);

            if (_spacePressed)
                return;

            var location = Cursor.Position;

            if (!ClientRectangle.Contains(location))
                SetButtonState(DarkControlState.Normal);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            _spacePressed = false;

            var location = Cursor.Position;

            if (!ClientRectangle.Contains(location))
                SetButtonState(DarkControlState.Normal);
            else
                SetButtonState(DarkControlState.Hover);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Space)
            {
                _spacePressed = true;
                SetButtonState(DarkControlState.Pressed);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.KeyCode == Keys.Space)
            {
                _spacePressed = false;

                var location = Cursor.Position;

                if (!ClientRectangle.Contains(location))
                    SetButtonState(DarkControlState.Normal);
                else
                    SetButtonState(DarkControlState.Hover);
            }
        }

        public override void NotifyDefault(bool value)
        {
            base.NotifyDefault(value);

            if (!DesignMode)
                return;

            _isDefault = value;
            Invalidate();
        }

        #endregion

        #region Paint Region

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            var rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);

            var textColor = ThemeProvider.Theme.Colors.LightText;
            var borderColor = ThemeProvider.Theme.Colors.GreySelection;
            var fillColor = _isDefault ? ThemeProvider.Theme.Colors.DarkBlueBackground : ThemeProvider.Theme.Colors.LightBackground;

            if (Enabled)
            {
                if (ButtonStyle == DarkButtonStyle.Normal || ButtonStyle == DarkButtonStyle.Rounded)
                {
                    if (Focused && TabStop)
                        borderColor = ThemeProvider.Theme.Colors.ControlHighlight;

                    switch (ButtonState)
                    {
                        case DarkControlState.Hover:
                            fillColor = _isDefault ? ThemeProvider.Theme.Colors.BlueBackground : ThemeProvider.Theme.Colors.LighterBackground;
                            break;
                        case DarkControlState.Pressed:
                            fillColor = _isDefault ? ThemeProvider.Theme.Colors.DarkBackground : ThemeProvider.Theme.Colors.DarkBackground;
                            break;
                    }
                }
                else if (ButtonStyle == DarkButtonStyle.Flat)
                {
                    switch (ButtonState)
                    {
                        case DarkControlState.Normal:
                            fillColor = ThemeProvider.Theme.Colors.MainBackgroundColor;
                            break;
                        case DarkControlState.Hover:
                            fillColor = ThemeProvider.Theme.Colors.MediumBackground;
                            break;
                        case DarkControlState.Pressed:
                            fillColor = ThemeProvider.Theme.Colors.DarkBackground;
                            break;
                    }
                }
            }
            else
            {
                textColor = ThemeProvider.Theme.Colors.DisabledText;
                fillColor = ThemeProvider.Theme.Colors.DarkGreySelection;
            }


            if (ButtonStyle != DarkButtonStyle.Rounded)
            {
                using (var b = new SolidBrush(fillColor))
                {
                    g.FillRectangle(b, rect);
                }
            }

            if (ButtonStyle == DarkButtonStyle.Normal)
            {
                using (var p = new Pen(borderColor, 1))
                {
                    var modRect = new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);

                    g.DrawRectangle(p, modRect);
                }
            }
            else if (ButtonStyle == DarkButtonStyle.Rounded)
            {
                using (var p = new Pen(borderColor, 1))
                using (var pr = new Pen(Color.Red, 1))
                using (var b = new SolidBrush(fillColor))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;

                    var relativeValue = (double)_roundedCornerRadius / 100;

                    if (relativeValue > 0.5)
                        relativeValue = 0.5;

                    int radius = _roundedCornerType == RoundedCornerType.Abosolut ? _roundedCornerRadius : (int)(ClientSize.Height * relativeValue);

                    if (_roundedCornerType == RoundedCornerType.Relative)
                        radius = radius * 2;

                    GraphicsPath path = RoundedRectangleHelper.CreateRoundedRectanglePath(new Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1), radius);
                    g.FillPath(b, path);
                    path.CloseFigure();

                    g.DrawPath(p, path);
                }
            }

            var textOffsetX = 0;
            var textOffsetXRight = 0;
            var textOffsetY = 0;
            var horizontalAlignment = StringAlignment.Center;

            if (Image != null)
            {
                var x = (ClientSize.Width / 2) - (Image.Size.Width / 2);
                var y = (ClientSize.Height / 2) - (Image.Size.Height / 2);
                var stringSize = g.MeasureString(Text, Font, rect.Size);
                int extraSpace = 0;

                switch (TextImageRelation)
                {
                    case TextImageRelation.ImageAboveText:
                        textOffsetY = (Image.Size.Height / 2) + (ImagePadding / 2);
                        y = y - ((int)(stringSize.Height / 2) + (ImagePadding / 2));
                        break;
                    case TextImageRelation.TextAboveImage:
                        textOffsetY = ((Image.Size.Height / 2) + (ImagePadding / 2)) * -1;
                        y = y + ((int)(stringSize.Height / 2) + (ImagePadding / 2));
                        break;
                    case TextImageRelation.ImageBeforeText:
                        extraSpace = Math.Max(0, rect.Width - ((int)stringSize.Width + Image.Size.Width + Padding.Horizontal + ImagePadding));
                        textOffsetX = extraSpace / 2 + Image.Size.Width + ImagePadding;
                        x = Padding.Left + extraSpace / 2;
                        horizontalAlignment = StringAlignment.Near;
                        break;
                    case TextImageRelation.TextBeforeImage:
                        extraSpace = Math.Max(0, rect.Width - ((int)stringSize.Width + Image.Size.Width + Padding.Horizontal + ImagePadding));
                        textOffsetXRight = extraSpace / 2 + Image.Width + ImagePadding;
                        x = rect.Width - (Padding.Right + Image.Width + extraSpace / 2);
                        horizontalAlignment = StringAlignment.Far;
                        break;
                }


                if (Enabled)
                    g.DrawImageUnscaled(Image, x, y);
                else
                    g.DrawImageUnscaled(ImageExtender.ToGrayScale(new Bitmap(Image)), x, y);
            }

            using (var b = new SolidBrush(textColor))
            {
                var modRect = new Rectangle(rect.Left + textOffsetX + Padding.Left,
                                             rect.Top + textOffsetY + Padding.Top,
                                             rect.Width - (Padding.Horizontal + textOffsetX + textOffsetXRight),
                                             rect.Height - Padding.Vertical);

                var stringFormat = new StringFormat
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = horizontalAlignment,
                    Trimming = StringTrimming.EllipsisCharacter
                };

                g.DrawString(Text, Font, b, modRect, stringFormat);
            }
        }

        #endregion

        #region For Transparency
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20; // WS_EX_TRANSPARENT
                return cp;
            }
        }
        #endregion
    }
}
