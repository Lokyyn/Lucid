using Lucid.Theming;
using Lucid.Win32;
using System.ComponentModel;

namespace Lucid.Docking;

/// <summary>
/// Root container for the docking system. Hosts <see cref="LucidDocument"/> instances in the centre
/// tab area and <see cref="LucidToolWindow"/> instances in the left, right, and bottom regions.
/// Drop this control onto a form and call <see cref="AddContent"/> to populate it.
/// </summary>
public class LucidDockPanel : UserControl
{
    #region Event Region

    /// <summary>Raised when the focused/active content changes.</summary>
    public event EventHandler<DockContentEventArgs> ActiveContentChanged;
    /// <summary>Raised after any content is successfully added to the panel.</summary>
    public event EventHandler<DockContentEventArgs> ContentAdded;
    /// <summary>Raised after any content is removed from the panel.</summary>
    public event EventHandler<DockContentEventArgs> ContentRemoved;

    #endregion

    #region Field Region

    private List<LucidDockContent> _contents;
    private Dictionary<LucidDockArea, LucidDockRegion> _regions;

    private LucidDockContent _activeContent;
    private bool _switchingContent = false;

    #endregion

    #region Property Region

    /// <summary>
    /// Gets or sets the content that currently has focus.
    /// Setting this property brings the content's tab to the front and raises <see cref="ActiveContentChanged"/>.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LucidDockContent ActiveContent
    {
        get { return _activeContent; }
        set
        {
            // Don't let content visibility changes re-trigger event
            if (_switchingContent)
                return;

            _switchingContent = true;

            _activeContent = value;

            ActiveGroup = _activeContent.DockGroup;
            ActiveRegion = ActiveGroup.DockRegion;

            foreach (var region in _regions.Values)
                region.Redraw();

            if (ActiveContentChanged != null)
                ActiveContentChanged(this, new DockContentEventArgs(_activeContent));

            _switchingContent = false;
        }
    }

    /// <summary>The dock region that contains <see cref="ActiveContent"/>.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LucidDockRegion ActiveRegion { get; internal set; }

    /// <summary>The dock group that contains <see cref="ActiveContent"/>.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LucidDockGroup ActiveGroup { get; internal set; }

    /// <summary>The currently visible <see cref="LucidDocument"/> in the centre document area.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LucidDockContent ActiveDocument
    {
        get
        {
            return _regions[LucidDockArea.Document].ActiveDocument;
        }
    }

    /// <summary>Message filter that handles drag-and-drop repositioning of dock content.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DockContentDragFilter DockContentDragFilter { get; private set; }

    /// <summary>Message filter that handles splitter resize interactions.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DockResizeFilter DockResizeFilter { get; private set; }

    /// <summary>All splitter controls that separate dock regions from each other.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<LucidDockSplitter> Splitters { get; private set; }

    /// <summary>Current mouse button state, used internally during drag operations.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public MouseButtons MouseButtonState
    {
        get
        {
            var buttonState = MouseButtons;
            return buttonState;
        }
    }

    /// <summary>Map of all four <see cref="LucidDockArea"/> values to their corresponding dock regions.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Dictionary<LucidDockArea, LucidDockRegion> Regions
    {
        get
        {
            return _regions;
        }
    }

    #endregion

    #region Constructor Region

    public LucidDockPanel()
    {
        Splitters = new List<LucidDockSplitter>();
        DockContentDragFilter = new DockContentDragFilter(this);
        DockResizeFilter = new DockResizeFilter(this);

        _regions = new Dictionary<LucidDockArea, LucidDockRegion>();
        _contents = new List<LucidDockContent>();

        BackColor = ThemeProvider.Theme.Colors.MainBackgroundColor;

        CreateRegions();
    }

    #endregion

    #region Method Region

    /// <summary>
    /// Adds <paramref name="dockContent"/> to the panel using its <see cref="LucidDockContent.DefaultDockArea"/>.
    /// </summary>
    /// <param name="dockContent">The content to add.</param>
    public void AddContent(LucidDockContent dockContent)
    {
        AddContent(dockContent, null);
    }

