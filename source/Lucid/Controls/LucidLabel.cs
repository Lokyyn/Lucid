﻿using Lucid.Theming;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using Lucid.Common;

namespace Lucid.Controls;

public class LucidLabel : Label
{
    #region Field Region

    private bool _autoUpdateHeight;
    private bool _isGrowing;
    private bool _lineVisible;
    private bool _overrideForeColor;
    #endregion

    #region Property Region

    [Category("Layout")]
    [Description("Enables automatic height sizing based on the contents of the label.")]
    [DefaultValue(false)]
    public bool AutoUpdateHeight
    {
        get { return _autoUpdateHeight; }
        set
        {
            _autoUpdateHeight = value;

            if (_autoUpdateHeight)
            {
                AutoSize = false;
                ResizeLabel();
            }
        }
    }

    public new bool AutoSize
    {
        get { return base.AutoSize; }
        set
        {
            base.AutoSize = value;

            if (AutoSize)
                AutoUpdateHeight = false;
        }
    }

    [Category("Appearance")]
    [Description("Draws a line behind the text if AutoSize is false.")]
    [DefaultValue(false)]
    public bool LineVisible
    {
        get { return _lineVisible; }
        set
        {
            _lineVisible = value;
            base.Refresh();
        }
    }

    [Category("Appearance")]
    [Description("Allows that the ForeColor can be set in code/designer. If set to false it is not possible to set a specific ForeColor")]
    [DefaultValue(false)]
    public bool OverrideForeColor
    {
        get { return _overrideForeColor; }
        set
        {
            _overrideForeColor = value;
            base.Refresh();
        }
    }

    #endregion

    #region Constructor Region

    public LucidLabel()
    {
        ForeColor = ThemeProvider.Theme.Colors.LightText;
        BackColor = Color.Transparent;
    }

    #endregion

    #region Method Region

    private void ResizeLabel()
    {
        if (!_autoUpdateHeight || _isGrowing)
            return;

        try
        {
            _isGrowing = true;
            var sz = new Size(Width, int.MaxValue);
            sz = TextRenderer.MeasureText(Text, Font, sz, TextFormatFlags.WordBreak);
            Height = sz.Height + Padding.Vertical;
        }
        finally
        {
            _isGrowing = false;
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        if (!_overrideForeColor)
            ForeColor = ThemeProvider.Theme.Colors.LightText;

        base.OnPaint(e);

        if (_lineVisible && !base.AutoSize)
        {
            using (SaveableGraphicsState state = new SaveableGraphicsState(e.Graphics))
            using (Pen p = new Pen(ThemeProvider.Theme.Colors.LightText, 0.5f))
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                Point p1 = new Point(TextRenderer.MeasureText(Text, Font).Width + 3, Height / 2);
                Point p2 = new Point(Width, p1.Y);

                e.Graphics.DrawLine(p, p1, p2);
            }
        }
    }

    #endregion

    #region Event Handler Region

    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
        ResizeLabel();
    }

    protected override void OnFontChanged(EventArgs e)
    {
        base.OnFontChanged(e);
        ResizeLabel();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        ResizeLabel();
    }

    #endregion
}
