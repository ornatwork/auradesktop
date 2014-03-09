//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

//
using org.auroracoin.aurcore.stocks;
using org.auroracoin.aurcore.util;



namespace org.auroracoin.desktop.ui
{
    public partial class FxStockDetail : Form
    {

        public FxStockDetail()
        {
            InitializeComponent();
        }

        private void FxStockDetail_Load(object sender, EventArgs e)
        {
        }

        public void setStock(CxMarketStock pxStock)
        {
            //
            this.Text += " " + pxStock.Symbol;
            //
            StringBuilder stockText = new StringBuilder();
            //
            this.lbName.Text = "Stock: " + pxStock.ToString();
            //this.lbSport.Text = "Sport: " + pxStock.Sport;
            this.lbIpoDate.Text = "IPO date: " + pxStock.IPODate.ToShortDateString();
            this.lbDebut.Text = "Debut: " + pxStock.DebutYear;

            //
            stockText.Append("Last trade: $" + pxStock.LastTrade + Environment.NewLine);
            stockText.Append("Last trade at: " + pxStock.TradeDateTime.ToString() + Environment.NewLine);
            stockText.Append("Price: " + pxStock.Price + Environment.NewLine);
            stockText.Append("Day change: " + pxStock.Change + " %" + Environment.NewLine);
            stockText.Append("Best buy offer : $" + pxStock.Bid + Environment.NewLine);
            stockText.Append("Best sell offer: $" + pxStock.Ask + Environment.NewLine);
            stockText.Append("Day range: $" + pxStock.DayRange + Environment.NewLine);
            stockText.Append("Prev Close: $" + pxStock.PrevClose + Environment.NewLine);
            stockText.Append("Open: $" + pxStock.Open + Environment.NewLine);
            stockText.Append("Volume: " + pxStock.Volume + Environment.NewLine);
            stockText.Append("Total value: $" + (pxStock.Volume * pxStock.Price) + Environment.NewLine);
            stockText.Append("52 week range: $" + pxStock.FiftyTwoWKRange + Environment.NewLine);
            stockText.Append("Splits: " + pxStock.NoOfSplits + Environment.NewLine);
            // Image of the playa
            Image stockPick = CxUtil.getUrlImage("");
            this.pcStock.Image = stockPick;
            //
            this.txDetail.Text = stockText.ToString();
            this.txDetail.Select(0, 0);
        }

        private void FxStockDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

    
    }  // EOC
}
