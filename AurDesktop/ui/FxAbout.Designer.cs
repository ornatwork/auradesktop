namespace org.auroracoin.desktop.ui
{
    partial class FxAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FxAbout));
            this.lbBy = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.lbAurDonate = new System.Windows.Forms.Label();
            this.btCopy1 = new System.Windows.Forms.Button();
            this.btCopy2 = new System.Windows.Forms.Button();
            this.lbBtcDonate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbBy
            // 
            this.lbBy.AutoSize = true;
            this.lbBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBy.Location = new System.Drawing.Point(12, 29);
            this.lbBy.Name = "lbBy";
            this.lbBy.Size = new System.Drawing.Size(72, 16);
            this.lbBy.TabIndex = 0;
            this.lbBy.Text = "By: Flipper";
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVersion.Location = new System.Drawing.Point(12, 105);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(60, 16);
            this.lbVersion.TabIndex = 2;
            this.lbVersion.Text = "Version: ";
            // 
            // lbAurDonate
            // 
            this.lbAurDonate.AutoSize = true;
            this.lbAurDonate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAurDonate.Location = new System.Drawing.Point(12, 51);
            this.lbAurDonate.Name = "lbAurDonate";
            this.lbAurDonate.Size = new System.Drawing.Size(37, 16);
            this.lbAurDonate.TabIndex = 3;
            this.lbAurDonate.Text = "AUR";
            // 
            // btCopy1
            // 
            this.btCopy1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCopy1.Location = new System.Drawing.Point(339, 43);
            this.btCopy1.Name = "btCopy1";
            this.btCopy1.Size = new System.Drawing.Size(58, 24);
            this.btCopy1.TabIndex = 4;
            this.btCopy1.Text = "Copy";
            this.btCopy1.UseVisualStyleBackColor = true;
            this.btCopy1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btCopy2
            // 
            this.btCopy2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCopy2.Location = new System.Drawing.Point(338, 67);
            this.btCopy2.Name = "btCopy2";
            this.btCopy2.Size = new System.Drawing.Size(58, 24);
            this.btCopy2.TabIndex = 6;
            this.btCopy2.Text = "Copy";
            this.btCopy2.UseVisualStyleBackColor = true;
            this.btCopy2.Click += new System.EventHandler(this.btCopy2_Click);
            // 
            // lbBtcDonate
            // 
            this.lbBtcDonate.AutoSize = true;
            this.lbBtcDonate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBtcDonate.Location = new System.Drawing.Point(12, 69);
            this.lbBtcDonate.Name = "lbBtcDonate";
            this.lbBtcDonate.Size = new System.Drawing.Size(35, 16);
            this.lbBtcDonate.TabIndex = 5;
            this.lbBtcDonate.Text = "BTC";
            // 
            // FxAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 154);
            this.Controls.Add(this.btCopy2);
            this.Controls.Add(this.lbBtcDonate);
            this.Controls.Add(this.btCopy1);
            this.Controls.Add(this.lbAurDonate);
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.lbBy);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FxAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbBy;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Label lbAurDonate;
        private System.Windows.Forms.Button btCopy1;
        private System.Windows.Forms.Button btCopy2;
        private System.Windows.Forms.Label lbBtcDonate;
    }
}