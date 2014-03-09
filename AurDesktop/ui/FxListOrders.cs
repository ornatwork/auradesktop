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
using org.auroracoin.desktop;
using org.auroracoin.desktop.core;

namespace org.auroracoin.desktop.ui
{
    public partial class FxListOrders : Form
    {
        // The orders 
        private IList<CxStockOrder> mlOrders = new List<CxStockOrder>();
        private static int[] iStockOrderTextColums = { 0, 1, 2, 3, 6 };
        private CxListViewColumnSorter lvwColumnSorter = new CxListViewColumnSorter(iStockOrderTextColums);
        private ExListOrderType mxType = ExListOrderType.MyExecuted;

        public FxListOrders( ExListOrderType pxType, bool pbEnableMenus, bool pbAllowCancel)
        {
            InitializeComponent();
            //
            this.ctxListMenu.Enabled = pbEnableMenus;
            this.mnuCancel.Enabled = pbAllowCancel;
            //
            mxType = pxType;
            //this.loadOrders(mxType);
            //
            this.initOrderView();
            this.FillView(this.mlOrders);
        }
        
        /*
        private void loadOrders( ExListOrderType pxType )
        {

            this.Cursor = Cursors.WaitCursor;

            if (pxType == ExListOrderType.OsLastPlaced)
            {
                // Last placed orders on OS
                this.Text = "Os Last Orders Placed";
                this.mlOrders = CxDeskUtil.getLastOsOrders();
                // Enable auto refresh
                this.chkRefresh.Enabled = this.chkRefresh.Visible = true;
            }
            else if (pxType == ExListOrderType.MyOpen)
            {
                // Get the orders 
                this.mlOrders = CxDeskUtil.getMyOrders();
                this.Text = "My Open Orders";
                // Disable auto refresh
                this.chkRefresh.Enabled = this.chkRefresh.Visible = false;
            }
            else
            {
                // Get the orders 
                this.Text = "My Executed Orders"; 
                this.mlOrders = CxDeskUtil.getMyTrades();
                // Disable auto refresh
                this.chkRefresh.Enabled = this.chkRefresh.Visible = false;
            }

            this.Cursor = Cursors.Default;
        }
        */

        private void FxStockDetail_Load(object sender, EventArgs e)
        {

            // Sort on the date
            lvwColumnSorter.SortColumn = 3;
            lvwColumnSorter.Order = SortOrder.Descending;
            // Perform the sort with these new sort options.
            this.lvOrders.Sort();
            CxUiUtil.setArrow(this.lvOrders, 3, SortOrder.Descending);
            this.lvOrders.Refresh();
            //
            CxUiUtil.shadeGrid(this.lvOrders);
        }

        // Fill the listveiw with buy and sell orders 
        private void FillView( IList<CxStockOrder> plOrders )
        {
            IList<CxMarketStock> lStocks = CxGlobal.getStocks();
            
            //
            this.lvOrders.Items.Clear();
            // Loop them
            for (int i = 0; i < plOrders.Count; i++)
            {
                // Make row out of it 
                this.lvOrders.Items.Add(CxUiUtil.getStockOrderItem(plOrders[i], lStocks) );
            }

        }


        private void initOrderView()
        {
            // Create an instance of a ListView column sorter and assign it 
            // to the ListView control.
            this.lvOrders.ListViewItemSorter = lvwColumnSorter;

            ColumnHeader colHead;

            colHead = new ColumnHeader();
            colHead.Text = "Stock";
            colHead.Width = 55;
            this.lvOrders.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Player";
            colHead.Width = 125;
            this.lvOrders.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Action";
            colHead.Width = 55;
            this.lvOrders.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Date";
            colHead.Width = 125;
            this.lvOrders.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Shares";
            colHead.Width = 55;
            this.lvOrders.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Price";
            colHead.Width = 55;
            this.lvOrders.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "IPO";
            colHead.Width = 35;
            this.lvOrders.Columns.Add(colHead);

        }


        // Sort the grid
        private void lvOrders_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            //
            this.lvOrders.BeginUpdate();

            // Perform the sort with these new sort options.
            this.lvOrders.Sort();
            //
            CxUiUtil.shadeGrid(this.lvOrders);
            CxUiUtil.setArrow(this.lvOrders, e.Column, lvwColumnSorter.Order);

            //
            this.lvOrders.EndUpdate();

        }


        private CxStockOrder getSelectedStockOrder(object sender, EventArgs e)
        {

            CxStockOrder st = new CxStockOrder();
            ListViewItem item = null;
            // Trap
            if (this.lvOrders.SelectedItems.Count == 0) return st;
            //
            item = this.lvOrders.SelectedItems[0];
            st = (CxStockOrder)item.Tag;

            //
            return st;
        }


