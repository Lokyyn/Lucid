﻿using Lucid.Theming;
using System.ComponentModel;
using Lucid.Controls;

namespace Lucid.Docking;

[ToolboxItem(false)]
public class LucidScrollableDocument : LucidDockContent
{
    #region Event Region

    public event EventHandler ViewportChanged;
    public event EventHandler ContentSizeChanged;

    #endregion

    #region Field Region

    protected readonly LucidScrollBar _vScrollBar;
    protected readonly LucidScrollBar _hScrollBar;

    private Size _visibleSize;
    private Size _contentSize;

    private Rectangle _viewport;

    #endregion

    #region Property Region

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new LucidDockArea DefaultDockArea
    {
        get { return base.DefaultDockArea; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Size ContentSize
    {
        get { return _contentSize; }
        set
        {
            _contentSize = value;
            UpdateScrollBars();

            if (ContentSizeChanged != null)
                ContentSizeChanged(this, null);
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Rectangle Viewport
    {
        get { return _viewport; }
        private set
        {
            _viewport = value;

            if (ViewportChanged != null)
                ViewportChanged(this, null);
        }
    }

    #endregion

    #region Constructor Region

    public LucidScrollableDocument()
    {
        BackColor = ThemeProvider.Theme.Colors.MainBackgroundColor;
        base.DefaultDockArea = LucidDockArea.Document;
    }

    #endregion

    #region Method Region

    private void UpdateScrollBars()
    {
        if (_vScrollBar.Maximum != ContentSize.Height)
            _vScrollBar.Maximum = ContentSize.Height;

        if (_hScrollBar.Maximum != ContentSize.Width)
            _hScrollBar.Maximum = ContentSize.Width;

        var scrollSize = ThemeProvider.Theme.Sizes.ScrollBarSize;

        _vScrollBar.Location = new Point(ClientSize.Width - scrollSize, 0);
        _vScrollBar.Size = new Size(scrollSize, ClientSize.Height);

        _hScrollBar.Location = new Point(0, ClientSize.Height - scrollSize);
        _hScrollBar.Size = new Size(ClientSize.Width, scrollSize);

        if (DesignMode)
            return;

        // Do this twice in case changing the visibility of the scrollbars
        // causes the VisibleSize to change in such a way as to require a second scrollbar.
        // Probably a better way to detect that scenario...
        SetVisibleSize();
        SetScrollBarVisibility();
        SetVisibleSize();
        SetScrollBarVisibility();

        if (_vScrollBar.Visible)
            _hScrollBar.Width -= scrollSize;

        if (_hScrollBar.Visible)
            _vScrollBar.Height -= scrollSize;

        _vScrollBar.ViewSize = _visibleSize.Height;
        _hScrollBar.ViewSize = _visibleSize.Width;

        UpdateViewport();
    }

    private void SetScrollBarVisibility()
    {
        _vScrollBar.Enabled = _visibleSize.Height < ContentSize.Height;
        _hScrollBar.Enabled = _visibleSize.Width < ContentSize.Width;

        //if (_hideScrollBars)
        //{
        _vScrollBar.Visible = _vScrollBar.Enabled;
        _hScrollBar.Visible = _hScrollBar.Enabled;
        //}
    }

    private void SetVisibleSize()
    {
        var scrollSize = ThemeProvider.Theme.Sizes.ScrollBarSize;

        _visibleSize = new Size(ClientSize.Width, ClientSize.Height);

        if (_vScrollBar.Visible)
            _visibleSize.Width -= scrollSize;

        if (_hScrollBar.Visible)
            _visibleSize.Height -= scrollSize;
    }

    private void UpdateViewport()
    {
        var left = 0;
        var top = 0;
        var width = ClientSize.Width;
        var height = ClientSize.Height;

        if (_hScrollBar.Visible)
        {
            left = _hScrollBar.Value;
            height -= _hScrollBar.Height;
        }

        if (_vScrollBar.Visible)
        {
            top = _vScrollBar.Value;
            width -= _vScrollBar.Width;
        }

        Viewport = new Rectangle(left, top, width, height);

        //var pos = PointToClient(MousePosition);
        //_offsetMousePosition = new Point(pos.X + Viewport.Left, pos.Y + Viewport.Top);

        Invalidate();
    }

    #endregion
}
