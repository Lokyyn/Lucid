using Lucid.Theming;

namespace Lucid.Controls;

public class LucidStatusStrip : StatusStrip
{
    #region Constructor Region

    public LucidStatusStrip()
    {
        AutoSize = false;
        BackColor = ThemeProvider.Theme.Colors.BackgroundSecondary;
        ForeColor = ThemeProvider.Theme.Colors.LightText;
        Padding = new Padding(0, 5, 0, 3);
        Size = new Size(Size.Width, 24);
        SizingGrip = false;
    }

    #endregion

    #region Paint Region

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        var g = e.Graphics;

        using (var b = new SolidBrush(ThemeProvider.Theme.Colors.BackgroundSecondary))
        {
            g.FillRectangle(b, ClientRectangle);
        }

        using (var p = new Pen(ThemeProvider.Theme.Colors.BorderDefault))
        {
            g.DrawLine(p, ClientRectangle.Left, 0, ClientRectangle.Right, 0);
        }

        using (var p = new Pen(ThemeProvider.Theme.Colors.BorderDefault))
        {
            g.DrawLine(p, ClientRectangle.Left, 1, ClientRectangle.Right, 1);
        }
    }

    #endregion
}
