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
        lucidProgressBar = new Controls.LucidProgressBar();
        btnProgressBarAdd = new Controls.LucidButton();
        btnProgressBarRemove = new Controls.LucidButton();
        lucidFileDrop1 = new Controls.LucidFileDrop();
        lbProgressBar = new Controls.LucidLabel();
        lucidProgressBar1 = new Controls.LucidProgressBar();
        lbFileDrop = new Controls.LucidLabel();
        lbChipControl = new Controls.LucidLabel();
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
        lucidChipControl1.Location = new Point(27, 129);
        lucidChipControl1.Name = "lucidChipControl1";
        lucidChipControl1.SelectionMode = Lucid.Controls.SelectionMode.Multiple;
        lucidChipControl1.Size = new Size(256, 204);
        lucidChipControl1.SymmetricPadding = 10;
        lucidChipControl1.TabIndex = 4;
        lucidChipControl1.OnChipDeleted += lucidChipControl1_OnChipDeleted;
        // 
        // btAddChip
        // 
        btAddChip.BackColor = Color.Transparent;
        btAddChip.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
        btAddChip.Location = new Point(27, 100);
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
        btRemoveChip.Location = new Point(108, 100);
        btRemoveChip.Name = "btRemoveChip";
        btRemoveChip.Padding = new Padding(5);
        btRemoveChip.RoundedCornerRadius = 16;
        btRemoveChip.Size = new Size(96, 23);
        btRemoveChip.TabIndex = 7;
        btRemoveChip.Text = "Remove Chip";
        btRemoveChip.Click += btRemoveChip_Click;
        // 
        // lucidProgressBar
        // 
        lucidProgressBar.Location = new Point(340, 107);
        lucidProgressBar.MinimumSize = new Size(40, 6);
        lucidProgressBar.Name = "lucidProgressBar";
        lucidProgressBar.Size = new Size(283, 21);
        lucidProgressBar.TabIndex = 9;
        // 
        // btnProgressBarAdd
        // 
        btnProgressBarAdd.BackColor = Color.Transparent;
        btnProgressBarAdd.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
        btnProgressBarAdd.Location = new Point(442, 78);
        btnProgressBarAdd.Name = "btnProgressBarAdd";
        btnProgressBarAdd.Padding = new Padding(5);
        btnProgressBarAdd.RoundedCornerRadius = 16;
        btnProgressBarAdd.Size = new Size(32, 23);
        btnProgressBarAdd.TabIndex = 10;
        btnProgressBarAdd.Text = "+";
        btnProgressBarAdd.Click += btnProgressBarAdd_Click;
        // 
        // btnProgressBarRemove
        // 
        btnProgressBarRemove.BackColor = Color.Transparent;
        btnProgressBarRemove.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
        btnProgressBarRemove.Location = new Point(480, 78);
        btnProgressBarRemove.Name = "btnProgressBarRemove";
        btnProgressBarRemove.Padding = new Padding(5);
        btnProgressBarRemove.RoundedCornerRadius = 16;
        btnProgressBarRemove.Size = new Size(32, 23);
        btnProgressBarRemove.TabIndex = 11;
        btnProgressBarRemove.Text = "-";
        btnProgressBarRemove.Click += btnProgressBarRemove_Click;
        // 
        // lucidFileDrop1
        // 
        lucidFileDrop1.AllowDrop = true;
        lucidFileDrop1.AllowedFileExtensions = (List<string>)resources.GetObject("lucidFileDrop1.AllowedFileExtensions");
        lucidFileDrop1.BackColor = Color.FromArgb(60, 63, 65);
        lucidFileDrop1.DisplayText = "Sample Text";
        lucidFileDrop1.DisplayTextDragOver = "Sample Drag Over Text";
        lucidFileDrop1.Location = new Point(340, 212);
        lucidFileDrop1.Name = "lucidFileDrop1";
        lucidFileDrop1.Size = new Size(283, 121);
        lucidFileDrop1.TabIndex = 10;
        // 
        // lbProgressBar
        // 
        lbProgressBar.AutoSize = true;
        lbProgressBar.BackColor = Color.Transparent;
        lbProgressBar.ForeColor = Color.FromArgb(220, 220, 220);
        lbProgressBar.Location = new Point(340, 82);
        lbProgressBar.Name = "lbProgressBar";
        lbProgressBar.Size = new Size(69, 15);
        lbProgressBar.TabIndex = 14;
        lbProgressBar.Text = "ProgressBar";
        // 
        // lucidProgressBar1
        // 
        lucidProgressBar1.Indeterminate = true;
        lucidProgressBar1.Location = new Point(340, 134);
        lucidProgressBar1.MinimumSize = new Size(40, 6);
        lucidProgressBar1.Name = "lucidProgressBar1";
        lucidProgressBar1.Size = new Size(283, 21);
        lucidProgressBar1.TabIndex = 15;
        // 
        // lbFileDrop
        // 
        lbFileDrop.AutoSize = true;
        lbFileDrop.BackColor = Color.Transparent;
        lbFileDrop.ForeColor = Color.FromArgb(220, 220, 220);
        lbFileDrop.Location = new Point(340, 184);
        lbFileDrop.Name = "lbFileDrop";
        lbFileDrop.Size = new Size(51, 15);
        lbFileDrop.TabIndex = 16;
        lbFileDrop.Text = "FileDrop";
        // 
        // lbChipControl
        // 
        lbChipControl.AutoSize = true;
        lbChipControl.BackColor = Color.Transparent;
        lbChipControl.ForeColor = Color.FromArgb(220, 220, 220);
        lbChipControl.Location = new Point(27, 82);
        lbChipControl.Name = "lbChipControl";
        lbChipControl.Size = new Size(72, 15);
        lbChipControl.TabIndex = 17;
        lbChipControl.Text = "ChipControl";
        // 
        // MainPage
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(lbChipControl);
        Controls.Add(lbFileDrop);
        Controls.Add(lucidProgressBar1);
        Controls.Add(lbProgressBar);
        Controls.Add(lucidFileDrop1);
        Controls.Add(btnProgressBarRemove);
        Controls.Add(btnProgressBarAdd);
        Controls.Add(lucidProgressBar);
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
        Controls.SetChildIndex(lucidProgressBar, 0);
        Controls.SetChildIndex(btnProgressBarAdd, 0);
        Controls.SetChildIndex(btnProgressBarRemove, 0);
        Controls.SetChildIndex(lucidFileDrop1, 0);
        Controls.SetChildIndex(lbProgressBar, 0);
        Controls.SetChildIndex(lucidProgressBar1, 0);
        Controls.SetChildIndex(lbFileDrop, 0);
        Controls.SetChildIndex(lbChipControl, 0);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Controls.LucidChipControl lucidChipControl1;
    private Controls.LucidButton btAddChip;
    private Controls.LucidButton btRemoveChip;
    private Controls.LucidProgressBar lucidProgressBar;
    private Controls.LucidButton btnProgressBarAdd;
    private Controls.LucidButton btnProgressBarRemove;
    private Controls.LucidFileDrop lucidFileDrop1;
    private Controls.LucidLabel lbProgressBar;
    private Controls.LucidProgressBar lucidProgressBar1;
    private Controls.LucidLabel lbFileDrop;
    private Controls.LucidLabel lbChipControl;
}
