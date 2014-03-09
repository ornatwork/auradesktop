//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
//
using org.auroracoin.aurcore.util;
using org.auroracoin.desktop.core;


namespace org.auroracoin.desktop.ui
{
    public partial class FxAbout : Form
    {

        public FxAbout()
        {
            InitializeComponent();
            //
            this.lbVersion.Text += Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.lbAurDonate.Text = "AUR: " + CxUtil.AUR_ADRESS;
            this.lbBtcDonate.Text = "BTC: " + CxUtil.BTC_ADRESS;
        }

 
        private void btWebsite_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            using (System.Diagnostics.Process.Start( "" )) { }
            this.Cursor = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText( CxUtil.AUR_ADRESS );
        }

        private void btCopy2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(CxUtil.BTC_ADRESS);
        }
    
    } // EOC
}
