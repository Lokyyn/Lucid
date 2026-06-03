using Lucid.Theming;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Lucid.Controls;

public class LucidDropdownList : Control
{
    #region Event Region

    public event EventHandler SelectedItemChanged;

    #endregion

    #region Field Region

    private LucidControlState _controlState = LucidControlState.Normal;

    private ObservableCollection<LucidDropdownItem> _items = new ObservableCollection<LucidDropdownItem>();
    private LucidDropdownItem _selectedItem;

    private ToolStripDropDown _popup;
    private DropdownPanel _panel;
    private bool _popupOpen = false;

    private bool _showBorder = true;

    private int _itemHeight = 22;
    private int _maxHeight = 180;

    private readonly int _iconSize = 16;

    private ToolStripDropDownDirection _dropdownDirection = ToolStripDropDownDirection.Default;

    #endregion

    #region Property Region

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ObservableCollection<LucidDropdownItem> Items => _items;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LucidDropdownItem SelectedItem
    {
        get { return _selectedItem; }
        set
        {
            _selectedItem = value;
            SelectedItemChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    [Category("Appearance")]
    [Description("Determines whether a border is drawn around the control.")]
    [DefaultValue(true)]
    public bool ShowBorder
    {
        get { return _showBorder; }
        set
        {
            _showBorder = value;
            Invalidate();
        }
    }

    protected override Size DefaultSize => new Size(100, 26);

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LucidControlState ControlState => _controlState;

    [Category("Appearance")]
    [Description("Determines the height of the individual list view items.")]
    [DefaultValue(22)]
    public int ItemHeight
    {
        get { return _itemHeight; }
        set
        {
            _itemHeight = value;
            _panel.ItemHeight = value;
        }
    }

    [Category("Appearance")]
    [Description("Determines the maximum height of the dropdown panel.")]
    [DefaultValue(180)]
    public int MaxHeight
    {
        get { return _maxHeight; }
        set { _maxHeight = value; }
    }

    [Category("Behavior")]
    [Description("Determines what location the dropdown list appears.")]
    [DefaultValue(ToolStripDropDownDirection.Default)]
    public ToolStripDropDownDirection DropdownDirection
    {
        get { return _dropdownDirection; }
        set { _dropdownDirection = value; }
    }

    #endregion

    #region Constructor Region

    public LucidDropdownList()
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.UserPaint |
                 ControlStyles.Selectable |
                 ControlStyles.UserMouse, true);

        _panel = new DropdownPanel(_items, _itemHeight);
        _panel.ItemClicked += Panel_ItemClicked;

        var host = new ToolStripControlHost(_panel)
        {
            Padding = Padding.Empty,
            Margin = Padding.Empty,
            AutoSize = false
        };

        _popup = new ToolStripDropDown
        {
            Padding = Padding.Empty,
            AutoSize = false,
            DropShadowEnabled = true
        };
        _popup.Items.Add(host);
        _popup.Closed += Popup_Closed;

        Items.CollectionChanged += Items_CollectionChanged;
        SelectedItemChanged += LucidDropdownList_SelectedItemChanged;

        SetControlState(LucidControlState.Normal);
    }

    #endregion

    #region Method Region

    private void SetControlState(LucidControlState controlState)
    {
        if (_popupOpen)
            return;

        if (_controlState != controlState)
        {
            _controlState = controlState;
            Invalidate();
        }
    }

    private void ShowMenu()
    {
        if (_popupOpen)
            return;

        SetControlState(LucidControlState.Pressed);
        _popupOpen = true;

        _panel.SelectedItem = _selectedItem;
        ResizePopup();

        var pos = new Point(0, ClientRectangle.Bottom);
        if (_dropdownDirection == ToolStripDropDownDirection.AboveLeft ||
            _dropdownDirection == ToolStripDropDownDirection.AboveRight)
            pos.Y = 0;

        _popup.Show(this, pos, _dropdownDirection);
        _panel.ScrollToSelected();
    }

    private void ResizePopup()
    {
        var width = ClientRectangle.Width;
        var totalHeight = _items.Count * _itemHeight;
        var panelHeight = totalHeight > _maxHeight ? _maxHeight : Math.Max(totalHeight, _itemHeight);

        _panel.Size = new Size(width, panelHeight);
        _panel.UpdateScrollBar(totalHeight, panelHeight);

        var host = (ToolStripControlHost)_popup.Items[0];
        host.Size = new Size(width, panelHeight);
        _popup.Size = new Size(width, panelHeight);
    }

    #endregion

    #region Event Handler Region

