using Lucid.Theming;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Lucid.Controls;

/// <summary>
/// Determines whether the slider uses a single value or a range (two handles).
/// </summary>
public enum LucidSliderMode
{
    SingleValue,
    Range
}

/// <summary>
/// A themed slider control inspired by shadcn/ui.
/// Supports single-value and range mode (two handles).
/// </summary>
[ToolboxBitmap(typeof(TrackBar))]
[DefaultEvent("ValueChanged")]
public class LucidSlider : Control
{
    #region Constants

    private const int TrackHeight = 4;
    private const int ThumbSize = 16;
    private const int ThumbHoverSize = 18;

    #endregion

    #region Fields

    private LucidSliderMode _mode = LucidSliderMode.SingleValue;

    private double _minimum = 0;
    private double _maximum = 100;
    private double _value = 50;
    private double _rangeStart = 25;
    private double _rangeEnd = 75;
    private double _step = 1;

    private bool _showValueLabel = true;
    private string _customValueLabel = "";
    private bool _showTickMarks = false;
    private int _tickFrequency = 10;

    // Interaction state
    private enum ActiveThumb { None, Single, RangeStart, RangeEnd }
    private ActiveThumb _dragging = ActiveThumb.None;
    private ActiveThumb _hovered = ActiveThumb.None;

    #endregion

    #region Events

    [Category("Behavior")]
    [Description("Fires when Value changes (SingleValue mode).")]
    public event EventHandler? ValueChanged;

    [Category("Behavior")]
    [Description("Fires when RangeStart or RangeEnd changes (Range mode).")]
    public event EventHandler? RangeChanged;

    #endregion

    #region Properties

    [Category("Behavior")]
    [Description("Single value slider or range slider with two handles.")]
    [DefaultValue(LucidSliderMode.SingleValue)]
    public LucidSliderMode Mode
    {
        get => _mode;
        set { _mode = value; Invalidate(); }
    }

    [Category("Behavior")]
    [DefaultValue(0.0)]
    public double Minimum
    {
        get => _minimum;
        set
        {
            _minimum = value;
            ClampAll();
            Invalidate();
        }
    }

    [Category("Behavior")]
    [DefaultValue(100.0)]
    public double Maximum
    {
        get => _maximum;
        set
        {
            _maximum = value;
            ClampAll();
            Invalidate();
        }
    }

    [Category("Behavior")]
    [DefaultValue(50.0)]
    public double Value
    {
        get => _value;
        set
        {
            var clamped = Clamp(value, _minimum, _maximum);
            if (Math.Abs(clamped - _value) < 1e-10) return;
            _value = clamped;
            ValueChanged?.Invoke(this, EventArgs.Empty);
            Invalidate();
        }
    }

    [Category("Behavior")]
    [DefaultValue(25.0)]
    public double RangeStart
    {
        get => _rangeStart;
        set
        {
            var clamped = Clamp(value, _minimum, _rangeEnd);
            if (Math.Abs(clamped - _rangeStart) < 1e-10) return;
            _rangeStart = clamped;
            RangeChanged?.Invoke(this, EventArgs.Empty);
            Invalidate();
        }
    }

    [Category("Behavior")]
    [DefaultValue(75.0)]
    public double RangeEnd
    {
        get => _rangeEnd;
        set
        {
            var clamped = Clamp(value, _rangeStart, _maximum);
            if (Math.Abs(clamped - _rangeEnd) < 1e-10) return;
            _rangeEnd = clamped;
            RangeChanged?.Invoke(this, EventArgs.Empty);
            Invalidate();
        }
    }

    [Category("Behavior")]
    [DefaultValue(1.0)]
    [Description("Snap increment. Set to 0 for smooth (no snapping).")]
    public double Step
    {
        get => _step;
        set { _step = value < 0 ? 0 : value; }
    }

    [Category("Appearance")]
    [DefaultValue(true)]
    [Description("Shows the current value above the thumb.")]
    public bool ShowValueLabel
    {
        get => _showValueLabel;
        set { _showValueLabel = value; Invalidate(); }
    }

