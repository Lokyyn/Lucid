﻿using Lucid.Controls;

namespace Lucid.Forms
{
    partial class LucidDialog
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
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.flowInner = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOk = new LucidButton();
            this.btnCancel = new LucidButton();
            this.btnClose = new LucidButton();
            this.btnYes = new LucidButton();
            this.btnNo = new LucidButton();
            this.btnAbort = new LucidButton();
            this.btnRetry = new LucidButton();
            this.btnIgnore = new LucidButton();
            this.pnlFooter.SuspendLayout();
            this.flowInner.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.flowInner);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 357);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(767, 45);
            this.pnlFooter.TabIndex = 1;
            // 
            // flowInner
            // 
            this.flowInner.Controls.Add(this.btnOk);
            this.flowInner.Controls.Add(this.btnCancel);
            this.flowInner.Controls.Add(this.btnClose);
            this.flowInner.Controls.Add(this.btnYes);
            this.flowInner.Controls.Add(this.btnNo);
            this.flowInner.Controls.Add(this.btnAbort);
            this.flowInner.Controls.Add(this.btnRetry);
            this.flowInner.Controls.Add(this.btnIgnore);
            this.flowInner.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowInner.Location = new System.Drawing.Point(104, 0);
            this.flowInner.Name = "flowInner";
            this.flowInner.Padding = new System.Windows.Forms.Padding(10);
            this.flowInner.Size = new System.Drawing.Size(663, 45);
            this.flowInner.TabIndex = 10014;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(10, 10);
            this.btnOk.Margin = new System.Windows.Forms.Padding(0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Padding = new System.Windows.Forms.Padding(5);
            this.btnOk.Size = new System.Drawing.Size(75, 26);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Ok";
            this.btnOk.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
            this.btnOk.RoundedCornerRadius = 16;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(85, 10);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 26);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
            this.btnCancel.RoundedCornerRadius = 16;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(160, 10);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(5);
            this.btnClose.Size = new System.Drawing.Size(75, 26);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
            this.btnClose.RoundedCornerRadius = 16;
            // 
            // btnYes
            // 
            this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnYes.Location = new System.Drawing.Point(235, 10);
            this.btnYes.Margin = new System.Windows.Forms.Padding(0);
            this.btnYes.Name = "btnYes";
            this.btnYes.Padding = new System.Windows.Forms.Padding(5);
            this.btnYes.Size = new System.Drawing.Size(75, 26);
            this.btnYes.TabIndex = 6;
            this.btnYes.Text = "Yes";
            this.btnYes.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
            this.btnYes.RoundedCornerRadius = 16;
            // 
            // btnNo
            // 
            this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnNo.Location = new System.Drawing.Point(310, 10);
            this.btnNo.Margin = new System.Windows.Forms.Padding(0);
            this.btnNo.Name = "btnNo";
            this.btnNo.Padding = new System.Windows.Forms.Padding(5);
            this.btnNo.Size = new System.Drawing.Size(75, 26);
            this.btnNo.TabIndex = 7;
            this.btnNo.Text = "No";
            this.btnNo.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
            this.btnNo.RoundedCornerRadius = 16;
            // 
            // btnAbort
            // 
            this.btnAbort.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnAbort.Location = new System.Drawing.Point(385, 10);
            this.btnAbort.Margin = new System.Windows.Forms.Padding(0);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Padding = new System.Windows.Forms.Padding(5);
            this.btnAbort.Size = new System.Drawing.Size(75, 26);
            this.btnAbort.TabIndex = 8;
            this.btnAbort.Text = "Abort";
            this.btnAbort.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
            this.btnAbort.RoundedCornerRadius = 16;
            // 
            // btnRetry
            // 
            this.btnRetry.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.btnRetry.Location = new System.Drawing.Point(460, 10);
            this.btnRetry.Margin = new System.Windows.Forms.Padding(0);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Padding = new System.Windows.Forms.Padding(5);
            this.btnRetry.Size = new System.Drawing.Size(75, 26);
            this.btnRetry.TabIndex = 9;
            this.btnRetry.Text = "Retry";
            this.btnRetry.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
            this.btnRetry.RoundedCornerRadius = 16;
            // 
            // btnIgnore
            // 
            this.btnIgnore.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.btnIgnore.Location = new System.Drawing.Point(535, 10);
            this.btnIgnore.Margin = new System.Windows.Forms.Padding(0);
            this.btnIgnore.Name = "btnIgnore";
            this.btnIgnore.Padding = new System.Windows.Forms.Padding(5);
            this.btnIgnore.Size = new System.Drawing.Size(75, 26);
            this.btnIgnore.TabIndex = 10;
            this.btnIgnore.Text = "Ignore";
            this.btnIgnore.ButtonStyle = Lucid.Controls.LucidButtonStyle.Rounded;
            this.btnIgnore.RoundedCornerRadius = 16;
            // 
            // LucidDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 402);
            this.Controls.Add(this.pnlFooter);
            this.Name = "LucidDialog";
            this.Text = "LucidDialog";
            this.pnlFooter.ResumeLayout(false);
            this.flowInner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.FlowLayoutPanel flowInner;
    }
}