    private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            if (_selectedItem == null && _items.Count > 0)
                SelectedItem = _items[0];
        }
        else if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            if (e.OldItems != null && e.OldItems.Contains(_selectedItem))
                SelectedItem = _items.Count > 0 ? _items[0] : null;
        }
        else if (e.Action == NotifyCollectionChangedAction.Reset)
        {
            SelectedItem = null;
        }

        _panel.Invalidate();
        Invalidate();
    }

    private void Panel_ItemClicked(object sender, LucidDropdownItem item)
    {
        if (_selectedItem != item)
            SelectedItem = item;
        _popup.Close();
    }

    private void LucidDropdownList_SelectedItemChanged(object sender, EventArgs e)
    {
        _panel.SelectedItem = _selectedItem;
        _panel.Invalidate();
        Invalidate();
    }

    private void Popup_Closed(object sender, ToolStripDropDownClosedEventArgs e)
    {
        _popupOpen = false;

        if (!ClientRectangle.Contains(PointToClient(MousePosition)))
            SetControlState(LucidControlState.Normal);
        else
            SetControlState(LucidControlState.Hover);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        if (e.Button == MouseButtons.Left)
            SetControlState(ClientRectangle.Contains(e.Location) ? LucidControlState.Pressed : LucidControlState.Hover);
        else
            SetControlState(LucidControlState.Hover);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        ShowMenu();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        SetControlState(LucidControlState.Normal);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        SetControlState(LucidControlState.Normal);
    }

    protected override void OnMouseCaptureChanged(EventArgs e)
    {
        base.OnMouseCaptureChanged(e);

        if (!ClientRectangle.Contains(PointToClient(Cursor.Position)))
            SetControlState(LucidControlState.Normal);
    }

    protected override void OnGotFocus(EventArgs e)
    {
        base.OnGotFocus(e);
        Invalidate();
    }

    protected override void OnLostFocus(EventArgs e)
    {
        base.OnLostFocus(e);

        if (!ClientRectangle.Contains(PointToClient(Cursor.Position)))
            SetControlState(LucidControlState.Normal);
        else
            SetControlState(LucidControlState.Hover);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (e.KeyCode == Keys.Space)
            ShowMenu();
    }

    #endregion

    #region Render Region

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        var rect = ClientRectangle;

        var borderColor = ThemeProvider.Theme.Colors.SurfaceHighlight;
        var fillColor = ThemeProvider.Theme.Colors.BackgroundTertiary;

        if (_controlState == LucidControlState.Pressed || Focused)
            borderColor = ThemeProvider.Theme.Colors.Accent;

        using (var b = new SolidBrush(fillColor))
        {
            g.FillRectangle(b, rect);
        }

        if (ShowBorder)
        {
            using (var p = new Pen(borderColor, 1))
            {
                var modRect = new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);
                g.DrawRectangle(p, modRect);
            }
        }

        var icon = ScrollIcons.scrollbar_arrow_hot;
        g.DrawImageUnscaled(icon,
            rect.Right - icon.Width - (ThemeProvider.Theme.Sizes.Padding / 2),
            rect.Top + (rect.Height / 2) - (icon.Height / 2));

        if (SelectedItem != null)
        {
            var hasIcon = SelectedItem.Icon != null;
            var hasIconColor = SelectedItem.IconColor != Color.Empty;

            if (hasIcon && !hasIconColor)
            {
                g.DrawImage(SelectedItem.Icon, new Point(rect.Left + 5, rect.Top + (rect.Height / 2) - (_iconSize / 2)));
            }
            else if (hasIconColor)
            {
                using (var b = new SolidBrush(SelectedItem.IconColor))
                using (var p = new Pen(SelectedItem.IconColor))
                {
                    var iconRect = new Rectangle(rect.Left + 5, rect.Top + (rect.Height / 2) - 8, 16, 16);
                    g.DrawRectangle(p, iconRect);
                    g.FillRectangle(b, iconRect);
                }
            }

            var padding = 2;
            var textRect = new Rectangle(
                rect.Left + padding,
                rect.Top + padding,
                rect.Width - icon.Width - (ThemeProvider.Theme.Sizes.Padding / 2) - (padding * 2),
                rect.Height - (padding * 2));

            if (hasIcon || hasIconColor)
            {
                textRect.X += _iconSize + 7;
                textRect.Width -= _iconSize + 7;
            }

            var textColor = Enabled ? ThemeProvider.Theme.Colors.TextPrimary : ThemeProvider.Theme.Colors.TextDisabled;
            using (var b = new SolidBrush(textColor))
            {
                var stringFormat = new StringFormat
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Near,
                    FormatFlags = StringFormatFlags.NoWrap,
                    Trimming = StringTrimming.EllipsisCharacter
                };
                g.DrawString(SelectedItem.Text, Font, b, textRect, stringFormat);
            }
        }
    }

    #endregion

    #region DropdownPanel

    private class DropdownPanel : Control
    {
        public event EventHandler<LucidDropdownItem> ItemClicked;

        private readonly ObservableCollection<LucidDropdownItem> _items;
        private int _itemHeight;
        private int _hoveredIndex = -1;
        private readonly LucidScrollBar _scrollBar;
        private const int ScrollBarWidth = 14;
        private readonly int _iconSize = 16;

        public LucidDropdownItem SelectedItem { get; set; }

        public int ItemHeight
        {
            get { return _itemHeight; }
            set { _itemHeight = value; Invalidate(); }
        }

        private bool ScrollbarVisible => _scrollBar.Visible;
        private int ItemAreaWidth => ScrollbarVisible ? Width - ScrollBarWidth : Width;
        private int ScrollOffset => ScrollbarVisible ? _scrollBar.Value : 0;

        public DropdownPanel(ObservableCollection<LucidDropdownItem> items, int itemHeight)
        {
            _items = items;
            _itemHeight = itemHeight;

            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint, true);

            _scrollBar = new LucidScrollBar
            {
                ScrollOrientation = LucidScrollOrientation.Vertical,
                Width = ScrollBarWidth,
                Visible = false
            };
            _scrollBar.ValueChanged += (s, e) => Invalidate();
            Controls.Add(_scrollBar);
        }

        public void UpdateScrollBar(int totalHeight, int visibleHeight)
        {
            var needsScrollbar = totalHeight > visibleHeight;
            _scrollBar.Visible = needsScrollbar;

            if (needsScrollbar)
            {
                _scrollBar.SetBounds(Width - ScrollBarWidth, 0, ScrollBarWidth, visibleHeight);
                _scrollBar.Minimum = 0;
                _scrollBar.Maximum = totalHeight;
                _scrollBar.ViewSize = visibleHeight;
                var maxScroll = totalHeight - visibleHeight;
                if (_scrollBar.Value > maxScroll)
                    _scrollBar.Value = maxScroll;
            }
            else
            {
                _scrollBar.Value = 0;
            }
        }

        public void ScrollToSelected()
        {
            if (SelectedItem == null || !ScrollbarVisible) return;
            var idx = _items.IndexOf(SelectedItem);
            if (idx < 0) return;
            var itemTop = idx * _itemHeight;
            var itemBottom = itemTop + _itemHeight;
            if (itemTop < ScrollOffset)
                _scrollBar.Value = itemTop;
            else if (itemBottom > ScrollOffset + Height)
                _scrollBar.Value = itemBottom - Height;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            var scrollOffset = ScrollOffset;
            var itemWidth = ItemAreaWidth;

            using (var b = new SolidBrush(ThemeProvider.Theme.Colors.BackgroundSecondary))
                g.FillRectangle(b, ClientRectangle);

            using (var p = new Pen(ThemeProvider.Theme.Colors.BorderDefault))
                g.DrawRectangle(p, new Rectangle(0, 0, itemWidth - 1, Height - 1));

            for (int i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
                var y = i * _itemHeight - scrollOffset;

                if (y + _itemHeight <= 0 || y >= Height) continue;

                var itemRect = new Rectangle(0, y, itemWidth, _itemHeight);

                Color bgColor;
                if (item == SelectedItem)
                    bgColor = ThemeProvider.Theme.Colors.AccentSecondary;
                else if (i == _hoveredIndex)
                    bgColor = ThemeProvider.Theme.Colors.Accent;
                else
                    bgColor = ThemeProvider.Theme.Colors.BackgroundSecondary;

                using (var b = new SolidBrush(bgColor))
                    g.FillRectangle(b, itemRect);

                var xOffset = 4;

                if (item.Icon != null && item.IconColor == Color.Empty)
                {
                    g.DrawImage(item.Icon, new Point(itemRect.Left + 5, itemRect.Top + (itemRect.Height / 2) - (_iconSize / 2)));
                    xOffset = _iconSize + 9;
                }
                else if (item.IconColor != Color.Empty)
                {
                    using (var b = new SolidBrush(item.IconColor))
                    using (var p = new Pen(item.IconColor))
                    {
                        var iconRect = new Rectangle(itemRect.Left + 5, itemRect.Top + (itemRect.Height / 2) - 8, 16, 16);
                        g.DrawRectangle(p, iconRect);
                        g.FillRectangle(b, iconRect);
                    }
                    xOffset = _iconSize + 9;
                }

                using (var b = new SolidBrush(ThemeProvider.Theme.Colors.TextPrimary))
                {
                    var sf = new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Near,
                        FormatFlags = StringFormatFlags.NoWrap,
                        Trimming = StringTrimming.EllipsisCharacter
                    };
                    var textRect = new Rectangle(itemRect.Left + xOffset, itemRect.Top, itemWidth - xOffset - 4, itemRect.Height);
                    g.DrawString(item.Text, Font, b, textRect, sf);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.X >= ItemAreaWidth)
            {
                if (_hoveredIndex != -1) { _hoveredIndex = -1; Invalidate(); }
                return;
            }

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
            _hoveredIndex = -1;
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.X >= ItemAreaWidth) return;
            var idx = HitTest(e.Y);
            if (idx >= 0 && idx < _items.Count)
                ItemClicked?.Invoke(this, _items[idx]);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (ScrollbarVisible)
                _scrollBar.Value -= e.Delta / 10;
        }

        private int HitTest(int y)
        {
            var idx = (y + ScrollOffset) / _itemHeight;
            return idx >= 0 ? idx : -1;
        }
    }

    #endregion
}
