namespace org.auroracoin.desktop.ui
{
    partial class FxGameReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FxGameReport));
            this.txContent = new System.Windows.Forms.TextBox();
            this.lbHeading = new System.Windows.Forms.Label();
            this.btEmail = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txContent
            // 
            this.txContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txContent.BackColor = System.Drawing.Color.White;
            this.txContent.Location = new System.Drawing.Point(11, 23);
            this.txContent.Multiline = true;
            this.txContent.Name = "txContent";
            this.txContent.ReadOnly = true;
            this.txContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txContent.Size = new System.Drawing.Size(606, 450);
            this.txContent.TabIndex = 1;
            // 
            // lbHeading
            // 
            this.lbHeading.AutoSize = true;
            this.lbHeading.Location = new System.Drawing.Point(9, 7);
            this.lbHeading.Name = "lbHeading";
            this.lbHeading.Size = new System.Drawing.Size(425, 13);
            this.lbHeading.TabIndex = 0;
            this.lbHeading.Text = "Data to be emailed contains sensitive information about your account activity, se" +
    "e below.";
            // 
            // btEmail
            // 
            this.btEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btEmail.Location = new System.Drawing.Point(530, 479);
            this.btEmail.Name = "btEmail";
            this.btEmail.Size = new System.Drawing.Size(87, 23);
            this.btEmail.TabIndex = 2;
            this.btEmail.Text = "Email";
            this.btEmail.UseVisualStyleBackColor = true;
            this.btEmail.Click += new System.EventHandler(this.btEmail_Click);
            // 
            // FxGameReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 510);
            this.Controls.Add(this.btEmail);
            this.Controls.Add(this.lbHeading);
            this.Controls.Add(this.txContent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FxGameReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Game report";
            this.Load += new System.EventHandler(this.FxGameReport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txContent;
        private System.Windows.Forms.Label lbHeading;
        private System.Windows.Forms.Button btEmail;

    }
}