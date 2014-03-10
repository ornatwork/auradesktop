//
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
//
using org.auroracoin.aurcore.stocks;
using org.auroracoin.aurcore.util;


namespace org.auroracoin.desktop.ui
{
    class CxUiUtil
    {
        //
        public const string DONT_SORT_TAG = "-1";
        public const string ALERT_INDICATOR = "X";
        private static FxStockDetail mxDetailForm = new FxStockDetail();

        //
        private CxUiUtil(){}

        // shade the grid light gray / white every other row
        public static void shadeGrid( ListView pxView)
        {
            int iCount = 0;
            //
            foreach(ListViewItem row in pxView.Items)
            {
                iCount++;
                //
                if (iCount % 2 == 0)
                {
                    row.BackColor = Color.WhiteSmoke;
                    foreach (ListViewItem.ListViewSubItem sub in row.SubItems)
                        sub.BackColor = Color.WhiteSmoke;
                }
                else
                {
                    row.BackColor = Color.White;
                    foreach (ListViewItem.ListViewSubItem sub in row.SubItems)
                        sub.BackColor = Color.White;
                }
            }
        }

        // set seperator
        public static void setSeperator( ListView pxView, int piColumn )
        {
            //
            foreach (ListViewItem row in pxView.Items)
            {
                row.SubItems[piColumn].BackColor = Color.Gray;
            }
        }

        public static void setArrow(ListView pxListView, int piColumn, SortOrder pxSort)
        {
            // Remove former arrows
            foreach (ColumnHeader thecol in pxListView.Columns)
                thecol.Text = StripColumn(thecol.Text);
            
            // Set the sort arrow
            if( pxSort == SortOrder.Descending )
                pxListView.Columns[piColumn].Text = CxUiUtil.StripColumn(pxListView.Columns[piColumn].Text) + CxUtil.DESCENDING_ARROW;
            else
                pxListView.Columns[piColumn].Text = CxUiUtil.StripColumn(pxListView.Columns[piColumn].Text) + CxUtil.ASCENDING_ARROW;

        }

        public static string StripColumn(string toStrip)
        {
            return toStrip.Replace(CxUtil.DESCENDING_ARROW, string.Empty).Replace(CxUtil.ASCENDING_ARROW, string.Empty);
        }


        // Regular
        public static ListViewItem getMainItem( CxMarketStock pxStock )
        {

            // Symbol / stock
            ListViewItem lvi = new ListViewItem();
            // tag the stock on the item in case it need to be accessed
            lvi.Tag = pxStock;
            lvi.Text = pxStock.Exchange.ToString();
            lvi.UseItemStyleForSubItems = false;

            // Symbol
            ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = pxStock.Symbol;
            lvi.SubItems.Add(lvsi);

            // Bid
            lvsi = new ListViewItem.ListViewSubItem();
            if (pxStock.Bid > 0)
            {
                if (pxStock.Bid > pxStock.Price)
                {
                    lvsi.Text = CxUtil.ASCENDING_ARROW + pxStock.Bid.ToString(CxUtil.FORMAT_PRICE);
                    lvsi.ForeColor = Color.Green;
                }
                else
                {
                    lvsi.Text = pxStock.Bid.ToString(CxUtil.FORMAT_PRICE);
                }
            }
            lvi.SubItems.Add(lvsi);

            // Price 
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = pxStock.Price.ToString(CxUtil.FORMAT_PRICE);
            lvi.SubItems.Add(lvsi);

            // Ask
            lvsi = new ListViewItem.ListViewSubItem();
            if (pxStock.Ask > 0)
            {
                if (pxStock.Ask < pxStock.Price)
                {
                    lvsi.Text = CxUtil.DESCENDING_ARROW + pxStock.Ask.ToString(CxUtil.FORMAT_PRICE);
                    lvsi.ForeColor = Color.Red;
                }
                else
                {
                    lvsi.Text = pxStock.Ask.ToString(CxUtil.FORMAT_PRICE);
                }
            }
            lvi.SubItems.Add(lvsi);

            // Last Exec
            lvsi = new ListViewItem.ListViewSubItem();
            if (pxStock.LastTrade > 0)
                lvsi.Text = pxStock.LastTrade.ToString(CxUtil.FORMAT_PRICE);
            lvi.SubItems.Add(lvsi);

            // Volume
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = pxStock.Volume.ToString(CxUtil.FORMAT_PRICE);
            lvi.SubItems.Add(lvsi); 
            
            return lvi;
        }


