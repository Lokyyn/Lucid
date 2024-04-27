namespace Lucid.Sample.Pages;

partial class MainPage
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPage));
        lucidChipControl1 = new Controls.LucidChipControl();
        btAddChip = new Controls.LucidButton();
        btRemoveChip = new Controls.LucidButton();
        lucidScrollableControl1 = new Controls.LucidScrollableControl();
        lucidButton1 = new Controls.LucidButton();
        btnShowMessageBox = new Controls.LucidButton();
        lucidFileDrop1 = new Controls.LucidFileDrop();
        lucidScrollableControl1.SuspendLayout();
        SuspendLayout();
        // 
        // lucidChipControl1
        // 
        lucidChipControl1.AllowChipDeletion = true;
        lucidChipControl1.AllowChipSelection = true;
        lucidChipControl1.BackColor = Color.FromArgb(60, 63, 65);
        lucidChipControl1.BorderStyle = BorderStyle.FixedSingle;
        lucidChipControl1.ChipsEnabled = true;
        lucidChipControl1.HighlightChipUnderCursor = true;
        lucidChipControl1.Location = new Point(27, 90);
        lucidChipControl1.Name = "lucidChipControl1";
        lucidChipControl1.SelectionMode = Lucid.Controls.SelectionMode.Multiple;
        lucidChipControl1.Size = new Size(256, 178);
        lucidChipControl1.SymmetricPadding = 10;
        lucidChipControl1.TabIndex = 4;
        lucidChipControl1.OnChipDeleted += lucidChipControl1_OnChipDeleted;
        // 
        // btAddChip
        // 
        btAddChip.BackColor = Color.Transparent;
        btAddChip.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
        btAddChip.Location = new Point(27, 61);
        btAddChip.Name = "btAddChip";
        btAddChip.Padding = new Padding(5);
        btAddChip.RoundedCornerRadius = 16;
        btAddChip.Size = new Size(75, 23);
        btAddChip.TabIndex = 6;
        btAddChip.Text = "Add Chip";
        btAddChip.Click += btAddChip_Click;
        // 
        // btRemoveChip
        // 
        btRemoveChip.BackColor = Color.Transparent;
        btRemoveChip.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
        btRemoveChip.Location = new Point(108, 61);
        btRemoveChip.Name = "btRemoveChip";
        btRemoveChip.Padding = new Padding(5);
        btRemoveChip.RoundedCornerRadius = 16;
        btRemoveChip.Size = new Size(96, 23);
        btRemoveChip.TabIndex = 7;
        btRemoveChip.Text = "Remove Chip";
        btRemoveChip.Click += btRemoveChip_Click;
        // 
        // lucidScrollableControl1
        // 
        lucidScrollableControl1.BorderStyle = BorderStyle.FixedSingle;
        lucidScrollableControl1.Controls.Add(lucidButton1);
        lucidScrollableControl1.Location = new Point(27, 341);
        lucidScrollableControl1.Name = "lucidScrollableControl1";
        lucidScrollableControl1.Size = new Size(248, 174);
        lucidScrollableControl1.TabIndex = 8;
        // 
        // lucidButton1
        // 
        lucidButton1.BackColor = Color.Transparent;
        lucidButton1.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
        lucidButton1.Location = new Point(205, 145);
        lucidButton1.Name = "lucidButton1";
        lucidButton1.Padding = new Padding(5);
        lucidButton1.RoundedCornerRadius = 16;
        lucidButton1.Size = new Size(75, 50);
        lucidButton1.TabIndex = 9;
        lucidButton1.Text = "lucidButton1";
        // 
        // btnShowMessageBox
        // 
        btnShowMessageBox.BackColor = Color.Transparent;
        btnShowMessageBox.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
        btnShowMessageBox.Location = new Point(449, 61);
        btnShowMessageBox.Name = "btnShowMessageBox";
        btnShowMessageBox.Padding = new Padding(5);
        btnShowMessageBox.RoundedCornerRadius = 16;
        btnShowMessageBox.Size = new Size(136, 23);
        btnShowMessageBox.TabIndex = 9;
        btnShowMessageBox.Text = "Show Message Box";
        btnShowMessageBox.Click += btnShowMessageBox_Click;
        // 
        // lucidFileDrop1
        // 
        lucidFileDrop1.AllowDrop = true;
        lucidFileDrop1.AllowedFileExtensions = (List<string>)resources.GetObject("lucidFileDrop1.AllowedFileExtensions");
        lucidFileDrop1.BackColor = Color.FromArgb(60, 63, 65);
        lucidFileDrop1.DisplayText = "Sample Text";
        lucidFileDrop1.DisplayTextDragOver = "Sample Drag Over Text";
        lucidFileDrop1.Location = new Point(374, 325);
        lucidFileDrop1.Name = "lucidFileDrop1";
        lucidFileDrop1.Size = new Size(261, 159);
        lucidFileDrop1.TabIndex = 10;
        // 
        // MainPage
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(lucidFileDrop1);
        Controls.Add(btnShowMessageBox);
        Controls.Add(lucidScrollableControl1);
        Controls.Add(btRemoveChip);
        Controls.Add(btAddChip);
        Controls.Add(lucidChipControl1);
        DockText = "MainPage";
        Name = "MainPage";
        Size = new Size(722, 606);
        Controls.SetChildIndex(_hScrollBar, 0);
        Controls.SetChildIndex(_vScrollBar, 0);
        Controls.SetChildIndex(lucidChipControl1, 0);
        Controls.SetChildIndex(btAddChip, 0);
        Controls.SetChildIndex(btRemoveChip, 0);
        Controls.SetChildIndex(lucidScrollableControl1, 0);
        Controls.SetChildIndex(btnShowMessageBox, 0);
        Controls.SetChildIndex(lucidFileDrop1, 0);
        lucidScrollableControl1.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Controls.LucidChipControl lucidChipControl1;
    private Controls.LucidButton btAddChip;
    private Controls.LucidButton btRemoveChip;
    private Controls.LucidScrollableControl lucidScrollableControl1;
    private Controls.LucidButton lucidButton1;
    private Controls.LucidButton btnShowMessageBox;
    private Controls.LucidFileDrop lucidFileDrop1;
}
