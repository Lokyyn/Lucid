namespace Lucid.Sample;

partial class LucidSampleForm
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        lucidDockPanel = new Docking.LucidDockPanel();
        lucidMenuStrip1 = new Controls.LucidMenuStrip();
        fileToolStripMenuItem = new ToolStripMenuItem();
        windowsToolStripMenuItem = new ToolStripMenuItem();
        pagesToolStripMenuItem = new ToolStripMenuItem();
        themesToolStripMenuItem = new ToolStripMenuItem();
        switchToDarkToolStripMenuItem = new ToolStripMenuItem();
        switchToLightToolStripMenuItem = new ToolStripMenuItem();
        lucidMenuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // lucidDockPanel
        // 
        lucidDockPanel.BackColor = Color.FromArgb(60, 63, 65);
        lucidDockPanel.Dock = DockStyle.Fill;
        lucidDockPanel.Location = new Point(0, 24);
        lucidDockPanel.Name = "lucidDockPanel";
        lucidDockPanel.Size = new Size(1078, 621);
        lucidDockPanel.TabIndex = 0;
        // 
        // lucidMenuStrip1
        // 
        lucidMenuStrip1.BackColor = Color.FromArgb(60, 63, 65);
        lucidMenuStrip1.ForeColor = Color.FromArgb(220, 220, 220);
        lucidMenuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, themesToolStripMenuItem, windowsToolStripMenuItem, pagesToolStripMenuItem });
        lucidMenuStrip1.Location = new Point(0, 0);
        lucidMenuStrip1.Name = "lucidMenuStrip1";
        lucidMenuStrip1.Padding = new Padding(3, 2, 0, 2);
        lucidMenuStrip1.Size = new Size(1078, 24);
        lucidMenuStrip1.TabIndex = 1;
        lucidMenuStrip1.Text = "lucidMenuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        fileToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
        fileToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
        fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        fileToolStripMenuItem.Size = new Size(37, 20);
        fileToolStripMenuItem.Text = "File";
        // 
        // windowsToolStripMenuItem
        // 
        windowsToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
        windowsToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
        windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
        windowsToolStripMenuItem.Size = new Size(71, 20);
        windowsToolStripMenuItem.Text = "SideViews";
        // 
        // pagesToolStripMenuItem
        // 
        pagesToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
        pagesToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
        pagesToolStripMenuItem.Name = "pagesToolStripMenuItem";
        pagesToolStripMenuItem.Size = new Size(50, 20);
        pagesToolStripMenuItem.Text = "Pages";
        // 
        // themesToolStripMenuItem
        // 
        themesToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
        themesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { switchToDarkToolStripMenuItem, switchToLightToolStripMenuItem });
        themesToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
        themesToolStripMenuItem.Name = "themesToolStripMenuItem";
        themesToolStripMenuItem.Size = new Size(60, 20);
        themesToolStripMenuItem.Text = "Themes";
        // 
        // switchToDarkToolStripMenuItem
        // 
        switchToDarkToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
        switchToDarkToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
        switchToDarkToolStripMenuItem.Name = "switchToDarkToolStripMenuItem";
        switchToDarkToolStripMenuItem.Size = new Size(180, 22);
        switchToDarkToolStripMenuItem.Text = "Switch to Dark";
        switchToDarkToolStripMenuItem.Click += switchToDarkToolStripMenuItem_Click;
        // 
        // switchToLightToolStripMenuItem
        // 
        switchToLightToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
        switchToLightToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
        switchToLightToolStripMenuItem.Name = "switchToLightToolStripMenuItem";
        switchToLightToolStripMenuItem.Size = new Size(180, 22);
        switchToLightToolStripMenuItem.Text = "Switch to Light";
        switchToLightToolStripMenuItem.Click += switchToLightToolStripMenuItem_Click;
        // 
        // LucidSampleForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1078, 645);
        Controls.Add(lucidDockPanel);
        Controls.Add(lucidMenuStrip1);
        MainMenuStrip = lucidMenuStrip1;
        Name = "LucidSampleForm";
        Text = "LucidSampleForm";
        lucidMenuStrip1.ResumeLayout(false);
        lucidMenuStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Docking.LucidDockPanel lucidDockPanel;
    private Controls.LucidMenuStrip lucidMenuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem windowsToolStripMenuItem;
    private ToolStripMenuItem pagesToolStripMenuItem;
    private ToolStripMenuItem themesToolStripMenuItem;
    private ToolStripMenuItem switchToDarkToolStripMenuItem;
    private ToolStripMenuItem switchToLightToolStripMenuItem;
}