        // Portfolio
        public static ListViewItem getMainItem(CxMarketStock pxStock, CxPortfolioStock pxPortStock, double pfChange )
        {
            // Symbol / stock
            ListViewItem lvi = new ListViewItem();
            // tag the stock on the item in case it need to be accessed
            lvi.Tag = pxStock;
            lvi.Text = pxStock.Symbol;
            lvi.UseItemStyleForSubItems = false;

            // Player
            ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = pxStock.Symbol;
            lvi.SubItems.Add(lvsi);

            // Sport
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = "Sport";
            lvi.SubItems.Add(lvsi);

            // Bid
            lvsi = new ListViewItem.ListViewSubItem();
            if (pxStock.Bid > pxStock.Price)
            {
                lvsi.Text = CxUtil.ASCENDING_ARROW + pxStock.Bid.ToString(CxUtil.FORMAT_PRICE);
                lvsi.ForeColor = Color.Green;
            }
            else
            {
                lvsi.Text = pxStock.Bid.ToString(CxUtil.FORMAT_PRICE);
            }
            lvi.SubItems.Add(lvsi);
            
            // Price 
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = pxStock.Price.ToString(CxUtil.FORMAT_PRICE);
            lvi.SubItems.Add(lvsi);
            
            // Ask
            lvsi = new ListViewItem.ListViewSubItem();
            if (pxStock.Ask < pxStock.Price)
            {
                lvsi.Text = CxUtil.DESCENDING_ARROW + pxStock.Ask.ToString(CxUtil.FORMAT_PRICE);
                lvsi.ForeColor = Color.Red;
            }
            else
            {
                lvsi.Text = pxStock.Ask.ToString(CxUtil.FORMAT_PRICE);
            }
            lvi.SubItems.Add(lvsi);
            
            // Change
            lvsi = new ListViewItem.ListViewSubItem();
            if (pxStock.Change < 0)
                lvsi.ForeColor = Color.Red;
            else
                lvsi.ForeColor = Color.Green;

            lvsi.Text = pxStock.Change.ToString(CxUtil.FORMAT_PERCENT) + "%";
            lvi.SubItems.Add(lvsi);

            // Last Exec.
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = pxStock.LastTrade.ToString(CxUtil.FORMAT_PRICE);
            lvi.SubItems.Add(lvsi);

            // Prv Close
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = pxStock.PrevClose.ToString(CxUtil.FORMAT_PRICE);
            lvi.SubItems.Add(lvsi);
            
            // Open
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = pxStock.Open.ToString(CxUtil.FORMAT_PRICE);
            lvi.SubItems.Add(lvsi);

            // Volume
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = pxStock.Volume.ToString(CxUtil.FORMAT_INT);
            lvi.SubItems.Add(lvsi);

            // Seperator
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = string.Empty;
            lvsi.BackColor = Color.Gray;
            lvi.SubItems.Add(lvsi);

            // Shares / Qty 
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = pxPortStock.Shares.ToString( CxUtil.FORMAT_DOUBLE );
            lvi.SubItems.Add(lvsi);

            // Paid
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = pxPortStock.Paid.ToString();
            lvi.SubItems.Add(lvsi);

            // Change %
            lvsi = new ListViewItem.ListViewSubItem();
            if( pfChange < 0)
                lvsi.ForeColor = Color.Red;
            else
                lvsi.ForeColor = Color.Green;
            //
            lvsi.Text = pfChange.ToString(CxUtil.FORMAT_PERCENT) + "%";
            lvi.SubItems.Add(lvsi);

            // Change $
            lvsi = new ListViewItem.ListViewSubItem();
            double gain = (pxStock.Price - pxPortStock.Paid) * pxPortStock.Shares;
            if (gain < 0)
                lvsi.ForeColor = Color.Red;
            else
                lvsi.ForeColor = Color.Green;
            lvsi.Text = "$" + gain.ToString(CxUtil.FORMAT_DOUBLE);
            lvi.SubItems.Add(lvsi);

            
            // Calculate Value
            lvsi = new ListViewItem.ListViewSubItem();
            Double value = pxStock.Price * pxPortStock.Shares;
            lvsi.Text = "$" + value.ToString(CxUtil.FORMAT_DOUBLE);
            lvi.SubItems.Add(lvsi);

            // Calculate Ownership
            lvsi = new ListViewItem.ListViewSubItem();
            value = ( pxPortStock.Shares / pxStock.TotalShares ) * 100;
            lvsi.Text = value.ToString( CxUtil.FORMAT_OWNERSHIP ) + "%";
            lvi.SubItems.Add(lvsi);

            // 
            lvsi = new ListViewItem.ListViewSubItem();
            if( pxPortStock.Ipo ) 
                lvsi.Text = ALERT_INDICATOR;
            lvi.SubItems.Add(lvsi);


            //
            return lvi;
        }

