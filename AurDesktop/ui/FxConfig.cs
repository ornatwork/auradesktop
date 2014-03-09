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
    public partial class FxConfig : Form
    {

        public FxConfig()
        {
            InitializeComponent();
            //
            this.chkSound.Checked = CxIniFile.getInstance().readBoolKey(CxIniFile.NOSOUND_KEY, false);
            this.txBuyOverWarn.Text = CxIniFile.getInstance().readDoubleKey(CxIniFile.WARN_PRICE_OVER_KEY, CxGlobal.OverPrice).ToString();
            this.txBuyUnderWarn.Text = CxIniFile.getInstance().readDoubleKey(CxIniFile.WARN_PRICE_UNDER_KEY, CxGlobal.UnderPrice).ToString();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            // Don't play sound 
            CxGlobal.NO_SOUND = this.chkSound.Checked;
            CxIniFile.getInstance().writeBoolKey(CxIniFile.NOSOUND_KEY, CxGlobal.NO_SOUND );

            // Set the over warn price
            double warnPrice = CxUtil.getDouble(this.txBuyOverWarn.Text);
            // convert error, fix it 
            if (warnPrice == 0)
                warnPrice = CxGlobal.OverPrice;
            CxIniFile.getInstance().writeDoubleKey(CxIniFile.WARN_PRICE_OVER_KEY, warnPrice);
            CxGlobal.OverPrice = warnPrice;

            // Set the under warn price
            warnPrice = CxUtil.getDouble(this.txBuyUnderWarn.Text);
            // convert error, fix it 
            if (warnPrice == 0)
                warnPrice = CxGlobal.UnderPrice;
            CxIniFile.getInstance().writeDoubleKey(CxIniFile.WARN_PRICE_UNDER_KEY, warnPrice);
            CxGlobal.UnderPrice = warnPrice;

            // close out the form
            this.Close();
        }

        private void btConfirmOsLogin_Click(object sender, EventArgs e)
        {

        }

       
 
   
    } // EOC
}
