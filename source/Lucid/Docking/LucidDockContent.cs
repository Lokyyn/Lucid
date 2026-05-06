using System.ComponentModel;
using Lucid.Controls;
using Lucid.Theming;

namespace Lucid.Docking;

/// <summary>
/// Abstract base class for all content that can be hosted inside a <see cref="LucidDockPanel"/>.
/// Derive from <see cref="LucidDocument"/> for centre-area tabs or from <see cref="LucidToolWindow"/>
/// for side/bottom panels.
/// </summary>
[ToolboxItem(false)]
public class LucidDockContent : UserControl
{
    #region Event Handler Region

    /// <summary>Raised when <see cref="DockText"/> is assigned a new value.</summary>
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

    /// <summary>
    /// When <see langword="true"/>, <see cref="DockText"/> longer than 20 characters is automatically
    /// truncated with an ellipsis in the tab header.
    /// </summary>
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

    /// <summary>
    /// The label shown in the dock tab and window header.
    /// Raises <see cref="DockTextChanged"/> when changed.
    /// </summary>
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

    /// <summary>Icon displayed to the left of <see cref="DockText"/> in the tab and header.</summary>
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

    /// <summary>
    /// The <see cref="LucidDockArea"/> this content is placed in when first added to a <see cref="LucidDockPanel"/>
    /// without an explicit target group.
    /// </summary>
    [Category("Layout")]
    [Description("Determines the default area of the dock panel this content will be added to.")]
    [DefaultValue(LucidDockArea.Document)]
    public LucidDockArea DefaultDockArea { get; set; }

    /// <summary>
    /// Unique string key identifying this content during dock-layout serialization and restore.
    /// Must be unique across all content instances in the same <see cref="LucidDockPanel"/>.
    /// </summary>
    [Category("Behavior")]
    [Description("Determines the key used by this content in the dock serialization.")]
    public string SerializationKey { get; set; }

    /// <summary>The <see cref="LucidDockPanel"/> currently hosting this content, or <see langword="null"/> when not docked.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LucidDockPanel DockPanel { get; internal set; }

    /// <summary>The dock region (Left, Right, Bottom, or Document) this content currently belongs to.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LucidDockRegion DockRegion { get; internal set; }

    /// <summary>The tab group within <see cref="DockRegion"/> that contains this content.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LucidDockGroup DockGroup { get; internal set; }

    /// <summary>The <see cref="LucidDockArea"/> this content is currently occupying.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LucidDockArea DockArea { get; set; }

    /// <summary>Zero-based tab order index within the content's dock group.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Order { get; set; }

    /// <summary>When set, overrides the insertion position used during a dock-layout restore.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int? InsertOrder { get; set; }

    /// <summary>
    /// Optional identifier that prevents duplicate instances: if content with the same
    /// <see cref="UniqueDockId"/> already exists in the panel it is replaced when this content is added.
    /// </summary>
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

    /// <summary>
    /// Removes this content from its <see cref="DockPanel"/>.
    /// Has no effect when the content is not currently hosted in a panel.
    /// </summary>
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
