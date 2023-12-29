using Lucid.Docking;
using Lucid.Theming;

namespace Lucid.Sample.SideViews;

public partial class ToolWindow : LucidToolWindow
{
    public ToolWindow()
    {
        InitializeComponent();

        lucidTreeView.Nodes.Add(new Lucid.Controls.LucidTreeNode("Node 1"));

        var node2 = new Lucid.Controls.LucidTreeNode("Node 2");
        var node3 = new Lucid.Controls.LucidTreeNode("Node 3");
        var node4 = new Lucid.Controls.LucidTreeNode("Node 4");

        node2.ShowProgressBar = true;
        node2.ProgressbarSize = Lucid.Controls.DataClasses.BadgeProgressbar.eProgressbarSize.Small;

        node2.BadgeCollection.AddBadge("b1", "Badge 1", "c1");
        node2.BadgeColors.AddColor("c1", Color.Yellow, Color.Black);

        //node3.ShowProgressBar = true;
        //node3.ProgressbarSize = Lucid.Controls.DataClasses.BadgeProgressbar.eProgressbarSize.Medium;

        node3.BadgeCollection.AddBadge("b1", "Tiny", "c1");
        node3.BadgeCollection.AddBadge("b2", "Badge 3 :O", "c2");
        node3.BadgeColors.AddColor("c1", Color.Teal, Color.White);
        node3.BadgeColors.AddColor("c2", ColorTranslator.FromHtml("#FFA726"), Color.Black);

        //node4.ShowProgressBar = true;
        //node4.ProgressbarSize = Lucid.Controls.DataClasses.BadgeProgressbar.eProgressbarSize.Large;

        node4.BadgeCollection.AddBadge("b1", "Badge with a much longer text", "c1");
        node4.BadgeColors.AddColor("c1", ColorTranslator.FromHtml("#5C6BC0"), Color.White);


        lucidTreeView.Nodes.Add(node2);
        lucidTreeView.Nodes.Add(node3);
        lucidTreeView.Nodes.Add(node4);


        var node5 = new Lucid.Controls.LucidTreeNode("Node 5");
        node5.Nodes.Add(new Lucid.Controls.LucidTreeNode("Child node 1"));

        var child2 = new Lucid.Controls.LucidTreeNode("Child node 2");
        child2.BadgeCollection.AddBadge("b1", "This child badge has much to say", "c1");
        child2.BadgeColors.AddColor("c1", ColorTranslator.FromHtml("#43A047"), Color.White);


        node5.Nodes.Add(child2);
        node5.Nodes.Add(new Lucid.Controls.LucidTreeNode("Child node 3"));
        node5.Nodes.Add(new Lucid.Controls.LucidTreeNode("Child node 4"));

        lucidTreeView.Nodes.Add(node5);

        lucidTreeView.Nodes.Add(new Lucid.Controls.LucidTreeNode("Node 6"));
        lucidTreeView.Nodes.Add(new Lucid.Controls.LucidTreeNode("Node 7"));
        lucidTreeView.Nodes.Add(new Lucid.Controls.LucidTreeNode("Node 8"));
    }
}
