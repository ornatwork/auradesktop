namespace org.auroracoin.desktop.ui
{
    partial class FxConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FxConfig));
            this.btSave = new System.Windows.Forms.Button();
            this.chkSound = new System.Windows.Forms.CheckBox();
            this.txOsUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txOsPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txBuyOverWarn = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txBuyUnderWarn = new System.Windows.Forms.TextBox();
            this.btConfirmOsLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(77, 171);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(87, 23);
            this.btSave.TabIndex = 7;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // chkSound
            // 
            this.chkSound.AutoSize = true;
            this.chkSound.Location = new System.Drawing.Point(13, 141);
            this.chkSound.Name = "chkSound";
            this.chkSound.Size = new System.Drawing.Size(151, 17);
            this.chkSound.TabIndex = 6;
            this.chkSound.Text = "Do not play sound on Alert";
            this.chkSound.UseVisualStyleBackColor = true;
            // 
            // txOsUserName
            // 
            this.txOsUserName.Location = new System.Drawing.Point(14, 27);
            this.txOsUserName.Name = "txOsUserName";
            this.txOsUserName.Size = new System.Drawing.Size(152, 20);
            this.txOsUserName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Email";
            // 
            // txOsPassword
            // 
            this.txOsPassword.Location = new System.Drawing.Point(13, 65);
            this.txOsPassword.Name = "txOsPassword";
            this.txOsPassword.PasswordChar = '*';
            this.txOsPassword.Size = new System.Drawing.Size(152, 20);
            this.txOsPassword.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Password";
            // 
            // txBuyOverWarn
            // 
            this.txBuyOverWarn.Location = new System.Drawing.Point(50, 111);
            this.txBuyOverWarn.Name = "txBuyOverWarn";
            this.txBuyOverWarn.Size = new System.Drawing.Size(47, 20);
            this.txBuyOverWarn.TabIndex = 5;
            this.txBuyOverWarn.Text = "0.9";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Warn if last executed orders are";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Over";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(106, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Under";
            // 
            // txBuyUnderWarn
            // 
            this.txBuyUnderWarn.Location = new System.Drawing.Point(148, 111);
            this.txBuyUnderWarn.Name = "txBuyUnderWarn";
            this.txBuyUnderWarn.Size = new System.Drawing.Size(47, 20);
            this.txBuyUnderWarn.TabIndex = 14;
            this.txBuyUnderWarn.Text = "0.01";
            // 
            // btConfirmOsLogin
            // 
            this.btConfirmOsLogin.Location = new System.Drawing.Point(171, 64);
            this.btConfirmOsLogin.Name = "btConfirmOsLogin";
            this.btConfirmOsLogin.Size = new System.Drawing.Size(64, 21);
            this.btConfirmOsLogin.TabIndex = 4;
            this.btConfirmOsLogin.Text = "Confirm";
            this.btConfirmOsLogin.UseVisualStyleBackColor = true;
            this.btConfirmOsLogin.Click += new System.EventHandler(this.btConfirmOsLogin_Click);
            // 
            // FxConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 206);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txBuyUnderWarn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txBuyOverWarn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btConfirmOsLogin);
            this.Controls.Add(this.txOsPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txOsUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkSound);
            this.Controls.Add(this.btSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FxConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.CheckBox chkSound;
        private System.Windows.Forms.TextBox txOsUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txOsPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txBuyOverWarn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txBuyUnderWarn;
        private System.Windows.Forms.Button btConfirmOsLogin;
    }
}