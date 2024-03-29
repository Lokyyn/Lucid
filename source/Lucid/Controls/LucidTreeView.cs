﻿using Lucid.Collections;
using Lucid.Theming;
using Lucid.Extensions;
using Lucid.Forms;
using Lucid.Icons;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using Lucid.Common;
using Lucid.Helper;

namespace Lucid.Controls;

public class LucidTreeView : LucidScrollView
{
    #region Event Region

    public event EventHandler SelectedNodesChanged;
    public event EventHandler AfterNodeExpand;
    public event EventHandler AfterNodeCollapse;

    #endregion

    #region Field Region

    private bool _disposed;

    private readonly int _expandAreaSize = 16;
    private readonly int _iconSize = 16;

    private int _itemHeight = 20;
    private int _indent = 20;

    private ObservableList<LucidTreeNode> _nodes;
    private ObservableCollection<LucidTreeNode> _selectedNodes;

    private LucidTreeNode _anchoredNodeStart;
    private LucidTreeNode _anchoredNodeEnd;

    private Bitmap _nodeClosed;
    private Bitmap _nodeClosedHover;
    private Bitmap _nodeClosedHoverSelected;
    private Bitmap _nodeOpen;
    private Bitmap _nodeOpenHover;
    private Bitmap _nodeOpenHoverSelected;

    private LucidTreeNode _provisionalNode;
    private LucidTreeNode _dropNode;
    private bool _provisionalDragging;
    private List<LucidTreeNode> _dragNodes;
    private Point _dragPos;

    private Font _badgeFont;
    private bool _orderNode;
    private ContextMenuStrip _contextMenu;
    #endregion

    #region Property Region

    [Category("Appearance")]
    [Description("Determines if nodes should be ordered.")]
    [DefaultValue(false)]
    public bool OrderNodes
    {
        get { return _orderNode; }
        set
        {
            _orderNode = value;

            if (OrderNodes)
                UpdateNodeOrder();
        }
    }

