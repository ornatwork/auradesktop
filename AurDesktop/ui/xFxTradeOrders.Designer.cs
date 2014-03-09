namespace org.auroracoin.desktop.ui
{
    partial class FxTradeOrders
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FxTradeOrders));
            this.tmOrders = new System.Windows.Forms.Timer(this.components);
            this.ctxListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuHiLo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuBuy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSell = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlExecuted = new System.Windows.Forms.Panel();
            this.chExcutedOrders = new ZedGraph.ZedGraphControl();
            this.rdGrid = new System.Windows.Forms.RadioButton();
            this.rdChart = new System.Windows.Forms.RadioButton();
            this.lvExecuted = new System.Windows.Forms.ListView();
            this.label6 = new System.Windows.Forms.Label();
            this.ctxListMenu.SuspendLayout();
            this.pnlExecuted.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmOrders
            // 
            this.tmOrders.Tick += new System.EventHandler(this.tmOrders_Tick);
            // 
            // ctxListMenu
            // 
            this.ctxListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHiLo,
            this.mnuDetails,
            this.toolStripMenuItem1,
            this.mnuBuy,
            this.mnuSell});
            this.ctxListMenu.Name = "ctxListMenu";
            this.ctxListMenu.Size = new System.Drawing.Size(230, 98);
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
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(226, 6);
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
            // pnlExecuted
            // 
            this.pnlExecuted.Controls.Add(this.rdGrid);
            this.pnlExecuted.Controls.Add(this.rdChart);
            this.pnlExecuted.Controls.Add(this.label6);
            this.pnlExecuted.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlExecuted.Location = new System.Drawing.Point(0, 0);
            this.pnlExecuted.Name = "pnlExecuted";
            this.pnlExecuted.Size = new System.Drawing.Size(690, 27);
            this.pnlExecuted.TabIndex = 8;
            // 
            // chExcutedOrders
            // 
            this.chExcutedOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chExcutedOrders.AutoSize = true;
            this.chExcutedOrders.Location = new System.Drawing.Point(0, 27);
            this.chExcutedOrders.Name = "chExcutedOrders";
            this.chExcutedOrders.ScrollGrace = 0D;
            this.chExcutedOrders.ScrollMaxX = 0D;
            this.chExcutedOrders.ScrollMaxY = 0D;
            this.chExcutedOrders.ScrollMaxY2 = 0D;
            this.chExcutedOrders.ScrollMinX = 0D;
            this.chExcutedOrders.ScrollMinY = 0D;
            this.chExcutedOrders.ScrollMinY2 = 0D;
            this.chExcutedOrders.Size = new System.Drawing.Size(690, 395);
            this.chExcutedOrders.TabIndex = 12;
            // 
            // rdGrid
            // 
            this.rdGrid.AutoSize = true;
            this.rdGrid.Location = new System.Drawing.Point(159, 4);
            this.rdGrid.Name = "rdGrid";
            this.rdGrid.Size = new System.Drawing.Size(44, 17);
            this.rdGrid.TabIndex = 14;
            this.rdGrid.Text = "Grid";
            this.rdGrid.UseVisualStyleBackColor = true;
            // 
            // rdChart
            // 
            this.rdChart.AutoSize = true;
            this.rdChart.Checked = true;
            this.rdChart.Location = new System.Drawing.Point(103, 4);
            this.rdChart.Name = "rdChart";
            this.rdChart.Size = new System.Drawing.Size(50, 17);
            this.rdChart.TabIndex = 13;
            this.rdChart.TabStop = true;
            this.rdChart.Text = "Chart";
            this.rdChart.UseVisualStyleBackColor = true;
            this.rdChart.CheckedChanged += new System.EventHandler(this.rdChart_CheckedChanged);
            // 
            // lvExecuted
            // 
            this.lvExecuted.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvExecuted.ContextMenuStrip = this.ctxListMenu;
            this.lvExecuted.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvExecuted.FullRowSelect = true;
            this.lvExecuted.Location = new System.Drawing.Point(2, 27);
            this.lvExecuted.MultiSelect = false;
            this.lvExecuted.Name = "lvExecuted";
            this.lvExecuted.Size = new System.Drawing.Size(690, 395);
            this.lvExecuted.TabIndex = 11;
            this.lvExecuted.UseCompatibleStateImageBehavior = false;
            this.lvExecuted.View = System.Windows.Forms.View.Details;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Executed orders";
            // 
            // FxTradeOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 419);
            this.Controls.Add(this.chExcutedOrders);
            this.Controls.Add(this.lvExecuted);
            this.Controls.Add(this.pnlExecuted);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FxTradeOrders";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trade Orders - ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FxTradeOrders_FormClosing);
            this.Load += new System.EventHandler(this.FxTradeOrders_Load);
            this.ctxListMenu.ResumeLayout(false);
            this.pnlExecuted.ResumeLayout(false);
            this.pnlExecuted.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmOrders;
        private System.Windows.Forms.ContextMenuStrip ctxListMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuBuy;
        private System.Windows.Forms.ToolStripMenuItem mnuSell;
        private System.Windows.Forms.ToolStripMenuItem mnuDetails;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuHiLo;
        private System.Windows.Forms.Panel pnlExecuted;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView lvExecuted;
        private ZedGraph.ZedGraphControl chExcutedOrders;
        private System.Windows.Forms.RadioButton rdGrid;
        private System.Windows.Forms.RadioButton rdChart;

    }
}