    [Category("Appearance")]
    [DefaultValue(true)]
    [Description("Shows the current value above the thumb.")]
    public string CustomValueLabel
    {
        get => _customValueLabel;
        set { _customValueLabel = value; Invalidate(); }
    }

    [Category("Appearance")]
    [DefaultValue(false)]
    public bool ShowTickMarks
    {
        get => _showTickMarks;
        set { _showTickMarks = value; Invalidate(); }
    }

    [Category("Behavior")]
    [DefaultValue(10)]
    public int TickFrequency
    {
        get => _tickFrequency;
        set { _tickFrequency = value < 1 ? 1 : value; Invalidate(); }
    }

    #endregion

    #region Constructor

    public LucidSlider()
    {
        SetStyle(
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw |
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.SupportsTransparentBackColor,
            true);

        Height = 36;
        MinimumSize = new Size(60, 28);
        Cursor = Cursors.Default;
    }

    #endregion

    #region Layout Helpers

    /// <summary>Returns the usable horizontal range for the track, accounting for thumb overhang.</summary>
    private Rectangle GetTrackRect()
    {
        int margin = ThumbHoverSize / 2 + 2;
        int trackY = ClientSize.Height / 2 - TrackHeight / 2;
        return new Rectangle(margin, trackY, ClientSize.Width - margin * 2, TrackHeight);
    }

    private int ValueToX(double val, Rectangle track)
    {
        if (Math.Abs(_maximum - _minimum) < 1e-10) return track.Left;
        double ratio = (val - _minimum) / (_maximum - _minimum);
        return track.Left + (int)(ratio * track.Width);
    }

    private double XToValue(int x, Rectangle track)
    {
        double ratio = (double)(x - track.Left) / track.Width;
        ratio = Math.Max(0, Math.Min(1, ratio));
        double raw = _minimum + ratio * (_maximum - _minimum);
        if (_step > 0)
            raw = Math.Round(raw / _step) * _step;
        return Clamp(raw, _minimum, _maximum);
    }

    private Rectangle GetThumbRect(int centerX, bool hovered)
    {
        int size = hovered ? ThumbHoverSize : ThumbSize;
        int trackY = ClientSize.Height / 2;
        return new Rectangle(centerX - size / 2, trackY - size / 2, size, size);
    }

    private static double Clamp(double v, double min, double max) =>
        v < min ? min : v > max ? max : v;

    private void ClampAll()
    {
        _value = Clamp(_value, _minimum, _maximum);
        _rangeStart = Clamp(_rangeStart, _minimum, _maximum);
        _rangeEnd = Clamp(_rangeEnd, _rangeStart, _maximum);
    }

    #endregion

    #region Mouse Events

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        var track = GetTrackRect();

        if (_dragging != ActiveThumb.None)
        {
            double val = XToValue(e.X, track);

            if (_dragging == ActiveThumb.Single)
            {
                Value = val;
            }
            else if (_dragging == ActiveThumb.RangeStart)
            {
                if (val > _rangeEnd)
                {
                    // Push end along and hand off drag to end handle
                    _rangeStart = _rangeEnd;
                    _rangeEnd = Clamp(val, _minimum, _maximum);
                    _dragging = ActiveThumb.RangeEnd;
                    RangeChanged?.Invoke(this, EventArgs.Empty);
                    Invalidate();
                }
                else
                {
                    RangeStart = val;
                }
            }
            else if (_dragging == ActiveThumb.RangeEnd)
            {
                if (val < _rangeStart)
                {
                    // Push start along and hand off drag to start handle
                    _rangeEnd = _rangeStart;
                    _rangeStart = Clamp(val, _minimum, _maximum);
                    _dragging = ActiveThumb.RangeStart;
                    RangeChanged?.Invoke(this, EventArgs.Empty);
                    Invalidate();
                }
                else
                {
                    RangeEnd = val;
                }
            }

            Cursor = Cursors.Hand;
            return;
        }

        // Update hover state
        var prev = _hovered;
        _hovered = HitTest(e.Location, track);
        Cursor = _hovered != ActiveThumb.None ? Cursors.Hand : Cursors.Default;
        if (_hovered != prev) Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (e.Button != MouseButtons.Left) return;

        var track = GetTrackRect();
        var hit = HitTest(e.Location, track);

