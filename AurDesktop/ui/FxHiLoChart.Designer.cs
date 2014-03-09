namespace org.auroracoin.desktop.ui
{
    partial class FxHiLoChart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FxHiLoChart));
            this.chHiLo = new ZedGraph.ZedGraphControl();
            this.rdOHLC = new System.Windows.Forms.RadioButton();
            this.rdCandle = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // chHiLo
            // 
            this.chHiLo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chHiLo.Location = new System.Drawing.Point(2, 25);
            this.chHiLo.Name = "chHiLo";
            this.chHiLo.ScrollGrace = 0D;
            this.chHiLo.ScrollMaxX = 0D;
            this.chHiLo.ScrollMaxY = 0D;
            this.chHiLo.ScrollMaxY2 = 0D;
            this.chHiLo.ScrollMinX = 0D;
            this.chHiLo.ScrollMinY = 0D;
            this.chHiLo.ScrollMinY2 = 0D;
            this.chHiLo.Size = new System.Drawing.Size(835, 390);
            this.chHiLo.TabIndex = 4;
            // 
            // rdOHLC
            // 
            this.rdOHLC.AutoSize = true;
            this.rdOHLC.Checked = true;
            this.rdOHLC.Location = new System.Drawing.Point(7, 4);
            this.rdOHLC.Name = "rdOHLC";
            this.rdOHLC.Size = new System.Drawing.Size(54, 17);
            this.rdOHLC.TabIndex = 5;
            this.rdOHLC.TabStop = true;
            this.rdOHLC.Text = "OHLC";
            this.rdOHLC.UseVisualStyleBackColor = true;
            this.rdOHLC.CheckedChanged += new System.EventHandler(this.rdOHLC_CheckedChanged);
            // 
            // rdCandle
            // 
            this.rdCandle.AutoSize = true;
            this.rdCandle.Location = new System.Drawing.Point(66, 4);
            this.rdCandle.Name = "rdCandle";
            this.rdCandle.Size = new System.Drawing.Size(80, 17);
            this.rdCandle.TabIndex = 6;
            this.rdCandle.Text = "Candlestick";
            this.rdCandle.UseVisualStyleBackColor = true;
            this.rdCandle.CheckedChanged += new System.EventHandler(this.rdCandle_CheckedChanged);
            // 
            // FxHiLoChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 417);
            this.Controls.Add(this.rdCandle);
            this.Controls.Add(this.rdOHLC);
            this.Controls.Add(this.chHiLo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FxHiLoChart";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Open High Low Close - ";
            this.Load += new System.EventHandler(this.FxHiLoChart_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl chHiLo;
        private System.Windows.Forms.RadioButton rdOHLC;
        private System.Windows.Forms.RadioButton rdCandle;

    }
}