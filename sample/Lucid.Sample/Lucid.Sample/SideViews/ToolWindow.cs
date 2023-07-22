using Lucid.Docking;

namespace Lucid.Sample.SideViews;

public partial class ToolWindow : LucidToolWindow
{
    public ToolWindow()
    {
        InitializeComponent();

        lucidTreeView.Nodes.Add(new Lucid.Controls.LucidTreeNode("Node 1"));
        lucidTreeView.Nodes.Add(new Lucid.Controls.LucidTreeNode("Node 2"));
        lucidTreeView.Nodes.Add(new Lucid.Controls.LucidTreeNode("Node 3"));
        lucidTreeView.Nodes.Add(new Lucid.Controls.LucidTreeNode("Node 4"));

        var node5 = new Lucid.Controls.LucidTreeNode("Node 5");
        node5.Nodes.Add(new Lucid.Controls.LucidTreeNode("Child node 1"));
        node5.Nodes.Add(new Lucid.Controls.LucidTreeNode("Child node 2"));
        node5.Nodes.Add(new Lucid.Controls.LucidTreeNode("Child node 3"));
        node5.Nodes.Add(new Lucid.Controls.LucidTreeNode("Child node 4"));

        lucidTreeView.Nodes.Add(node5);


        lucidTreeView.Nodes.Add(new Lucid.Controls.LucidTreeNode("Node 6"));
        lucidTreeView.Nodes.Add(new Lucid.Controls.LucidTreeNode("Node 7"));
        lucidTreeView.Nodes.Add(new Lucid.Controls.LucidTreeNode("Node 8"));
    }
}
