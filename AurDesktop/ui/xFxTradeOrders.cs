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
        }

        private void FxTradeOrders_Load(object sender, EventArgs e)
        {
            //
            
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
            colHead.Text = "Quantity";
            colHead.Width = 80;
            plvTheView.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Date/Time";
            colHead.TextAlign = HorizontalAlignment.Left;
            colHead.Width = 120;
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
