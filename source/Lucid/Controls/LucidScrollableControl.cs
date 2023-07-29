using System.ComponentModel;

namespace Lucid.Controls;

[ToolboxItem(false)]
public partial class LucidScrollableControl : Panel
{
    private readonly LucidScrollBar hscrollbar;
    private readonly LucidScrollBar vscrollbar;

    public LucidScrollableControl()
    {
        AutoScroll = false;
        DoubleBuffered = true;
        InitializeComponent();

        hscrollbar = new LucidScrollBar()
        {
            Visible = true,
            ScrollOrientation = LucidScrollOrientation.Horizontal,
            Dock = DockStyle.Bottom,
            Name = "hscrollbar",
            Location = new Point(0, 0),
            Size = new Size(Width, 10)
        };
        hscrollbar.ValueChanged += Hscrollbar_ValueChanged; ;

        vscrollbar = new LucidScrollBar()
        {
            Visible = true,
            ScrollOrientation = LucidScrollOrientation.Vertical,
            Dock = DockStyle.Right,
            Name = "vscrollbar",
            Location = new Point(0, 0),
            Size = new Size(10, Height)
        };
        vscrollbar.ValueChanged += Vscrollbar_ValueChanged; ;

        SizeChanged += (o, e) => UpdateScrollbarVisibility();
        VisibleChanged += (o, e) => UpdateScrollbarVisibility();

        Controls.Add(hscrollbar);
        Controls.Add(vscrollbar);
    }

    protected override void OnControlAdded(ControlEventArgs e)
    {
        base.OnControlAdded(e);
        UpdateScrollbarVisibility();
    }

    protected override void OnControlRemoved(ControlEventArgs e)
    {
        base.OnControlRemoved(e);
        UpdateScrollbarVisibility();
    }

    protected override void OnResize(EventArgs eventargs)
    {
        base.OnResize(eventargs);
        UpdateScrollbarVisibility();
    }

    public override void Refresh()
    {
        base.Refresh();
        UpdateScrollbarVisibility();
    }

    private void UpdateScrollbarVisibility()
    {
        bool showVerticalScrollbar = false;
        bool showHorizontalScrollbar = false;

        foreach (Control control in Controls)
        {
            if (control.Bottom > Height)
            {
                showVerticalScrollbar = true;
                break;
            }
        }

        foreach (Control control in Controls)
        {
            if (control.Right > Width)
            {
                showHorizontalScrollbar = true;
                break;
            }
        }

        vscrollbar.Visible = showVerticalScrollbar;
        hscrollbar.Visible = showHorizontalScrollbar;

        if (showVerticalScrollbar)
        {
            int scrollRange = CalculateVerticalScrollRange();
            vscrollbar.Minimum = 0;
            vscrollbar.Maximum = scrollRange;
            vscrollbar.Value = VerticalScroll.Value;
        }

        if (showHorizontalScrollbar)
        {
            int scrollRange = CalculateHorizontalScrollRange();
            hscrollbar.Minimum = 0;
            hscrollbar.Maximum = scrollRange;
            hscrollbar.Value = HorizontalScroll.Value;
        }
    }

    private int CalculateVerticalScrollRange()
    {
        int maxBottom = 0;

        foreach (Control control in Controls)
        {
            maxBottom = Math.Max(maxBottom, control.Bottom);
        }

        return Math.Max(0, maxBottom - Height);
    }

    private int CalculateHorizontalScrollRange()
    {
        int maxRight = 0;

        foreach (Control control in Controls)
        {
            maxRight = Math.Max(maxRight, control.Right);
        }

        return Math.Max(0, maxRight - Width);
    }

    private void Vscrollbar_ValueChanged(object sender, ScrollValueEventArgs e)
    {
        VerticalScroll.Value = e.Value;
        //Refresh();
    }

    private void Hscrollbar_ValueChanged(object sender, ScrollValueEventArgs e)
    {
        HorizontalScroll.Value = e.Value;
        //Refresh();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.TranslateTransform(-HorizontalScroll.Value, -VerticalScroll.Value);
        base.OnPaint(e);

        vscrollbar.BringToFront();
        hscrollbar.BringToFront();

    }


    /// <summary>
    /// Raises the Scroll event.
    /// </summary>
    protected virtual void OnScroll(ScrollEventArgs e) => Scroll?.Invoke(this, e);

    /// <summary>
    /// Raised when the ScrollableControl is scrolled.
    /// </summary>
    public event EventHandler<ScrollEventArgs>? Scroll;
}
