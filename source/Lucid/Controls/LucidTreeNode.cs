using Lucid.Collections;
using Lucid.Controls.DataClasses.Badge;
using Lucid.Controls.DataClasses.BadgeProgressbar;

namespace Lucid.Controls;

public class LucidTreeNode
{
    #region Event Region

    /// <summary>Raised when one or more child nodes are added to <see cref="Nodes"/>.</summary>
    public event EventHandler<ObservableListModified<LucidTreeNode>> ItemsAdded;
    /// <summary>Raised when one or more child nodes are removed from <see cref="Nodes"/>.</summary>
    public event EventHandler<ObservableListModified<LucidTreeNode>> ItemsRemoved;

    /// <summary>Raised when <see cref="Text"/> is assigned a new value.</summary>
    public event EventHandler TextChanged;
    /// <summary>Raised when the node transitions to the expanded state.</summary>
    public event EventHandler NodeExpanded;
    /// <summary>Raised when the node transitions to the collapsed state.</summary>
    public event EventHandler NodeCollapsed;

    #endregion

    #region Field Region

    private string _text;
    private bool _isRoot;
    private LucidTreeView _parentTree;
    private LucidTreeNode _parentNode;

    private ObservableList<LucidTreeNode> _nodes;

    private bool _expanded;

    #endregion

    #region Property Region

    /// <summary>The label text shown in the tree row for this node.</summary>
    public string Text
    {
        get { return _text; }
        set
        {
            if (_text == value)
                return;

            _text = value;

            OnTextChanged();
        }
    }

    /// <summary>Used for manual ordering when <see cref="LucidTreeView.OrderNodes"/> is <see langword="true"/>.</summary>
    public int OrderNumber { get; set; }

    internal Rectangle ExpandArea { get; set; }

    internal Rectangle IconArea { get; set; }

    internal Rectangle TextArea { get; set; }

    internal Rectangle BadgeArea { get; set; }

    internal Rectangle ProgressBarArea { get; set; }

    /// <summary>Colored badge labels rendered to the right of the node text.</summary>
    public BadgeCollection BadgeCollection { get; set; }

    /// <summary>Color definitions for each badge in <see cref="BadgeCollection"/>.</summary>
    public BadgeColorCollection BadgeColors { get; set; }

    /// <summary>When <see langword="true"/>, a small inline progress bar is rendered inside the node row.</summary>
    public bool ShowProgressBar { get; set; }

    /// <summary>Controls the height of the inline progress bar.</summary>
    public eProgressbarSize ProgressbarSize { get; set; } = eProgressbarSize.Medium;

    /// <summary>Fill level of the inline progress bar, expressed as a percentage (0–100).</summary>
    public double ProgressBarPercentage { get; set; } = 25;

    internal Rectangle FullArea { get; set; }

    internal bool ExpandAreaHot { get; set; }

    /// <summary>Icon drawn to the left of the node text when <see cref="LucidTreeView.ShowIcons"/> is <see langword="true"/>.</summary>
    public Bitmap Icon { get; set; }

    /// <summary>Alternative icon shown instead of <see cref="Icon"/> while the node is in the expanded state.</summary>
    public Bitmap ExpandedIcon { get; set; }

    /// <summary>
    /// Gets or sets whether the node's children are visible.
    /// Setting to <see langword="true"/> has no effect when <see cref="Nodes"/> is empty.
    /// </summary>
    public bool Expanded
    {
        get { return _expanded; }
        set
        {
            if (_expanded == value)
                return;

            if (value == true && Nodes.Count == 0)
                return;

            _expanded = value;

            if (_expanded)
            {
                if (NodeExpanded != null)
                    NodeExpanded(this, null);
            }
            else
            {
                if (NodeCollapsed != null)
                    NodeCollapsed(this, null);
            }
        }
    }

    /// <summary>The direct child nodes of this node.</summary>
    public ObservableList<LucidTreeNode> Nodes
    {
        get { return _nodes; }
        set
        {
            if (_nodes != null)
            {
                _nodes.ItemsAdded -= Nodes_ItemsAdded;
                _nodes.ItemsRemoved -= Nodes_ItemsRemoved;
            }

            _nodes = value;

            _nodes.ItemsAdded += Nodes_ItemsAdded;
            _nodes.ItemsRemoved += Nodes_ItemsRemoved;
        }
    }

