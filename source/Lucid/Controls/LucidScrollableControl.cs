using Lucid.Theming;
using System.ComponentModel;

namespace Lucid.Controls;

[ToolboxItem(true)]
public partial class LucidScrollableControl : Panel
{
    private readonly LucidScrollBar _hScrollBar;
    private readonly LucidScrollBar _vScrollBar;
    private readonly Panel _scrollCorner;

    private int _scrollX;
    private int _scrollY;
    private bool _updatingScrollBars;
    private bool _movingChildren;

    public LucidScrollableControl()
    {
        AutoScroll = false;
        DoubleBuffered = true;
        InitializeComponent();

        _vScrollBar = new LucidScrollBar
        {
            ScrollOrientation = LucidScrollOrientation.Vertical,
            Name = "vScrollBar"
        };
        _hScrollBar = new LucidScrollBar
        {
            ScrollOrientation = LucidScrollOrientation.Horizontal,
            Name = "hScrollBar"
        };

        _scrollCorner = new Panel
        {
            Name = "scrollCorner",
            Visible = false
        };

        _vScrollBar.ValueChanged += VScrollBar_ValueChanged;
        _hScrollBar.ValueChanged += HScrollBar_ValueChanged;

        Controls.Add(_scrollCorner);
        Controls.Add(_vScrollBar);
        Controls.Add(_hScrollBar);
    }

    private bool IsScrollBar(Control c) => c == _vScrollBar || c == _hScrollBar || c == _scrollCorner;

    protected override void OnControlAdded(ControlEventArgs e)
    {
        base.OnControlAdded(e);
        if (IsScrollBar(e.Control)) return;
        e.Control.SizeChanged += OnChildChanged;
        e.Control.LocationChanged += OnChildChanged;
        UpdateScrollBars();
    }

    protected override void OnControlRemoved(ControlEventArgs e)
    {
        base.OnControlRemoved(e);
        if (IsScrollBar(e.Control)) return;
        e.Control.SizeChanged -= OnChildChanged;
        e.Control.LocationChanged -= OnChildChanged;
        UpdateScrollBars();
    }

    private void OnChildChanged(object sender, EventArgs e)
    {
        if (_movingChildren) return;
        UpdateScrollBars();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        UpdateScrollBars();
    }

    private void VScrollBar_ValueChanged(object sender, ScrollValueEventArgs e)
    {
        if (_updatingScrollBars) return;
        int delta = e.Value - _scrollY;
        _scrollY = e.Value;
        MoveChildren(0, -delta);
        OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, e.Value));
    }

    private void HScrollBar_ValueChanged(object sender, ScrollValueEventArgs e)
    {
        if (_updatingScrollBars) return;
        int delta = e.Value - _scrollX;
        _scrollX = e.Value;
        MoveChildren(-delta, 0);
        OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, e.Value));
    }

    private void MoveChildren(int dx, int dy)
    {
        _movingChildren = true;
        SuspendLayout();
        foreach (Control c in Controls)
        {
            if (IsScrollBar(c)) continue;
            c.Left += dx;
            c.Top += dy;
        }
        ResumeLayout(false);
        _movingChildren = false;
    }

    private void UpdateScrollBars()
    {
        if (_updatingScrollBars) return;
        _updatingScrollBars = true;
        try
        {
            int sbSize = ThemeProvider.Theme.Sizes.ScrollBarSize;

            // Logical content extents: screen position + current scroll offset = content position
            int contentRight = 0;
            int contentBottom = 0;
            foreach (Control c in Controls)
            {
                if (IsScrollBar(c)) continue;
                contentRight = Math.Max(contentRight, c.Right + _scrollX);
                contentBottom = Math.Max(contentBottom, c.Bottom + _scrollY);
            }

            bool needV = contentBottom > Height;
            bool needH = contentRight > Width;
            // Re-check: the other scrollbar may consume space and push content over threshold
            if (needV && !needH) needH = contentRight > Width - sbSize;
            if (needH && !needV) needV = contentBottom > Height - sbSize;

            int visibleW = Width - (needV ? sbSize : 0);
            int visibleH = Height - (needH ? sbSize : 0);

            _vScrollBar.SetBounds(Width - sbSize, 0, sbSize, needH ? visibleH : Height);
            _hScrollBar.SetBounds(0, Height - sbSize, needV ? visibleW : Width, sbSize);
            _vScrollBar.Visible = needV;
            _hScrollBar.Visible = needH;
            bool showCorner = needV && needH;
            _scrollCorner.Visible = showCorner;
            if (showCorner)
            {
                _scrollCorner.BackColor = ThemeProvider.Theme.Colors.BackgroundSecondary;
                _scrollCorner.SetBounds(Width - sbSize, Height - sbSize, sbSize, sbSize);
                _scrollCorner.BringToFront();
            }

            _vScrollBar.BringToFront();
            _hScrollBar.BringToFront();

            if (needV)
            {
                _vScrollBar.Minimum = 0;
                _vScrollBar.Maximum = contentBottom;
                _vScrollBar.ViewSize = visibleH;
                int maxScroll = Math.Max(0, contentBottom - visibleH);
                if (_scrollY > maxScroll) SetScrollY(maxScroll);
                _vScrollBar.Value = _scrollY;
            }
            else
            {
                SetScrollY(0);
            }

            if (needH)
            {
                _hScrollBar.Minimum = 0;
                _hScrollBar.Maximum = contentRight;
                _hScrollBar.ViewSize = visibleW;
                int maxScroll = Math.Max(0, contentRight - visibleW);
                if (_scrollX > maxScroll) SetScrollX(maxScroll);
                _hScrollBar.Value = _scrollX;
            }
            else
            {
                SetScrollX(0);
            }
        }
        finally
        {
            _updatingScrollBars = false;
        }
    }

    private void SetScrollY(int newY)
    {
        if (_scrollY == newY) return;
        int delta = _scrollY - newY;
        _scrollY = newY;
        MoveChildren(0, delta);
    }

    private void SetScrollX(int newX)
    {
        if (_scrollX == newX) return;
        int delta = _scrollX - newX;
        _scrollX = newX;
        MoveChildren(delta, 0);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        using var b = new SolidBrush(ThemeProvider.Theme.Colors.BackgroundSecondary);
        e.Graphics.FillRectangle(b, ClientRectangle);
        base.OnPaint(e);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
        base.OnMouseWheel(e);
        if (_vScrollBar.Visible)
            _vScrollBar.ScrollByPhysical(e.Delta > 0 ? 10 : -10);
        else if (_hScrollBar.Visible)
            _hScrollBar.ScrollByPhysical(e.Delta > 0 ? 10 : -10);
    }

    protected override void OnScroll(ScrollEventArgs e)
    {
        base.OnScroll(e);
    }
}
