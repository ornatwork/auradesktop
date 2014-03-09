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
    public partial class FxGameReport : Form
    {
        List<CxPortfolioStock> mlStocks = new List<CxPortfolioStock>();

        public FxGameReport( List<CxPortfolioStock> plStocks )
        {
            InitializeComponent();
            //
            this.mlStocks = plStocks;
        }


        private void Init( List<CxPortfolioStock> plStocks )
        {
            StringBuilder builder = new StringBuilder();
            IList<CxMarketStock> tickerStocks = CxGlobal.getStocks();

            
            // Ownership
            builder.Append(Environment.NewLine);
            builder.Append( "Ownership" + Environment.NewLine);
            foreach( CxPortfolioStock pstock in plStocks )
            {
                CxMarketStock stock = CxUtil.getStock(tickerStocks, pstock.Symbol);
                if (stock != null)
                {
                    double value = (pstock.Shares / stock.TotalShares) * 100;
                    string ownership = value.ToString(CxUtil.FORMAT_OWNERSHIP) + "%";


                    //
                    builder.Append(pstock.Symbol + CxUtil.TAB +
                               ownership + CxUtil.TAB +
                               pstock.Shares.ToString(CxUtil.FORMAT_DOUBLE) + Environment.NewLine);
                }
            }
            builder.Append(Environment.NewLine);

            /*
            // Executed orders
            builder.Append("Executed orders" + Environment.NewLine);
            IList<CxStockOrder> Orders = CxDeskUtil.getMyTrades();
            //
            for (int i = Orders.Count-1; i > 0; i--)
            {
                CxStockOrder ord = Orders[i];
                CxMarketStock stock = CxUtil.getStock(tickerStocks, ord.Symbol);
                //
                builder.Append(ord.Symbol + CxUtil.TAB +
                    stock.Player + CxUtil.TAB +
                    ord.Action + CxUtil.TAB +
                    ord.AsOf + CxUtil.TAB +
                    ord.Shares + CxUtil.TAB +
                    ord.Price + CxUtil.TAB +
                    (ord.Ipo ? "X" : "") + CxUtil.TAB +
                    Environment.NewLine );
            }
            //
            builder.Append(Environment.NewLine);
            //
            this.txContent.Text = builder.ToString();
            this.txContent.Select(0, 0);
             */ 
        }

        private void btEmail_Click(object sender, EventArgs e)
        {

            try
            {
                FxInput inpt = new FxInput("Email", "Email address");
                if (inpt.ShowDialog() == DialogResult.OK)
                {
                    string emailaddress = inpt.getInput();
                    //
                    this.Cursor = Cursors.WaitCursor;

                    if (emailaddress != string.Empty)
                        CxUtil.sendEmail(this.Text + " - " + emailaddress, this.Text,
                                            emailaddress, this.createAttachment());
                    //
                    MessageBox.Show("Email", CxDeskUtil.EMAIL_SUCCESSFULLY_SENT);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error=" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        // create attachment file for email
        private string createAttachment()
        {
            StringBuilder fileContent = new StringBuilder();

            // Mark it 
            fileContent.Append(" " + CxUtil.COMMA + CxGlobal.OS_USERNAME + CxUtil.COMMA +
                                CxUtil.getEstTimeFromLocalTime(DateTime.Now).ToString() + Environment.NewLine);
            // The data 
            fileContent.Append( this.txContent.Text + Environment.NewLine );

            // Create the file 
            string cvsFile = CxUtil.getDataDir() + "\\gamereport.csv";
            // Make sure it's comma delim, not tabs which the user sees 
            CxUtil.writeTextToFile(cvsFile, fileContent.ToString().Replace( CxUtil.TAB, CxUtil.COMMA ) );

            //
            return cvsFile;
        }

        private void FxGameReport_Load(object sender, EventArgs e)
        {
            this.Init(this.mlStocks);
        }

    
    } // EOC
}
