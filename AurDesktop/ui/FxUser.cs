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
using org.auroracoin.aurcore.stocks;


namespace org.auroracoin.desktop.ui
{
    public partial class FxUser : Form
    {
        private IList<CxMarketStock> mlStocks = new List<CxMarketStock>();
        private string ADJUST_PRICE = "The price must be adjusted before submitting a trade.";
        private string TRADE_WARNING_CAPTION = "Trade warning";
        //
        private string msPlayerName = string.Empty;
        private string msSymbol = string.Empty;
        
        public FxUser()
        {
            InitializeComponent();
            //
            this.init( string.Empty, string.Empty );
        }

        public FxUser(ExAction pxAction, string psSymbol, string psPlayerName, int piShares, double pdPrice) 
        {
            InitializeComponent();
            
            //
            this.msPlayerName = psPlayerName;
            this.msSymbol = psSymbol.ToUpper();
            //
            this.init(psSymbol, msPlayerName);
            //
            this.cmbOsEmail.Text = psSymbol + ", " + psPlayerName;

            //
            this.txOsEmail.Text = pdPrice.ToString();

            // Always adjustable by the user
            this.txOsPassword.Text = piShares.ToString();
        }

        private void init( string psStock, string psPlayerName  )
        {
            // Put refreshes on hold, while this form is open
            CxGlobal.REFRESH_ON = false;

            mlStocks = CxGlobal.getStocks();
            this.cmbOsEmail.DataSource = mlStocks;

            //
            //this.cmbAction.Items.Add(ExAction.Buy);
            //this.cmbAction.Items.Add(ExAction.Sell);
            //this.cmbAction.SelectedIndex = 0;
        }

        //
        private void btSave_Click(object sender, EventArgs e)
        {

            double dPrice = CxUtil.getDouble(this.txOsEmail.Text);

            // Trap, make sure the user is not buying over $20 by mistake
            // or whatever is set in config
            //if (this.cmbAction.Text == ExAction.Buy.ToString() &&
            //    CxGlobal.MaxPrice < dPrice )
            //{
            //    // Include the warn price in the msg
            //    WARN_STRING = string.Format(WARN_STRING, CxGlobal.MaxPrice.ToString());
            //    // Let the user decide
            //    if (MessageBox.Show( this, WARN_STRING, TRADE_WARNING_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Warning ) == DialogResult.No)
            //        return;
            //}

            // Don't allow empty or zero price 
            if( this.txOsEmail.Text == string.Empty || dPrice == 0 )
            {
                MessageBox.Show(this, ADJUST_PRICE, TRADE_WARNING_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            
            // And then business as usual
            //this.Cursor = Cursors.WaitCursor;

            //string success = CxDeskUtil.trade(this.cmbOsEmail.Text.Substring(0, 4), this.cmbAction.Text,
            //                                        CxUtil.getInt(this.txOsPassword.Text), dPrice);
            //this.Cursor = Cursors.Default;

            //
            //MessageBox.Show(success, "Trade");
            // Don't close it down if unsuccessful
            //if (success.IndexOf("NOT") > -1) return;

            // close out the form
            this.Close();
            
        }

        // Only allows numeric input
        private void sharesKeyPress(object sender, KeyPressEventArgs e)
        {
            int iKey = (int)e.KeyChar;
            bool isDigit = (iKey >= 48 && iKey <= 57);
            bool isBackSpace = (iKey == 8);
            bool isForwardArrow = (iKey == 39);
            bool isBackwardArrow = (iKey == 37);

            // Make sure it's good input
            if (!isDigit && !isBackSpace && !isForwardArrow && !isBackwardArrow)
            {
                e.Handled = true;
                return;
            }

        }

        // Only allows numeric input, including decimal / dot 
        private void priceKeyPress(object sender, KeyPressEventArgs e)
        {
            int iKey = (int)e.KeyChar;
            bool isDigit = (iKey >= 48 && iKey <= 57);
            bool isDot = (iKey == 46);
            bool isBackSpace = (iKey == 8);
            bool isForwardArrow = (iKey == 39);
            bool isBackwardArrow = (iKey == 37);


            // Check for . already in box
            if (((TextBox)sender).Text.IndexOf(".") > -1 && isDot)
            {
                e.Handled = true;
                return;
            }

            // Make sure it's good input
            if (!isDigit && !isBackSpace && !isDot && !isForwardArrow && !isBackwardArrow)
            {
                e.Handled = true;
                return;
            }
        }

        // Stop the alert
        private void btStopAlert_Click(object sender, EventArgs e)
        {
            // Dont' allow the refresh though
            CxGlobal.REFRESH_ON = false;
        }

        private void FxBuySell_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Activate the refresh again
            CxGlobal.REFRESH_ON = true;
        }

        private void btCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    } // EOC
}
