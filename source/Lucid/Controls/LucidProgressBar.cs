using Lucid.Theming;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Lucid.Controls;

/// <summary>
/// Display style for the progress label.
/// </summary>
public enum LucidProgressLabelStyle
{
    None,
    Percentage,
    ValueOverMax,
    Custom          // uses LucidProgressBar.CustomLabel
}

/// <summary>
/// A themed, animated progress bar control.
/// Supports determinate and indeterminate modes.
/// </summary>
[ToolboxBitmap(typeof(ProgressBar))]
[DefaultEvent("ValueChanged")]
public partial class LucidProgressBar : Control
{
    #region Constants

    private const int DefaultHeight = 8;
    private const int IndeterminateBarWidth = 180;
    private const int AnimationInterval = 12;      // ms per timer tick

    #endregion

    #region Fields

    private double _minimum = 0;
    private double _maximum = 100;
    private double _value = 0;

    private bool _indeterminate = false;
    private LucidProgressLabelStyle _labelStyle = LucidProgressLabelStyle.Percentage;
    private string _customLabel = string.Empty;
    private ContentAlignment _labelAlignment = ContentAlignment.MiddleCenter;

    // Indeterminate animation
    private readonly System.Windows.Forms.Timer _timer = new();
    private float _indeterminatePos = 0f;   // 0..1 normalised, cycles
    private float _indeterminateDir = 1f;

    // Determinate fill animation
    private double _animatedValue = 0;
    private readonly System.Windows.Forms.Timer _fillTimer = new();

    #endregion

    #region Events

    [Category("Behavior")]
    public event EventHandler? ValueChanged;

    #endregion

    #region Properties

    [Category("Behavior"), DefaultValue(0.0)]
    public double Minimum
    {
        get => _minimum;
        set { _minimum = value; ClampValue(); Invalidate(); }
    }

    [Category("Behavior"), DefaultValue(100.0)]
    public double Maximum
    {
        get => _maximum;
        set { _maximum = value; ClampValue(); Invalidate(); }
    }

    [Category("Behavior"), DefaultValue(0.0)]
    public double Value
    {
        get => _value;
        set
        {
            var clamped = Clamp(value, _minimum, _maximum);
            if (Math.Abs(clamped - _value) < 1e-10) return;
            _value = clamped;
            ValueChanged?.Invoke(this, EventArgs.Empty);

            _fillTimer.Start();
            Invalidate();
        }
    }

    [Category("Behavior"), DefaultValue(false)]
    [Description("When true the bar animates continuously (indeterminate / loading state).")]
    public bool Indeterminate
    {
        get => _indeterminate;
        set
        {
            _indeterminate = value;
            if (value)
            {
                _fillTimer.Stop();
                _indeterminatePos = 0f;
                _timer.Start();
            }
            else
            {
                _timer.Stop();
                _animatedValue = _value;
            }
            Invalidate();
        }
    }

    [Category("Appearance"), DefaultValue(LucidProgressLabelStyle.Percentage)]
    public LucidProgressLabelStyle LabelStyle
    {
        get => _labelStyle;
        set { _labelStyle = value; Invalidate(); }
    }

    [Category("Appearance"), DefaultValue("")]
    [Description("Used when LabelStyle is set to Custom.")]
    public string CustomLabel
    {
        get => _customLabel;
        set { _customLabel = value; Invalidate(); }
    }

    [Category("Appearance"), DefaultValue(ContentAlignment.MiddleCenter)]
    public ContentAlignment LabelAlignment
    {
        get => _labelAlignment;
        set { _labelAlignment = value; Invalidate(); }
    }

    #endregion

    #region Constructor

