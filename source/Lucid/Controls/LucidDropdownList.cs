﻿using Lucid.Theming;
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

    private LucidContextMenu _menu = new LucidContextMenu();
    private bool _menuOpen = false;

    private bool _showBorder = true;

    private int _itemHeight = 22;
    private int _maxHeight = 180;

    private readonly int _iconSize = 16;

    private ToolStripDropDownDirection _dropdownDirection = ToolStripDropDownDirection.Default;

    #endregion

    #region Property Region

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ObservableCollection<LucidDropdownItem> Items
    {
        get { return _items; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LucidDropdownItem SelectedItem
    {
        get { return _selectedItem; }
        set
        {
            _selectedItem = value;
            SelectedItemChanged?.Invoke(this, new EventArgs());
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

    protected override Size DefaultSize
    {
        get { return new Size(100, 26); }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LucidControlState ControlState
    {
        get { return _controlState; }
    }

    [Category("Appearance")]
    [Description("Determines the height of the individual list view items.")]
    [DefaultValue(22)]
    public int ItemHeight
    {
        get { return _itemHeight; }
        set
        {
            _itemHeight = value;
            ResizeMenu();
        }
    }

    [Category("Appearance")]
    [Description("Determines the maximum height of the dropdown panel.")]
    [DefaultValue(130)]
    public int MaxHeight
    {
        get { return _maxHeight; }
        set
        {
            _maxHeight = value;
            ResizeMenu();
        }
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

        _menu.AutoSize = false;
        _menu.Closed += Menu_Closed;

        Items.CollectionChanged += Items_CollectionChanged;
        SelectedItemChanged += LucidDropdownList_SelectedItemChanged;

        SetControlState(LucidControlState.Normal);
    }

    #endregion

    #region Method Region

    private ToolStripMenuItem GetMenuItem(LucidDropdownItem item)
    {
        foreach (ToolStripMenuItem menuItem in _menu.Items)
        {
            if ((LucidDropdownItem)menuItem.Tag == item)
                return menuItem;
        }

        return null;
    }

    private void SetControlState(LucidControlState controlState)
    {
        if (_menuOpen)
            return;

        if (_controlState != controlState)
        {
            _controlState = controlState;
            Invalidate();
        }
    }

    private void ShowMenu()
    {
        if (_menu.Visible)
            return;

        SetControlState(LucidControlState.Pressed);

        _menuOpen = true;

        var pos = new Point(0, ClientRectangle.Bottom);

        if (_dropdownDirection == ToolStripDropDownDirection.AboveLeft || _dropdownDirection == ToolStripDropDownDirection.AboveRight)
            pos.Y = 0;

        _menu.Show(this, pos, _dropdownDirection);

        if (SelectedItem != null)
        {
            var selectedItem = GetMenuItem(SelectedItem);
            selectedItem.Select();
        }
    }

    private void ResizeMenu()
    {
        var width = ClientRectangle.Width;
        var height = (_menu.Items.Count * _itemHeight) + 4;

        if (height > _maxHeight)
            height = _maxHeight;

        // Dirty: Check what the autosized items are
        foreach (ToolStripMenuItem item in _menu.Items)
        {
            item.AutoSize = true;

            if (item.Size.Width > width)
                width = item.Size.Width;

            item.AutoSize = false;
        }

        // Force the size
        foreach (ToolStripMenuItem item in _menu.Items)
            item.Size = new Size(width - 1, _itemHeight);

        _menu.Size = new Size(width, height);
    }

    #endregion

    #region Event Handler Region

    private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            foreach (LucidDropdownItem item in e.NewItems)
            {
                var menuItem = new ToolStripMenuItem(item.Text)
                {
                    Image = item.Icon,
                    AutoSize = false,
                    Height = _itemHeight,
                    Font = Font,
                    Tag = item,
                    TextAlign = ContentAlignment.MiddleLeft
                };

                _menu.Items.Add(menuItem);
                menuItem.Click += Item_Select;

                if (SelectedItem == null)
                    SelectedItem = item;
            }
        }
        else if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (LucidDropdownItem item in e.OldItems)
            {
                foreach (ToolStripMenuItem menuItem in _menu.Items)
                {
                    if ((LucidDropdownItem)menuItem.Tag == item)
                    {
                        menuItem.Click -= Item_Select;
                        _menu.Items.Remove(menuItem);
                    }
                }
            }
        }
        else if (e.Action == NotifyCollectionChangedAction.Reset)
        {
            var listCount = _menu.Items.Count;

            for (int i = 0; i < listCount; i++)
            {
                var menuItem = _menu.Items[0];

                if (menuItem != null)
                {
                    menuItem.Click -= Item_Select;
                    _menu.Items.Remove(menuItem);
                }
            }
        }

        ResizeMenu();
    }

    private void Item_Select(object sender, EventArgs e)
    {
        var menuItem = sender as ToolStripMenuItem;
        if (menuItem == null)
            return;

        var dropdownItem = (LucidDropdownItem)menuItem.Tag;
        if (_selectedItem != dropdownItem)
            SelectedItem = dropdownItem;
    }

    private void LucidDropdownList_SelectedItemChanged(object sender, EventArgs e)
    {
        foreach (ToolStripMenuItem item in _menu.Items)
        {
            if ((LucidDropdownItem)item.Tag == SelectedItem)
            {
                item.BackColor = ThemeProvider.Theme.Colors.DarkBlueBackground;
                item.Font = new Font(Font, FontStyle.Bold);
            }
            else
            {
                item.BackColor = ThemeProvider.Theme.Colors.MainBackgroundColor;
                item.Font = new Font(Font, FontStyle.Regular);
            }
        }

        Invalidate();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        ResizeMenu();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        if (e.Button == MouseButtons.Left)
        {
            if (ClientRectangle.Contains(e.Location))
                SetControlState(LucidControlState.Pressed);
            else
                SetControlState(LucidControlState.Hover);
        }
        else
        {
            SetControlState(LucidControlState.Hover);
        }
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

        var location = Cursor.Position;

        if (!ClientRectangle.Contains(location))
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

        var location = Cursor.Position;

        if (!ClientRectangle.Contains(location))
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

    private void Menu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
    {
        _menuOpen = false;

        if (!ClientRectangle.Contains(MousePosition))
            SetControlState(LucidControlState.Normal);
        else
            SetControlState(LucidControlState.Hover);
    }

    #endregion

    #region Render Region

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;

        // Draw background
        using (var b = new SolidBrush(ThemeProvider.Theme.Colors.MediumBackground))
        {
            g.FillRectangle(b, ClientRectangle);
        }

        // Draw normal state
        if (ControlState == LucidControlState.Normal)
        {
            if (ShowBorder)
            {
                using (var p = new Pen(ThemeProvider.Theme.Colors.LightBorder, 1))
                {
                    var modRect = new Rectangle(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
                    g.DrawRectangle(p, modRect);
                }
            }
        }

        // Draw hover state
        if (ControlState == LucidControlState.Hover)
        {
            using (var b = new SolidBrush(ThemeProvider.Theme.Colors.DarkBorder))
            {
                g.FillRectangle(b, ClientRectangle);
            }

            using (var b = new SolidBrush(ThemeProvider.Theme.Colors.DarkBackground))
            {
                var arrowRect = new Rectangle(ClientRectangle.Right - DropdownIcons.small_arrow.Width - 8, ClientRectangle.Top, DropdownIcons.small_arrow.Width + 8, ClientRectangle.Height);
                g.FillRectangle(b, arrowRect);
            }

            using (var p = new Pen(ThemeProvider.Theme.Colors.MainAccent, 1))
            {
                var modRect = new Rectangle(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Width - 1 - DropdownIcons.small_arrow.Width - 8, ClientRectangle.Height - 1);
                g.DrawRectangle(p, modRect);
            }
        }

        // Draw pressed state
        if (ControlState == LucidControlState.Pressed)
        {
            using (var b = new SolidBrush(ThemeProvider.Theme.Colors.DarkBorder))
            {
                g.FillRectangle(b, ClientRectangle);
            }

            using (var b = new SolidBrush(ThemeProvider.Theme.Colors.MainAccent))
            {
                var arrowRect = new Rectangle(ClientRectangle.Right - DropdownIcons.small_arrow.Width - 8, ClientRectangle.Top, DropdownIcons.small_arrow.Width + 8, ClientRectangle.Height);
                g.FillRectangle(b, arrowRect);
            }
        }

        // Draw dropdown arrow
        using (var img = DropdownIcons.small_arrow)
        {
            g.DrawImageUnscaled(img, ClientRectangle.Right - img.Width - 4, ClientRectangle.Top + (ClientRectangle.Height / 2) - (img.Height / 2));
        }

        // Draw selected item
        if (SelectedItem != null)
        {
            // Draw Icon
            var hasIcon = SelectedItem.Icon != null;
            var hasIconColor = SelectedItem.IconColor != Color.Empty;

            if (hasIcon && !hasIconColor)
            {
                g.DrawImage(SelectedItem.Icon, new Point(ClientRectangle.Left + 5, ClientRectangle.Top + (ClientRectangle.Height / 2) - (_iconSize / 2)));
                //g.DrawImageUnscaled(SelectedItem.Icon, new Point(ClientRectangle.Left + 5, ClientRectangle.Top + (ClientRectangle.Height / 2) - (_iconSize / 2)));
            }
            else if (hasIconColor)
            {
                using (var b = new SolidBrush(SelectedItem.IconColor))
                using (var p = new Pen(SelectedItem.IconColor))
                {
                    var rect = new Rectangle(ClientRectangle.Left + 5, ClientRectangle.Top + (ClientRectangle.Height / 2) - 8, 16, 16);
                    g.DrawRectangle(p, rect);
                    g.FillRectangle(b, rect);
                }
            }

            // Draw Text
            using (var b = new SolidBrush(ThemeProvider.Theme.Colors.LightText))
            using (var bDisabled = new SolidBrush(ThemeProvider.Theme.Colors.DisabledText))
            {
                var stringFormat = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center
                };

                var rect = new Rectangle(ClientRectangle.Left + 2, ClientRectangle.Top, ClientRectangle.Width - 16, ClientRectangle.Height);

                if (hasIcon || hasIconColor)
                {
                    rect.X += _iconSize + 7;
                    rect.Width -= _iconSize + 7;
                }
                
                if (Enabled)
                    g.DrawString(SelectedItem.Text, Font, b, rect, stringFormat);
                else
                    g.DrawString(SelectedItem.Text, Font, bDisabled, rect, stringFormat);
            }
        }
    }

    #endregion
}