    /// <summary><see langword="true"/> when this node sits directly in <see cref="LucidTreeView.Nodes"/> (depth 0).</summary>
    public bool IsRoot
    {
        get { return _isRoot; }
        set { _isRoot = value; }
    }

    /// <summary>The <see cref="LucidTreeView"/> that owns this node.</summary>
    public LucidTreeView ParentTree
    {
        get { return _parentTree; }
        set
        {
            if (_parentTree == value)
                return;

            _parentTree = value;

            foreach (var node in Nodes)
                node.ParentTree = _parentTree;
        }
    }

    /// <summary>The immediate parent node, or <see langword="null"/> for root nodes.</summary>
    public LucidTreeNode ParentNode
    {
        get { return _parentNode; }
        set { _parentNode = value; }
    }

    public bool Odd { get; set; }

    /// <summary>Arbitrary type identifier that can be used to distinguish node categories in application code.</summary>
    public object NodeType { get; set; }

    /// <summary>Arbitrary user data attached to this node.</summary>
    public object Tag { get; set; }

    /// <summary>
    /// The backslash-separated path from the root node down to this node, e.g. <c>Assets\Icons\arrow.png</c>.
    /// </summary>
    public string FullPath
    {
        get
        {
            var parent = ParentNode;
            var path = Text;

            while (parent != null)
            {
                path = string.Format("{0}{1}{2}", parent.Text, "\\", path);
                parent = parent.ParentNode;
            }

            return path;
        }
    }

    public LucidTreeNode PrevVisibleNode { get; set; }

    public LucidTreeNode NextVisibleNode { get; set; }

    public int VisibleIndex { get; set; }

    /// <summary>
    /// Returns <see langword="true"/> when <paramref name="node"/> is a direct or indirect parent of this node.
    /// </summary>
    /// <param name="node">The candidate ancestor node to test.</param>
    public bool IsNodeAncestor(LucidTreeNode node)
    {
        var parent = ParentNode;
        while (parent != null)
        {
            if (parent == node)
                return true;

            parent = parent.ParentNode;
        }

        return false;
    }

    #endregion

    #region Constructor Region

    public LucidTreeNode()
    {
        Nodes = new ObservableList<LucidTreeNode>();
        BadgeCollection = new BadgeCollection();
        BadgeColors = new BadgeColorCollection();
    }

    public LucidTreeNode(string text)
        : this()
    {
        Text = text;
        BadgeCollection = new BadgeCollection();
        BadgeColors = new BadgeColorCollection();
    }

    #endregion

    #region Method Region

    /// <summary>
    /// Removes this node from its parent collection.
    /// If the node is a root node it is removed from <see cref="LucidTreeView.Nodes"/>;
    /// otherwise it is removed from its <see cref="ParentNode"/>'s <see cref="Nodes"/>.
    /// </summary>
    public void Remove()
    {
        if (ParentNode != null)
            ParentNode.Nodes.Remove(this);
        else
            ParentTree.Nodes.Remove(this);
    }

    /// <summary>
    /// Expands all ancestor nodes up to the root so that this node becomes visible in the tree.
    /// </summary>
    public void EnsureVisible()
    {
        var parent = ParentNode;

        while (parent != null)
        {
            parent.Expanded = true;
            parent = parent.ParentNode;
        }
    }

    #endregion

    #region Event Handler Region

    private void OnTextChanged()
    {
        if (ParentTree != null && ParentTree.TreeViewNodeSorter != null)
        {
            if (ParentNode != null)
                ParentNode.Nodes.Sort(ParentTree.TreeViewNodeSorter);
            else
                ParentTree.Nodes.Sort(ParentTree.TreeViewNodeSorter);
        }

        if (TextChanged != null)
            TextChanged(this, null);
    }

    private void Nodes_ItemsAdded(object sender, ObservableListModified<LucidTreeNode> e)
    {
        foreach (var node in e.Items)
        {
            node.ParentNode = this;
            node.ParentTree = ParentTree;
        }

        if (ParentTree != null && ParentTree.TreeViewNodeSorter != null)
            Nodes.Sort(ParentTree.TreeViewNodeSorter);

        if (ItemsAdded != null)
            ItemsAdded(this, e);
    }

    private void Nodes_ItemsRemoved(object sender, ObservableListModified<LucidTreeNode> e)
    {
        if (Nodes.Count == 0)
            Expanded = false;

        if (ItemsRemoved != null)
            ItemsRemoved(this, e);
    }

    #endregion
}
