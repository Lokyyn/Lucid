using Lucid.Theming;

namespace Lucid.Controls;

public partial class LucidProgressBar : UserControl
{
    int min = 0; // Minimum value for progress range
    int max = 100; // Maximum value for progress range
    int val = 0; // Current progress
    Color BarColor = ThemeProvider.Theme.Colors.ControlHighlight; // Color of progress meter

    public bool ShowPercentage { get; set; }

    public Color PercentageTextColor { get; set; } = Color.Black;

    public Font PercentageFont { get; set; } = new Font(new FontFamily("Arial"), 7);


    private float _CurrentPercentage
    {
        get
        {
            float percent = (float)(val - min) / (float)(max - min);

            return percent;
        }
    }

    public LucidProgressBar()
    {
        InitializeComponent();
    }

    protected override void OnResize(EventArgs e)
    {
        // Invalidate the control to get a repaint.
        this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        SolidBrush brush = new SolidBrush(BarColor);
        Rectangle rect = this.ClientRectangle;

        // Draw Theming Colors
        //Rectangle rControl = this
        SolidBrush brushTheming = new SolidBrush(ThemeProvider.Theme.Colors.InactivScrollbar); // todo: this color needs to be replaced with an official theming color
        g.FillRectangle(brushTheming, rect);

        // Calculate area for drawing the progress.
        rect.Width = (int)((float)rect.Width * _CurrentPercentage);

        // Draw the progress meter.
        g.FillRectangle(brush, rect);

        if (ShowPercentage)
        {
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            var percentageText = $"{Math.Round(_CurrentPercentage * 100)}%";
            var measuredString = g.MeasureString(percentageText, PercentageFont);

            var point = new Point((ClientRectangle.Width / 2) - (int)measuredString.Width / 2, (ClientRectangle.Height / 2) - (int)measuredString.Height / 2);

            g.DrawString(percentageText, PercentageFont, new SolidBrush(PercentageTextColor), point);
        }

        // Draw a three-dimensional border around the control.
        Draw3DBorder(g);

        // Clean up.
        brush.Dispose();
        g.Dispose();
    }

    public int Minimum
    {
        get
        {
            return min;
        }

        set
        {
            // Prevent a negative value.
            if (value < 0)
            {
                min = 0;
            }

            // Make sure that the minimum value is never set higher than the maximum value.
            if (value > max)
            {
                min = value;
                min = value;
            }

            // Ensure value is still in range
            if (val < min)
            {
                val = min;
            }

            // Invalidate the control to get a repaint.
            this.Invalidate();
        }
    }

    public int Maximum
    {
        get
        {
            return max;
        }

        set
        {
            // Make sure that the maximum value is never set lower than the minimum value.
            if (value < min)
            {
                min = value;
            }

            max = value;

            // Make sure that value is still in range.
            if (val > max)
            {
                val = max;
            }

            // Invalidate the control to get a repaint.
            this.Invalidate();
        }
    }

    public int Value
    {
        get
        {
            return val;
        }

        set
        {
            int oldValue = val;

            // Make sure that the value does not stray outside the valid range.
            if (value < min)
            {
                val = min;
            }
            else if (value > max)
            {
                val = max;
            }
            else
            {
                val = value;
            }

            // Invalidate only the changed area.
            float percent;

            Rectangle newValueRect = this.ClientRectangle;
            Rectangle oldValueRect = this.ClientRectangle;

            // Use a new value to calculate the rectangle for progress.
            percent = (float)(val - min) / (float)(max - min);
            newValueRect.Width = (int)((float)newValueRect.Width * percent);

            // Use an old value to calculate the rectangle for progress.
            percent = (float)(oldValue - min) / (float)(max - min);
            oldValueRect.Width = (int)((float)oldValueRect.Width * percent);

            Rectangle updateRect = new Rectangle();

            // Find only the part of the screen that must be updated.
            if (newValueRect.Width > oldValueRect.Width)
            {
                updateRect.X = oldValueRect.Size.Width;
                updateRect.Width = newValueRect.Width - oldValueRect.Width;
            }
            else
            {
                updateRect.X = newValueRect.Size.Width;
                updateRect.Width = oldValueRect.Width - newValueRect.Width;
            }

            updateRect.Height = this.Height;

            // Invalidate the intersection region only.
            this.Invalidate();
        }
    }

    public Color ProgressBarColor
    {
        get
        {
            return BarColor;
        }
        set
        {
            if (AllowProgressBarColorOverride)
                BarColor = value;

            // Invalidate the control to get a repaint.
            this.Invalidate();
        }
    }

    public bool AllowProgressBarColorOverride { get; set; } = false;

    private void Draw3DBorder(Graphics g)
    {
        int PenWidth = (int)Pens.White.Width;

        g.DrawLine(Pens.DarkGray,
        new Point(this.ClientRectangle.Left, this.ClientRectangle.Top),
        new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Top));
        g.DrawLine(Pens.DarkGray,
        new Point(this.ClientRectangle.Left, this.ClientRectangle.Top),
        new Point(this.ClientRectangle.Left, this.ClientRectangle.Height - PenWidth));
        g.DrawLine(Pens.DarkGray,
        new Point(this.ClientRectangle.Left, this.ClientRectangle.Height - PenWidth),
        new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Height - PenWidth));
        g.DrawLine(Pens.DarkGray,
        new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Top),
        new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Height - PenWidth));
    }
}
