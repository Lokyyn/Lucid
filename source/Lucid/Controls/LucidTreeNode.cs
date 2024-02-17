using Lucid.Collections;
using Lucid.Controls.DataClasses.Badge;
using Lucid.Controls.DataClasses.BadgeProgressbar;

namespace Lucid.Controls;

public class LucidTreeNode
{
    #region Event Region

    public event EventHandler<ObservableListModified<LucidTreeNode>> ItemsAdded;
    public event EventHandler<ObservableListModified<LucidTreeNode>> ItemsRemoved;

    public event EventHandler TextChanged;
    public event EventHandler NodeExpanded;
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

    public int OrderNumber { get; set; }

    internal Rectangle ExpandArea { get; set; }

    internal Rectangle IconArea { get; set; }

    internal Rectangle TextArea { get; set; }

    internal Rectangle BadgeArea { get; set; }

    internal Rectangle ProgressBarArea { get; set; }

    public BadgeCollection BadgeCollection { get; set; }

    public BadgeColorCollection BadgeColors { get; set; }

    public bool ShowProgressBar { get; set; }

    public eProgressbarSize ProgressbarSize { get; set; } = eProgressbarSize.Medium;

    public double ProgressBarPercentage { get; set; } = 25;

    internal Rectangle FullArea { get; set; }

    internal bool ExpandAreaHot { get; set; }

    public Bitmap Icon { get; set; }

    public Bitmap ExpandedIcon { get; set; }

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

    public bool IsRoot
    {
        get { return _isRoot; }
        set { _isRoot = value; }
    }

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

    public LucidTreeNode ParentNode
    {
        get { return _parentNode; }
        set { _parentNode = value; }
    }

    public bool Odd { get; set; }

    public object NodeType { get; set; }

    public object Tag { get; set; }

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

    public void Remove()
    {
        if (ParentNode != null)
            ParentNode.Nodes.Remove(this);
        else
            ParentTree.Nodes.Remove(this);
    }

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
