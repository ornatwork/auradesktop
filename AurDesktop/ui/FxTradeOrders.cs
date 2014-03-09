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
using ZedGraph;
//
using org.auroracoin.aurcore.stocks;
using org.auroracoin.aurcore.util;
using org.auroracoin.desktop.core;



namespace org.auroracoin.desktop.ui
{
    public partial class FxTradeOrders : Form
    {
        // The stock that orders are going to be displayed for
        private CxMarketStock mxStock;
        private List<CxStockOrder> mlExecuted = new List<CxStockOrder>();
        private const int SEP_COL = 3;
        private const int REFRESH_RATE = 1000 * 15;
        IList<CxStockOrder> mlOrders = new List<CxStockOrder>();
        

        public FxTradeOrders(CxMarketStock pxStock)
        {
            InitializeComponent();
            //
            mxStock = pxStock;
            this.Text += mxStock.ToString();
            //
            CxUiUtil.AddSplitter(this.pnlExecuted, this.pnlOpenOrders, false);
        }

        private void FxTradeOrders_Load(object sender, EventArgs e)
        {
            //
            this.initOrderView(this.lvOrders);
            this.initOrderView(this.lvExecuted);
            // Set the stock for the background worker and get the first time refresh
            mlOrders = CxGlobal.addOrderRequestStock(mxStock);
            this.refreshTrades( true );
            
            //
            this.tmOrders.Interval = REFRESH_RATE;
            this.tmOrders.Enabled = true;
        }

        // Fill the listveiw with buy and sell orders 
        private void FillView( ListView plvTheView, IList<CxStockOrder> plOrders, bool pbExecuted, Color pColor )
        {
            // Buy and sells
            IList<CxStockOrder> plBuy = new List<CxStockOrder>();
            IList<CxStockOrder> plSell = new List<CxStockOrder>();

            // Split out to two lists
            foreach (CxStockOrder ord in plOrders)
            {
                // Don't process executed orders
                if (ord.Executed != pbExecuted ) continue;
                
                // buy or sell
                if (ord.Action == ExAction.Buy) 
                    plBuy.Add( ord );
                else
                    plSell.Add( ord );
            }

            CxStockOrder buy;
            CxStockOrder sell;
            // Max rows from either list 
            int rows = plBuy.Count > plSell.Count ? plBuy.Count : plSell.Count;
            
            // Loop them
            for (int i = 0; i < rows; i++)
            {
                buy = new CxStockOrder();
                sell = new CxStockOrder();
                
                // Only set if there is an order
                if (i < plBuy.Count)
                    buy = plBuy[i];
                if (i < plSell.Count)
                    sell= plSell[i];
                
                // Make row out of it 
                plvTheView.Items.Add( CxUiUtil.getStockOrderItem(buy, sell, pColor ) );
            }

        }


        private void initOrderView( ListView plvTheView )
        {
            // Create an instance of a ListView column sorter and assign it 
            // to the ListView control.
            //this.lvOrders.ListViewItemSorter = lvwColumnSorter;

            ColumnHeader colHead;

            colHead = new ColumnHeader();
            colHead.Text = "Buy";
            colHead.Width = 55;
            plvTheView.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Qty";
            colHead.Width = 55;
            plvTheView.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Date/Time";
            colHead.TextAlign = HorizontalAlignment.Left;
            colHead.Width = 120;
            plvTheView.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = string.Empty;
            colHead.Width = 5;
            plvTheView.Columns.Add(colHead);
            
            colHead = new ColumnHeader();
            colHead.Text = "Sell";
            colHead.Width = 55;
            plvTheView.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Qty";
            colHead.Width = 55;
            plvTheView.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Date/Time";
            colHead.Width = 120;
            colHead.TextAlign = HorizontalAlignment.Left;
            plvTheView.Columns.Add(colHead);

        }

        private void tmOrders_Tick(object sender, EventArgs e)
        {
            // Dont' refresh if it has been shut off, for example when IPO hits
            if( CxGlobal.REFRESH_ON )
                this.refreshTrades( false );
        }

