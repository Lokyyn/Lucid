using Lucid.Renderers;
using Lucid.Theming;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Lucid.Controls;

[ToolboxBitmap(typeof(ComboBox))]
[DefaultEvent("SelectedIndexChanged")]
public class LucidComboBox : Control
{
    #region Event Region

    public event EventHandler SelectedIndexChanged;

    #endregion

    #region Field Region

    private LucidControlState _state = LucidControlState.Normal;
    private readonly LucidObjectCollection _items;
    private readonly LucidComboBoxDropdown _dropdown;
    private int _selectedIndex = -1;
    private int _maxVisibleItems = 8;
    private int _itemHeight = 22;

    #endregion

    #region Property Region

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Color ForeColor { get; set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Color BackColor { get; set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DrawMode DrawMode { get; set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LucidObjectCollection Items => _items;

    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            if (value < -1 || value >= _items.Count)
                return;
            if (_selectedIndex == value)
                return;
            _selectedIndex = value;
            Invalidate();
            SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object SelectedItem
    {
        get => _selectedIndex >= 0 && _selectedIndex < _items.Count ? _items[_selectedIndex] : null;
        set => SelectedIndex = _items.IndexOf(value);
    }

    [Category("Behavior")]
    [Description("Maximum number of items visible in the dropdown at once.")]
    [DefaultValue(8)]
    public int MaxVisibleItems
    {
        get => _maxVisibleItems;
        set => _maxVisibleItems = Math.Max(1, value);
    }

    [Category("Appearance")]
    [Description("Height of each item in the dropdown.")]
    [DefaultValue(22)]
    public int ItemHeight
    {
        get => _itemHeight;
        set => _itemHeight = Math.Max(14, value);
    }

    protected override Size DefaultSize => new Size(150, 26);

    #endregion

    #region Constructor Region

    public LucidComboBox()
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.UserPaint |
                 ControlStyles.Selectable |
                 ControlStyles.UserMouse, true);

        _items = new LucidObjectCollection(this);
        _dropdown = new LucidComboBoxDropdown(this);
    }

    #endregion

    #region Internal Methods

    internal void OnItemsChanged() => Invalidate();

    internal void CommitSelection(int index)
    {
        SelectedIndex = index;
        _dropdown.Close();
        Focus();
    }

    #endregion

    #region Event Handler Region

    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        SetState(LucidControlState.Hover);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        if (!_dropdown.Visible)
            SetState(LucidControlState.Normal);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        Focus();
        if (e.Button == MouseButtons.Left)
            ToggleDropdown();
    }

    protected override void OnGotFocus(EventArgs e)
    {
        base.OnGotFocus(e);
        Invalidate();
    }

    protected override void OnLostFocus(EventArgs e)
    {
        base.OnLostFocus(e);
        Invalidate();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        switch (e.KeyCode)
        {
            case Keys.Space:
            case Keys.Enter:
                ToggleDropdown();
                break;
            case Keys.Up when !_dropdown.Visible && _selectedIndex > 0:
                SelectedIndex--;
                break;
            case Keys.Down when !_dropdown.Visible && _selectedIndex < _items.Count - 1:
                SelectedIndex++;
                break;
        }
    }

    #endregion

    #region Private Methods

    private void SetState(LucidControlState state)
    {
        if (_state != state)
        {
            _state = state;
            Invalidate();
        }
    }

    private void ToggleDropdown()
    {
        if (_dropdown.Visible)
        {
            _dropdown.Close();
            return;
        }
        _dropdown.Open(_items, _selectedIndex, _maxVisibleItems, _itemHeight, this);
    }

    #endregion

