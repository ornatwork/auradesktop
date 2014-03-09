//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace org.au.desktop.ui
{
    partial class FxMain : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FxMain));
            this.lbValue = new System.Windows.Forms.Label();
            this.lbValueText = new System.Windows.Forms.Label();
            this.lbChange = new System.Windows.Forms.Label();
            this.lbChangeText = new System.Windows.Forms.Label();
            this.lbVolume = new System.Windows.Forms.Label();
            this.lbVolumeText = new System.Windows.Forms.Label();
            this.tmLoop = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOther = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMyOrders = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExecutedOrders = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOsLatestExecuted = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBuySell = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGameReport = new System.Windows.Forms.ToolStripMenuItem();
            this.someToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTutorial = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tpPanel = new System.Windows.Forms.TabControl();
            this.tbMarket = new System.Windows.Forms.TabPage();
            this.lvStocks = new System.Windows.Forms.ListView();
            this.ctxListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuHiLo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStockTrading = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuBuy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSell = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.tbPortfolio = new System.Windows.Forms.TabPage();
            this.txEditBox = new System.Windows.Forms.TextBox();
            this.cmbStocks = new System.Windows.Forms.ComboBox();
            this.lvPortfolio = new System.Windows.Forms.ListView();
            this.tbGraph = new System.Windows.Forms.TabPage();
            this.chStocks = new ZedGraph.ZedGraphControl();
            this.tbIpo = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btRefresh = new System.Windows.Forms.Button();
            this.lbPlayer = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btSubmitIpoRequest = new System.Windows.Forms.Button();
            this.txIpoQuestion = new System.Windows.Forms.TextBox();
            this.txIpoAnswer = new System.Windows.Forms.TextBox();
            this.txShares = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lstIpos = new System.Windows.Forms.ListBox();
            this.btStop = new System.Windows.Forms.Button();
            this.lbIpoMsg = new System.Windows.Forms.Label();
            this.nIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cxtMenu = new System.Windows.Forms.ContextMenu();
            this.mnuShowApp = new System.Windows.Forms.MenuItem();
            this.mnuExitApp = new System.Windows.Forms.MenuItem();
            this.tmCheckForIPO = new System.Windows.Forms.Timer(this.components);
            this.lblYesterdayValue = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tmPlayIpoSound = new System.Windows.Forms.Timer(this.components);
            this.lblCtrlKey = new System.Windows.Forms.Label();
            this.cmbGraphStocks = new System.Windows.Forms.ComboBox();
            this.lbCash = new System.Windows.Forms.Label();
            this.lbCashText = new System.Windows.Forms.Label();
            this.lbReserveCashText = new System.Windows.Forms.Label();
            this.lbReserveCash = new System.Windows.Forms.Label();
            this.tbTwitter = new System.Windows.Forms.TabPage();
            this.wbRender = new System.Windows.Forms.WebBrowser();
            this.menuStrip1.SuspendLayout();
            this.tpPanel.SuspendLayout();
            this.tbMarket.SuspendLayout();
            this.ctxListMenu.SuspendLayout();
            this.tbPortfolio.SuspendLayout();
            this.tbGraph.SuspendLayout();
            this.tbIpo.SuspendLayout();
            this.tbTwitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbValue
            // 
            this.lbValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbValue.AutoSize = true;
            this.lbValue.Location = new System.Drawing.Point(154, 412);
            this.lbValue.Name = "lbValue";
            this.lbValue.Size = new System.Drawing.Size(41, 13);
            this.lbValue.TabIndex = 2;
            this.lbValue.Text = "Current";
            // 
            // lbValueText
            // 
            this.lbValueText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbValueText.AutoSize = true;
            this.lbValueText.Location = new System.Drawing.Point(195, 412);
            this.lbValueText.Name = "lbValueText";
            this.lbValueText.Size = new System.Drawing.Size(16, 13);
            this.lbValueText.TabIndex = 3;
            this.lbValueText.Text = "---";
            // 
            // lbChange
            // 
            this.lbChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbChange.AutoSize = true;
            this.lbChange.Location = new System.Drawing.Point(295, 412);
            this.lbChange.Name = "lbChange";
            this.lbChange.Size = new System.Drawing.Size(44, 13);
            this.lbChange.TabIndex = 4;
            this.lbChange.Text = "Change";
            // 
            // lbChangeText
            // 
            this.lbChangeText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbChangeText.AutoSize = true;
            this.lbChangeText.Location = new System.Drawing.Point(341, 412);
            this.lbChangeText.Name = "lbChangeText";
            this.lbChangeText.Size = new System.Drawing.Size(16, 13);
            this.lbChangeText.TabIndex = 5;
            this.lbChangeText.Text = "---";
            // 
            // lbVolume
            // 
            this.lbVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbVolume.AutoSize = true;
            this.lbVolume.Location = new System.Drawing.Point(399, 412);
            this.lbVolume.Name = "lbVolume";
            this.lbVolume.Size = new System.Drawing.Size(42, 13);
            this.lbVolume.TabIndex = 6;
            this.lbVolume.Text = "Volume";
            // 
            // lbVolumeText
            // 
            this.lbVolumeText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbVolumeText.AutoSize = true;
            this.lbVolumeText.Location = new System.Drawing.Point(443, 412);
            this.lbVolumeText.Name = "lbVolumeText";
            this.lbVolumeText.Size = new System.Drawing.Size(16, 13);
            this.lbVolumeText.TabIndex = 7;
            this.lbVolumeText.Text = "---";
            // 
            // tmLoop
            // 
            this.tmLoop.Enabled = true;
            this.tmLoop.Interval = 3000;
            this.tmLoop.Tick += new System.EventHandler(this.tmLoop_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.mnuOther,
            this.someToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(873, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuConfig,
            this.mnuExit});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "&File";
            // 
            // mnuConfig
            // 
            this.mnuConfig.Name = "mnuConfig";
            this.mnuConfig.Size = new System.Drawing.Size(148, 22);
            this.mnuConfig.Text = "&Configuration";
            this.mnuConfig.Click += new System.EventHandler(this.mnuConfig_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(148, 22);
            this.mnuExit.Text = "&Exit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuOther
            // 
            this.mnuOther.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMyOrders,
            this.mnuExecutedOrders,
            this.toolStripMenuItem3,
            this.mnuOsLatestExecuted,
            this.mnuBuySell,
            this.toolStripMenuItem4,
            this.mnuMc,
            this.toolStripMenuItem5,
            this.mnuGameReport});
            this.mnuOther.Name = "mnuOther";
            this.mnuOther.Size = new System.Drawing.Size(49, 20);
            this.mnuOther.Text = "&Other";
            // 
            // mnuMyOrders
            // 
            this.mnuMyOrders.Name = "mnuMyOrders";
            this.mnuMyOrders.Size = new System.Drawing.Size(183, 22);
            this.mnuMyOrders.Text = "My &open orders";
            this.mnuMyOrders.Click += new System.EventHandler(this.mnuMyOrders_Click);
            // 
            // mnuExecutedOrders
            // 
            this.mnuExecutedOrders.Name = "mnuExecutedOrders";
            this.mnuExecutedOrders.Size = new System.Drawing.Size(183, 22);
            this.mnuExecutedOrders.Text = "My &executed orders";
            this.mnuExecutedOrders.Click += new System.EventHandler(this.mnuExecutedOrders_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(180, 6);
            // 
            // mnuOsLatestExecuted
            // 
            this.mnuOsLatestExecuted.Name = "mnuOsLatestExecuted";
            this.mnuOsLatestExecuted.Size = new System.Drawing.Size(183, 22);
            this.mnuOsLatestExecuted.Text = "Os last orders placed";
            this.mnuOsLatestExecuted.Click += new System.EventHandler(this.mnuOsLatestExecuted_Click);
            // 
            // mnuBuySell
            // 
            this.mnuBuySell.Name = "mnuBuySell";
            this.mnuBuySell.Size = new System.Drawing.Size(183, 22);
            this.mnuBuySell.Text = "Buy / Sell";
            this.mnuBuySell.Click += new System.EventHandler(this.mnuBuySell_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(180, 6);
            // 
            // mnuMc
            // 
            this.mnuMc.Name = "mnuMc";
            this.mnuMc.Size = new System.Drawing.Size(183, 22);
            this.mnuMc.Text = "Market Cap";
            this.mnuMc.Click += new System.EventHandler(this.mnuMc_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(180, 6);
            // 
            // mnuGameReport
            // 
            this.mnuGameReport.Name = "mnuGameReport";
            this.mnuGameReport.Size = new System.Drawing.Size(183, 22);
            this.mnuGameReport.Text = "Game report";
            this.mnuGameReport.Click += new System.EventHandler(this.mnuGameReport_Click);
            // 
            // someToolStripMenuItem
            // 
            this.someToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTutorial,
            this.mnuAbout});
            this.someToolStripMenuItem.Name = "someToolStripMenuItem";
            this.someToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.someToolStripMenuItem.Text = "&Help";
            // 
            // mnuTutorial
            // 
            this.mnuTutorial.Name = "mnuTutorial";
            this.mnuTutorial.Size = new System.Drawing.Size(115, 22);
            this.mnuTutorial.Text = "&Tutorial";
            this.mnuTutorial.Click += new System.EventHandler(this.mnuTutorial_Click);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(115, 22);
            this.mnuAbout.Text = "&About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // tpPanel
            // 
            this.tpPanel.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tpPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tpPanel.Controls.Add(this.tbMarket);
            this.tpPanel.Controls.Add(this.tbPortfolio);
            this.tpPanel.Controls.Add(this.tbGraph);
            this.tpPanel.Controls.Add(this.tbIpo);
            this.tpPanel.Controls.Add(this.tbTwitter);
            this.tpPanel.Location = new System.Drawing.Point(0, 25);
            this.tpPanel.Multiline = true;
            this.tpPanel.Name = "tpPanel";
            this.tpPanel.SelectedIndex = 0;
            this.tpPanel.Size = new System.Drawing.Size(873, 380);
            this.tpPanel.TabIndex = 11;
            this.tpPanel.SelectedIndexChanged += new System.EventHandler(this.tpPanel_SelectedIndexChanged);
            // 
            // tbMarket
            // 
            this.tbMarket.Controls.Add(this.lvStocks);
            this.tbMarket.Location = new System.Drawing.Point(4, 4);
            this.tbMarket.Name = "tbMarket";
            this.tbMarket.Padding = new System.Windows.Forms.Padding(3);
            this.tbMarket.Size = new System.Drawing.Size(865, 354);
            this.tbMarket.TabIndex = 0;
            this.tbMarket.Text = "Summary";
            this.tbMarket.UseVisualStyleBackColor = true;
            // 
            // lvStocks
            // 
            this.lvStocks.ContextMenuStrip = this.ctxListMenu;
            this.lvStocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvStocks.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvStocks.FullRowSelect = true;
            this.lvStocks.Location = new System.Drawing.Point(3, 3);
            this.lvStocks.MultiSelect = false;
            this.lvStocks.Name = "lvStocks";
            this.lvStocks.Size = new System.Drawing.Size(859, 348);
            this.lvStocks.TabIndex = 0;
            this.lvStocks.UseCompatibleStateImageBehavior = false;
            this.lvStocks.View = System.Windows.Forms.View.Details;
            this.lvStocks.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvStocks_ColumnClick);
            this.lvStocks.SelectedIndexChanged += new System.EventHandler(this.lvStocks_SelectedIndexChanged);
            this.lvStocks.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
            // 
            // ctxListMenu
            // 
            this.ctxListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHiLo,
            this.mnuDetails,
            this.mnuStockTrading,
            this.toolStripMenuItem1,
            this.mnuBuy,
            this.mnuSell,
            this.toolStripMenuItem2,
            this.mnuRemove});
            this.ctxListMenu.Name = "ctxListMenu";
            this.ctxListMenu.Size = new System.Drawing.Size(230, 148);
            this.ctxListMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ctxListMenu_Opening);
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
            // mnuStockTrading
            // 
            this.mnuStockTrading.Name = "mnuStockTrading";
            this.mnuStockTrading.Size = new System.Drawing.Size(229, 22);
            this.mnuStockTrading.Text = "&Trade orders";
            this.mnuStockTrading.Click += new System.EventHandler(this.mnuStockTrading_Click);
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
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(226, 6);
            // 
            // mnuRemove
            // 
            this.mnuRemove.Name = "mnuRemove";
            this.mnuRemove.Size = new System.Drawing.Size(229, 22);
            this.mnuRemove.Text = "&Remove";
            this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
            // 
            // tbPortfolio
            // 
            this.tbPortfolio.Controls.Add(this.txEditBox);
            this.tbPortfolio.Controls.Add(this.cmbStocks);
            this.tbPortfolio.Controls.Add(this.lvPortfolio);
            this.tbPortfolio.Location = new System.Drawing.Point(4, 4);
            this.tbPortfolio.Name = "tbPortfolio";
            this.tbPortfolio.Padding = new System.Windows.Forms.Padding(3);
            this.tbPortfolio.Size = new System.Drawing.Size(865, 354);
            this.tbPortfolio.TabIndex = 1;
            this.tbPortfolio.Text = "Portfolio";
            this.tbPortfolio.UseVisualStyleBackColor = true;
            // 
            // txEditBox
            // 
            this.txEditBox.Location = new System.Drawing.Point(7, 85);
            this.txEditBox.Name = "txEditBox";
            this.txEditBox.Size = new System.Drawing.Size(57, 20);
            this.txEditBox.TabIndex = 3;
            this.txEditBox.Visible = false;
            this.txEditBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txEditBox_KeyPress);
            this.txEditBox.Leave += new System.EventHandler(this.control_leave);
            // 
            // cmbStocks
            // 
            this.cmbStocks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStocks.FormattingEnabled = true;
            this.cmbStocks.Location = new System.Drawing.Point(7, 58);
            this.cmbStocks.Name = "cmbStocks";
            this.cmbStocks.Size = new System.Drawing.Size(162, 21);
            this.cmbStocks.TabIndex = 2;
            this.cmbStocks.Visible = false;
            this.cmbStocks.Leave += new System.EventHandler(this.control_leave);
            // 
            // lvPortfolio
            // 
            this.lvPortfolio.ContextMenuStrip = this.ctxListMenu;
            this.lvPortfolio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPortfolio.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvPortfolio.FullRowSelect = true;
            this.lvPortfolio.Location = new System.Drawing.Point(3, 3);
            this.lvPortfolio.MultiSelect = false;
            this.lvPortfolio.Name = "lvPortfolio";
            this.lvPortfolio.Size = new System.Drawing.Size(859, 348);
            this.lvPortfolio.TabIndex = 1;
            this.lvPortfolio.UseCompatibleStateImageBehavior = false;
            this.lvPortfolio.View = System.Windows.Forms.View.Details;
            this.lvPortfolio.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvPortfolio_ColumnClick);
            this.lvPortfolio.SelectedIndexChanged += new System.EventHandler(this.lvPortfolio_SelectedIndexChanged);
            this.lvPortfolio.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
            this.lvPortfolio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvPortfolio_KeyDown);
            this.lvPortfolio.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvPortfolio_MouseClick);
            this.lvPortfolio.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvPortfolio_MouseDown);
            // 
            // tbGraph
            // 
            this.tbGraph.Controls.Add(this.chStocks);
            this.tbGraph.Location = new System.Drawing.Point(4, 4);
            this.tbGraph.Name = "tbGraph";
            this.tbGraph.Size = new System.Drawing.Size(865, 354);
            this.tbGraph.TabIndex = 2;
            this.tbGraph.Text = "Chart";
            this.tbGraph.UseVisualStyleBackColor = true;
            // 
            // chStocks
            // 
            this.chStocks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chStocks.Location = new System.Drawing.Point(0, 0);
            this.chStocks.Name = "chStocks";
            this.chStocks.ScrollGrace = 0D;
            this.chStocks.ScrollMaxX = 0D;
            this.chStocks.ScrollMaxY = 0D;
            this.chStocks.ScrollMaxY2 = 0D;
            this.chStocks.ScrollMinX = 0D;
            this.chStocks.ScrollMinY = 0D;
            this.chStocks.ScrollMinY2 = 0D;
            this.chStocks.Size = new System.Drawing.Size(862, 351);
            this.chStocks.TabIndex = 0;
            // 
            // tbIpo
            // 
            this.tbIpo.Controls.Add(this.label4);
            this.tbIpo.Controls.Add(this.label1);
            this.tbIpo.Controls.Add(this.btRefresh);
            this.tbIpo.Controls.Add(this.lbPlayer);
            this.tbIpo.Controls.Add(this.label7);
            this.tbIpo.Controls.Add(this.label6);
            this.tbIpo.Controls.Add(this.btSubmitIpoRequest);
            this.tbIpo.Controls.Add(this.txIpoQuestion);
            this.tbIpo.Controls.Add(this.txIpoAnswer);
            this.tbIpo.Controls.Add(this.txShares);
            this.tbIpo.Controls.Add(this.label3);
            this.tbIpo.Controls.Add(this.lstIpos);
            this.tbIpo.Controls.Add(this.btStop);
            this.tbIpo.Controls.Add(this.lbIpoMsg);
            this.tbIpo.Location = new System.Drawing.Point(4, 4);
            this.tbIpo.Name = "tbIpo";
            this.tbIpo.Size = new System.Drawing.Size(865, 354);
            this.tbIpo.TabIndex = 3;
            this.tbIpo.Text = "P2Pools";
            this.tbIpo.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(214, 217);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "IPO Alert";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Answer";
            // 
            // btRefresh
            // 
            this.btRefresh.Location = new System.Drawing.Point(14, 312);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(87, 23);
            this.btRefresh.TabIndex = 1;
            this.btRefresh.Text = "Refresh";
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // lbPlayer
            // 
            this.lbPlayer.AutoSize = true;
            this.lbPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPlayer.Location = new System.Drawing.Point(216, 29);
            this.lbPlayer.Name = "lbPlayer";
            this.lbPlayer.Size = new System.Drawing.Size(79, 13);
            this.lbPlayer.TabIndex = 29;
            this.lbPlayer.Text = "Issued stock";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(215, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Challenge question";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "IPO List";
            // 
            // btSubmitIpoRequest
            // 
            this.btSubmitIpoRequest.Location = new System.Drawing.Point(294, 172);
            this.btSubmitIpoRequest.Name = "btSubmitIpoRequest";
            this.btSubmitIpoRequest.Size = new System.Drawing.Size(87, 23);
            this.btSubmitIpoRequest.TabIndex = 6;
            this.btSubmitIpoRequest.Text = "Submit order";
            this.btSubmitIpoRequest.UseVisualStyleBackColor = true;
            this.btSubmitIpoRequest.Click += new System.EventHandler(this.btSubmitIpoRequest_Click);
            // 
            // txIpoQuestion
            // 
            this.txIpoQuestion.BackColor = System.Drawing.Color.White;
            this.txIpoQuestion.Location = new System.Drawing.Point(216, 111);
            this.txIpoQuestion.Multiline = true;
            this.txIpoQuestion.Name = "txIpoQuestion";
            this.txIpoQuestion.ReadOnly = true;
            this.txIpoQuestion.Size = new System.Drawing.Size(280, 44);
            this.txIpoQuestion.TabIndex = 4;
            this.txIpoQuestion.Text = "IPO challenge question";
            // 
            // txIpoAnswer
            // 
            this.txIpoAnswer.Location = new System.Drawing.Point(215, 175);
            this.txIpoAnswer.Name = "txIpoAnswer";
            this.txIpoAnswer.Size = new System.Drawing.Size(68, 20);
            this.txIpoAnswer.TabIndex = 5;
            // 
            // txShares
            // 
            this.txShares.Location = new System.Drawing.Point(216, 68);
            this.txShares.MaxLength = 6;
            this.txShares.Name = "txShares";
            this.txShares.Size = new System.Drawing.Size(67, 20);
            this.txShares.TabIndex = 3;
            this.txShares.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(214, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Number of shares, $5 per";
            // 
            // lstIpos
            // 
            this.lstIpos.FormattingEnabled = true;
            this.lstIpos.Location = new System.Drawing.Point(14, 29);
            this.lstIpos.Name = "lstIpos";
            this.lstIpos.Size = new System.Drawing.Size(193, 277);
            this.lstIpos.TabIndex = 0;
            this.lstIpos.SelectedIndexChanged += new System.EventHandler(this.lstIpos_SelectedIndexChanged);
            // 
            // btStop
            // 
            this.btStop.Location = new System.Drawing.Point(217, 248);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(100, 32);
            this.btStop.TabIndex = 7;
            this.btStop.Text = "Stop Alert";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // lbIpoMsg
            // 
            this.lbIpoMsg.AutoSize = true;
            this.lbIpoMsg.BackColor = System.Drawing.Color.Yellow;
            this.lbIpoMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIpoMsg.Location = new System.Drawing.Point(216, 232);
            this.lbIpoMsg.Name = "lbIpoMsg";
            this.lbIpoMsg.Size = new System.Drawing.Size(59, 13);
            this.lbIpoMsg.TabIndex = 14;
            this.lbIpoMsg.Text = "IPO MSG";
            // 
            // nIcon
            // 
            this.nIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("nIcon.Icon")));
            this.nIcon.Text = "Auroracoin desktop";
            this.nIcon.Visible = true;
            this.nIcon.DoubleClick += new System.EventHandler(this.nIcon_DoubleClick);
            // 
            // cxtMenu
            // 
            this.cxtMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuShowApp,
            this.mnuExitApp});
            // 
            // mnuShowApp
            // 
            this.mnuShowApp.Index = 0;
            this.mnuShowApp.Text = "&Show";
            this.mnuShowApp.Click += new System.EventHandler(this.mnuShowApp_Click);
            // 
            // mnuExitApp
            // 
            this.mnuExitApp.Index = 1;
            this.mnuExitApp.Text = "&Exit";
            this.mnuExitApp.Click += new System.EventHandler(this.mnuExitApp_Click);
            // 
            // tmCheckForIPO
            // 
            this.tmCheckForIPO.Tick += new System.EventHandler(this.tmCheckForIPO_Tick);
            // 
            // lblYesterdayValue
            // 
            this.lblYesterdayValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblYesterdayValue.AutoSize = true;
            this.lblYesterdayValue.Location = new System.Drawing.Point(58, 412);
            this.lblYesterdayValue.Name = "lblYesterdayValue";
            this.lblYesterdayValue.Size = new System.Drawing.Size(16, 13);
            this.lblYesterdayValue.TabIndex = 15;
            this.lblYesterdayValue.Text = "---";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 412);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Yesterday";
            // 
            // tmPlayIpoSound
            // 
            this.tmPlayIpoSound.Interval = 2000;
            this.tmPlayIpoSound.Tick += new System.EventHandler(this.tmPlayIpoSound_Tick);
            // 
            // lblCtrlKey
            // 
            this.lblCtrlKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCtrlKey.AutoSize = true;
            this.lblCtrlKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCtrlKey.Location = new System.Drawing.Point(412, 392);
            this.lblCtrlKey.Name = "lblCtrlKey";
            this.lblCtrlKey.Size = new System.Drawing.Size(181, 13);
            this.lblCtrlKey.TabIndex = 18;
            this.lblCtrlKey.Text = "Hold Ctrl key down to compare";
            this.lblCtrlKey.Visible = false;
            // 
            // cmbGraphStocks
            // 
            this.cmbGraphStocks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbGraphStocks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGraphStocks.FormattingEnabled = true;
            this.cmbGraphStocks.Location = new System.Drawing.Point(244, 387);
            this.cmbGraphStocks.Name = "cmbGraphStocks";
            this.cmbGraphStocks.Size = new System.Drawing.Size(162, 21);
            this.cmbGraphStocks.TabIndex = 17;
            this.cmbGraphStocks.Visible = false;
            this.cmbGraphStocks.SelectedIndexChanged += new System.EventHandler(this.cmbGraphStocks_SelectedIndexChanged);
            // 
            // lbCash
            // 
            this.lbCash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbCash.AutoSize = true;
            this.lbCash.Location = new System.Drawing.Point(247, 389);
            this.lbCash.Name = "lbCash";
            this.lbCash.Size = new System.Drawing.Size(31, 13);
            this.lbCash.TabIndex = 25;
            this.lbCash.Text = "Cash";
            this.lbCash.Visible = false;
            // 
            // lbCashText
            // 
            this.lbCashText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbCashText.AutoSize = true;
            this.lbCashText.Location = new System.Drawing.Point(242, 389);
            this.lbCashText.Name = "lbCashText";
            this.lbCashText.Size = new System.Drawing.Size(16, 13);
            this.lbCashText.TabIndex = 26;
            this.lbCashText.Text = "---";
            this.lbCashText.Visible = false;
            // 
            // lbReserveCashText
            // 
            this.lbReserveCashText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbReserveCashText.AutoSize = true;
            this.lbReserveCashText.Location = new System.Drawing.Point(379, 389);
            this.lbReserveCashText.Name = "lbReserveCashText";
            this.lbReserveCashText.Size = new System.Drawing.Size(16, 13);
            this.lbReserveCashText.TabIndex = 24;
            this.lbReserveCashText.Text = "---";
            this.lbReserveCashText.Visible = false;
            // 
            // lbReserveCash
            // 
            this.lbReserveCash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbReserveCash.AutoSize = true;
            this.lbReserveCash.Location = new System.Drawing.Point(337, 389);
            this.lbReserveCash.Name = "lbReserveCash";
            this.lbReserveCash.Size = new System.Drawing.Size(80, 13);
            this.lbReserveCash.TabIndex = 23;
            this.lbReserveCash.Text = "Reserved Cash";
            this.lbReserveCash.Visible = false;
            // 
            // tbTwitter
            // 
            this.tbTwitter.Controls.Add(this.wbRender);
            this.tbTwitter.Location = new System.Drawing.Point(4, 4);
            this.tbTwitter.Name = "tbTwitter";
            this.tbTwitter.Size = new System.Drawing.Size(865, 354);
            this.tbTwitter.TabIndex = 4;
            this.tbTwitter.Text = "Twitter";
            this.tbTwitter.UseVisualStyleBackColor = true;
            // 
            // wbRender
            // 
            this.wbRender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbRender.Location = new System.Drawing.Point(0, 0);
            this.wbRender.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbRender.Name = "wbRender";
            this.wbRender.Size = new System.Drawing.Size(865, 354);
            this.wbRender.TabIndex = 0;
            this.wbRender.Url = new System.Uri("https://twitter.com/auroracoinIS", System.UriKind.Absolute);
            // 
            // FxMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 431);
            this.Controls.Add(this.lbCash);
            this.Controls.Add(this.lbCashText);
            this.Controls.Add(this.lbReserveCashText);
            this.Controls.Add(this.lbReserveCash);
            this.Controls.Add(this.lblCtrlKey);
            this.Controls.Add(this.cmbGraphStocks);
            this.Controls.Add(this.lblYesterdayValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lbVolume);
            this.Controls.Add(this.tpPanel);
            this.Controls.Add(this.lbVolumeText);
            this.Controls.Add(this.lbValueText);
            this.Controls.Add(this.lbChangeText);
            this.Controls.Add(this.lbChange);
            this.Controls.Add(this.lbValue);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FxMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = " Aurora fun - Til tunglsins !";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FxMain_FormClosing);
            this.Load += new System.EventHandler(this.CxMain_Load);
            this.Resize += new System.EventHandler(this.FxMain_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tpPanel.ResumeLayout(false);
            this.tbMarket.ResumeLayout(false);
            this.ctxListMenu.ResumeLayout(false);
            this.tbPortfolio.ResumeLayout(false);
            this.tbPortfolio.PerformLayout();
            this.tbGraph.ResumeLayout(false);
            this.tbIpo.ResumeLayout(false);
            this.tbIpo.PerformLayout();
            this.tbTwitter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbValue;
        private System.Windows.Forms.Label lbValueText;
        private System.Windows.Forms.Label lbChange;
        private System.Windows.Forms.Label lbChangeText;
        private System.Windows.Forms.Label lbVolume;
        private System.Windows.Forms.Label lbVolumeText;
        private System.Windows.Forms.Timer tmLoop;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem someToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuTutorial;
        private System.Windows.Forms.TabControl tpPanel;
        private System.Windows.Forms.TabPage tbMarket;
        private System.Windows.Forms.ListView lvStocks;
        private System.Windows.Forms.TabPage tbPortfolio;
        private System.Windows.Forms.TextBox txEditBox;
        private System.Windows.Forms.ComboBox cmbStocks;
        private System.Windows.Forms.ListView lvPortfolio;
        private System.Windows.Forms.NotifyIcon nIcon;
        private System.Windows.Forms.TabPage tbGraph;
        private ZedGraph.ZedGraphControl chStocks;
        private System.Windows.Forms.ContextMenu cxtMenu;
        private System.Windows.Forms.MenuItem mnuShowApp;
        private System.Windows.Forms.MenuItem mnuExitApp;
        private System.Windows.Forms.Timer tmCheckForIPO;
        private System.Windows.Forms.ContextMenuStrip ctxListMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuDetails;
        private System.Windows.Forms.ToolStripMenuItem mnuStockTrading;
        private System.Windows.Forms.ToolStripMenuItem mnuRemove;
        private System.Windows.Forms.ToolStripMenuItem mnuBuy;
        private System.Windows.Forms.ToolStripMenuItem mnuSell;
        private System.Windows.Forms.Label lblYesterdayValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem mnuConfig;
        private System.Windows.Forms.ToolStripMenuItem mnuHiLo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuOther;
        private System.Windows.Forms.ToolStripMenuItem mnuMyOrders;
        private System.Windows.Forms.ToolStripMenuItem mnuExecutedOrders;
        private System.Windows.Forms.ToolStripMenuItem mnuOsLatestExecuted;
        private System.Windows.Forms.ToolStripMenuItem mnuBuySell;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem mnuMc;
        private System.Windows.Forms.Timer tmPlayIpoSound;
        private System.Windows.Forms.TabPage tbIpo;
        private System.Windows.Forms.Label lblCtrlKey;
        private System.Windows.Forms.ComboBox cmbGraphStocks;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.Label lbIpoMsg;
        private System.Windows.Forms.ListBox lstIpos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btSubmitIpoRequest;
        private System.Windows.Forms.TextBox txIpoQuestion;
        private System.Windows.Forms.TextBox txIpoAnswer;
        private System.Windows.Forms.TextBox txShares;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbPlayer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbCash;
        private System.Windows.Forms.Label lbCashText;
        private System.Windows.Forms.Label lbReserveCashText;
        private System.Windows.Forms.Label lbReserveCash;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem mnuGameReport;
        private System.Windows.Forms.TabPage tbTwitter;
        private System.Windows.Forms.WebBrowser wbRender;
    }
}

