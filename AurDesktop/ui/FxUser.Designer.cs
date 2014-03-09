//
using org.auroracoin.desktop.core;

namespace org.auroracoin.desktop.ui
{
    partial class FxUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FxUser));
            this.btSave = new System.Windows.Forms.Button();
            this.txOsPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txOsEmail = new System.Windows.Forms.TextBox();
            this.btCLose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbOsEmail = new System.Windows.Forms.ComboBox();
            this.btAdd = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(97, 109);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(87, 23);
            this.btSave.TabIndex = 7;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // txOsPassword
            // 
            this.txOsPassword.Location = new System.Drawing.Point(5, 74);
            this.txOsPassword.MaxLength = 6;
            this.txOsPassword.Name = "txOsPassword";
            this.txOsPassword.Size = new System.Drawing.Size(183, 20);
            this.txOsPassword.TabIndex = 2;
            this.txOsPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sharesKeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = " password";
            // 
            // txOsEmail
            // 
            this.txOsEmail.Location = new System.Drawing.Point(6, 35);
            this.txOsEmail.MaxLength = 6;
            this.txOsEmail.Name = "txOsEmail";
            this.txOsEmail.Size = new System.Drawing.Size(182, 20);
            this.txOsEmail.TabIndex = 3;
            this.txOsEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.priceKeyPress);
            // 
            // btCLose
            // 
            this.btCLose.Location = new System.Drawing.Point(194, 109);
            this.btCLose.Name = "btCLose";
            this.btCLose.Size = new System.Drawing.Size(87, 23);
            this.btCLose.TabIndex = 8;
            this.btCLose.Text = "Cancel";
            this.btCLose.UseVisualStyleBackColor = true;
            this.btCLose.Click += new System.EventHandler(this.btCLose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = " email";
            // 
            // cmbOsEmail
            // 
            this.cmbOsEmail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOsEmail.FormattingEnabled = true;
            this.cmbOsEmail.Location = new System.Drawing.Point(6, 23);
            this.cmbOsEmail.Name = "cmbOsEmail";
            this.cmbOsEmail.Size = new System.Drawing.Size(182, 21);
            this.cmbOsEmail.TabIndex = 1;
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(8, 109);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(87, 23);
            this.btAdd.TabIndex = 10;
            this.btAdd.Text = "Add";
            this.btAdd.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(194, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Confirm";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // FxUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 141);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.btCLose);
            this.Controls.Add(this.txOsEmail);
            this.Controls.Add(this.cmbOsEmail);
            this.Controls.Add(this.txOsPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FxUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FxBuySell_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.TextBox txOsPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txOsEmail;
        private System.Windows.Forms.Button btCLose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbOsEmail;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button button1;
    }
}