    #region Paint Region

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        var rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);

        var isOpen = _dropdown.Visible;
        var isFocused = Focused || isOpen;

        var borderColor = isFocused
            ? ThemeProvider.Theme.Colors.Accent
            : _state == LucidControlState.Hover
                ? ThemeProvider.Theme.Colors.BorderAccent
                : ThemeProvider.Theme.Colors.BorderDefault;

        using (var b = new SolidBrush(ThemeProvider.Theme.Colors.BackgroundTertiary))
            g.FillRectangle(b, rect);

        using (var p = new Pen(borderColor, 1))
            g.DrawRectangle(p, new Rectangle(0, 0, rect.Width - 1, rect.Height - 1));

        const int chevronAreaW = 22;
        int chevronX = rect.Right - chevronAreaW;

        var text = _selectedIndex >= 0 && _selectedIndex < _items.Count
            ? _items[_selectedIndex]?.ToString() ?? string.Empty
            : string.Empty;

        var textColor = Enabled
            ? ThemeProvider.Theme.Colors.TextPrimary
            : ThemeProvider.Theme.Colors.TextDisabled;

        using (var b = new SolidBrush(textColor))
        using (var sf = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter })
        {
            var textRect = new Rectangle(rect.Left + 7, rect.Top + 1, chevronX - rect.Left - 10, rect.Height - 2);
            g.DrawString(text, Font, b, textRect, sf);
        }

        DrawChevron(g, chevronX, rect.Height, isOpen);
    }

    private static void DrawChevron(Graphics g, int areaX, int areaH, bool flipped)
    {
        int cx = areaX + 11;
        int cy = areaH / 2;
        const int hw = 5;
        const int hh = 3;

        var prev = g.SmoothingMode;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        using (var p = new Pen(ThemeProvider.Theme.Colors.TextDisabled, 1.5f))
        {
            if (!flipped)
            {
                g.DrawLine(p, cx - hw, cy - hh, cx, cy + hh);
                g.DrawLine(p, cx, cy + hh, cx + hw, cy - hh);
            }
            else
            {
                g.DrawLine(p, cx - hw, cy + hh, cx, cy - hh);
                g.DrawLine(p, cx, cy - hh, cx + hw, cy + hh);
            }
        }

        g.SmoothingMode = prev;
    }

    #endregion
}

public sealed class LucidObjectCollection : IList
{
    private readonly List<object> _inner = new();
    private readonly LucidComboBox _owner;

    internal LucidObjectCollection(LucidComboBox owner) => _owner = owner;

    public object this[int index]
    {
        get => _inner[index];
        set { _inner[index] = value; _owner.OnItemsChanged(); }
    }

    public int Count => _inner.Count;
    public bool IsReadOnly => false;
    public bool IsFixedSize => false;
    public bool IsSynchronized => false;
    public object SyncRoot => this;

    public int Add(object value) { _inner.Add(value); _owner.OnItemsChanged(); return _inner.Count - 1; }

    public void AddRange(object[] items) { _inner.AddRange(items); _owner.OnItemsChanged(); }

    public void Clear() { _inner.Clear(); _owner.OnItemsChanged(); }
    public bool Contains(object value) => _inner.Contains(value);
    public void CopyTo(Array array, int index) => ((IList)_inner).CopyTo(array, index);
    public IEnumerator GetEnumerator() => _inner.GetEnumerator();
    public int IndexOf(object value) => _inner.IndexOf(value);
    public void Insert(int index, object value) { _inner.Insert(index, value); _owner.OnItemsChanged(); }
    public void Remove(object value) { _inner.Remove(value); _owner.OnItemsChanged(); }
    public void RemoveAt(int index) { _inner.RemoveAt(index); _owner.OnItemsChanged(); }
}

internal sealed class LucidComboBoxDropdown : ToolStripDropDown
{
    private readonly LucidComboBox _owner;
    private readonly LucidComboBoxPanel _panel;
    private readonly ToolStripControlHost _host;

    public LucidComboBoxDropdown(LucidComboBox owner)
    {
        _owner = owner;
        AutoSize = false;
        DropShadowEnabled = true;
        Padding = Padding.Empty;
        Margin = Padding.Empty;
        Renderer = new LucidMenuRenderer();

        _panel = new LucidComboBoxPanel(owner);
        _host = new ToolStripControlHost(_panel)
        {
            Padding = Padding.Empty,
            Margin = Padding.Empty,
            AutoSize = false
        };
        Items.Add(_host);
    }

    public void Open(LucidObjectCollection items, int selectedIndex, int maxVisible, int itemHeight, Control anchor)
    {
        var visibleCount = Math.Min(items.Count, maxVisible);
        if (visibleCount == 0) return;

        var needsScrollbar = items.Count > visibleCount;
        var width = anchor.Width;
        var height = visibleCount * itemHeight;

        _panel.Populate(items, selectedIndex, visibleCount, itemHeight, width, needsScrollbar);
        _panel.Size = new Size(width, height);
        _host.Size = new Size(width, height);
        Size = new Size(width, height);

        Show(anchor.PointToScreen(new Point(0, anchor.Height)));
    }

    public void ScrollToItem(int index) => _panel.ScrollToItem(index);

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        using (var b = new SolidBrush(ThemeProvider.Theme.Colors.BackgroundSecondary))
            e.Graphics.FillRectangle(b, ClientRectangle);
    }
}

internal sealed class LucidComboBoxPanel : Control
{
    private readonly LucidComboBox _owner;
    private List<object> _items = new();
    private int _selectedIndex;
    private int _hoveredIndex = -1;
    private int _scrollOffset;
    private int _visibleCount;
    private int _itemHeight;
    private bool _hasScrollbar;
    private readonly LucidScrollBar _scrollBar;

