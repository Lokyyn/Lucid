namespace Lucid.Controls;

public partial class ScrollableControl : UserControl
{
    private readonly DarkScrollBar hscrollbar;
    private readonly DarkScrollBar vscrollbar;

    private Point scroll_position = Point.Empty;
    private Size canvas_size = Size.Empty;
    private Size auto_scroll_min_size = Size.Empty;
    private Size auto_scroll_margin = Size.Empty;
    private bool auto_scroll = false;
    private bool force_hscroll_visible = false;
    private bool force_vscroll_visible = false;

    public ScrollableControl()
    {
        //InitializeComponent();

        hscrollbar = new DarkScrollBar()
        {
            Visible = true,
            ScrollOrientation = DarkScrollOrientation.Horizontal,
            Dock = DockStyle.Bottom
        };
        hscrollbar.ValueChanged += HandleScroll;

        vscrollbar = new DarkScrollBar()
        {
            Visible = true,
            ScrollOrientation = DarkScrollOrientation.Vertical,
            Dock = DockStyle.Right
        };
        vscrollbar.ValueChanged += HandleScroll;

        SizeChanged += (o, e) => Recalculate(true);
        VisibleChanged += (o, e) => Recalculate(true);
    }

    public void Recalculate(bool doLayout)
    {
        var canvas = canvas_size;
        var client = ClientSize;

        canvas.Width += auto_scroll_margin.Width;
        canvas.Height += auto_scroll_margin.Height;

        var right_edge = client.Width;
        var bottom_edge = client.Height;
        var prev_right_edge = 0;
        var prev_bottom_edge = 0;

        var hscroll_visible = false;
        var vscroll_visible = false;

        var bar_size = LogicalToDeviceUnits(15);

        do
        {
            prev_right_edge = right_edge;
            prev_bottom_edge = bottom_edge;

            if ((force_hscroll_visible || (canvas.Width > right_edge && auto_scroll)) && client.Width > 0)
            {
                hscroll_visible = true;
                bottom_edge = client.Height - bar_size;// SystemInformation.HorizontalScrollBarHeight;
            }
            else
            {
                hscroll_visible = false;
                bottom_edge = client.Height;
            }

            if ((force_vscroll_visible || (canvas.Height > bottom_edge && auto_scroll)) && client.Height > 0)
            {
                vscroll_visible = true;
                right_edge = client.Width - bar_size;// SystemInformation.VerticalScrollBarWidth;
            }
            else
            {
                vscroll_visible = false;
                right_edge = client.Width;
            }
        } while (right_edge != prev_right_edge || bottom_edge != prev_bottom_edge);


        right_edge = Math.Max(right_edge, 0);
        bottom_edge = Math.Max(bottom_edge, 0);

        if (!vscroll_visible)
            vscrollbar.Value = 0;
        if (!hscroll_visible)
            hscrollbar.Value = 0;

        if (hscroll_visible)
        {
            hscrollbar.Maximum = canvas.Width - client.Width + bar_size;
            //hscrollbar.LargeChange = right_edge;
            //hscrollbar.SmallChange = 5;
        }
        else
        {
            if (hscrollbar.Visible)
                ScrollWindow(-scroll_position.X, 0);

            scroll_position.X = 0;
        }

        if (vscroll_visible)
        {
            vscrollbar.Maximum = canvas.Height - client.Height + bar_size;
            //vscrollbar.LargeChange = bottom_edge;
            //vscrollbar.SmallChange = 5;
        }
        else
        {
            if (vscrollbar.Visible)
                ScrollWindow(0, -scroll_position.Y);

            scroll_position.X = 0;
        }

        SuspendLayout();

        //hscrollbar.SetScaledBounds(0, client.Height - bar_size, ClientRectangle.Width - (vscroll_visible ? bar_size : 0), bar_size, BoundsSpecified.None);
        hscrollbar.Visible = hscroll_visible;
        //vscrollbar.SetScaledBounds(client.Width - bar_size, 0, bar_size, ClientRectangle.Height - (hscroll_visible ? bar_size : 0), BoundsSpecified.None);
        vscrollbar.Visible = vscroll_visible;

        //sizegrip.Visible = hscroll_visible && vscroll_visible;
        //sizegrip.SetScaledBounds(client.Width - bar_size, client.Height - bar_size, bar_size, bar_size, BoundsSpecified.None);

        ResumeLayout(doLayout);
    }

    private void HandleScroll (object sender, EventArgs e)
    {

    }

    // Scrolls the control by the requested offsets.
    private void ScrollWindow(int xOffset, int yOffset)
    {
        if (xOffset == 0 && yOffset == 0)
            return;

        SuspendLayout();

        //foreach (var c in Controls)
        //    c.SetScaledBounds(c.ScaledLeft - xOffset, c.ScaledTop - yOffset, c.ScaledWidth, c.ScaledHeight, BoundsSpecified.Location);

        scroll_position.Offset(xOffset, yOffset);

        ResumeLayout(false);
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
