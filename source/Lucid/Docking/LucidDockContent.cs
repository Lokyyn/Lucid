﻿using System.ComponentModel;
using Lucid.Controls;
using Lucid.Theming;

namespace Lucid.Docking;

[ToolboxItem(false)]
public class LucidDockContent : UserControl
{
    #region Event Handler Region

    public event EventHandler DockTextChanged;

    #endregion

    #region Field Region

    private string _dockText;
    private Image _icon;
    private readonly LucidScrollBar _vScrollBar = new LucidScrollBar { ScrollOrientation = LucidScrollOrientation.Vertical };
    private readonly LucidScrollBar _hScrollBar = new LucidScrollBar { ScrollOrientation = LucidScrollOrientation.Horizontal };
    private bool _limitedTitleLength;
    internal string DockTextOriginal;

    #endregion

    #region Property Region

    [Browsable(true)]
    [DefaultValue(false)]
    [Category("Appearance")]
    [Description("Determines if the text in the headers should have an limited length.")]
    public bool LimitedTitleLength
    {
        get
        {
            return _limitedTitleLength;
        }
        set
        {
            _limitedTitleLength = value;
        }
    }

    [Category("Appearance")]
    [Description("Determines the text that will appear in the content tabs and headers.")]
    public string DockText
    {
        get { return _dockText; }
        set
        {
            var oldText = _dockText;
            DockTextOriginal = value;

            if (_limitedTitleLength && value.Length > 20)
                _dockText = $"{value.Substring(0, 20)}…";
            else
                _dockText = value;

            if (DockTextChanged != null)
                DockTextChanged(this, null);

            Invalidate();
        }
    }

    [Category("Appearance")]
    [Description("Determines the icon that will appear in the content tabs and headers.")]
    public Image Icon
    {
        get { return _icon; }
        set
        {
            _icon = value;
            Invalidate();
        }
    }

    [Category("Layout")]
    [Description("Determines the default area of the dock panel this content will be added to.")]
    [DefaultValue(LucidDockArea.Document)]
    public LucidDockArea DefaultDockArea { get; set; }

    [Category("Behavior")]
    [Description("Determines the key used by this content in the dock serialization.")]
    public string SerializationKey { get; set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LucidDockPanel DockPanel { get; internal set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LucidDockRegion DockRegion { get; internal set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LucidDockGroup DockGroup { get; internal set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LucidDockArea DockArea { get; set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Order { get; set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int? InsertOrder { get; set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string UniqueDockId { get; set; }

    #endregion

    #region Constructor Region

    public LucidDockContent()
    {
        BackColor = System.Drawing.Color.Transparent;// ThemeProvider.Theme.Colors.GreyBackground;

        // Configure scroll bars
        _vScrollBar.BackColor = ThemeProvider.Theme.Colors.MediumBackground;
        _vScrollBar.Minimum = 0;
        _vScrollBar.Maximum = 0;
        //_vScrollBar.ValueChanged += _vScrollBar_ValueChanged;

        _hScrollBar.BackColor = ThemeProvider.Theme.Colors.MediumBackground;
        _hScrollBar.Minimum = 0;
        _hScrollBar.Maximum = 0;
        //_hScrollBar.ValueChanged += _hScrollBar_ValueChanged;

        
        //UpdateScrollBarLayout();

        Controls.Add(_vScrollBar);
        Controls.Add(_hScrollBar);
    }

    #endregion

    #region Method Region

    public virtual void Close()
    {
        if (DockPanel != null)
            DockPanel.RemoveContent(this);
    }

    #endregion

    #region Event Handler Region

    protected override void OnEnter(EventArgs e)
    {
        base.OnEnter(e);

        if (DockPanel == null)
            return;

        DockPanel.ActiveContent = this;
    }

    #endregion
}
