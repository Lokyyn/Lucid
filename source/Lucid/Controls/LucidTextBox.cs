using Lucid.Theming;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Lucid.Controls;

public class LucidTextBox : TextBox
{
    #region Win32 Region

    [DllImport("user32")]
    private static extern IntPtr GetWindowDC(IntPtr hwnd);

    [DllImport("user32")]
    private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

    [DllImport("user32", CharSet = CharSet.Unicode)]
    private static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wParam, string lParam);

    [DllImport("user32")]
    private static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

    private const int WM_NCPAINT    = 0x0085;
    private const int WM_PAINT      = 0x000F;
    private const int EM_SETCUEBANNER = 0x1501;
    private const int EM_SETMARGINS = 0x00D3;
    private const int EC_LEFTMARGIN  = 0x1;
    private const int EC_RIGHTMARGIN = 0x2;

    #endregion

    #region Field Region

    private bool _mouseOver;
    private bool _clearButtonHot;
    private string _placeholderText = string.Empty;
    private bool _showClearButton;
    private Image _icon;

    private const int OverlaySize = 14;
    private const int OverlayPad  = 5;

    #endregion

    #region Property Region

    [Category("Appearance")]
    [Description("Hint text shown when the TextBox is empty.")]
    [DefaultValue("")]
    public string PlaceholderText
    {
        get => _placeholderText;
        set
        {
            _placeholderText = value ?? string.Empty;
            if (IsHandleCreated)
                ApplyCueBanner();
        }
    }

    [Category("Appearance")]
    [Description("Shows a clear (×) button when the TextBox contains text.")]
    [DefaultValue(false)]
    public bool ShowClearButton
    {
        get => _showClearButton;
        set
        {
            _showClearButton = value;
            UpdateMargins();
            Invalidate();
        }
    }

    [Category("Appearance")]
    [Description("Icon displayed on the left side of the TextBox.")]
    [DefaultValue(null)]
    public Image Icon
    {
        get => _icon;
        set
        {
            _icon = value;
            UpdateMargins();
            Invalidate();
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color ForeColor { get => base.ForeColor; set => base.ForeColor = value; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor { get => base.BackColor; set => base.BackColor = value; }

    #endregion

    #region Constructor Region

    public LucidTextBox()
    {
        BackColor  = ThemeProvider.Theme.Colors.BackgroundTertiary;
        ForeColor  = ThemeProvider.Theme.Colors.TextPrimary;
        Padding    = new Padding(2, 2, 2, 2);
        BorderStyle = BorderStyle.FixedSingle;

        ThemeProvider.OnThemeChanged += ThemeProvider_OnThemeChanged;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            ThemeProvider.OnThemeChanged -= ThemeProvider_OnThemeChanged;

        base.Dispose(disposing);
    }

    #endregion

    #region Event Handler Region

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        ApplyCueBanner();
        UpdateMargins();
    }

    private void ThemeProvider_OnThemeChanged()
    {
        BackColor = ThemeProvider.Theme.Colors.BackgroundTertiary;
        ForeColor = ThemeProvider.Theme.Colors.TextPrimary;
        Invalidate();
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        _mouseOver = true;
        RedrawBorder();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        _mouseOver = false;
        if (_clearButtonHot)
        {
            _clearButtonHot = false;
            Invalidate();
        }
        RedrawBorder();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        if (!_showClearButton || string.IsNullOrEmpty(Text))
            return;

        var wasHot = _clearButtonHot;
        _clearButtonHot = GetClearButtonRect().Contains(e.Location);

        if (_clearButtonHot != wasHot)
        {
            Cursor = _clearButtonHot ? Cursors.Default : Cursors.IBeam;
            Invalidate();
        }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);

        if (e.Button == MouseButtons.Left
            && _showClearButton
            && !string.IsNullOrEmpty(Text)
            && GetClearButtonRect().Contains(e.Location))
        {
            Clear();
            _clearButtonHot = false;
            Invalidate();
        }
    }

    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
        if (_showClearButton)
            Invalidate();
    }

    protected override void OnGotFocus(EventArgs e)
    {
        base.OnGotFocus(e);
        RedrawBorder();
    }

    protected override void OnLostFocus(EventArgs e)
    {
        base.OnLostFocus(e);
        RedrawBorder();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        UpdateMargins();
    }

    #endregion

    #region WndProc Region

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        switch (m.Msg)
        {
            case WM_NCPAINT:
                DrawBorder();
                break;
            case WM_PAINT:
                DrawOverlays();
                break;
        }
    }

    #endregion

    #region Private Methods

    private void ApplyCueBanner()
    {
        SendMessage(Handle, EM_SETCUEBANNER, IntPtr.Zero, _placeholderText);
    }

    private void UpdateMargins()
    {
        if (!IsHandleCreated) return;

        int left  = _icon != null ? OverlayPad + OverlaySize + OverlayPad : 4;
        int right = _showClearButton ? OverlaySize + OverlayPad * 2 : 4;

        SendMessage(Handle, EM_SETMARGINS,
            (IntPtr)(EC_LEFTMARGIN | EC_RIGHTMARGIN),
            (IntPtr)((right << 16) | left));
    }

    private Rectangle GetClearButtonRect()
    {
        int x = Width - OverlayPad - OverlaySize - 2;
        int y = (Height - OverlaySize) / 2;
        return new Rectangle(x, y, OverlaySize, OverlaySize);
    }

    private void RedrawBorder()
    {
        var m = Message.Create(Handle, WM_NCPAINT, IntPtr.Zero, IntPtr.Zero);
        WndProc(ref m);
    }

    private void DrawBorder()
    {
        var dc = GetWindowDC(Handle);
        try
        {
            using (var g = Graphics.FromHdc(dc))
            using (var p = new Pen(Focused
                ? ThemeProvider.Theme.Colors.Accent
                : _mouseOver
                    ? ThemeProvider.Theme.Colors.BorderAccent
                    : ThemeProvider.Theme.Colors.BorderDefault, 1))
            {
                g.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
            }
        }
        finally
        {
            ReleaseDC(Handle, dc);
        }
    }

    private void DrawOverlays()
    {
        var hasIcon  = _icon != null;
        var hasClear = _showClearButton && !string.IsNullOrEmpty(Text);

        if (!hasIcon && !hasClear)
            return;

        using (var g = CreateGraphics())
        {
            if (hasIcon)
            {
                int y = (Height - OverlaySize) / 2;
                g.DrawImage(_icon, new Rectangle(OverlayPad, y, OverlaySize, OverlaySize));
            }

            if (hasClear)
                DrawClearButton(g);
        }
    }

    private void DrawClearButton(Graphics g)
    {
        var rect  = GetClearButtonRect();
        var color = _clearButtonHot
            ? ThemeProvider.Theme.Colors.TextPrimary
            : ThemeProvider.Theme.Colors.TextDisabled;

        var prev = g.SmoothingMode;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        const int pad = 3;
        using (var p = new Pen(color, 1.5f))
        {
            g.DrawLine(p, rect.Left + pad, rect.Top + pad, rect.Right - pad, rect.Bottom - pad);
            g.DrawLine(p, rect.Right - pad, rect.Top + pad, rect.Left + pad, rect.Bottom - pad);
        }

        g.SmoothingMode = prev;
    }

    #endregion
}