        // Put in a buy order at OS
        private void mnuBuy_Click(object sender, EventArgs e)
        {
            /*
            this.Cursor = Cursors.WaitCursor;
            //
            CxStockOrder st = this.getSelectedStockOrder(sender, e);
            CxMarketStock stock = CxUtil.getStock(CxGlobal.getStocks(), st.Symbol);
            if( st != null)
            {
                CxUiUtil.showBuySellDialog(this, ExAction.Buy, stock.Symbol, 
                                        stock.Player, 1, 0 );
            }
            //
            this.Cursor = Cursors.Default;
             */ 
        }
        
        // Put in a sale order at OS
        private void mnuSell_Click(object sender, EventArgs e)
        {
            /*
            this.Cursor = Cursors.WaitCursor;
            //
            CxStockOrder st = this.getSelectedStockOrder(sender, e);
            CxMarketStock stock = CxUtil.getStock(CxGlobal.getStocks(), st.Symbol);
            if (st != null)
            {
                CxUiUtil.showBuySellDialog(this, ExAction.Sell, stock.Symbol, 
                                            stock.Player, 1, 0 );
            }
            //
            this.Cursor = Cursors.Default;
             */ 
        }

        // Cancel a trade order at OS
        private void mnuCancel_Click(object sender, EventArgs e)
        {
            /*
            this.Cursor = Cursors.WaitCursor;
            //
            CxStockOrder st = this.getSelectedStockOrder(sender, e);
            if (st != null)
            {
                // Cancel the order
                if (DialogResult.Yes == MessageBox.Show(this, "Do you want to cancel the order ?", "Confirm", MessageBoxButtons.YesNo))
                {
                    string success = CxDeskUtil.cancelTrade( st.OsId, st.Ipo );
                    MessageBox.Show(success, "Cancel Trade");
                }
            }
            //
            this.Cursor = Cursors.Default;
             */ 
        }

        private void mnuHiLo_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
                CxMarketStock stock = this.getStock(sender, e);
                if (stock != null)
                    CxUiUtil.showHiLo(this, stock);
            this.Cursor = Cursors.Default;
        }

        private void mnuDetails_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
                CxMarketStock stock = this.getStock(sender, e);
                if (stock != null)
                    CxUiUtil.showStockDetails(stock, true);
            this.Cursor = Cursors.Default;
        }

        private void mnuTradeOrders_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
                CxMarketStock stock = this.getStock( sender, e );
                if (stock != null)
                    CxUiUtil.showTradeOrders(stock);
            this.Cursor = Cursors.Default;
        }

        private CxMarketStock getStock(object sender, EventArgs e)
        {
            CxStockOrder st = this.getSelectedStockOrder(sender, e);
            CxMarketStock stock = CxUtil.getStock(CxGlobal.getStocks(), st.Symbol);

            return stock;
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            this.refreshOrders();
        }

        private void refreshOrders()
        {
            //this.loadOrders(mxType);
            this.FillView(this.mlOrders);
            CxUiUtil.shadeGrid(this.lvOrders);
        }


        private void chkRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkRefresh.Checked)
            {
                this.btRefresh.Enabled = false;
                this.tmRefresh.Enabled = true;
                this.tmRefresh.Interval = CxGlobal.DEFAULT_INTERVAL;
            }
            else
            {
                this.btRefresh.Enabled = true;
                this.tmRefresh.Enabled = false;
            }
        }

        private void tmRefresh_Tick(object sender, EventArgs e)
        {
            this.refreshOrders();
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
            fileContent.Append( "" + CxUtil.COMMA + CxGlobal.OS_USERNAME + CxUtil.COMMA + 
                                CxUtil.getEstTimeFromLocalTime( DateTime.Now ).ToString() + Environment.NewLine );
            // Loop the data 
            foreach (ListViewItem item in this.lvOrders.Items)
            {
                fileContent.Append(item.SubItems[0].Text + CxUtil.COMMA +
                    item.SubItems[1].Text + CxUtil.COMMA +
                    item.SubItems[2].Text + CxUtil.COMMA +
                    item.SubItems[3].Text + CxUtil.COMMA +
                    item.SubItems[4].Text + CxUtil.COMMA +
                    item.SubItems[5].Text + CxUtil.COMMA +
                    item.SubItems[6].Text + Environment.NewLine );
            }
            
            string cvsFile = CxUtil.getDataDir() + "\\orders.csv" ;
            CxUtil.writeTextToFile( cvsFile,  fileContent.ToString() );

            //
            return cvsFile;
        }

    
    }  // EOC
}
