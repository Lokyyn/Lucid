using Lucid.Theming;
using System.Drawing.Drawing2D;

namespace Lucid.Controls;

public partial class LucidPerformanceToolTip : ToolTip
{
    private readonly Font _toolTipFont = new Font("Segoe UI", 10);
    private readonly Font _diffFont = new Font("Segoe UI", 10, FontStyle.Bold);

    private const int PlainPaddingH = 14;
    private const int PlainPaddingV = 10;
    private const int PerfHeight = 48;

    /// <summary>
    /// Optional numeric delta shown alongside a directional triangle.
    /// When <see langword="null"/> (default) the tooltip renders as a plain themed tooltip.
    /// </summary>
    public double? Difference { get; set; }

    public LucidPerformanceToolTip()
    {
        InitializeComponent();
        OwnerDraw = true;
        Popup += OnPopup;
        Draw += OnDraw;
    }

    /// <summary>
    /// Attaches this tooltip to <paramref name="control"/> using the tooltip text
    /// that was previously set via <see cref="ToolTip.SetToolTip"/>, or — for
    /// performance mode — the value of <see cref="ToolTip.GetToolTip"/>.
    /// For performance tooltips where no <see cref="ToolTip.SetToolTip"/> text exists,
    /// pass the label text explicitly via <paramref name="text"/>.
    /// </summary>
    public void Set(Control control, string text = "")
    {
        var existing = GetToolTip(control);
        if (string.IsNullOrEmpty(existing))
            SetToolTip(control, text);
    }

    public void Show(Form parentForm, string text, Point position)
    {
        Show(text, parentForm, position.X + 15, position.Y + 15);
    }

    private void OnPopup(object sender, PopupEventArgs e)
    {
        var text = GetToolTip(e.AssociatedControl);
        var textSize = TextRenderer.MeasureText(text, _toolTipFont);

        if (Difference.HasValue)
        {
            var diffText = PrintDifference(Difference.Value);
            var diffSize = TextRenderer.MeasureText(diffText, _diffFont);
            var width = Math.Max(textSize.Width, 25 + diffSize.Width) + PlainPaddingH;
            e.ToolTipSize = new Size(width, PerfHeight);
        }
        else
        {
            e.ToolTipSize = new Size(textSize.Width + PlainPaddingH, textSize.Height + PlainPaddingV);
        }
    }

    private void OnDraw(object sender, DrawToolTipEventArgs e)
    {
        var g = e.Graphics;
        var text = e.ToolTipText;

        using (var backBrush = new SolidBrush(ThemeProvider.Theme.Colors.BackgroundSecondary))
        using (var foreBrush = new SolidBrush(ThemeProvider.Theme.Colors.TextPrimary))
        using (var borderPen = new Pen(ThemeProvider.Theme.Colors.BorderDefault))
        {
            g.FillRectangle(backBrush, e.Bounds);
            g.DrawRectangle(borderPen, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1));

            if (!Difference.HasValue)
            {
                g.DrawString(text, _toolTipFont, foreBrush, new PointF(PlainPaddingH / 2f, PlainPaddingV / 2f));
                return;
            }

            g.DrawString(text, _toolTipFont, foreBrush, new Point(10, 5));
            g.SmoothingMode = SmoothingMode.HighQuality;

            var diff = Difference.Value;

            if (diff > 0)
            {
                using (var brush = new SolidBrush(ColorTranslator.FromHtml("#00e676")))
                using (var pen = new Pen(ColorTranslator.FromHtml("#00e676")))
                {
                    g.FillPath(brush, DrawTriangle(true));
                    g.DrawString(PrintDifference(diff), _diffFont, brush, new Point(25, 24));
                }
            }
            else if (diff == 0)
            {
                using (var brush = new SolidBrush(ColorTranslator.FromHtml("#fdd835")))
                {
                    g.FillRectangle(brush, new Rectangle(15, 29, 7, 7));
                    g.DrawString(PrintDifference(diff), _diffFont, brush, new Point(25, 24));
                }
            }
            else
            {
                using (var brush = new SolidBrush(ColorTranslator.FromHtml("#ff1744")))
                using (var pen = new Pen(ColorTranslator.FromHtml("#ff1744")))
                {
                    g.FillPath(brush, DrawTriangle(false));
                    g.DrawString(PrintDifference(diff), _diffFont, brush, new Point(25, 24));
                }
            }
        }
    }

    private static string PrintDifference(double number)
    {
        return Math.Abs(number % 1) <= double.Epsilon * 100
            ? Math.Round(number, 0).ToString()
            : number.ToString();
    }

    private static GraphicsPath DrawTriangle(bool isPositive)
    {
        var path = new GraphicsPath();
        var x = 17;
        var y = 30;

        if (isPositive)
        {
            path.AddLine(new Point(x, y), new Point(x + 5, y + 5));
            path.AddLine(new Point(x + 5, y + 5), new Point(x - 5, y + 5));
        }
        else
        {
            path.AddLine(new Point(x - 5, y), new Point(x + 5, y));
            path.AddLine(new Point(x + 5, y), new Point(x, y + 5));
        }

        path.CloseFigure();
        return path;
    }
}
