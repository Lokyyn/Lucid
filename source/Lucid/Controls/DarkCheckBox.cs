using Lucid.Theming;
using System.ComponentModel;

namespace Lucid.Controls;

public class DarkCheckBox : CheckBox
{
    #region Field Region

    private DarkControlState _controlState = DarkControlState.Normal;

    private bool _spacePressed;

    #endregion

    #region Property Region

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Appearance Appearance
    {
        get { return base.Appearance; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool AutoEllipsis
    {
        get { return base.AutoEllipsis; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Image BackgroundImage
    {
        get { return base.BackgroundImage; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ImageLayout BackgroundImageLayout
    {
        get { return base.BackgroundImageLayout; }
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
    public new Image Image
    {
        get { return base.Image; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ContentAlignment ImageAlign
    {
        get { return base.ImageAlign; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new int ImageIndex
    {
        get { return base.ImageIndex; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new string ImageKey
    {
        get { return base.ImageKey; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ImageList ImageList
    {
        get { return base.ImageList; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ContentAlignment TextAlign
    {
        get { return base.TextAlign; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new TextImageRelation TextImageRelation
    {
        get { return base.TextImageRelation; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool ThreeState
    {
        get { return base.ThreeState; }
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

    public bool UseBackColorProperty { get; set; }

    #endregion

    #region Constructor Region

    public DarkCheckBox()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.UserPaint, true);
    }

    #endregion

    #region Method Region

    private void SetControlState(DarkControlState controlState)
    {
        if (_controlState != controlState)
        {
            _controlState = controlState;
            Invalidate();
        }
    }

    #endregion

    #region Event Handler Region

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        if (_spacePressed)
            return;

        if (e.Button == MouseButtons.Left)
        {
            if (ClientRectangle.Contains(e.Location))
                SetControlState(DarkControlState.Pressed);
            else
                SetControlState(DarkControlState.Hover);
        }
        else
        {
            SetControlState(DarkControlState.Hover);
        }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);

        if (!ClientRectangle.Contains(e.Location))
            return;

        SetControlState(DarkControlState.Pressed);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);

        if (_spacePressed)
            return;

        SetControlState(DarkControlState.Normal);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);

        if (_spacePressed)
            return;

        SetControlState(DarkControlState.Normal);
    }

    protected override void OnMouseCaptureChanged(EventArgs e)
    {
        base.OnMouseCaptureChanged(e);

        if (_spacePressed)
            return;

        var location = Cursor.Position;

        if (!ClientRectangle.Contains(location))
            SetControlState(DarkControlState.Normal);
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
            SetControlState(DarkControlState.Normal);
        else
            SetControlState(DarkControlState.Hover);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (e.KeyCode == Keys.Space)
        {
            _spacePressed = true;
            SetControlState(DarkControlState.Pressed);
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
                SetControlState(DarkControlState.Normal);
            else
                SetControlState(DarkControlState.Hover);
        }
    }

    #endregion

    #region Paint Region

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        var rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);

        var size = ThemeProvider.Theme.Sizes.CheckBoxSize;

        var textColor = ThemeProvider.Theme.Colors.LightText;
        var borderColor = ThemeProvider.Theme.Colors.LightText;
        var fillColor = Checked ? ThemeProvider.Theme.Colors.DarkBackground : ThemeProvider.Theme.Colors.LightestBackground;

        if (Enabled)
        {
            if (Focused)
            {
                borderColor = ThemeProvider.Theme.Colors.ControlHighlight;
                fillColor = ThemeProvider.Theme.Colors.MainAccent;
            }

            if (_controlState == DarkControlState.Hover)
            {
                borderColor = ThemeProvider.Theme.Colors.ControlHighlight;
                fillColor = ThemeProvider.Theme.Colors.MainAccent;
            }
            else if (_controlState == DarkControlState.Pressed)
            {
                borderColor = ThemeProvider.Theme.Colors.GreyHighlight;
                fillColor = ThemeProvider.Theme.Colors.GreySelection;
            }
        }
        else
        {
            textColor = ThemeProvider.Theme.Colors.DisabledText;
            borderColor = ThemeProvider.Theme.Colors.GreyHighlight;
            fillColor = ThemeProvider.Theme.Colors.GreySelection;
        }

        var backColor = UseBackColorProperty ? this.BackColor : ThemeProvider.Theme.Colors.MainBackgroundColor;

        using (var sg = new Common.SaveableGraphicsState(g))
        {
            using (var b = new SolidBrush(backColor))
            {
                g.FillRectangle(b, rect);
            }

            using (var p = new Pen(borderColor))
            {
                var boxRect = new Rectangle(0, (rect.Height / 2) - (size / 2), size, size);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                var path = Helper.RoundedRectangleHelper.CreateRoundedRectanglePath(boxRect, 5);

                g.DrawPath(p, path);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

                if (Checked)
                {
                    using (var b = new SolidBrush(ThemeProvider.Theme.Colors.MainAccent))
                    {
                        // Blue fill color
                        g.FillPath(b, Helper.RoundedRectangleHelper.CreateRoundedRectanglePath(new Rectangle(1, (rect.Height / 2) - (size / 2) + 1, size - 1, size - 1), 2));

                        // Replaced the old Checkstate with an icon
                        Rectangle imageRect = new Rectangle(1, (rect.Height / 2) - ((size) / 2), size, size);

                        if (ThemeProvider.Theme.Type == ThemeType.Dark)
                            g.DrawImage(Icons.ControlIcons.icon_checked_white, imageRect);
                        else
                            g.DrawImage(Icons.ControlIcons.icon_checked_black, imageRect);
                    }
                }
            }

            using (var b = new SolidBrush(textColor))
            {
                var stringFormat = new StringFormat
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Near
                };

                var modRect = new Rectangle(size + 4, 0, rect.Width - size, rect.Height);
                g.DrawString(Text, Font, b, modRect, stringFormat);
            }
        }
    }

    #endregion
}