    /// <summary>
    /// Adds <paramref name="dockContent"/> to the panel, optionally placing it into an existing <paramref name="dockGroup"/>.
    /// If content with the same <see cref="LucidDockContent.UniqueDockId"/> already exists it is replaced.
    /// </summary>
    /// <param name="dockContent">The content to add.</param>
    /// <param name="dockGroup">Target group to add the content into, or <see langword="null"/> to use the default area.</param>
    public void AddContent(LucidDockContent dockContent, LucidDockGroup dockGroup)
    {
        if (_contents.Contains(dockContent))
            RemoveContent(dockContent);

        int oldContentOrderIndex = -1;

        // If an UniqueDockId is set we remove the one with the same id
        if (_contents.Any(u => !string.IsNullOrEmpty(u.UniqueDockId) && u.UniqueDockId == dockContent.UniqueDockId))
        {
            var dock = _contents.FirstOrDefault(u => !string.IsNullOrEmpty(u.UniqueDockId) && u.UniqueDockId == dockContent.UniqueDockId);

            oldContentOrderIndex = dock.Order;
            RemoveContent(dock);
        }

        dockContent.DockPanel = this;

        if (oldContentOrderIndex >= 0)
            dockContent.InsertOrder = oldContentOrderIndex;

        _contents.Add(dockContent);

        if (dockGroup != null)
            dockContent.DockArea = dockGroup.DockArea;

        if (dockContent.DockArea == LucidDockArea.None)
            dockContent.DockArea = dockContent.DefaultDockArea;

        var region = _regions[dockContent.DockArea];
        region.AddContent(dockContent, dockGroup);

        if (ContentAdded != null)
            ContentAdded(this, new DockContentEventArgs(dockContent));

        dockContent.Select();
    }

    /// <summary>
    /// Inserts <paramref name="dockContent"/> relative to an existing <paramref name="dockGroup"/>
    /// according to <paramref name="insertType"/> (e.g. before or after).
    /// </summary>
    /// <param name="dockContent">The content to insert.</param>
    /// <param name="dockGroup">The reference group that determines the insertion position.</param>
    /// <param name="insertType">Whether to place the content before or after <paramref name="dockGroup"/>.</param>
    public void InsertContent(LucidDockContent dockContent, LucidDockGroup dockGroup, DockInsertType insertType)
    {
        if (_contents.Contains(dockContent))
            RemoveContent(dockContent);

        dockContent.DockPanel = this;
        _contents.Add(dockContent);

        dockContent.DockArea = dockGroup.DockArea;

        var region = _regions[dockGroup.DockArea];
        region.InsertContent(dockContent, dockGroup, insertType);

        if (ContentAdded != null)
            ContentAdded(this, new DockContentEventArgs(dockContent));

        dockContent.Select();
    }

    /// <summary>
    /// Removes <paramref name="dockContent"/> from the panel. Has no effect if the content is not currently hosted here.
    /// </summary>
    /// <param name="dockContent">The content to remove.</param>
    public void RemoveContent(LucidDockContent dockContent)
    {
        if (!_contents.Contains(dockContent))
            return;

        dockContent.DockPanel = null;
        _contents.Remove(dockContent);

        var region = _regions[dockContent.DockArea];
        region.RemoveContent(dockContent);

        if (ContentRemoved != null)
            ContentRemoved(this, new DockContentEventArgs(dockContent));
    }

    /// <summary>Returns <see langword="true"/> when <paramref name="dockContent"/> is currently hosted in this panel.</summary>
    /// <param name="dockContent">The content to look up.</param>
    public bool ContainsContent(LucidDockContent dockContent)
    {
        return _contents.Contains(dockContent);
    }

    /// <summary>Returns all <see cref="LucidDocument"/> instances currently in the document area.</summary>
    public List<LucidDockContent> GetDocuments()
    {
        return _regions[LucidDockArea.Document].GetContents();
    }

