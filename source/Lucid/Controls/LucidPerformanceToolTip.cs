using System.Drawing.Drawing2D;

namespace Lucid.Controls;

public partial class LucidPerformanceToolTip : ToolTip
{
    /// <summary>
    /// This control can show an numeric difference with an leading triangle. If the difference is positiv the triangle is positive, if negative it will be red.
    /// </summary>
    public LucidPerformanceToolTip()
    {
        InitializeComponent();
        OwnerDraw = true;
        Popup += new PopupEventHandler(this.OnPopup);
        Draw += new DrawToolTipEventHandler(this.OnDraw);
        

        _ToolTipFont = new Font("Segoe UI", 10);
    }

    /// <summary>
    /// This indicates the difference that is shown on the ToolTip. This value should be negative if the diffence should be printed negative.
    /// </summary>
    public double Difference { get; set; }

    /// <summary>
    /// This text is printed on the ToolTip
    /// </summary>
    public string Text { get; set; } = string.Empty;

    private Font _ToolTipFont;

    private void OnPopup(object sender, PopupEventArgs e)
    {
        e.ToolTipSize = new Size(TextRenderer.MeasureText(Text, _ToolTipFont).Width + 6, 48);
    }

    /// <summary>
    /// This method is used to set the ToolTip to an Control
    /// </summary>
    public void Set(Control control)
    {
        SetToolTip(control, ".");
    }

    public void Show(Form parentForm, Point position)
    {
        Show(Text, parentForm, position.X + 15, position.Y + 15);   
    }

    private void OnDraw(object sender, DrawToolTipEventArgs e)
    {
        Graphics g = e.Graphics;


        using (SolidBrush backBrush = new SolidBrush(Color.White))
        using (SolidBrush foreBrush = new SolidBrush(Color.Black))
        {
            g.FillRectangle(backBrush, e.Bounds);
            g.DrawString(Text, _ToolTipFont, foreBrush, new Point(10, 5));
            g.SmoothingMode = SmoothingMode.HighQuality;

            if (Difference > 0) // Positive
            {
                using (Pen greenPen = new Pen(ColorTranslator.FromHtml("#00e676")))
                using (SolidBrush greenBrush = new SolidBrush(ColorTranslator.FromHtml("#00e676")))
                {
                    var triangle = DrawTriangle(true);
                    g.FillPath(greenBrush, triangle);
                    g.DrawPath(greenPen, triangle);

                    g.DrawString(PrintDifference(Difference), new Font("Segoe UI", 10, FontStyle.Bold), greenBrush, new Point(17 + 8, 30 - 6)); // Static values are for testing
                }
            }
            else if (Difference == 0) // Equal
            {
                using (Pen yellowPen = new Pen(ColorTranslator.FromHtml("#fdd835")))
                using (SolidBrush yellowBrush = new SolidBrush(ColorTranslator.FromHtml("#fdd835")))
                {
                    //g.DrawRectangle(yellowPen, new Rectangle(17, 30, 7, 7));
                    g.FillRectangle(yellowBrush, new Rectangle(15, 29, 7, 7));
                    g.DrawString(PrintDifference(Difference), new Font("Segoe UI", 10, FontStyle.Bold), yellowBrush, new Point(17 + 8, 30 - 6)); // Static values are for testing)
                }
            }
            else // Negative
            {
                using (Pen redPen = new Pen(ColorTranslator.FromHtml("#ff1744")))
                using (SolidBrush redBrush = new SolidBrush(ColorTranslator.FromHtml("#ff1744")))
                {
                    // This is for an gred triangle
                    var triangle = DrawTriangle(false);
                    g.FillPath(redBrush, triangle);
                    g.DrawPath(redPen, triangle);

                    // Draw the text right next to the triangle
                    g.DrawString(PrintDifference(Difference), new Font("Segoe UI", 10, FontStyle.Bold), redBrush, new Point(17 + 8, 30 - 6)); // Static values are for testing
                }
            }
        }
    }

    private string PrintDifference(double number)
    {
        if (Math.Abs(number % 1) <= (double.Epsilon * 100))
            return Math.Round(number, 0).ToString();
        else
            return number.ToString();
    }

    private GraphicsPath DrawTriangle(bool isPositive)
    {
        var startPoint = new Point(17, 30);
        GraphicsPath path = new GraphicsPath();

        if (isPositive)
        {
            path.AddLine(startPoint, new Point(startPoint.X + 5, startPoint.Y + 5));
            path.AddLine(new Point(startPoint.X + 5, startPoint.Y + 5), new Point(startPoint.X - 5, startPoint.Y + 5));
            path.CloseFigure();
        }
        else
        {
            path.AddLine(new Point(startPoint.X - 5, startPoint.Y), new Point(startPoint.X + 5, startPoint.Y));
            path.AddLine(new Point(startPoint.X + 5, startPoint.Y), new Point(startPoint.X, startPoint.Y + 5));
            path.CloseFigure();
        }

        return path;
    }
}