        // The buy sell row
        public static ListViewItem getStockOrderItem( CxStockOrder pxOrder, IList<CxMarketStock> plStocks  )
        {
            // The stock 
            CxMarketStock stock = CxUtil.getStock(plStocks, pxOrder.Symbol);
            
            // Symbol 
            ListViewItem lvi = new ListViewItem();
            lvi.Text = pxOrder.Symbol;
            lvi.UseItemStyleForSubItems = false;
            // set the order on the tag for menu stuff later 
            lvi.Tag = pxOrder;            

            // Player
            ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();
            // ipo orders are not available yet as an stock, just use the symbol
            if( stock == null )
                lvsi.Text = pxOrder.Symbol;
            else
                lvsi.Text = "N/A";
            lvi.SubItems.Add(lvsi);


            // Action 
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = pxOrder.Action.ToString();
            lvi.SubItems.Add(lvsi);

            // Date
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = pxOrder.AsOf.ToString(CxUtil.DATE_FORMAT + " " + CxUtil.TIME_FORMAT );
            lvi.SubItems.Add(lvsi);

            // Shares
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = pxOrder.Shares.ToString();
            lvi.SubItems.Add(lvsi);

            // Price
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = pxOrder.Price.ToString();
            lvi.SubItems.Add(lvsi);

            // Ipo
            lvsi = new ListViewItem.ListViewSubItem();
            if( pxOrder.Ipo ) 
                lvsi.Text = "X";
            lvi.SubItems.Add(lvsi);

            return lvi;
        }


        // The buy sell row
        public static ListViewItem getStockOrderItem( CxStockOrder pxBuy, CxStockOrder pxSell, Color pColor )
        {
            // BUY
            // Price
            ListViewItem lvi = new ListViewItem();
            if (pxBuy.Price > 0) lvi.Text = pxBuy.Price.ToString( CxUtil.FORMAT_PRICE );
            lvi.UseItemStyleForSubItems = false;
            lvi.ForeColor = pColor;

            // Shares
            ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();
            if (pxBuy.Price > 0) lvsi.Text = pxBuy.Shares.ToString(CxUtil.FORMAT_PRICE);
            lvsi.ForeColor = pColor; 
            lvi.SubItems.Add(lvsi);
            

            // DateTime
            lvsi = new ListViewItem.ListViewSubItem();
            if (pxBuy.Price > 0 && pxBuy.AsOf != DateTime.MinValue ) 
                lvsi.Text = pxBuy.AsOf.ToString(CxUtil.DATE_FORMAT + "  " + CxUtil.TIME_FORMAT);
            lvsi.ForeColor = pColor;
            lvi.SubItems.Add(lvsi);
            
            

            // Seperator
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = string.Empty;
            lvsi.ForeColor = pColor; 
            lvi.SubItems.Add(lvsi);
            

            
            // SELL
            // Price
            lvsi = new ListViewItem.ListViewSubItem();
            if (pxSell.Price > 0) lvsi.Text = pxSell.Price.ToString(CxUtil.FORMAT_PRICE);
            lvsi.ForeColor = pColor; 
            lvi.SubItems.Add(lvsi);
            

            // Shares
            lvsi = new ListViewItem.ListViewSubItem();
            if (pxSell.Price > 0) lvsi.Text = pxSell.Shares.ToString(CxUtil.FORMAT_PRICE);
            lvsi.ForeColor = pColor; 
            lvi.SubItems.Add(lvsi);
            

            // Time 
            lvsi = new ListViewItem.ListViewSubItem();
            if (pxSell.Price > 0 && pxSell.AsOf != DateTime.MinValue ) 
                lvsi.Text = pxSell.AsOf.ToString(CxUtil.DATE_FORMAT + "  " + CxUtil.TIME_FORMAT);
            lvsi.ForeColor = pColor;
            lvi.SubItems.Add(lvsi);

            return lvi;
        }
                