    private void CreateRegions()
    {
        var documentRegion = new LucidDockRegion(this, LucidDockArea.Document);
        _regions.Add(LucidDockArea.Document, documentRegion);

        var leftRegion = new LucidDockRegion(this, LucidDockArea.Left);
        _regions.Add(LucidDockArea.Left, leftRegion);

        var rightRegion = new LucidDockRegion(this, LucidDockArea.Right);
        _regions.Add(LucidDockArea.Right, rightRegion);

        var bottomRegion = new LucidDockRegion(this, LucidDockArea.Bottom);
        _regions.Add(LucidDockArea.Bottom, bottomRegion);

        // Add the regions in this order to force the bottom region to be positioned
        // between the left and right regions properly.
        Controls.Add(documentRegion);
        Controls.Add(bottomRegion);
        Controls.Add(leftRegion);
        Controls.Add(rightRegion);

        // Create tab index for intuitive tabbing order
        documentRegion.TabIndex = 0;
        rightRegion.TabIndex = 1;
        bottomRegion.TabIndex = 2;
        leftRegion.TabIndex = 3;
    }

    /// <summary>Initiates a drag operation for <paramref name="content"/>, allowing the user to reposition it.</summary>
    /// <param name="content">The content to start dragging.</param>
    public void DragContent(LucidDockContent content)
    {
        DockContentDragFilter.StartDrag(content);
    }

    #endregion

    #region Serialization Region

    /// <summary>
    /// Captures the current layout (regions, groups, visible tabs, and region sizes) as a
    /// serializable <see cref="DockPanelState"/> snapshot that can be persisted and later restored.
    /// </summary>
    public DockPanelState GetDockPanelState()
    {
        var state = new DockPanelState();

        state.Regions.Add(new DockRegionState(LucidDockArea.Document));
        state.Regions.Add(new DockRegionState(LucidDockArea.Left, _regions[LucidDockArea.Left].Size));
        state.Regions.Add(new DockRegionState(LucidDockArea.Right, _regions[LucidDockArea.Right].Size));
        state.Regions.Add(new DockRegionState(LucidDockArea.Bottom, _regions[LucidDockArea.Bottom].Size));

        var _groupStates = new Dictionary<LucidDockGroup, DockGroupState>();

        var orderedContent = _contents.OrderBy(c => c.Order);
        foreach (var content in orderedContent)
        {
            foreach (var region in state.Regions)
            {
                if (region.Area == content.DockArea)
                {
                    DockGroupState groupState;

                    if (_groupStates.ContainsKey(content.DockGroup))
                    {
                        groupState = _groupStates[content.DockGroup];
                    }
                    else
                    {
                        groupState = new DockGroupState();
                        region.Groups.Add(groupState);
                        _groupStates.Add(content.DockGroup, groupState);
                    }

                    groupState.Contents.Add(content.SerializationKey);

                    groupState.VisibleContent = content.DockGroup.VisibleContent.SerializationKey;
                }
            }
        }

        return state;
    }

    /// <summary>
    /// Restores a previously captured layout snapshot, re-adding all content in the correct regions,
    /// groups, and tab order.
    /// </summary>
    /// <param name="state">The snapshot produced by <see cref="GetDockPanelState"/>.</param>
    /// <param name="getContentBySerializationKey">
    /// A factory callback that maps a <see cref="LucidDockContent.SerializationKey"/> back to the
    /// corresponding content instance. Return <see langword="null"/> to skip a key.
    /// </param>
    public void RestoreDockPanelState(DockPanelState state, Func<string, LucidDockContent> getContentBySerializationKey)
    {
        foreach (var region in state.Regions)
        {
            switch (region.Area)
            {
                case LucidDockArea.Left:
                    _regions[LucidDockArea.Left].Size = region.Size;
                    break;
                case LucidDockArea.Right:
                    _regions[LucidDockArea.Right].Size = region.Size;
                    break;
                case LucidDockArea.Bottom:
                    _regions[LucidDockArea.Bottom].Size = region.Size;
                    break;
            }

            foreach (var group in region.Groups)
            {
                LucidDockContent previousContent = null;
                LucidDockContent visibleContent = null;

                foreach (var contentKey in group.Contents)
                {
                    var content = getContentBySerializationKey(contentKey);

                    if (content == null)
                        continue;

                    content.DockArea = region.Area;

                    if (previousContent == null)
                        AddContent(content);
                    else
                        AddContent(content, previousContent.DockGroup);

                    previousContent = content;

                    if (group.VisibleContent == contentKey)
                        visibleContent = content;
                }

                if (visibleContent != null)
                    visibleContent.Select();
            }
        }
    }

    #endregion
}