    public LucidProgressBar()
    {
        SetStyle(
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw |
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.SupportsTransparentBackColor,
            true);

        Height = DefaultHeight;
        MinimumSize = new Size(40, 6);

        _timer.Interval = AnimationInterval;
        _timer.Tick += (_, _) =>
        {
            _indeterminatePos += _indeterminateDir * 0.012f;
            if (_indeterminatePos >= 1f) { _indeterminatePos = 1f; _indeterminateDir = -1f; }
            if (_indeterminatePos <= 0f) { _indeterminatePos = 0f; _indeterminateDir = 1f; }
            Invalidate();
        };

        _fillTimer.Interval = AnimationInterval;
        _fillTimer.Tick += (_, _) =>
        {
            double diff = _value - _animatedValue;
            if (Math.Abs(diff) < 0.01)
            {
                _animatedValue = _value;
                _fillTimer.Stop();
            }
            else
            {
                _animatedValue += diff * 0.18;
            }
            Invalidate();
        };
    }

    #endregion

    #region Helpers

    private void ClampValue() => _value = Clamp(_value, _minimum, _maximum);

    private static double Clamp(double v, double min, double max) =>
        v < min ? min : v > max ? max : v;

    private double FillRatio => Math.Abs(_maximum - _minimum) < 1e-10
        ? 0
        : (_animatedValue - _minimum) / (_maximum - _minimum);

    private Rectangle GetTrackRect()
    {
        bool hasLabel = _labelStyle != LucidProgressLabelStyle.None;
        if (!hasLabel || _labelAlignment == ContentAlignment.MiddleCenter || Height <= 16)
            return ClientRectangle;

        if (_labelAlignment is ContentAlignment.TopLeft or ContentAlignment.TopCenter or ContentAlignment.TopRight)
        {
            int lh = (int)Font.GetHeight() + 2;
            return new Rectangle(0, lh, ClientSize.Width, ClientSize.Height - lh);
        }
        else if (_labelAlignment is ContentAlignment.BottomLeft or ContentAlignment.BottomCenter or ContentAlignment.BottomRight)
        {
            int lh = (int)Font.GetHeight() + 2;
            return new Rectangle(0, 0, ClientSize.Width, ClientSize.Height - lh);
        }

        return ClientRectangle;
    }

    #endregion

    #region Paint

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        var colors = ThemeProvider.Theme.Colors;
        var track = GetTrackRect();
        int r = track.Height / 4;

        // Background
        using (var b = new SolidBrush(colors.MainBackgroundColor))
            g.FillRectangle(b, ClientRectangle);

        // Track
        using (var path = RoundedRect(track, r))
        using (var b = new SolidBrush(colors.LightBorder))
            g.FillPath(b, path);

