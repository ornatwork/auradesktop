namespace com.seasonalerts.desktop.ui
{
    partial class FxMain
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
            this.lvStocks = new System.Windows.Forms.ListView();
            this.lbValue = new System.Windows.Forms.Label();
            this.lbValueText = new System.Windows.Forms.Label();
            this.lbChange = new System.Windows.Forms.Label();
            this.lbChangeText = new System.Windows.Forms.Label();
            this.lbVolume = new System.Windows.Forms.Label();
            this.lbVolumeText = new System.Windows.Forms.Label();
            this.tmLoop = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.someToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvStocks
            // 
            this.lvStocks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvStocks.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvStocks.FullRowSelect = true;
            this.lvStocks.Location = new System.Drawing.Point(3, 24);
            this.lvStocks.MultiSelect = false;
            this.lvStocks.Name = "lvStocks";
            this.lvStocks.Size = new System.Drawing.Size(359, 435);
            this.lvStocks.TabIndex = 0;
            this.lvStocks.UseCompatibleStateImageBehavior = false;
            this.lvStocks.View = System.Windows.Forms.View.Details;
            this.lvStocks.DoubleClick += new System.EventHandler(this.lvStocks_DoubleClick);
            this.lvStocks.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvStocks_ColumnClick);
            // 
            // lbValue
            // 
            this.lbValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbValue.AutoSize = true;
            this.lbValue.Location = new System.Drawing.Point(0, 484);
            this.lbValue.Name = "lbValue";
            this.lbValue.Size = new System.Drawing.Size(34, 13);
            this.lbValue.TabIndex = 2;
            this.lbValue.Text = "Value";
            // 
            // lbValueText
            // 
            this.lbValueText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbValueText.AutoSize = true;
            this.lbValueText.Location = new System.Drawing.Point(34, 484);
            this.lbValueText.Name = "lbValueText";
            this.lbValueText.Size = new System.Drawing.Size(16, 13);
            this.lbValueText.TabIndex = 3;
            this.lbValueText.Text = "---";
            // 
            // lbChange
            // 
            this.lbChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbChange.AutoSize = true;
            this.lbChange.Location = new System.Drawing.Point(131, 484);
            this.lbChange.Name = "lbChange";
            this.lbChange.Size = new System.Drawing.Size(44, 13);
            this.lbChange.TabIndex = 4;
            this.lbChange.Text = "Change";
            // 
            // lbChangeText
            // 
            this.lbChangeText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbChangeText.AutoSize = true;
            this.lbChangeText.Location = new System.Drawing.Point(175, 484);
            this.lbChangeText.Name = "lbChangeText";
            this.lbChangeText.Size = new System.Drawing.Size(16, 13);
            this.lbChangeText.TabIndex = 5;
            this.lbChangeText.Text = "---";
            // 
            // lbVolume
            // 
            this.lbVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbVolume.AutoSize = true;
            this.lbVolume.Location = new System.Drawing.Point(235, 484);
            this.lbVolume.Name = "lbVolume";
            this.lbVolume.Size = new System.Drawing.Size(42, 13);
            this.lbVolume.TabIndex = 6;
            this.lbVolume.Text = "Volume";
            // 
            // lbVolumeText
            // 
            this.lbVolumeText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbVolumeText.AutoSize = true;
            this.lbVolumeText.Location = new System.Drawing.Point(277, 484);
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
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 465);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Market";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.someToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(365, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExit});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem1.Text = "&File";
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(103, 22);
            this.mnuExit.Text = "&Exit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // someToolStripMenuItem
            // 
            this.someToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.someToolStripMenuItem.Name = "someToolStripMenuItem";
            this.someToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.someToolStripMenuItem.Text = "&Help";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(114, 22);
            this.mnuAbout.Text = "&About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // FxMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 505);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbVolumeText);
            this.Controls.Add(this.lbVolume);
            this.Controls.Add(this.lbChangeText);
            this.Controls.Add(this.lbChange);
            this.Controls.Add(this.lbValueText);
            this.Controls.Add(this.lbValue);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lvStocks);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FxMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = " One Season market summary";
            this.Load += new System.EventHandler(this.CxMain_Load);
            this.SizeChanged += new System.EventHandler(this.FxMain_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvStocks;
        private System.Windows.Forms.Label lbValue;
        private System.Windows.Forms.Label lbValueText;
        private System.Windows.Forms.Label lbChange;
        private System.Windows.Forms.Label lbChangeText;
        private System.Windows.Forms.Label lbVolume;
        private System.Windows.Forms.Label lbVolumeText;
        private System.Windows.Forms.Timer tmLoop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem someToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
    }
}