        // The total line, for the porfolio, doesn't sort 
        public static ListViewItem getTotal( double pxTotal, double pxChange, double pxPaid )
        {
            // Symbol / stock
            ListViewItem lvi = new ListViewItem();
            // tag the stock on the item in case it need to be accessed
            lvi.Tag = CxUiUtil.DONT_SORT_TAG;
            lvi.Text = FxMain.ADD_TEXT;
            lvi.UseItemStyleForSubItems = false;
            // bold it
            lvi.Font = new Font(lvi.Font, FontStyle.Bold);

            // Paid
            ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = FxMain.IMPORT_TEXT;
            lvsi.Font = new Font(lvi.Font, FontStyle.Bold);
            lvi.SubItems.Add(lvsi);

            // Filler
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = string.Empty;
            lvsi.Font = new Font(lvi.Font, FontStyle.Bold);
            lvi.SubItems.Add(lvsi);
            // Filler
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = string.Empty;
            lvi.SubItems.Add(lvsi);
            // Filler
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = string.Empty;
            lvsi.Font = new Font(lvi.Font, FontStyle.Bold);
            lvi.SubItems.Add(lvsi);
            // Filler
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = string.Empty;
            lvi.SubItems.Add(lvsi);
            // Filler
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = string.Empty;
            lvi.SubItems.Add(lvsi);
            // Filler
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = string.Empty;
            lvi.SubItems.Add(lvsi);
            // Filler
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = string.Empty;
            lvi.SubItems.Add(lvsi);
            // Filler
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = string.Empty;
            lvi.SubItems.Add(lvsi);
            // Filler
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = string.Empty;
            lvi.SubItems.Add(lvsi);
            // Filler
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = string.Empty;
            lvi.SubItems.Add(lvsi);
            // Filler
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = string.Empty;
            lvi.SubItems.Add(lvsi);

            // Price 
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = "Total";
            lvsi.Font = new Font(lvi.Font, FontStyle.Bold);
            //lvsi.Font = new Font(lvsi.Font, FontStyle.Bold);
            lvi.SubItems.Add(lvsi);


            // Change %
            lvsi = new ListViewItem.ListViewSubItem();
            if (pxChange < 0)
                lvsi.ForeColor = Color.Red;
            else
                lvsi.ForeColor = Color.Green;
            //
            lvsi.Text = pxChange.ToString( CxUtil.FORMAT_PERCENT ) + "%";
            //lvsi.Font = new Font(lvsi.Font, FontStyle.Bold);
            lvi.SubItems.Add(lvsi);


            // Gain $
            lvsi = new ListViewItem.ListViewSubItem();
            double change = pxTotal - pxPaid;
            if (change < 0)
                lvsi.ForeColor = Color.Red;
            else
                lvsi.ForeColor = Color.Green;
            lvsi.Text = "$" + change.ToString(CxUtil.FORMAT_DOUBLE);
            lvi.SubItems.Add(lvsi);

            // Value
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = "$" + pxTotal.ToString(CxUtil.FORMAT_DOUBLE);
            //lvsi.Font = new Font(lvsi.Font, FontStyle.Bold);
            lvi.SubItems.Add(lvsi);


            // empty
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = string.Empty;
            lvi.SubItems.Add(lvsi);
            // empty
            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = string.Empty;
            lvi.SubItems.Add(lvsi);

            return lvi;
        }


