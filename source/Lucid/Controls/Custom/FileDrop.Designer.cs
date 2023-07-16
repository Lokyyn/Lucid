namespace Lucid.Controls.Custom
{
    partial class FileDrop
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
            this.labelDisplayText = new Lucid.Controls.DarkLabel();
            this.SuspendLayout();
            // 
            // labelDisplayText
            // 
            this.labelDisplayText.BackColor = System.Drawing.Color.Transparent;
            this.labelDisplayText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDisplayText.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelDisplayText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.labelDisplayText.Location = new System.Drawing.Point(0, 0);
            this.labelDisplayText.Name = "labelDisplayText";
            this.labelDisplayText.Size = new System.Drawing.Size(394, 311);
            this.labelDisplayText.TabIndex = 0;
            this.labelDisplayText.Text = "labelDisplayText";
            this.labelDisplayText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FileDrop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.Controls.Add(this.labelDisplayText);
            this.Name = "FileDrop";
            this.AllowDrop = true;
            this.Size = new System.Drawing.Size(394, 311);
            this.ResumeLayout(false);

        }

        #endregion

        private DarkLabel labelDisplayText;
    }
}
