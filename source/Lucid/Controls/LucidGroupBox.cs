﻿using Lucid.Theming;
using System.ComponentModel;

namespace Lucid.Controls;

public class LucidGroupBox : GroupBox
{
    private Color _borderColor = ThemeProvider.Theme.Colors.DarkBorder;

    [Category("Appearance")]
    [Description("Determines the color of the border.")]
    public Color BorderColor
    {
        get { return _borderColor; }
        set
        {
            _borderColor = value;
            Invalidate();
        }
    }

    public LucidGroupBox()
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint, true);

        ResizeRedraw = true;
        DoubleBuffered = true;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        var rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);
        var stringSize = g.MeasureString(Text, Font);

        var textColor = ThemeProvider.Theme.Colors.LightText;
        var fillColor = ThemeProvider.Theme.Colors.MainBackgroundColor;

        using (var b = new SolidBrush(fillColor))
        {
            g.FillRectangle(b, rect);
        }

        using (var p = new Pen(BorderColor, 1))
        {
            var borderRect = new Rectangle(0, (int)stringSize.Height / 2, rect.Width - 1, rect.Height - ((int)stringSize.Height / 2) - 1);
            g.DrawRectangle(p, borderRect);
        }

        var textRect = new Rectangle(rect.Left + ThemeProvider.Theme.Sizes.Padding,
                rect.Top,
                rect.Width - (ThemeProvider.Theme.Sizes.Padding * 2),
                (int)stringSize.Height);

        using (var b2 = new SolidBrush(fillColor))
        {
            var modRect = new Rectangle(textRect.Left, textRect.Top, Math.Min(textRect.Width, (int)stringSize.Width), textRect.Height);
            g.FillRectangle(b2, modRect);
        }

        using (var b = new SolidBrush(textColor))
        {
            var stringFormat = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.NoWrap,
                Trimming = StringTrimming.EllipsisCharacter
            };

            g.DrawString(Text, Font, b, textRect, stringFormat);
        }
    }
}
