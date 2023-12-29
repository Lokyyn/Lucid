namespace Lucid.Sample.SideViews;

partial class ToolWindow
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
        lucidTreeView = new Controls.LucidTreeView();
        SuspendLayout();
        // 
        // lucidTreeView
        // 
        lucidTreeView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lucidTreeView.ContextMenu = null;
        lucidTreeView.Location = new Point(0, 24);
        lucidTreeView.MaxDragChange = 20;
        lucidTreeView.Name = "lucidTreeView";
        lucidTreeView.Size = new Size(319, 273);
        lucidTreeView.ShowSelectedNodeRoundedRectangle = true;
        lucidTreeView.TabIndex = 2;
        lucidTreeView.Text = "lucidTreeView1";
        // 
        // ToolWindow
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(lucidTreeView);
        DefaultDockArea = Docking.LucidDockArea.Left;
        DockText = "ToolWindow";
        Name = "ToolWindow";
        Size = new Size(319, 431);
        Controls.SetChildIndex(lucidTreeView, 0);
        ResumeLayout(false);
    }

    #endregion

    private Controls.LucidTreeView lucidTreeView;
}