    private const int ScrollBarWidth = 14;
    private const int ItemPadH = 8;

    public LucidComboBoxPanel(LucidComboBox owner)
    {
        _owner = owner;
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint |
                 ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);

        _scrollBar = new LucidScrollBar { ScrollOrientation = LucidScrollOrientation.Vertical, Visible = false };
        _scrollBar.ValueChanged += (_, e) =>
        {
            _scrollOffset = e.Value;
            Invalidate();
        };
        Controls.Add(_scrollBar);
    }

    public void Populate(LucidObjectCollection items, int selectedIndex, int visibleCount, int itemHeight, int width, bool hasScrollbar)
    {
        _items = items.Cast<object>().ToList();
        _selectedIndex = selectedIndex;
        _visibleCount = visibleCount;
        _itemHeight = itemHeight;
        _hasScrollbar = hasScrollbar;
        _hoveredIndex = -1;

        // Scroll so selected item is centered in view
        _scrollOffset = selectedIndex >= 0
            ? Math.Clamp(selectedIndex - visibleCount / 2, 0, Math.Max(0, _items.Count - visibleCount))
            : 0;

        if (hasScrollbar)
        {
            _scrollBar.Bounds = new Rectangle(width - ScrollBarWidth, 0, ScrollBarWidth, visibleCount * itemHeight);
            _scrollBar.Minimum = 0;
            _scrollBar.Maximum = _items.Count;
            _scrollBar.ViewSize = visibleCount;
            _scrollBar.Value = _scrollOffset;
            _scrollBar.Visible = true;
        }
        else
        {
            _scrollBar.Visible = false;
        }
    }

    public void ScrollToItem(int index)
    {
        if (!_hasScrollbar) return;
        _scrollOffset = Math.Clamp(index, 0, Math.Max(0, _items.Count - _visibleCount));
        _scrollBar.Value = _scrollOffset;
        Invalidate();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        var idx = HitTest(e.Y);
        if (idx != _hoveredIndex)
        {
            _hoveredIndex = idx;
            Invalidate();
        }
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        if (_hoveredIndex != -1)
        {
            _hoveredIndex = -1;
            Invalidate();
        }
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);
        if (e.Button != MouseButtons.Left) return;
        var idx = HitTest(e.Y);
        if (idx >= 0)
            _owner.CommitSelection(idx);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
        base.OnMouseWheel(e);
        if (!_hasScrollbar) return;
        var delta = e.Delta > 0 ? -1 : 1;
        var newOffset = Math.Clamp(_scrollOffset + delta, 0, Math.Max(0, _items.Count - _visibleCount));
        if (newOffset == _scrollOffset) return;
        _scrollOffset = newOffset;
        _scrollBar.Value = _scrollOffset;
        Invalidate();
    }

    private int HitTest(int y)
    {
        var idx = y / _itemHeight + _scrollOffset;
        return idx >= 0 && idx < _items.Count ? idx : -1;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        var itemW = _hasScrollbar ? Width - ScrollBarWidth : Width;

        for (int i = 0; i < _visibleCount; i++)
        {
            var di = _scrollOffset + i;
            if (di >= _items.Count) break;

            var itemRect = new Rectangle(0, i * _itemHeight, itemW, _itemHeight);

            Color fill;
            if (di == _selectedIndex)
                fill = ThemeProvider.Theme.Colors.SurfaceHighlight;
            else if (di == _hoveredIndex)
                fill = ThemeProvider.Theme.Colors.SurfaceDefault;
            else
                fill = ThemeProvider.Theme.Colors.BackgroundSecondary;

            using (var b = new SolidBrush(fill))
                g.FillRectangle(b, itemRect);

            var isSelected = di == _selectedIndex;
            var textColor = isSelected
                ? ThemeProvider.Theme.Colors.TextPrimary
                : ThemeProvider.Theme.Colors.TextPrimary;

            using (var b = new SolidBrush(textColor))
            using (var sf = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter })
            {
                var textRect = new Rectangle(itemRect.Left + ItemPadH, itemRect.Top, itemRect.Width - ItemPadH * 2, itemRect.Height);
                g.DrawString(_items[di]?.ToString() ?? string.Empty, Font, b, textRect, sf);
            }
        }

        using (var p = new Pen(ThemeProvider.Theme.Colors.BorderDefault, 1))
            g.DrawRectangle(p, new Rectangle(0, 0, Width - 1, Height - 1));
    }
}
