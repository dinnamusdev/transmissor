﻿namespace Transmissor
{
    partial class frmLog
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtData = new System.Windows.Forms.DateTimePicker();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data";
            // 
            // dtData
            // 
            this.dtData.Location = new System.Drawing.Point(40, 9);
            this.dtData.Name = "dtData";
            this.dtData.Size = new System.Drawing.Size(220, 20);
            this.dtData.TabIndex = 1;
            this.dtData.ValueChanged += new System.EventHandler(this.dtData_ValueChanged);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(2, 35);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(536, 285);
            this.txtLog.TabIndex = 2;
            this.txtLog.Text = "";
            // 
            // frmLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 332);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.dtData);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmLog";
            this.Text = "Log do Atividades";
            this.Load += new System.EventHandler(this.frmLog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtData;
        private System.Windows.Forms.RichTextBox txtLog;
    }
}