        // Selects index of a stock in the listview
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void selectSockInList(ListView lstViewer, string psStock)
        {
            //
            Console.WriteLine("selectSockInList " + CxUtil.getTimeString( DateTime.Now ) );
            
            try
            {
                foreach (ListViewItem item in lstViewer.Items)
                {
                    if (item.Tag is CxMarketStock && ((CxMarketStock)item.Tag).Symbol.Equals(psStock))
                    {
                        item.Selected = true;
                        break;
                    }
                    else if (item.Text.Equals(psStock))
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
            catch( Exception ex)
            {
                Console.WriteLine("selectSockInList error=" + ex );    
            }
        }

        public static void changeTabsColor(TabControl control, System.Drawing.Color color)
        {
            for (int i = 0; i < control.TabCount; i++)
                control.TabPages[i].BackColor = color;
        }

        // Show the buy sell form as a dialog 
        public static void showBuySellDialog( IWin32Window piwOwner, ExAction pxAction, string psSymbol, string psPlayerName, int piShares, Double pdPrice )
        {
            FxBuySell tradeorder = new FxBuySell(pxAction, psSymbol, psPlayerName, piShares, pdPrice );
            tradeorder.ShowDialog(piwOwner);
        }

        public static void showHiLo( IWin32Window piwOwner, CxMarketStock pxStock)
        {
            FxHiLoChart trades = new FxHiLoChart(pxStock);
            trades.Show(piwOwner);
        }

        public static void showStockDetails(CxMarketStock pxStock, bool pbForceOpen )
        {
            // Trap 
            if (!mxDetailForm.Visible && pbForceOpen == false ||
                pxStock == null || pxStock.Symbol == string.Empty ) return;
            
            //
            mxDetailForm.setStock(pxStock);
            mxDetailForm.Show();
        }

        public static void showTradeOrders(CxMarketStock pxStock)
        {
            FxTradeOrders trades = new FxTradeOrders(pxStock);
            trades.Show();
        }

        /// <summary>
        /// Add a splitter between the two given controls
        /// </summary>
        /// <param name="control1">Control 1</param>
        /// <param name="control2">Control 2</param>
        /// <param name="vertical">Vertical orientation</param>
        /// <returns>The created Splitter</returns>
        public static Splitter AddSplitter(Control control1, Control control2, bool vertical)
        {
            Control parent = control1.Parent;

            // Validate
            if (parent != control2.Parent)
                throw new ArgumentException(
                        "Both controls must be placed on the same Containter");

            if (parent.Controls.Count > 2)
                throw new ArgumentException(
                        "There may only be 2 controls on the Container");

            parent.SuspendLayout();

            // Move control2 before control1
            if (parent.Controls.IndexOf(control2) > parent.Controls.IndexOf(control1))
                parent.Controls.SetChildIndex(control2, 0);

            // Create splitter
            Splitter splitter = new Splitter();
            splitter.Dock = System.Windows.Forms.DockStyle.Left;
            splitter.BackColor = Color.DarkGray;


            // Set controls properties
            control2.Dock = DockStyle.Fill;
            if (vertical)
            {
                control1.Dock = DockStyle.Left;
                splitter.Dock = DockStyle.Left;
                splitter.Width = 6;
            }
            else
            {
                control1.Dock = DockStyle.Top;
                splitter.Dock = DockStyle.Top;
                splitter.Height = 6;
                splitter.Cursor = Cursors.HSplit;
            }

            // Add splitter to the parent in the middle
            parent.Controls.Add(splitter);
            parent.Controls.SetChildIndex(splitter, 1);
            parent.ResumeLayout();

            return splitter;
        } 


    }  // EOC
}