        if (hit != ActiveThumb.None)
        {
            _dragging = hit;
        }
        else
        {
            // Click on track -> move nearest thumb
            double val = XToValue(e.X, track);
            if (_mode == LucidSliderMode.SingleValue)
            {
                _dragging = ActiveThumb.Single;
                Value = val;
            }
            else
            {
                // Move whichever range handle is closer
                double distStart = Math.Abs(val - _rangeStart);
                double distEnd = Math.Abs(val - _rangeEnd);
                if (distStart <= distEnd)
                {
                    _dragging = ActiveThumb.RangeStart;
                    RangeStart = Math.Min(val, _rangeEnd);
                }
                else
                {
                    _dragging = ActiveThumb.RangeEnd;
                    RangeEnd = Math.Max(val, _rangeStart);
                }
            }
        }

        Capture = true;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        _dragging = ActiveThumb.None;
        Capture = false;

        var track = GetTrackRect();
        _hovered = HitTest(e.Location, track);
        Cursor = _hovered != ActiveThumb.None ? Cursors.Hand : Cursors.Default;
        Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        if (_dragging == ActiveThumb.None)
        {
            _hovered = ActiveThumb.None;
            Cursor = Cursors.Default;
            Invalidate();
        }
    }

    private ActiveThumb HitTest(Point p, Rectangle track)
    {
        if (_mode == LucidSliderMode.SingleValue)
        {
            int cx = ValueToX(_value, track);
            if (GetThumbRect(cx, false).Inflate2(4).Contains(p))
                return ActiveThumb.Single;
        }
        else
        {
            int cxEnd = ValueToX(_rangeEnd, track);
            int cxStart = ValueToX(_rangeStart, track);

            // Test end first if they overlap (end is "on top")
            if (GetThumbRect(cxEnd, false).Inflate2(4).Contains(p))
                return ActiveThumb.RangeEnd;
            if (GetThumbRect(cxStart, false).Inflate2(4).Contains(p))
                return ActiveThumb.RangeStart;
        }
        return ActiveThumb.None;
    }

    #endregion

    #region Keyboard

    protected override bool IsInputKey(Keys keyData) =>
        keyData is Keys.Left or Keys.Right or Keys.Up or Keys.Down || base.IsInputKey(keyData);

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        double inc = _step > 0 ? _step : (_maximum - _minimum) / 100.0;

        switch (e.KeyCode)
        {
            case Keys.Left:
            case Keys.Down:
                if (_mode == LucidSliderMode.SingleValue) Value -= inc;
                else RangeStart -= inc;
                break;
            case Keys.Right:
            case Keys.Up:
                if (_mode == LucidSliderMode.SingleValue) Value += inc;
                else RangeEnd += inc;
                break;
        }
    }

    #endregion

    #region Paint

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        var colors = ThemeProvider.Theme.Colors;
        var track = GetTrackRect();

        // Background
        using (var b = new SolidBrush(colors.BackgroundSecondary))
            g.FillRectangle(b, ClientRectangle);

        // Tick marks
        if (_showTickMarks && _tickFrequency > 0)
        {
            using var tickPen = new Pen(colors.BorderDefault, 1);
            double range = _maximum - _minimum;
            for (double t = _minimum; t <= _maximum + 1e-9; t += _tickFrequency)
            {
                int x = ValueToX(t, track);
                g.DrawLine(tickPen, x, track.Bottom + 4, x, track.Bottom + 8);
            }
        }

        // Track (background)
        using (var path = RoundedRect(track, TrackHeight / 2))
        using (var b = new SolidBrush(colors.BorderDefault))
            g.FillPath(b, path);

        // Track (filled / accent)
        Rectangle filledRect;
        if (_mode == LucidSliderMode.SingleValue)
        {
            int fillX = ValueToX(_value, track);
            filledRect = new Rectangle(track.Left, track.Top, fillX - track.Left, TrackHeight);
        }
        else
        {
            int startX = ValueToX(_rangeStart, track);
            int endX = ValueToX(_rangeEnd, track);
            filledRect = new Rectangle(startX, track.Top, endX - startX, TrackHeight);
        }

        if (filledRect.Width > 0)
        {
            using var path = RoundedRect(filledRect, TrackHeight / 2);
            using var b = new SolidBrush(colors.Accent);
            g.FillPath(b, path);
        }

        // Thumbs
        if (_mode == LucidSliderMode.SingleValue)
        {
            DrawThumb(g, colors, ValueToX(_value, track), _hovered == ActiveThumb.Single || _dragging == ActiveThumb.Single);
            if (_showValueLabel)
                DrawLabel(g, colors, ValueToX(_value, track), _value);
        }
        else
        {
            // Draw start then end (end renders on top when overlapping)
            DrawThumb(g, colors, ValueToX(_rangeStart, track), _hovered == ActiveThumb.RangeStart || _dragging == ActiveThumb.RangeStart);
            DrawThumb(g, colors, ValueToX(_rangeEnd, track), _hovered == ActiveThumb.RangeEnd || _dragging == ActiveThumb.RangeEnd);

            if (_showValueLabel)
            {
                DrawLabel(g, colors, ValueToX(_rangeStart, track), _rangeStart);
                DrawLabel(g, colors, ValueToX(_rangeEnd, track), _rangeEnd);
            }
        }

        // Focus ring
        if (Focused && TabStop)
        {
            using var p = new Pen(colors.Accent, 1.5f) { DashStyle = DashStyle.Dot };
            g.DrawRectangle(p, new Rectangle(1, 1, ClientSize.Width - 3, ClientSize.Height - 3));
        }
    }

    private void DrawThumb(Graphics g, Colors colors, int cx, bool active)
    {
        int size = active ? ThumbHoverSize : ThumbSize;
        int trackY = ClientSize.Height / 2;
        var rect = new Rectangle(cx - size / 2, trackY - size / 2, size, size);

        // Shadow / glow for active
        if (active)
        {
            var glow = new Rectangle(rect.X - 3, rect.Y - 3, rect.Width + 6, rect.Height + 6);
            using var glowBrush = new SolidBrush(Color.FromArgb(40, colors.Accent));
            using var glowPath = RoundedRect(glow, glow.Height / 2);
            g.FillPath(glowBrush, glowPath);
        }

        // White circle with border
        using var bgBrush = new SolidBrush(colors.SurfaceHighlight);
        using var borderPen = new Pen(active ? colors.Accent : colors.BorderDefault, active ? 2f : 1.5f);
        using var path = RoundedRect(rect, rect.Height / 2);

        g.FillPath(bgBrush, path);
        g.DrawPath(borderPen, path);
    }

    private void DrawLabel(Graphics g, Colors colors, int cx, double val)
    {
        string text = _step >= 1 ? ((int)Math.Round(val)).ToString()
                                 : val.ToString("0.##");

        if (!string.IsNullOrEmpty(_customValueLabel))
            text = $"{text} {_customValueLabel}";

        using var font = new Font(Font.FontFamily, Font.Size - 1f, FontStyle.Regular);
        var size = g.MeasureString(text, font);
        int labelY = ClientSize.Height / 2 - ThumbHoverSize / 2 - (int)size.Height - 2;
        int labelX = cx - (int)(size.Width / 2);

        // Keep label within control bounds
        labelX = Math.Max(0, Math.Min(labelX, ClientSize.Width - (int)size.Width));

        using var b = new SolidBrush(colors.TextPrimary);
        g.DrawString(text, font, b, labelX, labelY);
    }

    private static GraphicsPath RoundedRect(Rectangle r, int radius)
    {
        radius = Math.Max(0, Math.Min(radius, Math.Min(r.Width, r.Height) / 2));
        var path = new GraphicsPath();
        if (radius == 0)
        {
            path.AddRectangle(r);
        }
        else
        {
            int d = radius * 2;
            path.AddArc(r.Left, r.Top, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Top, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.Left, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
        }
        return path;
    }

    #endregion
}

/// <summary>Helper to inflate a Rectangle without mutating it.</summary>
internal static class RectangleExtensions
{
    public static Rectangle Inflate2(this Rectangle r, int amount) =>
        new Rectangle(r.X - amount, r.Y - amount, r.Width + amount * 2, r.Height + amount * 2);
}