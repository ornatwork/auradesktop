//
using org.auroracoin.desktop.core;

namespace org.auroracoin.desktop.ui
{
    partial class FxInput
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
            CxGlobal.REFRESH_ON = true;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FxInput));
            this.btOk = new System.Windows.Forms.Button();
            this.txValue = new System.Windows.Forms.TextBox();
            this.lbInput = new System.Windows.Forms.Label();
            this.btCLose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(13, 53);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(87, 23);
            this.btOk.TabIndex = 7;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // txValue
            // 
            this.txValue.Location = new System.Drawing.Point(12, 27);
            this.txValue.MaxLength = 256;
            this.txValue.Name = "txValue";
            this.txValue.Size = new System.Drawing.Size(185, 20);
            this.txValue.TabIndex = 2;
            // 
            // lbInput
            // 
            this.lbInput.AutoSize = true;
            this.lbInput.Location = new System.Drawing.Point(10, 11);
            this.lbInput.Name = "lbInput";
            this.lbInput.Size = new System.Drawing.Size(34, 13);
            this.lbInput.TabIndex = 9;
            this.lbInput.Text = "Value";
            // 
            // btCLose
            // 
            this.btCLose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCLose.Location = new System.Drawing.Point(110, 53);
            this.btCLose.Name = "btCLose";
            this.btCLose.Size = new System.Drawing.Size(87, 23);
            this.btCLose.TabIndex = 8;
            this.btCLose.Text = "Cancel";
            this.btCLose.UseVisualStyleBackColor = true;
            this.btCLose.Click += new System.EventHandler(this.btCLose_Click);
            // 
            // FxInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 83);
            this.Controls.Add(this.btCLose);
            this.Controls.Add(this.txValue);
            this.Controls.Add(this.lbInput);
            this.Controls.Add(this.btOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FxInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.TextBox txValue;
        private System.Windows.Forms.Label lbInput;
        private System.Windows.Forms.Button btCLose;
    }
}