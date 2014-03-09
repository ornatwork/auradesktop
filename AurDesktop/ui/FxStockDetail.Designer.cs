namespace org.auroracoin.desktop.ui
{
    partial class FxStockDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FxStockDetail));
            this.txDetail = new System.Windows.Forms.TextBox();
            this.pcStock = new System.Windows.Forms.PictureBox();
            this.lbName = new System.Windows.Forms.Label();
            this.lbSport = new System.Windows.Forms.Label();
            this.lbIpoDate = new System.Windows.Forms.Label();
            this.lbDebut = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pcStock)).BeginInit();
            this.SuspendLayout();
            // 
            // txDetail
            // 
            this.txDetail.BackColor = System.Drawing.SystemColors.Window;
            this.txDetail.Location = new System.Drawing.Point(12, 104);
            this.txDetail.Multiline = true;
            this.txDetail.Name = "txDetail";
            this.txDetail.ReadOnly = true;
            this.txDetail.Size = new System.Drawing.Size(268, 200);
            this.txDetail.TabIndex = 0;
            // 
            // pcStock
            // 
            this.pcStock.Location = new System.Drawing.Point(12, 12);
            this.pcStock.Name = "pcStock";
            this.pcStock.Size = new System.Drawing.Size(85, 85);
            this.pcStock.TabIndex = 1;
            this.pcStock.TabStop = false;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(103, 12);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(43, 13);
            this.lbName.TabIndex = 2;
            this.lbName.Text = "lbName";
            // 
            // lbSport
            // 
            this.lbSport.AutoSize = true;
            this.lbSport.Location = new System.Drawing.Point(103, 31);
            this.lbSport.Name = "lbSport";
            this.lbSport.Size = new System.Drawing.Size(40, 13);
            this.lbSport.TabIndex = 3;
            this.lbSport.Text = "lbSport";
            // 
            // lbIpoDate
            // 
            this.lbIpoDate.AutoSize = true;
            this.lbIpoDate.Location = new System.Drawing.Point(103, 69);
            this.lbIpoDate.Name = "lbIpoDate";
            this.lbIpoDate.Size = new System.Drawing.Size(53, 13);
            this.lbIpoDate.TabIndex = 4;
            this.lbIpoDate.Text = "lbIpoDate";
            // 
            // lbDebut
            // 
            this.lbDebut.AutoSize = true;
            this.lbDebut.Location = new System.Drawing.Point(103, 50);
            this.lbDebut.Name = "lbDebut";
            this.lbDebut.Size = new System.Drawing.Size(46, 13);
            this.lbDebut.TabIndex = 5;
            this.lbDebut.Text = "lblDebut";
            // 
            // FxStockDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 312);
            this.Controls.Add(this.lbDebut);
            this.Controls.Add(this.lbIpoDate);
            this.Controls.Add(this.lbSport);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.pcStock);
            this.Controls.Add(this.txDetail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FxStockDetail";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stock detail -";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FxStockDetail_FormClosing);
            this.Load += new System.EventHandler(this.FxStockDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcStock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txDetail;
        private System.Windows.Forms.PictureBox pcStock;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbSport;
        private System.Windows.Forms.Label lbIpoDate;
        private System.Windows.Forms.Label lbDebut;
    }
}