        // Fill
        if (_indeterminate)
        {
            PaintIndeterminate(g, colors, track, r);
        }
        else
        {
            int fillWidth = (int)(track.Width * FillRatio);
            if (fillWidth > 0)
            {
                var fillRect = new Rectangle(track.Left, track.Top, fillWidth, track.Height);

                using var trackClip = RoundedRect(track, r);
                g.SetClip(trackClip);

                using var fillPath = RoundedRect(fillRect, r);
                using var fillBrush = new SolidBrush(colors.MainAccent);
                g.FillPath(fillBrush, fillPath);

                if (fillRect.Height > 2)
                {
                    var shineRect = new Rectangle(fillRect.Left, fillRect.Top, fillRect.Width, fillRect.Height);
                    using var shinePath = RoundedRect(fillRect, r);
                    using var shineBrush = new LinearGradientBrush(
                        shineRect,
                        Color.FromArgb(45, Color.White),
                        Color.Transparent,
                        LinearGradientMode.Vertical);
                    g.FillPath(shineBrush, shinePath);
                }

                g.ResetClip();
            }

            // Label
            if (_labelStyle != LucidProgressLabelStyle.None)
                PaintLabel(g, colors, track);
        }
    }

    private void PaintIndeterminate(Graphics g, Colors colors, Rectangle track, int r)
    {
        using var clip = RoundedRect(track, r);
        g.SetClip(clip);

        float eased = (float)((1 - Math.Cos(_indeterminatePos * Math.PI)) / 2);
        int travel = track.Width + IndeterminateBarWidth;
        int blockLeft = track.Left - IndeterminateBarWidth + (int)(eased * travel);
        int blockWidth = IndeterminateBarWidth;

        var blockRect = new Rectangle(blockLeft, track.Top, blockWidth, track.Height);

        using var brush = new LinearGradientBrush(
            new Rectangle(blockRect.Left, blockRect.Top, blockRect.Width + 2, blockRect.Height + 1),
            Color.Transparent,
            Color.Transparent,
            LinearGradientMode.Horizontal);

        var blend = new ColorBlend(4)
        {
            Colors = new[]
            {
                Color.Transparent,
                colors.MainAccent,
                colors.MainAccent,
                Color.Transparent
            },
            Positions = new[] { 0f, 0.3f, 0.7f, 1f }
        };
        brush.InterpolationColors = blend;

        g.FillRectangle(brush, blockRect);
        g.ResetClip();
    }

    private void PaintLabel(Graphics g, Colors colors, Rectangle track)
    {
        string text = _labelStyle switch
        {
            LucidProgressLabelStyle.Percentage => $"{FillRatio * 100:0}%",
            LucidProgressLabelStyle.ValueOverMax => $"{_value:0.##} / {_maximum:0.##}",
            LucidProgressLabelStyle.Custom => _customLabel,
            _ => string.Empty
        };

        if (string.IsNullOrEmpty(text)) return;

        using var b = new SolidBrush(colors.LightText);
        var flags = StringFormat.GenericDefault;
        var sf = new StringFormat
        {
            Alignment = _labelAlignment switch
            {
                ContentAlignment.TopLeft or ContentAlignment.MiddleLeft or ContentAlignment.BottomLeft
                    => StringAlignment.Near,
                ContentAlignment.TopRight or ContentAlignment.MiddleRight or ContentAlignment.BottomRight
                    => StringAlignment.Far,
                _ => StringAlignment.Center
            },
            LineAlignment = _labelAlignment switch
            {
                ContentAlignment.TopLeft or ContentAlignment.TopCenter or ContentAlignment.TopRight
                    => StringAlignment.Near,
                ContentAlignment.BottomLeft or ContentAlignment.BottomCenter or ContentAlignment.BottomRight
                    => StringAlignment.Far,
                _ => StringAlignment.Center
            },
            Trimming = StringTrimming.EllipsisCharacter
        };

        RectangleF labelRect = _labelAlignment switch
        {
            ContentAlignment.MiddleCenter or ContentAlignment.MiddleLeft or ContentAlignment.MiddleRight
                => track,
            ContentAlignment.TopLeft or ContentAlignment.TopCenter or ContentAlignment.TopRight
                => new RectangleF(0, 0, ClientSize.Width, track.Top),
            _ => new RectangleF(0, track.Bottom, ClientSize.Width, ClientSize.Height - track.Bottom)
        };

        if (_labelAlignment is ContentAlignment.MiddleCenter or ContentAlignment.MiddleLeft or ContentAlignment.MiddleRight)
        {
            using var whiteBrush = new SolidBrush(Color.White);
            g.DrawString(text, Font, whiteBrush, labelRect, sf);
        }
        else
        {
            g.DrawString(text, Font, b, labelRect, sf);
        }
    }

    private static GraphicsPath RoundedRect(Rectangle r, int radius)
    {
        radius = Math.Max(0, Math.Min(radius, Math.Min(r.Width, r.Height) / 2));
        var path = new GraphicsPath();
        if (radius == 0 || r.Width == 0 || r.Height == 0)
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

    #region Dispose

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _timer.Stop();
            _timer.Dispose();
            _fillTimer.Stop();
            _fillTimer.Dispose();
        }
        base.Dispose(disposing);
    }

    #endregion
}