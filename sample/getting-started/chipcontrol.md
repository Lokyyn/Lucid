### ChipControl

This control allows you to create, select and highlight chips. When adding a chip to this control it has a default color which can be adjusted when needed. Furthermore it has two selection modes and different options for adjusting it's visual appearance.

![ChipControl](sample/resources/ChipControl.gif)
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