    [Category("Behavior")]
    [Description("Gets or sets the current ContextMenu")]
    [DefaultValue(false)]
    public ContextMenuStrip ContextMenu
    {
        get { return _contextMenu; }
        set
        {
            _contextMenu = value;
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Font BadgeFont
    {
        get => _badgeFont;
        set => _badgeFont = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ObservableList<LucidTreeNode> Nodes
    {
        get { return _nodes; }
        set
        {
            if (_nodes != null)
            {
                _nodes.ItemsAdded -= Nodes_ItemsAdded;
                _nodes.ItemsRemoved -= Nodes_ItemsRemoved;

                foreach (var node in _nodes)
                    UnhookNodeEvents(node);
            }

            _nodes = value;

            _nodes.ItemsAdded += Nodes_ItemsAdded;
            _nodes.ItemsRemoved += Nodes_ItemsRemoved;

            foreach (var node in _nodes)
                HookNodeEvents(node);

            UpdateNodes();
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ObservableCollection<LucidTreeNode> SelectedNodes
    {
        get { return _selectedNodes; }
    }

    [Category("Appearance")]
    [Description("Determines the height of tree nodes.")]
    [DefaultValue(20)]
    public int ItemHeight
    {
        get { return _itemHeight; }
        set
        {
            _itemHeight = value;
            MaxDragChange = _itemHeight;
            UpdateNodes();
        }
    }

    [Category("Appearance")]
    [Description("Determines the amount of horizontal space given by parent node.")]
    [DefaultValue(20)]
    public int Indent
    {
        get { return _indent; }
        set
        {
            _indent = value;
            UpdateNodes();
        }
    }

    [Category("Behavior")]
    [Description("Determines whether nodes should be ordered by its order number or not.")]
    [DefaultValue(false)]
    public bool AllowSorting { get; set; }

    [Category("Behavior")]
    [Description("Determines whether multiple tree nodes can be selected at once.")]
    [DefaultValue(false)]
    public bool MultiSelect { get; set; }

    [Category("Behavior")]
    [Description("Determines whether nodes can be moved within this tree view.")]
    [DefaultValue(false)]
    public bool AllowMoveNodes { get; set; }

    [Category("Appearance")]
    [Description("Determines whether icons are rendered with the tree nodes.")]
    [DefaultValue(false)]
    public bool ShowIcons { get; set; }

    [Category("Appearance")]
    [Description("Determines whether selected node is displayed with a rounded rectangle.")]
    [DefaultValue(false)]
    public bool ShowSelectedNodeRoundedRectangle { get; set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int VisibleNodeCount { get; private set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IComparer<LucidTreeNode> TreeViewNodeSorter { get; set; }

    #endregion

    #region Constructor Region

    public LucidTreeView()
    {
        Nodes = new ObservableList<LucidTreeNode>();
        _selectedNodes = new ObservableCollection<LucidTreeNode>();
        _selectedNodes.CollectionChanged += SelectedNodes_CollectionChanged;
        _badgeFont = new Font("Tahoma Sans Serif", 7f, FontStyle.Bold);

        MaxDragChange = _itemHeight;

        ThemeProvider.OnThemeChanged += ThemeProvider_OnThemeChanged;

        LoadIcons();
    }

    #endregion

    #region Dispose Region

    protected override void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            DisposeIcons();

            if (SelectedNodesChanged != null)
                SelectedNodesChanged = null;

            if (AfterNodeExpand != null)
                AfterNodeExpand = null;

            if (AfterNodeCollapse != null)
                AfterNodeExpand = null;

            if (_nodes != null)
                _nodes.Dispose();

            if (_selectedNodes != null)
                _selectedNodes.CollectionChanged -= SelectedNodes_CollectionChanged;

            _disposed = true;
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Event Handler Region

    private void ThemeProvider_OnThemeChanged()
    {
        LoadIcons();
    }

    private void Nodes_ItemsAdded(object sender, ObservableListModified<LucidTreeNode> e)
    {
        foreach (var node in e.Items)
        {
            node.ParentTree = this;
            node.IsRoot = true;

            HookNodeEvents(node);
        }

        if (TreeViewNodeSorter != null)
            Nodes.Sort(TreeViewNodeSorter);

        if (OrderNodes)
            UpdateNodeOrder();

        UpdateNodes();
    }

    private void Nodes_ItemsRemoved(object sender, ObservableListModified<LucidTreeNode> e)
    {
        foreach (var node in e.Items)
        {
            node.ParentTree = this;
            node.IsRoot = true;

            HookNodeEvents(node);
        }

        if (OrderNodes)
            UpdateNodeOrder();

        UpdateNodes();
    }

    private void ChildNodes_ItemsAdded(object sender, ObservableListModified<LucidTreeNode> e)
    {
        foreach (var node in e.Items)
            HookNodeEvents(node);

        UpdateNodes();
    }

    private void ChildNodes_ItemsRemoved(object sender, ObservableListModified<LucidTreeNode> e)
    {
        foreach (var node in e.Items)
        {
            if (SelectedNodes.Contains(node))
                SelectedNodes.Remove(node);

            UnhookNodeEvents(node);
        }

        UpdateNodes();
    }

    private void SelectedNodes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (SelectedNodesChanged != null)
            SelectedNodesChanged(this, null);
    }

    private void Nodes_TextChanged(object sender, EventArgs e)
    {
        UpdateNodes();
    }

    private void Nodes_NodeExpanded(object sender, EventArgs e)
    {
        UpdateNodes();

        if (AfterNodeExpand != null)
            AfterNodeExpand(this, null);
    }

    private void Nodes_NodeCollapsed(object sender, EventArgs e)
    {
        UpdateNodes();

        if (AfterNodeCollapse != null)
            AfterNodeCollapse(this, null);
    }

    private void BadgeCollection_OnCollectionChanged()
    {
        this.UpdateNodes();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (_provisionalDragging)
        {
            if (OffsetMousePosition != _dragPos)
            {
                StartDrag();
                HandleDrag();
                return;
            }
        }

        if (IsDragging)
        {
            if (_dropNode != null)
            {
                var rect = GetNodeFullRowArea(_dropNode);
                if (!rect.Contains(OffsetMousePosition))
                {
                    _dropNode = null;
                    Invalidate();
                }
            }
        }

        CheckHover();

        if (IsDragging)
        {
            HandleDrag();
        }

        base.OnMouseMove(e);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
        CheckHover();

        base.OnMouseWheel(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (ContextMenuStrip != null)
            ContextMenuStrip.Close();

        Focus();

        if (!ContainsFocus)
            return;


        if (e.Button == MouseButtons.Left)
        {
        }

        // Select the clicked node
        //foreach (var node in Nodes)
        //    CheckNodeClick(node, OffsetMousePosition, e.Button);

        base.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (IsDragging)
        {
            HandleDrop();
        }

        if (_provisionalDragging)
        {

            if (_provisionalNode != null)
            {
                var pos = _dragPos;
                if (OffsetMousePosition == pos)
                    SelectNode(_provisionalNode);
            }

            _provisionalDragging = false;
        }

        // Select the clicked node
        foreach (var node in Nodes)
            CheckNodeClick(node, OffsetMousePosition, e.Button);

        if (e.Button == MouseButtons.Right && ContextMenuStrip != null)
        {
            ContextMenuStrip.Show(this, e.X, e.Y);
        }

        base.OnMouseUp(e);
    }

    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
        if (ModifierKeys == Keys.Control)
            return;

        if (e.Button == MouseButtons.Left)
        {
            foreach (var node in Nodes)
                CheckNodeDoubleClick(node, OffsetMousePosition);
        }

        base.OnMouseDoubleClick(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);

        foreach (var node in Nodes)
            NodeMouseLeave(node);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (IsDragging)
            return;

        if (Nodes.Count == 0)
            return;

        if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
            return;

        if (_anchoredNodeEnd == null)
        {
            if (Nodes.Count > 0)
                SelectNode(Nodes[0]);
            return;
        }

        if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
        {
            if (MultiSelect && ModifierKeys == Keys.Shift)
            {
                if (e.KeyCode == Keys.Up)
                {
                    if (_anchoredNodeEnd.PrevVisibleNode != null)
                    {
                        SelectAnchoredRange(_anchoredNodeEnd.PrevVisibleNode);
                        EnsureVisible();
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    if (_anchoredNodeEnd.NextVisibleNode != null)
                    {
                        SelectAnchoredRange(_anchoredNodeEnd.NextVisibleNode);
                        EnsureVisible();
                    }
                }
            }
            else
            {
                if (e.KeyCode == Keys.Up)
                {
                    if (_anchoredNodeEnd.PrevVisibleNode != null)
                    {
                        SelectNode(_anchoredNodeEnd.PrevVisibleNode);
                        EnsureVisible();
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    if (_anchoredNodeEnd.NextVisibleNode != null)
                    {
                        SelectNode(_anchoredNodeEnd.NextVisibleNode);
                        EnsureVisible();
                    }
                }
            }
        }

        if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
        {
            if (e.KeyCode == Keys.Left)
            {
                if (_anchoredNodeEnd.Expanded && _anchoredNodeEnd.Nodes.Count > 0)
                {
                    _anchoredNodeEnd.Expanded = false;
                }
                else
                {
                    if (_anchoredNodeEnd.ParentNode != null)
                    {
                        SelectNode(_anchoredNodeEnd.ParentNode);
                        EnsureVisible();
                    }
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (!_anchoredNodeEnd.Expanded)
                {
                    _anchoredNodeEnd.Expanded = true;
                }
                else
                {
                    if (_anchoredNodeEnd.Nodes.Count > 0)
                    {
                        SelectNode(_anchoredNodeEnd.Nodes[0]);
                        EnsureVisible();
                    }
                }
            }
        }
    }

    private void DragTimer_Tick(object sender, EventArgs e)
    {
        if (!IsDragging)
        {
            StopDrag();
            return;
        }

        if (MouseButtons != MouseButtons.Left)
        {
            StopDrag();
            return;
        }

        var pos = PointToClient(MousePosition);

        if (_vScrollBar.Visible)
        {
            // Scroll up
            if (pos.Y < ClientRectangle.Top)
            {
                var difference = (pos.Y - ClientRectangle.Top) * -1;

                if (difference > ItemHeight)
                    difference = ItemHeight;

                _vScrollBar.Value = _vScrollBar.Value - difference;
            }

            // Scroll down
            if (pos.Y > ClientRectangle.Bottom)
            {
                var difference = pos.Y - ClientRectangle.Bottom;

                if (difference > ItemHeight)
                    difference = ItemHeight;

                _vScrollBar.Value = _vScrollBar.Value + difference;
            }
        }

        if (_hScrollBar.Visible)
        {
            // Scroll left
            if (pos.X < ClientRectangle.Left)
            {
                var difference = (pos.X - ClientRectangle.Left) * -1;

                if (difference > ItemHeight)
                    difference = ItemHeight;

                _hScrollBar.Value = _hScrollBar.Value - difference;
            }

            // Scroll right
            if (pos.X > ClientRectangle.Right)
            {
                var difference = pos.X - ClientRectangle.Right;

                if (difference > ItemHeight)
                    difference = ItemHeight;

                _hScrollBar.Value = _hScrollBar.Value + difference;
            }
        }
    }

    protected override void WndProc(ref Message m)
    {
        // Request for a context menu
        if (m.Msg == 0x007B)
        {
            uint x = ((uint)m.LParam & 0x0000FFFFU);
            uint y = (((uint)m.LParam & 0xFFFF0000U) >> 16);

            if (x == 65535 && y == 65535)
            {
                foreach (var node in Nodes)
                    CheckNodeClick(node, OffsetMousePosition, MouseButtons.Right);

                if (this.SelectedNodes.Count > 0)
                {

                    ContextMenu.Show(this, new Point(2, 2));
                }
            }

        }

        base.WndProc(ref m);
    }

    public override void Refresh()
    {
        this.UpdateNodes();
        base.Refresh();
    }

    #endregion

    #region Method Region

    private void HookNodeEvents(LucidTreeNode node)
    {
        node.Nodes.ItemsAdded += ChildNodes_ItemsAdded;
        node.Nodes.ItemsRemoved += ChildNodes_ItemsRemoved;

        node.TextChanged += Nodes_TextChanged;
        node.NodeExpanded += Nodes_NodeExpanded;
        node.NodeCollapsed += Nodes_NodeCollapsed;
        node.BadgeCollection.OnCollectionChanged += BadgeCollection_OnCollectionChanged;

        foreach (var childNode in node.Nodes)
            HookNodeEvents(childNode);
    }

    private void UnhookNodeEvents(LucidTreeNode node)
    {
        node.Nodes.ItemsAdded -= ChildNodes_ItemsAdded;
        node.Nodes.ItemsRemoved -= ChildNodes_ItemsRemoved;

        node.TextChanged -= Nodes_TextChanged;
        node.NodeExpanded -= Nodes_NodeExpanded;
        node.NodeCollapsed -= Nodes_NodeCollapsed;
        node.BadgeCollection.OnCollectionChanged -= BadgeCollection_OnCollectionChanged;

        foreach (var childNode in node.Nodes)
            UnhookNodeEvents(childNode);
    }

    private void UpdateNodeOrder()
    {
        var nodesNoOrderNo = Nodes.Where(u => u.OrderNumber <= 0); // Get all nodes that don't need ordering

        var ordered = Nodes.Except(nodesNoOrderNo).OrderBy(u => u.OrderNumber);

        var oList = new ObservableList<LucidTreeNode>();
        oList.AddRange(ordered.ToList());
        oList.AddRange(nodesNoOrderNo);

        foreach (var node in oList)
        {
            if (oList.IndexOf(node) != 0)
                node.PrevVisibleNode = oList[oList.IndexOf(node) - 1];
            else
                node.PrevVisibleNode = null;

            if (oList.IndexOf(node) != oList.Count - 1)
                node.NextVisibleNode = oList[oList.IndexOf(node) + 1];
            else
                node.NextVisibleNode = null;
        }

        Nodes = oList;
    }

    internal void UpdateNodes()
    {
        if (IsDragging)
            return;

        if (Nodes.Count == 0)
            return;

        var yOffset = 0;
        var isOdd = false;
        var index = 0;
        LucidTreeNode prevNode = null;

        ContentSize = new Size(0, 0);

        for (var i = 0; i <= Nodes.Count - 1; i++)
        {
            var node = Nodes[i];
            UpdateNode(node, ref prevNode, 0, ref yOffset, ref isOdd, ref index);
        }

        ContentSize = new Size(ContentSize.Width, yOffset);

        VisibleNodeCount = index;

        Invalidate();
    }

    private void UpdateNode(LucidTreeNode node, ref LucidTreeNode prevNode, int indent, ref int yOffset,
                            ref bool isOdd, ref int index)
    {
        UpdateNodeBounds(node, yOffset, indent);

        yOffset += ItemHeight;

        node.Odd = isOdd;
        isOdd = !isOdd;

        node.VisibleIndex = index;
        index++;

        node.PrevVisibleNode = prevNode;

        if (prevNode != null)
            prevNode.NextVisibleNode = node;

        prevNode = node;

        if (node.Expanded)
        {
            foreach (var childNode in node.Nodes)
                UpdateNode(childNode, ref prevNode, indent + Indent, ref yOffset, ref isOdd, ref index);
        }
    }

    private void UpdateNodeBounds(LucidTreeNode node, int yOffset, int indent)
    {
        var expandTop = yOffset + (ItemHeight / 2) - (_expandAreaSize / 2);
        node.ExpandArea = new Rectangle(indent + 3, expandTop, _expandAreaSize, _expandAreaSize);

        var iconTop = yOffset + (ItemHeight / 2) - (_iconSize / 2);

        if (ShowIcons)
            node.IconArea = new Rectangle(node.ExpandArea.Right + 2, iconTop, _iconSize, _iconSize);
        else
            node.IconArea = new Rectangle(node.ExpandArea.Right, iconTop, 0, 0);

        using (var g = CreateGraphics())
        {
            var textSize = (int)(g.MeasureString(node.Text, Font).Width);
            node.TextArea = new Rectangle(node.IconArea.Right + 2, yOffset, textSize + 1, ItemHeight);
        }

        var badgeWidth = 0;
        foreach (var badge in node.BadgeCollection.Badges)
        {
            var textLength = TextRenderer.MeasureText(badge.Value, this.BadgeFont).Width;

            badgeWidth += textLength;
        }

        var progressBarWidth = (int)node.ProgressbarSize;

        node.ProgressBarArea = new Rectangle(node.TextArea.Right + 7, yOffset + 3, node.ShowProgressBar ? progressBarWidth + 18 : 0, 12);

        node.BadgeArea = new Rectangle(node.ProgressBarArea.Right + 7, yOffset + 3, badgeWidth, 12);

        node.FullArea = new Rectangle(indent, yOffset, (node.BadgeArea.Right - indent), ItemHeight);

        if (ContentSize.Width < node.BadgeArea.Right + 2)
            ContentSize = new Size(node.BadgeArea.Right + 2, ContentSize.Height);
    }

    private void LoadIcons()
    {
        DisposeIcons();

        _nodeClosed = TreeViewIcons.node_closed_empty.SetColor(ThemeProvider.Theme.Colors.LightText);
        _nodeClosedHover = TreeViewIcons.node_closed_empty.SetColor(ThemeProvider.Theme.Colors.ControlHighlight);
        _nodeClosedHoverSelected = TreeViewIcons.node_closed_full.SetColor(ThemeProvider.Theme.Colors.LightText);
        _nodeOpen = TreeViewIcons.node_open.SetColor(ThemeProvider.Theme.Colors.LightText);
        _nodeOpenHover = TreeViewIcons.node_open.SetColor(ThemeProvider.Theme.Colors.ControlHighlight);
        _nodeOpenHoverSelected = TreeViewIcons.node_open_empty.SetColor(ThemeProvider.Theme.Colors.LightText);
    }

    private void DisposeIcons()
    {
        if (_nodeClosed != null)
            _nodeClosed.Dispose();

        if (_nodeClosedHover != null)
            _nodeClosedHover.Dispose();

        if (_nodeClosedHoverSelected != null)
            _nodeClosedHoverSelected.Dispose();

        if (_nodeOpen != null)
            _nodeOpen.Dispose();

        if (_nodeOpenHover != null)
            _nodeOpenHover.Dispose();

        if (_nodeOpenHoverSelected != null)
            _nodeOpenHoverSelected.Dispose();
    }

    private void CheckHover()
    {
        if (!ClientRectangle.Contains(PointToClient(MousePosition)))
        {
            if (IsDragging)
            {
                if (_dropNode != null)
                {
                    _dropNode = null;
                    Invalidate();
                }
            }

            return;
        }

        foreach (var node in Nodes)
            CheckNodeHover(node, OffsetMousePosition);
    }

    private void NodeMouseLeave(LucidTreeNode node)
    {
        node.ExpandAreaHot = false;

        foreach (var childNode in node.Nodes)
            NodeMouseLeave(childNode);

        Invalidate();
    }

    private void CheckNodeHover(LucidTreeNode node, Point location)
    {
        if (IsDragging)
        {
            var rect = GetNodeFullRowArea(node);
            if (rect.Contains(OffsetMousePosition))
            {
                var newDropNode = _dragNodes.Contains(node) ? null : node;

                if (_dropNode != newDropNode)
                {
                    _dropNode = newDropNode;
                    Invalidate();
                }
            }
        }
        else
        {
            var hot = node.ExpandArea.Contains(location);
            if (node.ExpandAreaHot != hot)
            {
                node.ExpandAreaHot = hot;
                Invalidate();
            }
        }

        foreach (var childNode in node.Nodes)
            CheckNodeHover(childNode, location);
    }

    private void CheckNodeClick(LucidTreeNode node, Point location, MouseButtons button)
    {
        var rect = GetNodeFullRowArea(node);
        if (rect.Contains(location))
        {
            if (node.ExpandArea.Contains(location))
            {
                if (button == MouseButtons.Left)
                    node.Expanded = !node.Expanded;
            }
            else
            {
                if (button == MouseButtons.Left)
                {
                    if (MultiSelect && ModifierKeys == Keys.Shift)
                    {
                        SelectAnchoredRange(node);
                    }
                    else if (MultiSelect && ModifierKeys == Keys.Control)
                    {
                        ToggleNode(node);
                    }
                    else
                    {
                        if (!SelectedNodes.Contains(node))
                            SelectNode(node);

                        _dragPos = OffsetMousePosition;
                        _provisionalDragging = true;
                        _provisionalNode = node;
                    }

                    return;
                }
                else if (button == MouseButtons.Right)
                {
                    if (MultiSelect && ModifierKeys == Keys.Shift)
                        return;

                    if (MultiSelect && ModifierKeys == Keys.Control)
                        return;

                    if (!SelectedNodes.Contains(node))
                        SelectNode(node);

                    return;
                }
            }
        }

        if (node.Expanded)
        {
            foreach (var childNode in node.Nodes)
                CheckNodeClick(childNode, location, button);
        }
    }

    private void CheckNodeDoubleClick(LucidTreeNode node, Point location)
    {
        var rect = GetNodeFullRowArea(node);
        if (rect.Contains(location))
        {
            if (!node.ExpandArea.Contains(location))
                node.Expanded = !node.Expanded;

            return;
        }

        if (node.Expanded)
        {
            foreach (var childNode in node.Nodes)
                CheckNodeDoubleClick(childNode, location);
        }
    }

    public void SelectNode(LucidTreeNode node)
    {
        _selectedNodes.Clear();
        _selectedNodes.Add(node);

        _anchoredNodeStart = node;
        _anchoredNodeEnd = node;

        Invalidate();
    }

    public void SelectNodeByLocation(Point location)
    {
        _selectedNodes.Clear();

        location = this.PointToClient(location);

        // Select the correct node
        foreach (var node in Nodes)
            CheckNodeClick(node, location, MouseButtons.Right);

        Invalidate();
    }

    public void SelectNodes(LucidTreeNode startNode, LucidTreeNode endNode)
    {
        var nodes = new List<LucidTreeNode>();

        if (startNode == endNode)
            nodes.Add(startNode);

        if (startNode.VisibleIndex < endNode.VisibleIndex)
        {
            var node = startNode;
            nodes.Add(node);
            while (node != endNode && node != null)
            {
                node = node.NextVisibleNode;
                nodes.Add(node);
            }
        }
        else if (startNode.VisibleIndex > endNode.VisibleIndex)
        {
            var node = startNode;
            nodes.Add(node);
            while (node != endNode && node != null)
            {
                node = node.PrevVisibleNode;
                nodes.Add(node);
            }
        }

        SelectNodes(nodes, false);
    }

    public void SelectNodes(List<LucidTreeNode> nodes, bool updateAnchors = true)
    {
        _selectedNodes.Clear();

        foreach (var node in nodes)
            _selectedNodes.Add(node);

        if (updateAnchors && _selectedNodes.Count > 0)
        {
            _anchoredNodeStart = _selectedNodes[_selectedNodes.Count - 1];
            _anchoredNodeEnd = _selectedNodes[_selectedNodes.Count - 1];
        }

        Invalidate();
    }

    private void SelectAnchoredRange(LucidTreeNode node)
    {
        _anchoredNodeEnd = node;
        SelectNodes(_anchoredNodeStart, _anchoredNodeEnd);
    }

    public void ToggleNode(LucidTreeNode node)
    {
        if (_selectedNodes.Contains(node))
        {
            _selectedNodes.Remove(node);

            // If we just removed both the anchor start AND end then reset them
            if (_anchoredNodeStart == node && _anchoredNodeEnd == node)
            {
                if (_selectedNodes.Count > 0)
                {
                    _anchoredNodeStart = _selectedNodes[0];
                    _anchoredNodeEnd = _selectedNodes[0];
                }
                else
                {
                    _anchoredNodeStart = null;
                    _anchoredNodeEnd = null;
                }
            }

            // If we just removed the anchor start then update it accordingly
            if (_anchoredNodeStart == node)
            {
                if (_anchoredNodeEnd.VisibleIndex < node.VisibleIndex)
                    _anchoredNodeStart = node.PrevVisibleNode;
                else if (_anchoredNodeEnd.VisibleIndex > node.VisibleIndex)
                    _anchoredNodeStart = node.NextVisibleNode;
                else
                    _anchoredNodeStart = _anchoredNodeEnd;
            }

            // If we just removed the anchor end then update it accordingly
            if (_anchoredNodeEnd == node)
            {
                if (_anchoredNodeStart.VisibleIndex < node.VisibleIndex)
                    _anchoredNodeEnd = node.PrevVisibleNode;
                else if (_anchoredNodeStart.VisibleIndex > node.VisibleIndex)
                    _anchoredNodeEnd = node.NextVisibleNode;
                else
                    _anchoredNodeEnd = _anchoredNodeStart;
            }
        }
        else
        {
            _selectedNodes.Add(node);

            _anchoredNodeStart = node;
            _anchoredNodeEnd = node;
        }

        Invalidate();
    }

    public Rectangle GetNodeFullRowArea(LucidTreeNode node)
    {
        if (node.ParentNode != null && !node.ParentNode.Expanded)
            return new Rectangle(-1, -1, -1, -1);

        var width = Math.Max(ContentSize.Width, Viewport.Width);
        var rect = new Rectangle(0, node.FullArea.Top, width, ItemHeight);
        return rect;
    }

    public void EnsureVisible()
    {
        if (SelectedNodes.Count == 0)
            return;

        foreach (var node in SelectedNodes)
            node.EnsureVisible();

        var itemTop = -1;

        if (!MultiSelect)
            itemTop = SelectedNodes[0].FullArea.Top;
        else
            itemTop = _anchoredNodeEnd.FullArea.Top;

        var itemBottom = itemTop + ItemHeight;

        if (itemTop < Viewport.Top)
            VScrollTo(itemTop);

        if (itemBottom > Viewport.Bottom)
            VScrollTo((itemBottom - Viewport.Height));
    }

    public void Sort()
    {
        if (TreeViewNodeSorter == null)
            return;

        Nodes.Sort(TreeViewNodeSorter);

        foreach (var node in Nodes)
            SortChildNodes(node);
    }

    private void SortChildNodes(LucidTreeNode node)
    {
        node.Nodes.Sort(TreeViewNodeSorter);

        foreach (var childNode in node.Nodes)
            SortChildNodes(childNode);
    }

    public LucidTreeNode FindNode(string path)
    {
        foreach (var node in Nodes)
        {
            var compNode = FindNode(node, path);
            if (compNode != null)
                return compNode;
        }

        return null;
    }

    private LucidTreeNode FindNode(LucidTreeNode parentNode, string path, bool recursive = true)
    {
        if (parentNode.FullPath == path)
            return parentNode;

        foreach (var node in parentNode.Nodes)
        {
            if (node.FullPath == path)
                return node;

            if (recursive)
            {
                var compNode = FindNode(node, path);
                if (compNode != null)
                    return compNode;
            }
        }

        return null;
    }

    #endregion

    #region Drag & Drop Region

    protected override void StartDrag()
    {
        if (!AllowMoveNodes)
        {
            _provisionalDragging = false;
            return;
        }

        // Create initial list of nodes to drag
        _dragNodes = new List<LucidTreeNode>();
        foreach (var node in SelectedNodes)
            _dragNodes.Add(node);

        // Clear out any nodes with a parent that is being dragged
        foreach (var node in _dragNodes.ToList())
        {
            if (node.ParentNode == null)
                continue;

            if (_dragNodes.Contains(node.ParentNode))
                _dragNodes.Remove(node);
        }

        _provisionalDragging = false;

        Cursor = Cursors.SizeAll;

        base.StartDrag();
    }

    private void HandleDrag()
    {
        if (!AllowMoveNodes)
            return;

        var dropNode = _dropNode;

        if (dropNode == null)
        {
            if (Cursor != Cursors.No)
                Cursor = Cursors.No;

            return;
        }

        if (ForceDropToParent(dropNode))
            dropNode = dropNode.ParentNode;

        if (!CanMoveNodes(_dragNodes, dropNode))
        {
            if (Cursor != Cursors.No)
                Cursor = Cursors.No;

            return;
        }

        if (Cursor != Cursors.SizeAll)
            Cursor = Cursors.SizeAll;
    }

    private void HandleDrop()
    {
        if (!AllowMoveNodes)
            return;

        var dropNode = _dropNode;

        if (dropNode == null)
        {
            StopDrag();
            return;
        }

        if (ForceDropToParent(dropNode))
            dropNode = dropNode.ParentNode;

        if (CanMoveNodes(_dragNodes, dropNode, true))
        {
            var cachedSelectedNodes = SelectedNodes.ToList();

            MoveNodes(_dragNodes, dropNode);

            foreach (var node in _dragNodes)
            {
                if (node.ParentNode == null)
                    Nodes.Remove(node);
                else
                    node.ParentNode.Nodes.Remove(node);

                dropNode.Nodes.Add(node);
            }

            if (TreeViewNodeSorter != null)
                dropNode.Nodes.Sort(TreeViewNodeSorter);

            dropNode.Expanded = true;

            NodesMoved(_dragNodes);

            foreach (var node in cachedSelectedNodes)
                _selectedNodes.Add(node);
        }

        StopDrag();
        UpdateNodes();
    }

    protected override void StopDrag()
    {
        _dragNodes = null;
        _dropNode = null;

        Cursor = Cursors.Default;

        Invalidate();

        base.StopDrag();
    }

    protected virtual bool ForceDropToParent(LucidTreeNode node)
    {
        return false;
    }

    protected virtual bool CanMoveNodes(List<LucidTreeNode> dragNodes, LucidTreeNode dropNode, bool isMoving = false)
    {
        if (dropNode == null)
            return false;

        foreach (var node in dragNodes)
        {
            if (node == dropNode)
            {
                if (isMoving)
                    LucidMessageBox.ShowError($"Cannot move {node.Text}. The destination folder is the same as the source folder.", Application.ProductName);

                return false;
            }

            if (node.ParentNode != null && node.ParentNode == dropNode)
            {
                if (isMoving)
                    LucidMessageBox.ShowError($"Cannot move {node.Text}. The destination folder is the same as the source folder.", Application.ProductName);

                return false;
            }

            var parentNode = dropNode.ParentNode;
            while (parentNode != null)
            {
                if (node == parentNode)
                {
                    if (isMoving)
                        LucidMessageBox.ShowError($"Cannot move {node.Text}. The destination folder is a subfolder of the source folder.", Application.ProductName);

                    return false;
                }

                parentNode = parentNode.ParentNode;
            }
        }

        return true;
    }

    protected virtual void MoveNodes(List<LucidTreeNode> dragNodes, LucidTreeNode dropNode)
    { }

    protected virtual void NodesMoved(List<LucidTreeNode> nodesMoved)
    { }

    #endregion

    #region Paint Region

    protected override void PaintContent(Graphics g)
    {
        // Fill body
        using (var b = new SolidBrush(ThemeProvider.Theme.Colors.DockBackground))
        {
            g.FillRectangle(b, ClientRectangle);
        }

        foreach (var node in Nodes)
        {
            DrawNode(node, g);
        }
    }

    private void DrawNode(LucidTreeNode node, Graphics g)
    {
        var rect = GetNodeFullRowArea(node);

        // 1. Draw background
        //var bgColor = node.Odd ? ThemeProvider.Theme.Colors.DockBackground : ThemeProvider.Theme.Colors.DockBackground;
        var bgColor = ThemeProvider.Theme.Colors.DockBackground;

        if (SelectedNodes.Count > 0 && SelectedNodes.Contains(node))
            bgColor = Focused ? ThemeProvider.Theme.Colors.MainAccent : ThemeProvider.Theme.Colors.GreySelection;

        if (IsDragging && _dropNode == node)
            bgColor = Focused ? ThemeProvider.Theme.Colors.MainAccent : ThemeProvider.Theme.Colors.GreySelection;

        using (var b = new SolidBrush(bgColor))
        {

            if (ShowSelectedNodeRoundedRectangle)
                g.FillPath(b, RoundedRectangleHelper.CreateRoundedRectanglePath(rect, 8));
            else
                g.FillRectangle(b, rect);
        }

        // 2. Draw plus/minus icon
        if (node.Nodes.Count > 0)
        {
            var pos = new Point(node.ExpandArea.Location.X - 1, node.ExpandArea.Location.Y - 1);

            var icon = _nodeOpen;

            if (node.Expanded && !node.ExpandAreaHot)
                icon = _nodeOpen;
            else if (node.Expanded && node.ExpandAreaHot && !SelectedNodes.Contains(node))
                icon = _nodeOpenHover;
            else if (node.Expanded && node.ExpandAreaHot && SelectedNodes.Contains(node))
                icon = _nodeOpenHoverSelected;
            else if (!node.Expanded && !node.ExpandAreaHot)
                icon = _nodeClosed;
            else if (!node.Expanded && node.ExpandAreaHot && !SelectedNodes.Contains(node))
                icon = _nodeClosedHover;
            else if (!node.Expanded && node.ExpandAreaHot && SelectedNodes.Contains(node))
                icon = _nodeClosedHoverSelected;

            g.DrawImageUnscaled(icon, pos);
        }

        // 3. Draw icon
        if (ShowIcons && node.Icon != null)
        {
            if (node.Expanded && node.ExpandedIcon != null)
                g.DrawImageUnscaled(node.ExpandedIcon, node.IconArea.Location);
            else
                g.DrawImageUnscaled(node.Icon, node.IconArea.Location);
        }

        // 4. Draw text
        using (var b = new SolidBrush(ThemeProvider.Theme.Colors.LightText))
        using (var bContrast = new SolidBrush(Helper.ColorExtender.GetContrastColor(ThemeProvider.Theme.Colors.MainAccent)))
        {
            var stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };

            if (((SelectedNodes.Count > 0 && SelectedNodes.Contains(node)) || (IsDragging && _dropNode == node)) && Focused)
                g.DrawString(node.Text, Font, bContrast, node.TextArea, stringFormat);
            else
                g.DrawString(node.Text, Font, b, node.TextArea, stringFormat);
        }

        // 5. Draw Badge Progressbar
        if (node.ShowProgressBar)
        {
            var progressBarBackColor = ColorTranslator.FromHtml("#939dd5");
            var progressBarForeColor = ColorTranslator.FromHtml("#ffffff");
            var progressBarFillColor = ColorTranslator.FromHtml("#5c6bc0");

            var width = (int)node.ProgressbarSize;
            var percentage = node.ProgressBarPercentage;

            using (var p = new Pen(ThemeProvider.Theme.Colors.LightText))
            using (var gState = new SaveableGraphicsState(g))
            using (var brushBack = new SolidBrush(progressBarBackColor))
            using (var brushFore = new SolidBrush(progressBarForeColor))
            using (var brushFill = new SolidBrush(progressBarFillColor))
            {
                var path = BadgePath(new SizeF(width, 0), node.ProgressBarArea.X, node.ProgressBarArea.Y);

                var progressBarWidth = path.GetBounds().Width;
                var realPercentage = progressBarWidth * (percentage / 100);


                var pathFilled = BadgePath(new SizeF((float)realPercentage, 0), node.ProgressBarArea.X, node.ProgressBarArea.Y);


                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.FillPath(brushBack, path);
                g.FillPath(brushFill, pathFilled);

                //g.DrawPath(p, BadgeCornerPath(new SizeF(width, 0), node.ProgressBarArea.X, node.ProgressBarArea.Y));
            }
        }

        // 6. Draw Badge
        var newXPosition = 0;
        foreach (var badge in node.BadgeCollection.Badges)
        {
            if (!badge.Visible || string.IsNullOrEmpty(badge.Value))
                return;

            var badgeBackColor = node.BadgeColors.BadgeColors.FirstOrDefault(u => u.ColorId == badge.BadgeColorId)?.BackColor ?? ColorTranslator.FromHtml("#5c6bc0");
            var badgeBackColor2 = node.BadgeColors.BadgeColors.FirstOrDefault(u => u.ColorId == badge.BadgeColorId)?.BackColor2 ?? ColorTranslator.FromHtml("#5c6bc0");
            var badgeForeColor = node.BadgeColors.BadgeColors.FirstOrDefault(u => u.ColorId == badge.BadgeColorId)?.ForeColor ?? ColorTranslator.FromHtml("#ffffff");

            using (var p = new Pen(ThemeProvider.Theme.Colors.LightText))
            using (var pr = new Pen(Color.Red))
            using (var b = new SolidBrush(badgeBackColor))
            using (var bF = new SolidBrush(badgeForeColor))
            using (var gState = new SaveableGraphicsState(g))
            {
                var xCord = newXPosition == 0 ? node.BadgeArea.X : newXPosition;
                var yCord = node.BadgeArea.Y;

                var measuredSize = g.MeasureString(badge.Value, this.BadgeFont);
                measuredSize.Width = measuredSize.Width - 10; // Correcting the measurement

                var path = BadgePath(measuredSize, xCord, yCord);

                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;


                if (badge.ShowGradientIfAvailable)
                {
                    using var pg = new PathGradientBrush(path);

                    pg.CenterPoint = new PointF(xCord + 3, yCord);
                    pg.CenterColor = badgeBackColor;
                    pg.SurroundColors = new Color[] { badgeBackColor2 };
                    g.FillPath(pg, path);
                }
                else
                    g.FillPath(b, path);

                g.DrawPath(p, path);

                // Corner lower left
                g.DrawArc(p, xCord, yCord + 5, 7, 7, 90, 90);

                // Corner upper left
                g.DrawArc(p, xCord, yCord, 7, 7, 180, 90);

                // Corner upper right
                g.DrawArc(p, xCord + 11 + measuredSize.Width, yCord, 7, 7, 270, 90);

                // Corner lower right
                g.DrawArc(p, xCord + 11 + measuredSize.Width, yCord + 5, 7, 7, 360, 90);

                g.DrawString(badge.Value, _badgeFont, bF, xCord + 4, yCord);

                // Shows the BadgeArea-Bounds (Debug only)
                //g.DrawRectangle(pr, node.BadgeArea);

                if (newXPosition == 0)
                    newXPosition = node.BadgeArea.X + 15 + (int)measuredSize.Width + 7;
                else
                    newXPosition += 15 + (int)measuredSize.Width + 7;
            }
        }

        // 7. Draw child nodes
        DrawChildNodes(node, g);
    }

    private GraphicsPath BadgePath(SizeF badgeSize, int xCoordinate, int yCoordinate)
    {
        GraphicsPath path = new GraphicsPath();

        // Top
        path.AddLine(xCoordinate + 3, yCoordinate, xCoordinate + 15 + badgeSize.Width, yCoordinate);

        // Right
        path.AddLine(xCoordinate + 18 + badgeSize.Width, yCoordinate + 3, xCoordinate + 18 + badgeSize.Width, yCoordinate + 9);

        // Bottom
        path.AddLine(xCoordinate + 15 + badgeSize.Width, yCoordinate + 12, xCoordinate + 3, yCoordinate + 12);

        // Left
        path.AddLine(xCoordinate, yCoordinate + 9, xCoordinate, yCoordinate + 3);

        path.CloseFigure();
        return path;
    }

    //private GraphicsPath BadgePathFilled(SizeF badgeSize, int xCoordinate, int yCoordinate)
    //{
    //    GraphicsPath path = new GraphicsPath();

    //    // Top
    //    path.AddLine(xCoordinate + 3, yCoordinate, xCoordinate + 15 + badgeSize.Width, yCoordinate);

    //    // Right
    //    path.AddLine(xCoordinate + 18 + badgeSize.Width, yCoordinate + 3, xCoordinate  badgeSize.Width, yCoordinate + 9);

    //    // Bottom
    //    path.AddLine(xCoordinate + 15 + badgeSize.Width, yCoordinate + 12, xCoordinate + 3, yCoordinate + 12);

    //    // Left
    //    path.AddLine(xCoordinate, yCoordinate + 9, xCoordinate, yCoordinate + 3);

    //    path.CloseFigure();
    //    return path;
    //}


    private GraphicsPath BadgeCornerPath(SizeF badgeSize, int xCoordinate, int yCoordinate)
    {
        GraphicsPath path = new GraphicsPath();

        // Corner lower left
        path.AddArc(xCoordinate, yCoordinate + 5, 7, 7, 90, 90);

        // Corner upper left
        path.AddArc(xCoordinate, yCoordinate, 7, 7, 180, 90);

        // Corner upper right
        path.AddArc(xCoordinate + 11 + badgeSize.Width, yCoordinate, 7, 7, 270, 90);

        // Corner lower right
        path.AddArc(xCoordinate + 11 + badgeSize.Width, yCoordinate + 5, 7, 7, 360, 90);

        path.CloseFigure();

        return path;
    }

    private GraphicsPath ProgessBarPercentagePath(double percentage, int xCoordinate, int yCoordinate)
    {
        GraphicsPath path = new GraphicsPath();

        // Top
        //path.AddLine(xCoordinate + 3, yCoordinate, xCoordinate + 15 + (int)percentage, yCoordinate);

        return path;
    }

    /// <summary>
    /// Recursively paints only the nodes and child nodes within the viewport.
    /// </summary>
    private void DrawChildNodes(LucidTreeNode node, Graphics g)
    {
        if (node.Expanded)
        {
            foreach (var childNode in node.Nodes)
            {
                if (childNode.Expanded)
                    DrawChildNodes(childNode, g);

                bool isInTopView = Viewport.Top <= childNode.FullArea.Y;
                bool isWithin = childNode.FullArea.Y < Viewport.Top + Viewport.Height;
                bool isPastBottomView = childNode.FullArea.Y > Viewport.Top + Viewport.Height;

                if (isInTopView && isWithin)
                    DrawNode(childNode, g);

                if (isPastBottomView)
                    break;
            }
        }
    }

    #endregion
}