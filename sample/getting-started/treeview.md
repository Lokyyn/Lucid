### TreeView

This TreeView has almost all features a normal TreeView has e.g. it has the option to set a node icon when collapsed or expanded. It also features badges which can be used to highlight individual nodes with an information. This badges are fully customizable with its color and text.

![TreeView](sample/resources/TreeView.gif)

Adding a node is pretty straight foward. Just add an node instance to the _Nodes_ collection.
~~~
lucidTreeView.Nodes.Add(new Lucid.Controls.LucidTreeNode("Your first node"));
~~~

In order to add a single or multiple badges to a node lets first declare a variable for this target node.

~~~
var node = new Lucid.Controls.LucidTreeNode("NodeText");
~~~

Now that we can access its instance we can also access a nodes own collection of badges. This two properties are imported for us: _BadgeCollection_ and _BadgeColors_.

Inside the _BadgeCollection_ we only define the text for an badge. You can either directly add an instance to this collection or you just use its overload which accepts three strings.

~~~
node.BadgeCollection.AddBadge("uniqueBadgeId", "This is your Badge Text", "referenceToColorById");
~~~

You noticed that we also need to provide an _ColorId_ for each badge we create. This colors are managed inside the _BadgeColors_ property. Here you have the option to add a color by a direct instance of _System.Drawing.Color_ or by just passing a hex color string to its overload method.

~~~
node.BadgeColors.AddColor("myColorId", Color.Yellow, Color.Black);
node.BadgeColors.AddColor("mySecondColorId", "#5C6BC0", "#000000");
~~~

Thats it! If your _ColorId_ matches any of the given badges it will use this visual options to display the badge. This has the advantage that you can reuse the same color for the same node by just creating one color and linking them with the _ColorId_ from the badge.

**NOTE: Badges as well as Colors are only valid inside the same node and can not be used across nodes**