        // reloads the trade information  
        private void refreshTrades( bool pbFirstTime )
        {
            // Clear out the holders 
            this.mlExecuted.Clear();
            
            // Gets open stock orders for the specific stock
            if (!pbFirstTime)
            {
                // Make sure new list is populated, sometimes it's not, 
                // if there is a bad conneciton to the server for example
                IList<CxStockOrder> lOrders = CxGlobal.getStockOrders(this.mxStock);
                //
                if (lOrders.Count > 0)
                    mlOrders = lOrders;
            }
            
            // Now start filling the controls
            string sRememberSummaryStock = string.Empty;

            int iSelected = -1;
            if (this.lvOrders.SelectedIndices.Count > 0)
                iSelected = this.lvOrders.SelectedIndices[0]; 

            // Orders
            this.lvOrders.BeginUpdate();
            this.lvOrders.Items.Clear();
            this.FillView(this.lvOrders, mlOrders, false, Color.Black );
            CxUiUtil.shadeGrid(this.lvOrders);
            CxUiUtil.setSeperator(this.lvOrders, SEP_COL);
            this.lvOrders.EndUpdate();

            // select back the stock that was already selected
            if( iSelected > -1 && this.lvOrders.Items.Count >= iSelected )
            {
                this.lvOrders.Items[iSelected].Selected = true;
                this.lvOrders.EnsureVisible(iSelected);
                // Calculate the buy sell orders from selected row up
                this.setBuysSells(iSelected);
            }


            // The executed grid 
            this.mlExecuted = getExecutedOrders(mlOrders);
            this.lvExecuted.BeginUpdate();
            this.lvExecuted.Items.Clear();
            this.FillView(this.lvExecuted, mlExecuted, true, Color.Gray );
            CxUiUtil.shadeGrid(this.lvExecuted);
            CxUiUtil.setSeperator(this.lvExecuted, SEP_COL);
            this.lvExecuted.EndUpdate();

            // Executed chart
            this.mlExecuted.Sort();
            CxMarketStockPointPairList points = new CxMarketStockPointPairList(mxStock.Symbol, mlExecuted);
            this.chExcutedOrders.IsShowPointValues = true;
            this.createGraph(this.chExcutedOrders, points);
        }

        private List<CxStockOrder> getExecutedOrders(IList<CxStockOrder> plOrders)
        {
            List<CxStockOrder> rList = new List<CxStockOrder>();
            // Split out to two lists
            foreach (CxStockOrder ord in plOrders)
            {
                if( ord.Executed ) 
                    rList.Add( ord );
            }
        
            return rList;
        }

        // Add upp buy and sells and display them 
        private void setBuysSells( int piSelected )
        {
            // Trap
            if (piSelected == -1) return;


            double dBuyShares = 0;
            double dBuyValue = 0;
            double dSellShares = 0;
            double dSellValue = 0;
            double tmp = 0;
            //
            for (int i=0; i<piSelected+1; i++)
            {
                ListViewItem item = this.lvOrders.Items[i];
                // Buys
                tmp = CxUtil.getDouble(item.SubItems[1].Text);
                dBuyShares += tmp;
                dBuyValue += CxUtil.getDouble(item.SubItems[0].Text) * tmp;
                // Sells
                tmp = CxUtil.getDouble(item.SubItems[5].Text);
                dSellShares += tmp;
                dSellValue += CxUtil.getDouble(item.SubItems[4].Text) * tmp;
            }

            // Buys 
            this.lbBuysShares.Text = "$" + dBuyValue.ToString(CxUtil.FORMAT_DOUBLE) + " / " + dBuyShares.ToString(CxUtil.FORMAT_INT);
            // Sells
            this.lbSellsShares.Text = "$" + dSellValue.ToString(CxUtil.FORMAT_DOUBLE) + " / " + dSellShares.ToString(CxUtil.FORMAT_INT);
        }

        // Build the Chart
        private void createGraph(ZedGraphControl zg1, CxMarketStockPointPairList pxPoints)
        {

            // Get a reference to the GraphPane
            GraphPane myPane = zg1.GraphPane;
            // First clear it
            myPane.CurveList.Clear();

            // Set the titles
            myPane.Title.Text = string.Empty;
            myPane.XAxis.Title.Text = string.Empty;
            myPane.YAxis.Title.Text = string.Empty;

            // symbols, and in the legend
            CurveItem myCurve = myPane.AddCurve(string.Empty, pxPoints.List,
                                                    Color.Black, SymbolType.None);

            // Set the XAxis to date type
            myPane.XAxis.Type = AxisType.Date;

            // Tell ZedGraph to refigure the axes since the data 
            // has changed
            zg1.AxisChange();
            zg1.Refresh();
        }

        private void mnuBuy_Click(object sender, EventArgs e)
        {
            /*
            this.Cursor = Cursors.WaitCursor;
            CxUiUtil.showBuySellDialog(this, ExAction.Buy, this.mxStock.Symbol,
                this.mxStock.Player, 1, 0 );
            this.Cursor = Cursors.Default;
            */
        }

        private void mnuSell_Click(object sender, EventArgs e)
        {
            /*
            this.Cursor = Cursors.WaitCursor;
            CxUiUtil.showBuySellDialog(this, ExAction.Sell, this.mxStock.Symbol,
                this.mxStock.Player, 1, 0 );
            this.Cursor = Cursors.Default;
             */ 
        }

        private void mnuHiLo_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CxUiUtil.showHiLo(this, this.mxStock);
            this.Cursor = Cursors.Default;
        }

        private void mnuDetails_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CxUiUtil.showStockDetails(this.mxStock, true);
            this.Cursor = Cursors.Default;
        }

        private void lvOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Recalc the buys / sells 
            if (this.lvOrders.SelectedIndices.Count > 0)
                this.setBuysSells(this.lvOrders.SelectedIndices[0]);
        }

        private void FxTradeOrders_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Remove from update queue
            CxGlobal.removeRequestStock(this.mxStock);
        }

        private void rdChart_CheckedChanged(object sender, EventArgs e)
        {
            this.chExcutedOrders.Visible = this.rdChart.Checked; 
            this.lvExecuted.Visible = !this.rdChart.Checked;
        }

       
    }  // EOC
}
