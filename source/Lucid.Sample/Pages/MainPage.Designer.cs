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
        lucidChipControl1 = new Lucid.Controls.LucidChipControl();
        btAddChip = new Lucid.Controls.LucidButton();
        btRemoveChip = new Lucid.Controls.LucidButton();
        lucidProgressBar = new Lucid.Controls.LucidProgressBar();
        btnProgressBarAdd = new Lucid.Controls.LucidButton();
        btnProgressBarRemove = new Lucid.Controls.LucidButton();
        lucidFileDrop1 = new Lucid.Controls.LucidFileDrop();
        lbProgressBar = new Lucid.Controls.LucidLabel();
        lucidProgressBar1 = new Lucid.Controls.LucidProgressBar();
        lbFileDrop = new Lucid.Controls.LucidLabel();
        lbChipControl = new Lucid.Controls.LucidLabel();
        lbInputs = new Lucid.Controls.LucidLabel();
        lucidTextBox1 = new Lucid.Controls.LucidTextBox();
        lucidComboBox1 = new Lucid.Controls.LucidComboBox();
        lucidCheckBox1 = new Lucid.Controls.LucidCheckBox();
        lucidCheckBox2 = new Lucid.Controls.LucidCheckBox();
        lucidRadioButton1 = new Lucid.Controls.LucidRadioButton();
        lucidRadioButton2 = new Lucid.Controls.LucidRadioButton();
        lucidNumericUpDown1 = new Lucid.Controls.LucidNumericUpDown();
        lucidButtonNormal = new Lucid.Controls.LucidButton();
        lbTreeView = new Lucid.Controls.LucidLabel();
        lucidTreeView1 = new Lucid.Controls.LucidTreeView();
        lucidScrollableControl1 = new Lucid.Controls.LucidScrollableControl();
        lucidButton3 = new Lucid.Controls.LucidButton();
        lucidButton2 = new Lucid.Controls.LucidButton();
        lucidButton1 = new Lucid.Controls.LucidButton();
        lbScrollableControl = new Lucid.Controls.LucidLabel();
        btnTreeViewProgressAdd = new Lucid.Controls.LucidButton();
        btnTreeViewProgressRemove = new Lucid.Controls.LucidButton();
        lbSlider = new Lucid.Controls.LucidLabel();
        lucidSlider1 = new Lucid.Controls.LucidSlider();
        btnToggleSliderMode = new Lucid.Controls.LucidButton();
        ((System.ComponentModel.ISupportInitialize)lucidNumericUpDown1).BeginInit();
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
        // lbInputs
        // 
        lbInputs.AutoSize = true;
        lbInputs.BackColor = Color.Transparent;
        lbInputs.ForeColor = Color.FromArgb(220, 220, 220);
        lbInputs.Location = new Point(27, 348);
        lbInputs.Name = "lbInputs";
        lbInputs.Size = new Size(40, 15);
        lbInputs.TabIndex = 18;
        lbInputs.Text = "Inputs";
        // 
        // lucidTextBox1
        // 
        lucidTextBox1.BorderStyle = BorderStyle.FixedSingle;
        lucidTextBox1.Location = new Point(27, 368);
        lucidTextBox1.Name = "lucidTextBox1";
        lucidTextBox1.PlaceholderText = "Enter text...";
        lucidTextBox1.Size = new Size(200, 23);
        lucidTextBox1.TabIndex = 19;
        // 
        // lucidComboBox1
        // 
        lucidComboBox1.DrawMode = DrawMode.OwnerDrawVariable;
        lucidComboBox1.Location = new Point(27, 397);
        lucidComboBox1.Name = "lucidComboBox1";
        lucidComboBox1.Size = new Size(150, 24);
        lucidComboBox1.TabIndex = 20;
        // 
        // lucidCheckBox1
        // 
        lucidCheckBox1.BackColor = Color.Transparent;
        lucidCheckBox1.Location = new Point(27, 428);
        lucidCheckBox1.Name = "lucidCheckBox1";
        lucidCheckBox1.Size = new Size(150, 20);
        lucidCheckBox1.TabIndex = 21;
        lucidCheckBox1.Text = "CheckBox";
        lucidCheckBox1.UseBackColorProperty = false;
        // 
        // lucidCheckBox2
        // 
        lucidCheckBox2.BackColor = Color.Transparent;
        lucidCheckBox2.Checked = true;
        lucidCheckBox2.CheckState = CheckState.Checked;
        lucidCheckBox2.Location = new Point(27, 449);
        lucidCheckBox2.Name = "lucidCheckBox2";
        lucidCheckBox2.Size = new Size(150, 20);
        lucidCheckBox2.TabIndex = 22;
        lucidCheckBox2.Text = "Checked";
        lucidCheckBox2.UseBackColorProperty = false;
        // 
        // lucidRadioButton1
        // 
        lucidRadioButton1.AllowCustomBackColor = false;
        lucidRadioButton1.BackColor = Color.Transparent;
        lucidRadioButton1.Checked = true;
        lucidRadioButton1.Location = new Point(27, 476);
        lucidRadioButton1.Name = "lucidRadioButton1";
        lucidRadioButton1.Size = new Size(130, 20);
        lucidRadioButton1.TabIndex = 23;
        lucidRadioButton1.TabStop = true;
        lucidRadioButton1.Text = "Option A";
        // 
        // lucidRadioButton2
        // 
        lucidRadioButton2.AllowCustomBackColor = false;
        lucidRadioButton2.BackColor = Color.Transparent;
        lucidRadioButton2.Location = new Point(27, 497);
        lucidRadioButton2.Name = "lucidRadioButton2";
        lucidRadioButton2.Size = new Size(130, 20);
        lucidRadioButton2.TabIndex = 24;
        lucidRadioButton2.Text = "Option B";
        // 
        // lucidNumericUpDown1
        // 
        lucidNumericUpDown1.Location = new Point(27, 524);
        lucidNumericUpDown1.Name = "lucidNumericUpDown1";
        lucidNumericUpDown1.Size = new Size(100, 23);
        lucidNumericUpDown1.TabIndex = 25;
        lucidNumericUpDown1.Value = new decimal(new int[] { 42, 0, 0, 0 });
        // 
        // lucidButtonNormal
        // 
        lucidButtonNormal.BackColor = Color.Transparent;
        lucidButtonNormal.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
        lucidButtonNormal.Location = new Point(150, 521);
        lucidButtonNormal.Name = "lucidButtonNormal";
        lucidButtonNormal.Padding = new Padding(5);
        lucidButtonNormal.RoundedCornerRadius = 16;
        lucidButtonNormal.Size = new Size(90, 28);
        lucidButtonNormal.TabIndex = 26;
        lucidButtonNormal.Text = "Normal";
        // 
        // lbTreeView
        // 
        lbTreeView.AutoSize = true;
        lbTreeView.BackColor = Color.Transparent;
        lbTreeView.ForeColor = Color.FromArgb(220, 220, 220);
        lbTreeView.Location = new Point(340, 348);
        lbTreeView.Name = "lbTreeView";
        lbTreeView.Size = new Size(54, 15);
        lbTreeView.TabIndex = 27;
        lbTreeView.Text = "TreeView";
        // 
        // lucidTreeView1
        // 
        lucidTreeView1.ContextMenu = null;
        lucidTreeView1.Location = new Point(340, 368);
        lucidTreeView1.MaxDragChange = 20;
        lucidTreeView1.Name = "lucidTreeView1";
        lucidTreeView1.Size = new Size(283, 210);
        lucidTreeView1.TabIndex = 28;
        // 
        // lucidScrollableControl1
        // 
        lucidScrollableControl1.Controls.Add(lucidButton3);
        lucidScrollableControl1.Controls.Add(lucidButton2);
        lucidScrollableControl1.Controls.Add(lucidButton1);
        lucidScrollableControl1.Location = new Point(690, 107);
        lucidScrollableControl1.Name = "lucidScrollableControl1";
        lucidScrollableControl1.Size = new Size(401, 226);
        lucidScrollableControl1.TabIndex = 29;
        // 
        // lucidButton3
        // 
        lucidButton3.BackColor = Color.Transparent;
        lucidButton3.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
        lucidButton3.Location = new Point(58, 315);
        lucidButton3.Name = "lucidButton3";
        lucidButton3.Padding = new Padding(5);
        lucidButton3.RoundedCornerRadius = 16;
        lucidButton3.Size = new Size(75, 23);
        lucidButton3.TabIndex = 4;
        lucidButton3.Text = "button3";
        // 
        // lucidButton2
        // 
        lucidButton2.BackColor = Color.Transparent;
        lucidButton2.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
        lucidButton2.Location = new Point(359, 27);
        lucidButton2.Name = "lucidButton2";
        lucidButton2.Padding = new Padding(5);
        lucidButton2.RoundedCornerRadius = 16;
        lucidButton2.Size = new Size(75, 23);
        lucidButton2.TabIndex = 3;
        lucidButton2.Text = "button2";
        // 
        // lucidButton1
        // 
        lucidButton1.BackColor = Color.Transparent;
        lucidButton1.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
        lucidButton1.Location = new Point(345, 200);
        lucidButton1.Name = "lucidButton1";
        lucidButton1.Padding = new Padding(5);
        lucidButton1.RoundedCornerRadius = 16;
        lucidButton1.Size = new Size(75, 23);
        lucidButton1.TabIndex = 2;
        lucidButton1.Text = "button1";
        // 
        // lbScrollableControl
        // 
        lbScrollableControl.AutoSize = true;
        lbScrollableControl.BackColor = Color.Transparent;
        lbScrollableControl.ForeColor = Color.FromArgb(220, 220, 220);
        lbScrollableControl.Location = new Point(690, 82);
        lbScrollableControl.Name = "lbScrollableControl";
        lbScrollableControl.Size = new Size(98, 15);
        lbScrollableControl.TabIndex = 30;
        lbScrollableControl.Text = "ScrollableControl";
        // 
        // btnTreeViewProgressAdd
        // 
        btnTreeViewProgressAdd.BackColor = Color.Transparent;
        btnTreeViewProgressAdd.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
        btnTreeViewProgressAdd.Location = new Point(420, 344);
        btnTreeViewProgressAdd.Name = "btnTreeViewProgressAdd";
        btnTreeViewProgressAdd.Padding = new Padding(5);
        btnTreeViewProgressAdd.RoundedCornerRadius = 16;
        btnTreeViewProgressAdd.Size = new Size(32, 23);
        btnTreeViewProgressAdd.TabIndex = 31;
        btnTreeViewProgressAdd.Text = "+";
        btnTreeViewProgressAdd.Click += btnTreeViewProgressAdd_Click;
        // 
        // btnTreeViewProgressRemove
        // 
        btnTreeViewProgressRemove.BackColor = Color.Transparent;
        btnTreeViewProgressRemove.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
        btnTreeViewProgressRemove.Location = new Point(458, 344);
        btnTreeViewProgressRemove.Name = "btnTreeViewProgressRemove";
        btnTreeViewProgressRemove.Padding = new Padding(5);
        btnTreeViewProgressRemove.RoundedCornerRadius = 16;
        btnTreeViewProgressRemove.Size = new Size(32, 23);
        btnTreeViewProgressRemove.TabIndex = 32;
        btnTreeViewProgressRemove.Text = "-";
        btnTreeViewProgressRemove.Click += btnTreeViewProgressRemove_Click;
        // 
        // lbSlider
        // 
        lbSlider.AutoSize = true;
        lbSlider.BackColor = Color.Transparent;
        lbSlider.ForeColor = Color.FromArgb(220, 220, 220);
        lbSlider.Location = new Point(690, 348);
        lbSlider.Name = "lbSlider";
        lbSlider.Size = new Size(36, 15);
        lbSlider.TabIndex = 33;
        lbSlider.Text = "Slider";
        // 
        // lucidSlider1
        // 
        lucidSlider1.CustomValueLabel = "$";
        lucidSlider1.Location = new Point(690, 368);
        lucidSlider1.MinimumSize = new Size(60, 28);
        lucidSlider1.Name = "lucidSlider1";
        lucidSlider1.Size = new Size(276, 59);
        lucidSlider1.TabIndex = 34;
        lucidSlider1.Text = "lucidSlider1";
        // 
        // btnToggleSliderMode
        // 
        btnToggleSliderMode.BackColor = Color.Transparent;
        btnToggleSliderMode.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
        btnToggleSliderMode.Location = new Point(760, 348);
        btnToggleSliderMode.Name = "btnToggleSliderMode";
        btnToggleSliderMode.Padding = new Padding(5);
        btnToggleSliderMode.RoundedCornerRadius = 16;
        btnToggleSliderMode.Size = new Size(159, 19);
        btnToggleSliderMode.TabIndex = 35;
        btnToggleSliderMode.Text = "Toggle Slider Mode";
        btnToggleSliderMode.Click += btnToggleSliderMode_Click;
        // 
        // MainPage
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(btnToggleSliderMode);
        Controls.Add(lucidSlider1);
        Controls.Add(lbSlider);
        Controls.Add(lbScrollableControl);
        Controls.Add(btnTreeViewProgressRemove);
        Controls.Add(btnTreeViewProgressAdd);
        Controls.Add(lucidScrollableControl1);
        Controls.Add(lucidTreeView1);
        Controls.Add(lbTreeView);
        Controls.Add(lucidButtonNormal);
        Controls.Add(lucidNumericUpDown1);
        Controls.Add(lucidRadioButton2);
        Controls.Add(lucidRadioButton1);
        Controls.Add(lucidCheckBox2);
        Controls.Add(lucidCheckBox1);
        Controls.Add(lucidComboBox1);
        Controls.Add(lucidTextBox1);
        Controls.Add(lbInputs);
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
        Size = new Size(1127, 642);
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
        Controls.SetChildIndex(lbInputs, 0);
        Controls.SetChildIndex(lucidTextBox1, 0);
        Controls.SetChildIndex(lucidComboBox1, 0);
        Controls.SetChildIndex(lucidCheckBox1, 0);
        Controls.SetChildIndex(lucidCheckBox2, 0);
        Controls.SetChildIndex(lucidRadioButton1, 0);
        Controls.SetChildIndex(lucidRadioButton2, 0);
        Controls.SetChildIndex(lucidNumericUpDown1, 0);
        Controls.SetChildIndex(lucidButtonNormal, 0);
        Controls.SetChildIndex(lbTreeView, 0);
        Controls.SetChildIndex(lucidTreeView1, 0);
        Controls.SetChildIndex(lucidScrollableControl1, 0);
        Controls.SetChildIndex(btnTreeViewProgressAdd, 0);
        Controls.SetChildIndex(btnTreeViewProgressRemove, 0);
        Controls.SetChildIndex(lbScrollableControl, 0);
        Controls.SetChildIndex(lbSlider, 0);
        Controls.SetChildIndex(lucidSlider1, 0);
        Controls.SetChildIndex(btnToggleSliderMode, 0);
        ((System.ComponentModel.ISupportInitialize)lucidNumericUpDown1).EndInit();
        lucidScrollableControl1.ResumeLayout(false);
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
    private Controls.LucidLabel lbInputs;
    private Controls.LucidTextBox lucidTextBox1;
    private Controls.LucidComboBox lucidComboBox1;
    private Controls.LucidCheckBox lucidCheckBox1;
    private Controls.LucidCheckBox lucidCheckBox2;
    private Controls.LucidRadioButton lucidRadioButton1;
    private Controls.LucidRadioButton lucidRadioButton2;
    private Controls.LucidNumericUpDown lucidNumericUpDown1;
    private Controls.LucidButton lucidButtonNormal;
    private Controls.LucidLabel lbTreeView;
    private Controls.LucidTreeView lucidTreeView1;
    private Controls.LucidScrollableControl lucidScrollableControl1;
    private Controls.LucidButton lucidButton1;
    private Controls.LucidButton lucidButton3;
    private Controls.LucidButton lucidButton2;
    private Controls.LucidLabel lbScrollableControl;
    private Controls.LucidButton btnTreeViewProgressAdd;
    private Controls.LucidButton btnTreeViewProgressRemove;
    private Controls.LucidLabel lbSlider;
    private Controls.LucidSlider lucidSlider1;
    private Controls.LucidButton btnToggleSliderMode;
}
