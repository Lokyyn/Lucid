using Lucid.Theming;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Lucid.Controls;

public class LucidTextBox : TextBox
{
    #region Constructor Region

    public LucidTextBox()
    {
        BackColor = ThemeProvider.Theme.Colors.LightBackground;
        ForeColor = ThemeProvider.Theme.Colors.LightText;
        Padding = new Padding(2, 2, 2, 2);
        BorderStyle = BorderStyle.FixedSingle;

        ThemeProvider.OnThemeChanged += ThemeProvider_OnThemeChanged;
    }

    private void ThemeProvider_OnThemeChanged()
    {
        BackColor = ThemeProvider.Theme.Colors.LightBackground;
        ForeColor = ThemeProvider.Theme.Colors.LightText;
    }

    #endregion

    [DllImport("user32")]
    private static extern IntPtr GetWindowDC(IntPtr hwnd);
    private const int WM_NCPAINT = 0x85;
    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);
        if (Focused)
        {
            var dc = GetWindowDC(Handle);
            using (Graphics g = Graphics.FromHdc(dc))
            using (Pen accentColor = new Pen(ThemeProvider.Theme.Colors.MainAccent))
            {
                g.DrawRectangle(accentColor, 0, 0, Width - 1, Height - 1);
            }
        }
    }


    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color ForeColor { get => base.ForeColor; set => base.ForeColor = value; }


    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor { get => base.BackColor; set => base.BackColor = value; }

}
