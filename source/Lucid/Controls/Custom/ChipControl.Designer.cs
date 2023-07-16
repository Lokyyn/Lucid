﻿namespace Lucid.Controls.Custom
{
    partial class ChipControl
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
            this.VScrollBar = new Lucid.Controls.DarkScrollBar();
            this.SuspendLayout();
            // 
            // VScrollBar
            // 
            this.VScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.VScrollBar.Location = new System.Drawing.Point(223, 0);
            this.VScrollBar.Name = "VScrollBar";
            this.VScrollBar.Size = new System.Drawing.Size(15, 187);
            this.VScrollBar.TabIndex = 0;
            this.VScrollBar.Text = "darkScrollBar1";
            // 
            // ChipControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.Controls.Add(this.VScrollBar);
            this.Name = "ChipControl";
            this.Size = new System.Drawing.Size(256, 178);
            this.ResumeLayout(false);

        }

        #endregion

        private DarkScrollBar VScrollBar;
    }
}
