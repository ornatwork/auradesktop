namespace org.auroracoin.desktop.ui
{
    partial class FxListOrders
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FxListOrders));
            this.ctxListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuHiLo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTradeOrders = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuBuy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSell = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.lvOrders = new System.Windows.Forms.ListView();
            this.btRefresh = new System.Windows.Forms.Button();
            this.chkRefresh = new System.Windows.Forms.CheckBox();
            this.tmRefresh = new System.Windows.Forms.Timer(this.components);
            this.btEmail = new System.Windows.Forms.Button();
            this.ctxListMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctxListMenu
            // 
            this.ctxListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHiLo,
            this.mnuDetails,
            this.mnuTradeOrders,
            this.toolStripMenuItem2,
            this.mnuBuy,
            this.mnuSell,
            this.toolStripMenuItem1,
            this.mnuCancel});
            this.ctxListMenu.Name = "ctxListMenu";
            this.ctxListMenu.Size = new System.Drawing.Size(230, 148);
            // 
            // mnuHiLo
            // 
            this.mnuHiLo.Name = "mnuHiLo";
            this.mnuHiLo.Size = new System.Drawing.Size(229, 22);
            this.mnuHiLo.Text = "&Chart - Open High Low Close";
            this.mnuHiLo.Click += new System.EventHandler(this.mnuHiLo_Click);
            // 
            // mnuDetails
            // 
            this.mnuDetails.Name = "mnuDetails";
            this.mnuDetails.Size = new System.Drawing.Size(229, 22);
            this.mnuDetails.Text = "&Details";
            this.mnuDetails.Click += new System.EventHandler(this.mnuDetails_Click);
            // 
            // mnuTradeOrders
            // 
            this.mnuTradeOrders.Name = "mnuTradeOrders";
            this.mnuTradeOrders.Size = new System.Drawing.Size(229, 22);
            this.mnuTradeOrders.Text = "&Trade orders";
            this.mnuTradeOrders.Click += new System.EventHandler(this.mnuTradeOrders_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(226, 6);
            // 
            // mnuBuy
            // 
            this.mnuBuy.Name = "mnuBuy";
            this.mnuBuy.Size = new System.Drawing.Size(229, 22);
            this.mnuBuy.Text = "&Buy";
            this.mnuBuy.Click += new System.EventHandler(this.mnuBuy_Click);
            // 
            // mnuSell
            // 
            this.mnuSell.Name = "mnuSell";
            this.mnuSell.Size = new System.Drawing.Size(229, 22);
            this.mnuSell.Text = "&Sell";
            this.mnuSell.Click += new System.EventHandler(this.mnuSell_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(226, 6);
            // 
            // mnuCancel
            // 
            this.mnuCancel.Name = "mnuCancel";
            this.mnuCancel.Size = new System.Drawing.Size(229, 22);
            this.mnuCancel.Text = "&Cancel";
            this.mnuCancel.Click += new System.EventHandler(this.mnuCancel_Click);
            // 
            // lvOrders
            // 
            this.lvOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvOrders.ContextMenuStrip = this.ctxListMenu;
            this.lvOrders.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvOrders.FullRowSelect = true;
            this.lvOrders.Location = new System.Drawing.Point(0, -1);
            this.lvOrders.MultiSelect = false;
            this.lvOrders.Name = "lvOrders";
            this.lvOrders.Size = new System.Drawing.Size(534, 422);
            this.lvOrders.TabIndex = 0;
            this.lvOrders.UseCompatibleStateImageBehavior = false;
            this.lvOrders.View = System.Windows.Forms.View.Details;
            this.lvOrders.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvOrders_ColumnClick);
            // 
            // btRefresh
            // 
            this.btRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btRefresh.Location = new System.Drawing.Point(447, 427);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(87, 23);
            this.btRefresh.TabIndex = 3;
            this.btRefresh.Text = "Refresh";
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // chkRefresh
            // 
            this.chkRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkRefresh.AutoSize = true;
            this.chkRefresh.Location = new System.Drawing.Point(3, 431);
            this.chkRefresh.Name = "chkRefresh";
            this.chkRefresh.Size = new System.Drawing.Size(127, 17);
            this.chkRefresh.TabIndex = 1;
            this.chkRefresh.Text = "Refresh automatically";
            this.chkRefresh.UseVisualStyleBackColor = true;
            this.chkRefresh.CheckedChanged += new System.EventHandler(this.chkRefresh_CheckedChanged);
            // 
            // tmRefresh
            // 
            this.tmRefresh.Tick += new System.EventHandler(this.tmRefresh_Tick);
            // 
            // btEmail
            // 
            this.btEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btEmail.Location = new System.Drawing.Point(358, 427);
            this.btEmail.Name = "btEmail";
            this.btEmail.Size = new System.Drawing.Size(87, 23);
            this.btEmail.TabIndex = 2;
            this.btEmail.Text = "Email";
            this.btEmail.UseVisualStyleBackColor = true;
            this.btEmail.Click += new System.EventHandler(this.btEmail_Click);
            // 
            // FxListOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 454);
            this.Controls.Add(this.btEmail);
            this.Controls.Add(this.chkRefresh);
            this.Controls.Add(this.btRefresh);
            this.Controls.Add(this.lvOrders);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FxListOrders";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trade Orders - ";
            this.Load += new System.EventHandler(this.FxStockDetail_Load);
            this.ctxListMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip ctxListMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuBuy;
        private System.Windows.Forms.ToolStripMenuItem mnuSell;
        private System.Windows.Forms.ListView lvOrders;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuCancel;
        private System.Windows.Forms.ToolStripMenuItem mnuTradeOrders;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuDetails;
        private System.Windows.Forms.ToolStripMenuItem mnuHiLo;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.CheckBox chkRefresh;
        private System.Windows.Forms.Timer tmRefresh;
        private System.Windows.Forms.Button btEmail;

    }
}