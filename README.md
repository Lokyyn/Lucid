[![.NET 6 Build](https://github.com/xmln17/LokConLib/actions/workflows/dotnet_6_build.yml/badge.svg)](https://github.com/xmln17/LokConLib/actions/workflows/dotnet_6_build.yml)
[![Publish package](https://github.com/xmln17/Lucid/actions/workflows/publish.yml/badge.svg?event=release)](https://github.com/xmln17/Lucid/actions/workflows/publish.yml)

[![](https://img.shields.io/nuget/dt/lucidui?color=004880&label=Downloads&logo=NuGet)](https://www.nuget.org/packages/LucidUI/)
[![](https://img.shields.io/nuget/v/lucidui?color=004880&label=Current%20Version&logo=NuGet)](https://www.nuget.org/packages/LucidUI/)

# Lucid
Lucid is a free WinForms control library with theming, docking and other functionality. This repository builds upon *[DarkUI](https://github.com/RobinPerris/DarkUI)* which is no longer actively maintained. Recognizing this, I decided to enhance it by incorporating my own features that align with my specific requirements. By doing so, I have transformed my private repository into a standalone repository, ready to be shared with the community.

**Feel free to use this in your own projects.**

*NOTE: I will occasionally dedicate time to work on this repository, but please note that new features will be introduced sporadically. If you come across any bugs, I kindly request you to create an issue so that I can address them accordingly.*

# Controls

### ChipControl

This control allows you to create, select and highlight chips. When adding a chip to this control it has a default color which can be adjusted when needed. Furthermore it has two selection modes and different options for adjusting it's visual appearance.

![ChipControl](sample\resources\ChipControl.gif)
~~~
// Adding a chip
lucidChipControl1.Chips.Add(new Controls.Chip() { Text = "Sample Chip", BackColor = Color.BlueViolet });

// Different selection modes
lucidChipControl1.SelectionMode = Lucid.Controls.SelectionMode.Multiple;
lucidChipControl1.SelectionMode = Lucid.Controls.SelectionMode.Single;

// Access chips
var allChips = lucidChipControl1.Chips; 

// Check wether the control has chips
var hasChips = lucidChipControl1.HasChips;


// Other look and feel options
lucidChipControl1.SymmetricPadding = 10;
lucidChipControl1.AllowChipSelection = true;
lucidChipControl1.AllowChipDeletion = true;
lucidChipControl1.HighlightChipUnderCursor = true;
lucidChipControl1.ChipsEnabled = true;
~~~

### TreeView

This TreeView has almost all features a normal TreeView has e.g. it has the option to set a node icon when collapsed or expanded. It also features badges which can be used to highlight individual nodes with an information. This badges are fully customizable with its color and text.

![TreeView](sample\resources\TreeView.gif)

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

### FileDrop

### Button

### DockPanel