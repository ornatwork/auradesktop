//
using org.auroracoin.desktop.core;

namespace org.auroracoin.desktop.ui
{
    partial class FxBuySell
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FxBuySell));
            this.btSubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txShares = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbStocks = new System.Windows.Forms.ComboBox();
            this.txPrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbAction = new System.Windows.Forms.ComboBox();
            this.btCLose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btSubmit
            // 
            this.btSubmit.Location = new System.Drawing.Point(101, 109);
            this.btSubmit.Name = "btSubmit";
            this.btSubmit.Size = new System.Drawing.Size(87, 23);
            this.btSubmit.TabIndex = 7;
            this.btSubmit.Text = "Submit order";
            this.btSubmit.UseVisualStyleBackColor = true;
            this.btSubmit.Click += new System.EventHandler(this.btSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Action";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Stock";
            // 
            // txShares
            // 
            this.txShares.Location = new System.Drawing.Point(5, 74);
            this.txShares.MaxLength = 6;
            this.txShares.Name = "txShares";
            this.txShares.Size = new System.Drawing.Size(67, 20);
            this.txShares.TabIndex = 2;
            this.txShares.Text = "1";
            this.txShares.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sharesKeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Number of shares";
            // 
            // cmbStocks
            // 
            this.cmbStocks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStocks.FormattingEnabled = true;
            this.cmbStocks.Location = new System.Drawing.Point(103, 26);
            this.cmbStocks.Name = "cmbStocks";
            this.cmbStocks.Size = new System.Drawing.Size(182, 21);
            this.cmbStocks.TabIndex = 1;
            // 
            // txPrice
            // 
            this.txPrice.Location = new System.Drawing.Point(101, 74);
            this.txPrice.MaxLength = 6;
            this.txPrice.Name = "txPrice";
            this.txPrice.Size = new System.Drawing.Size(67, 20);
            this.txPrice.TabIndex = 3;
            this.txPrice.Text = "0";
            this.txPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.priceKeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(99, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Price";
            // 
            // cmbAction
            // 
            this.cmbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAction.FormattingEnabled = true;
            this.cmbAction.Location = new System.Drawing.Point(6, 26);
            this.cmbAction.Name = "cmbAction";
            this.cmbAction.Size = new System.Drawing.Size(66, 21);
            this.cmbAction.TabIndex = 0;
            // 
            // btCLose
            // 
            this.btCLose.Location = new System.Drawing.Point(198, 109);
            this.btCLose.Name = "btCLose";
            this.btCLose.Size = new System.Drawing.Size(87, 23);
            this.btCLose.TabIndex = 8;
            this.btCLose.Text = "Cancel";
            this.btCLose.UseVisualStyleBackColor = true;
            this.btCLose.Click += new System.EventHandler(this.btCLose_Click);
            // 
            // FxBuySell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 141);
            this.Controls.Add(this.btCLose);
            this.Controls.Add(this.cmbAction);
            this.Controls.Add(this.txPrice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbStocks);
            this.Controls.Add(this.txShares);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btSubmit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FxBuySell";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Buy or Sell Stock";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FxBuySell_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSubmit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txShares;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbStocks;
        private System.Windows.Forms.TextBox txPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbAction;
        private System.Windows.Forms.Button btCLose;
    }
}