//
using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Media;
using System.IO;
using System.Net;
using System.Web;
//
using ZedGraph;
using log4net;
//
using org.auroracoin.aurcore.util;
using org.auroracoin.aurcore.stocks;
using org.auroracoin.desktop.core;



namespace org.auroracoin.desktop.ui
{
    public partial class FxMain : Form
    {

        // The listview control needs to have Arial as it's font
        // otherwise the sort  up / down arrow won't show proper
        //-----------------------------------------------------------------------------
        private IList<CxMarketStock> mlStocks = new List<CxMarketStock>();
        private IList<CxMarketStock> mlGraphStocks = new List<CxMarketStock>();
        //        
        private List<CxPortfolioStock> mlPortfolioStocks = new List<CxPortfolioStock>();
        private DateTime mdtLastAlert = DateTime.Now.AddDays(-1);
        
        private const string TITLE = "Aurora fun - Til tunglsins";
        
        private int STRING_NOT_FOUND = -1;
        private int SEP_COLUMN = 11;
        
        double mdCash = 0;
        double mdReserveCash = 0;
        
        private static ILog Logger = LogManager.GetLogger(typeof(FxMain));

        
        // Thirty seconds, 60 is too slow, hit OS server for stocks
        private const int STOCK_INTERVAL = 10 * 1000;
        // Every 10 secs hit for alert
        private const int ALERT_INTERVAL = 10 * 1000;  


        
        //
        private const int MENU_DETAIL   = 0;
        private const int MENU_WEBSITE  = 1;
        private const int MENU_ADD      = 3;
        private const int MENU_DEL      = 4;
        private const int MENU_EDIT     = 5;
        //
        private const int SUMMARY_TAB   = 0;
        private const int PORTFOLIO_TAB = 1;
        private const int GRAPH_TAB     = 2;
        private const int ALERT_TAB     = 3;
        private const int P2Pool_TAB    = 4;
        //
        private const int ENTER_KEY     = 13;
        private const int TAB_KEY       = 9;
       
        
        // For edit controls 
        private const int SHARES_COL_MARKER = 12;
        private const int PAID_COL_MARKER   = 13;
        private const int ALERT_COL_MARKER    = 18;
        // listview sorters 
        private static int[] iStockTextColums = { 0, 1, 2 };
        private CxListViewColumnSorter lvwColumnSorter = new CxListViewColumnSorter(iStockTextColums);
        private static int[] iPortfTextColums = { 0, 1, 2, ALERT_COL_MARKER };
        private CxListViewColumnSorter lvwPortfSorter = new CxListViewColumnSorter(iPortfTextColums);
        
        //
        private ListViewItem mliSelected = new ListViewItem();
        private int miMouseX = 0;
        private int miMouseY = 0;
        //
        private double mdPortfolio_change = 0;
        private bool mbRefresh = false;
        //
        private bool mbEditMode = false;
        public const string ADD_TEXT        = "Add+";
        public const string IMPORT_TEXT     = "Update+-";
        public const string WAVE_RESOURCE   = "org.auroracoin.desktop.res.stockalert.wav";
        
        //
        private ListViewItem.ListViewSubItem mlvSubItem = new ListViewItem.ListViewSubItem();
        //
        private int miSelectedCol = 0;
        //
        SoundPlayer mSoundPlayer = null;
        

        //
        public FxMain()
        {
            InitializeComponent();

            try
            {
                // The show / exit menu when in sys tray
                this.nIcon.ContextMenu = this.cxtMenu;

                // this will show you all available resource 
                //Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
                //string[] resNames = a.GetManifestResourceNames();
                // extract the wav file
                Stream mstrWaveFile = Assembly.GetExecutingAssembly().GetManifestResourceStream(WAVE_RESOURCE);
                mSoundPlayer = new SoundPlayer(mstrWaveFile);


                // porfolio
                //MessageBox.Show("reading file=" + CxUtil.XML_FILE);
                mlPortfolioStocks = CxUtil.readXML();
                CxGlobal.NO_SOUND = CxIniFile.getInstance().readBoolKey(CxIniFile.NOSOUND_KEY, false);

                // Set the warn price
                CxGlobal.OverPrice = CxIniFile.getInstance().readDoubleKey(CxIniFile.WARN_PRICE_OVER_KEY, CxGlobal.OverPrice);
                CxGlobal.UnderPrice = CxIniFile.getInstance().readDoubleKey(CxIniFile.WARN_PRICE_UNDER_KEY, CxGlobal.UnderPrice);

                
                // init grid etc
                this.initStockView();
                this.initPortfolioView();
                this.chStocks.IsShowPointValues = true;

                //  initially two seconds
                this.tmLoop.Interval = 2 * 1000;
            }
            catch (Exception ex)
            {
                MessageBox.Show("err=" + ex );
            }
        }

        private void CxMain_Load(object sender, EventArgs e)
        {
            try
            {
                // Set the main form back to where it was left off last time 
                this.Top = CxIniFile.getInstance().readIntKey(this.Name + "_top", 0);
                this.Left = CxIniFile.getInstance().readIntKey(this.Name + "_left", 0);
                this.Width = CxIniFile.getInstance().readIntKey(this.Name + "_width", 880);
                this.Height = CxIniFile.getInstance().readIntKey(this.Name + "_height", 460);
                
                // In case there are some strange numbers in ini file
                if (this.Width < 100) this.Width = 880;
                if (this.Height < 100) this.Height = 460;
                if (this.Top < 0) this.Top = 0;
                if (this.Left < 0) this.Left = 0;
            }
            catch (Exception ex)
            {
                Logger.Error("Err=" + ex); 
            }
        }


        // Stock grid
        private void initStockView()
        {
            // Create an instance of a ListView column sorter and assign it 
            // to the ListView control.
            this.lvStocks.ListViewItemSorter = lvwColumnSorter;

            ColumnHeader colHead;
            
            colHead = new ColumnHeader();
            colHead.Text = "Exchange";
            colHead.Width = CxIniFile.getInstance().readIntKey( this.lvStocks.Name + "_0", 55);
            this.lvStocks.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Stock";
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvStocks.Name + "_1", 120);
            this.lvStocks.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Buy";
            colHead.TextAlign = HorizontalAlignment.Right;
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvStocks.Name + "_2", 55);
            this.lvStocks.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Price";
            colHead.TextAlign = HorizontalAlignment.Right;
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvStocks.Name + "_3", 50);
            this.lvStocks.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Sell";
            colHead.TextAlign = HorizontalAlignment.Right;
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvStocks.Name + "_4", 55);
            this.lvStocks.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Last Exec";
            colHead.TextAlign = HorizontalAlignment.Right;
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvStocks.Name + "_5", 65);
            this.lvStocks.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Volume" + CxUtil.DESCENDING_ARROW;
            colHead.TextAlign = HorizontalAlignment.Right;
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvStocks.Name + "_6", 65);
            this.lvStocks.Columns.Add(colHead);


            // Make sure there is no column that's set too small
            foreach (ColumnHeader colh in this.lvStocks.Columns)
                if (colh.Width < 5) colh.Width = 5;
        }

        // Portfolio grid
        private void initPortfolioView()
        {
            // Create an instance of a ListView column sorter and assign it 
            // to the ListView control.
            this.lvPortfolio.ListViewItemSorter = lvwPortfSorter;

            ColumnHeader colHead;
            //
            colHead = new ColumnHeader();
            colHead.Text = "Stock" + CxUtil.ASCENDING_ARROW;
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvPortfolio.Name + "_0", 55);
            this.lvPortfolio.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "xxxx";
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvPortfolio.Name + "_1", 120);
            this.lvPortfolio.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "xxxxx";
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvPortfolio.Name + "_2", 70);
            this.lvPortfolio.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Buy";
            colHead.TextAlign = HorizontalAlignment.Right;
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvPortfolio.Name + "_3", 50);
            this.lvPortfolio.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Price";
            colHead.TextAlign = HorizontalAlignment.Right;
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvPortfolio.Name + "_4", 50);
            this.lvPortfolio.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Sell";
            colHead.TextAlign = HorizontalAlignment.Right;
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvPortfolio.Name + "_5", 55);
            this.lvPortfolio.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Change";
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvPortfolio.Name + "_6", 60);
            colHead.TextAlign = HorizontalAlignment.Right;
            this.lvPortfolio.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Last Exec";
            colHead.TextAlign = HorizontalAlignment.Right;
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvPortfolio.Name + "_7", 65);
            this.lvPortfolio.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Prv close";
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvPortfolio.Name + "_8", 60);
            colHead.TextAlign = HorizontalAlignment.Right;
            this.lvPortfolio.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Open";
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvPortfolio.Name + "_9", 60);
            colHead.TextAlign = HorizontalAlignment.Right;
            this.lvPortfolio.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Volume";
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvPortfolio.Name + "_10", 60);
            colHead.TextAlign = HorizontalAlignment.Right;
            this.lvPortfolio.Columns.Add(colHead);

            // Seperator
            colHead = new ColumnHeader();
            colHead.Text = string.Empty;
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvPortfolio.Name + "_11", 5);
            colHead.TextAlign = HorizontalAlignment.Right;
            this.lvPortfolio.Columns.Add(colHead);

            // Portfolio info
            colHead = new ColumnHeader();
            colHead.Text = "Qty";
            colHead.TextAlign = HorizontalAlignment.Right;
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvPortfolio.Name + "_12", 50);
            this.lvPortfolio.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Paid";
            colHead.TextAlign = HorizontalAlignment.Right;
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvPortfolio.Name + "_13", 50);
            this.lvPortfolio.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Gain";
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvPortfolio.Name + "_14", 60);
            colHead.TextAlign = HorizontalAlignment.Right;
            this.lvPortfolio.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "-";
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvPortfolio.Name + "_15", 60);
            colHead.TextAlign = HorizontalAlignment.Right;
            this.lvPortfolio.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Value";
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvPortfolio.Name + "_16", 65);
            colHead.TextAlign = HorizontalAlignment.Right;
            this.lvPortfolio.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "xxxx";
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvPortfolio.Name + "_17", -2);
            this.lvPortfolio.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "xxxx";
            colHead.Width = CxIniFile.getInstance().readIntKey(this.lvPortfolio.Name + "_18", -2);
            this.lvPortfolio.Columns.Add(colHead);

            // Make sure there is no column that's set too small
            foreach (ColumnHeader colh in this.lvPortfolio.Columns)
                if (colh.Width < 5) colh.Width = 5;

        }


        private void lvStocks_ColumnClick(object sender, ColumnClickEventArgs e)
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
            this.lvStocks.BeginUpdate();

            // Perform the sort with these new sort options.
            this.lvStocks.Sort();
            //
            CxUiUtil.shadeGrid(this.lvStocks);
            CxUiUtil.setArrow(this.lvStocks, e.Column, lvwColumnSorter.Order );

            //
            //
            this.lvStocks.EndUpdate();
        }

        private void initialSort()
        {

            // Stock grid 
            // ------------------
            // Set the column number that is to be sorted; default to ascending.
            lvwColumnSorter.SortColumn = 6;
            lvwColumnSorter.Order = SortOrder.Descending;
            //
            this.lvStocks.BeginUpdate();
                // Perform the sort with these new sort options.
                this.lvStocks.Sort();
                CxUiUtil.shadeGrid(this.lvStocks);
                CxUiUtil.setArrow(this.lvStocks, 6, lvwColumnSorter.Order );
            this.lvStocks.EndUpdate();
            this.lvStocks.Refresh();

            
            // Portfolio grid 
            // ------------------
            // Set the column number that is to be sorted; default to ascending.
            this.lvwPortfSorter.SortColumn = 0;
            this.lvwPortfSorter.Order = SortOrder.Ascending;
            //
            this.lvPortfolio.BeginUpdate();
                // Perform the sort with these new sort options.
                this.lvPortfolio.Sort();
                CxUiUtil.shadeGrid(this.lvPortfolio);
                CxUiUtil.setSeperator(this.lvPortfolio, SEP_COLUMN);
                CxUiUtil.setArrow(this.lvPortfolio, 0, lvwPortfSorter.Order);
            this.lvPortfolio.EndUpdate();

        }

        
        private void tmLoop_Tick(object sender, EventArgs e)
        {
            Logger.Debug(" getStocks, " + CxUtil.getTimeString(DateTime.Now) ); 

            this.Cursor = Cursors.WaitCursor;

            //
            bool bFirstTime = false;
            if (this.tmLoop.Interval == 2000)
            {
                this.tmLoop.Interval = STOCK_INTERVAL;
                bFirstTime = true;
            }


            // Pop grid etc
            this.refreshGrid();
            if (bFirstTime)
                this.initialSort();

            // mlGraphStocks
            if (mlStocks.Count != mlGraphStocks.Count)
            {
                mlGraphStocks = mlStocks;
                this.cmbGraphStocks.DataSource = this.mlGraphStocks;
            }


            // version check
            if( bFirstTime )
            {
                // Fixit, research vista updates and 64 bit errors
                //MessageBox.Show("datapath=" + CxUtil.getDataDir());
                // 
                string sVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

                // version in the format 0.0.0.85
                //bool bNewVersion = false; 
                /*
                bool bNewVersion = CxDeskUtil.newVersionAvailable(sVersion);
                
                
                // if there is a new version attempt download and optionally 
                // install it for the user 
                if (bNewVersion)
                {
                    bool bDownloaded = false;

                    // Download a new file if neded 
                    if (!File.Exists(CxGlobal.DOWNLOAD_EXE))
                        bDownloaded = CxUtil.getFileFromUrl(CxGlobal.DOWNLOAD_URL, CxGlobal.DOWNLOAD_EXE);

                    // If download succeeded 
                    if( bDownloaded )
                    {
                        DialogResult YesNoMaybe = MessageBox.Show(NEW_VERSION, "New version", MessageBoxButtons.YesNo);
                        if (YesNoMaybe == DialogResult.Yes)
                        {
                            updateMe();
                        }
                    }
                }
                */


                // start the checking too
                this.tmCheckAlarm.Interval = ALERT_INTERVAL;
                this.tmCheckAlarm.Enabled = true;
            }
            
            //
            this.Cursor = Cursors.Default;
        }


        // This will fetch the info from the website
        // and load it into the grid and calculate the market cap/change/volume
        internal void refreshGrid()
        {
            Logger.Debug("refreshGrid, edit mode=" + mbEditMode + ", " + CxUtil.getTimeString(DateTime.Now));

            // Don't do the refreshing if the user is adding a new one, or if refresh is turned off
            if( mbEditMode || !CxGlobal.REFRESH_ON ) return;
            

            //
            mbRefresh = true;
            this.Cursor = Cursors.WaitCursor;
            int iSelected = -1;
            if(this.lvStocks.SelectedIndices.Count > 0 )  
                    iSelected = this.lvStocks.SelectedIndices[0]; 

            try
            {
                //        
                double cap = 0;
                double percent = 0;
                double price = 0;
                double volume = 0;
                //
                mlStocks = CxGlobal.getStocks();
                
                
                // The grid refresh
                this.lvStocks.BeginUpdate();
                this.lvStocks.Items.Clear();

                // Those are all of the stocks 
                for (int i = 0; i < mlStocks.Count; i++)
                {
                    CxMarketStock st = mlStocks[i];
                    // Add up market totals
                    cap += st.MarketCap;
                    percent += st.Change;
                    volume += st.Volume;
                    price += st.Price;
                    //
                    this.lvStocks.Items.Add(CxUiUtil.getMainItem(st));
                }
                
                //
                CxUiUtil.shadeGrid(this.lvStocks);
                //
                //this.lvStocks.Columns[9].Width = -2;

                // select back the stock that was already selected
                if (iSelected > -1 && this.lvStocks.Items.Count >= iSelected)
                {
                    this.lvStocks.Items[iSelected].Selected = true;
                    this.lvStocks.EnsureVisible(iSelected);
                }
                
                // Done with grid refresh 
                this.lvStocks.EndUpdate();




                // Then the portfolioGrid
                this.refreshPortfolioGrid();


                // Pop the combos
                this.cmbStocks.DataSource = this.mlStocks;

                //  Market numbers
                //*******************************************************************
                // Value
                this.lbValueText.Text = "$ " + cap.ToString(CxUtil.FORMAT_DOUBLE);

                // volume
                this.lbVolumeText.Text = "" + volume.ToString(CxUtil.FORMAT_INT);


                // update system tray icon with indicators
                this.nIcon.Text = "$ " + cap.ToString(CxUtil.FORMAT_DOUBLE) + " / " + this.lbChangeText.Text + " / " + this.lbVolumeText.Text + " / " + mdPortfolio_change.ToString(CxUtil.FORMAT_PERCENT) + "%";
            }
            catch (Exception ex)
            {
                Logger.Error("Err=" + ex); 
            }
            finally
            {
                this.lvStocks.EndUpdate();
                mbRefresh = false;
            }
            

            this.Cursor = Cursors.Default;
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
           FxAbout about = new FxAbout();
           about.ShowDialog();
        }


        private void lvPortfolio_ColumnClick(object sender, ColumnClickEventArgs e)
        {

            // Determine if clicked column is already the column that is being sorted.
            if( e.Column == this.lvwPortfSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (this.lvwPortfSorter.Order == SortOrder.Ascending)
                {
                    this.lvwPortfSorter.Order = SortOrder.Descending;
                }
                else
                {
                    this.lvwPortfSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                this.lvwPortfSorter.SortColumn = e.Column;
                this.lvwPortfSorter.Order = SortOrder.Ascending;
            }

            this.lvPortfolio.BeginUpdate();

            // Perform the sort with these new sort options.
            this.lvPortfolio.Sort();
            //
            CxUiUtil.shadeGrid(this.lvPortfolio);
            CxUiUtil.setSeperator(this.lvPortfolio, SEP_COLUMN);
            CxUiUtil.setArrow(this.lvPortfolio, e.Column, lvwPortfSorter.Order);
            
            
            this.lvPortfolio.EndUpdate();
        }

        private void showStockDetails(CxMarketStock pxStock)
        {
            CxUiUtil.showStockDetails(pxStock, true );
        }
        
        private void lvPortfolio_MouseDown(object sender, MouseEventArgs e)
        {
            mliSelected = ((ListView)sender).GetItemAt(e.X, e.Y);
            miMouseX = e.X;
            miMouseY = e.Y;
        }

        private void lvPortfolio_MouseClick(object sender, MouseEventArgs e)
        {

            // Trap
            if (this.mliSelected == null) return;


            // Check the subitem clicked .
            int nStart = this.miMouseX;
            ListView view = (ListView)sender;
            int spos = 0;
            int epos = 0;
            miSelectedCol = 0;

            // Sniff out the right column, according to width etc, also calculate location
            for (int i = 0; i < view.Columns.Count; i++)
            {
                epos += view.Columns[i].Width;
                if( nStart > spos && nStart < epos)
                {
                    miSelectedCol = i;
                    break;
                }
                spos = epos;
                
            }

            //
            mlvSubItem = this.mliSelected.SubItems[miSelectedCol];
            string subItemText = mlvSubItem.Text;
            Logger.Debug("SUB ITEM SELECTED = " + subItemText);
            

            
            // The first col is the stock symbols
            if (miSelectedCol == 0 && subItemText.Equals(FxMain.ADD_TEXT))
            {
                // In edit mode
                mbEditMode = true;

                //
                Rectangle r = new Rectangle(spos, mliSelected.Bounds.Y, epos, mliSelected.Bounds.Bottom);
                this.cmbStocks.Location = new System.Drawing.Point(spos + 3, mliSelected.Bounds.Y + 3);
                this.cmbStocks.Show();
                this.cmbStocks.SelectAll();
                this.cmbStocks.Focus();
            }
            else if( miSelectedCol == 1 && subItemText.Equals( FxMain.IMPORT_TEXT ))
            {
                /*
                this.Cursor = Cursors.WaitCursor;

                try
                {
                    IList<CxPortfolioStock> syncStocks = CxDeskUtil.getPortfolio();
                    //
                    this.mlPortfolioStocks = CxDeskUtil.mergePortfolio(syncStocks, this.mlPortfolioStocks);
                    // Save
                    CxUtil.writeXML(mlPortfolioStocks);
                    // Updates the portfolio grid info
                    this.refreshPortfolioGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                this.Cursor = Cursors.Default;
                 */ 

            }
            else if ((miSelectedCol == SHARES_COL_MARKER ||
                miSelectedCol == PAID_COL_MARKER) &&
                !mliSelected.Text.Equals(ADD_TEXT))
            {

                // In edit mode
                mbEditMode = true;
                //
                Rectangle r = new Rectangle(spos, mliSelected.Bounds.Y, epos, mliSelected.Bounds.Bottom);
                this.txEditBox.Size = new System.Drawing.Size(epos - spos, mliSelected.Bounds.Bottom - mliSelected.Bounds.Top);
                this.txEditBox.Location = new System.Drawing.Point(spos + 3, mliSelected.Bounds.Y + 3);
                this.txEditBox.Show();
                this.txEditBox.Text = subItemText;
                this.txEditBox.SelectAll();
                this.txEditBox.Focus();
            }

        }

        // Hide the edit controls 
        private void control_leave(object sender, EventArgs e)
        {
            // Hide the boxes, might as well do both
            this.txEditBox.Visible = false;
            this.cmbStocks.Visible = false;

            
            // Only add if leaving the combo box
            if(sender.Equals(this.cmbStocks))
            {
                // For example not populated yet
                if (this.cmbStocks.SelectedItem != null)
                {
                    string newst = this.cmbStocks.SelectedItem.ToString().Substring(0, 4);
                    CxPortfolioStock port = CxUtil.getPortfolioStock(this.mlPortfolioStocks, newst, false);
                    //
                    if (this.cmbStocks.SelectedItem != null &&
                        port == null)
                    {
                        mlPortfolioStocks.Add(new CxPortfolioStock(newst, "0", "0", false));
                    }
                }
            }
            else
            {
                double val = 0 ;
                try
                {
                    val = CxUtil.getDouble(this.txEditBox.Text);
                    // Don't accept negative values, just turn them into positive
                    if (val < -1) val = val * -1;
                }
                catch (Exception) { } // Don't care

                // The value
                mlvSubItem.Text = val.ToString();
                bool stock = ( mliSelected.SubItems[FxMain.ALERT_COL_MARKER].Text == CxUiUtil.ALERT_INDICATOR );
                CxPortfolioStock port = CxUtil.getPortfolioStock(this.mlPortfolioStocks, ((CxMarketStock)mliSelected.Tag).Symbol, stock);
                // Set the correct value, depending on col
                if (miSelectedCol == SHARES_COL_MARKER )
                    port.Shares = val;
                else if (miSelectedCol == PAID_COL_MARKER )
                    port.Paid = val;

            }
            
            // Save
            CxUtil.writeXML( mlPortfolioStocks );
            // Updates the portfolio grid info
            this.refreshPortfolioGrid();
            
            
            // done
            mbEditMode = false;
        }



        private void cmbStocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.Debug("index=" + this.cmbStocks.SelectedIndex);
        }


        private void lvPortfolio_KeyDown(object sender, KeyEventArgs e)
        {
            // Delete stock from grid
            if (e.KeyCode == Keys.Delete)
            {
                this.RemovePortfolioStock();
            }
        }

        private void RemovePortfolioStock()
        {
            //
            if (this.lvPortfolio.SelectedItems.Count > 0)
            {
                ListViewItem tmp = this.lvPortfolio.SelectedItems[0];
                CxUtil.removePortfolioStock(this.mlPortfolioStocks, ((CxMarketStock)tmp.Tag).Symbol );

                // Save
                CxUtil.writeXML(mlPortfolioStocks);
                //
                this.refreshPortfolioGrid();
            }
        }


        // updates the portfolio grid 
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void refreshPortfolioGrid()
        {
            //
            Logger.Debug("refreshPortfolioGrid " + CxUtil.getTimeString(DateTime.Now));

            double portfolio_price = 0;
            double portfolio_paid = 0;
            mdPortfolio_change = 0;
            string sRememberPortfStock = string.Empty;

            // What stock is selected before the refresh
            if (this.lvPortfolio.SelectedItems.Count > 0)
            {
                if (this.lvPortfolio.SelectedItems[0].Tag.ToString().Equals(CxUiUtil.DONT_SORT_TAG))
                    sRememberPortfStock = ((ListViewItem)this.lvPortfolio.SelectedItems[0]).Text;
                else
                    sRememberPortfStock = ((CxMarketStock)this.lvPortfolio.SelectedItems[0].Tag).Symbol;
            }

            // Sniff out the cash totals
            for (int i = this.mlPortfolioStocks.Count - 1; i > -1; i--)
            {
                CxPortfolioStock thestock = mlPortfolioStocks[i];
                if (thestock.Info)
                {
                    if (thestock.Symbol == CxPortfolioStock.CASH)
                        mdCash = thestock.Paid;
                    if (thestock.Symbol == CxPortfolioStock.RESV_CASH)
                        mdReserveCash = thestock.Paid;

                    // Remove the fake cash stocks
                    this.mlPortfolioStocks.RemoveAt(i);
                }
            }

            
            //
            this.lvPortfolio.BeginUpdate();
            this.lvPortfolio.Items.Clear();

            // Update each portfolio stock
            foreach (CxPortfolioStock loopStock in this.mlPortfolioStocks)
            {
                // Portfolio
                CxMarketStock stock = CxUtil.getStock(this.mlStocks, loopStock.Symbol);
                if (stock != null)
                {
                    // Figure out the change in price, paid vs current price
                    // make sure not to have infinity number
                    double pfchange = CxUtil.getStockPercentChange(
                                                loopStock.Paid, stock.Price);

                    ListViewItem tmp = CxUiUtil.getMainItem(stock, loopStock, pfchange);
                    this.lvPortfolio.Items.Add(tmp);

                    // The value 
                    double priceval = stock.Price * loopStock.Shares;
                    double paidval = loopStock.Paid * loopStock.Shares;
                    portfolio_price += priceval;
                    portfolio_paid += paidval;
                }
            }

            // The total % from dollar amount of the portfolio
            // If devided by zero ( nothiing in portfolio yet ), set it to value zero
            mdPortfolio_change = CxUtil.getPercentChange(portfolio_paid, portfolio_price);

            // The total row
            this.lvPortfolio.Items.Add(CxUiUtil.getTotal(portfolio_price, mdPortfolio_change, portfolio_paid));

            //
            this.lvPortfolio.Columns[5].Width = -2;
            CxUiUtil.shadeGrid(this.lvPortfolio);
            CxUiUtil.setSeperator(this.lvPortfolio, SEP_COLUMN);
            

            // The one to select in the grid
            if (!sRememberPortfStock.Equals(string.Empty))
                CxUiUtil.selectSockInList(this.lvPortfolio, sRememberPortfStock);

            //
            this.lvPortfolio.EndUpdate();
        }


        // Tabs flipped, let's choose the right listview, so the selected item shows
        private void tpPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Defaults 
            this.cmbGraphStocks.Visible = false;
            this.lblCtrlKey.Visible = false;



            // Diff things for diff tabs
            if (tpPanel.SelectedIndex == SUMMARY_TAB)
            {
                this.lvStocks.Focus();
            }
            else if (tpPanel.SelectedIndex == PORTFOLIO_TAB)
            {
                this.lvPortfolio.Focus();
            }
            else if (tpPanel.SelectedIndex == GRAPH_TAB)
            {
                this.lblCtrlKey.Visible = true;
                this.cmbGraphStocks.Visible = true;
            }
            
        }

        private void mnuTutorial_Click(object sender, EventArgs e)
        {
            /*
            this.Cursor = Cursors.WaitCursor;
            using (System.Diagnostics.Process.Start("")) { }
            this.Cursor = Cursors.Default;
             */
        }


        // Enter key needs to trigger entry / leave 
        private void txEditBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ENTER_KEY)
                this.control_leave(this, new EventArgs());
        }


        //
        public void btStop_Click(object sender, EventArgs e)
        {
            this.stopAlert();
        }

        public void stopAlert()
        {
                        // Stops the alert mode
            this.Text = TITLE;
            
            //
            this.tmPlayAlarmSound.Enabled = false;
            this.BackColor = Color.LightGray;
            this.tpPanel.Visible = true;
            this.TopMost = false;
            // set combo true if on the graph tab
            if (tpPanel.SelectedIndex == GRAPH_TAB)
                this.cmbGraphStocks.Visible = true;
            
            // Disable the alert song
            this.mSoundPlayer.Stop();
            this.mdtLastAlert = DateTime.Now;
            //  Back to the normal loop
            CxGlobal.REFRESH_ON = true;
            this.tmLoop.Enabled = CxGlobal.REFRESH_ON;
            this.tmCheckAlarm.Enabled = CxGlobal.REFRESH_ON;

        }

        private void FxMain_Resize(object sender, EventArgs e)
        {
            if( FormWindowState.Minimized == this.WindowState )
            {
                this.ShowInTaskbar = true;
                this.Hide();
            }
        }

        private void nIcon_DoubleClick(object sender, EventArgs e)
        {
            this.showMe();
        }

        private void showMe()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }


        // Menus
        private void mnuShow_Click(object sender, EventArgs e)
            { this.showMe(); }
        private void mnuExit_Click(object sender, EventArgs e)
            { this.Close(); }
        private void mnuShowApp_Click(object sender, EventArgs e)
            { this.showMe(); }
        private void mnuExitApp_Click(object sender, EventArgs e)
            { this.Close(); }


        // Build the Chart
        private void createGraph(ZedGraphControl zg1, string psSymbol, CxMarketStockPointPairList pxPoints, bool pbClear)
        {
            // Get a reference to the GraphPane
            GraphPane myPane = zg1.GraphPane;
            Color graphColor = CxUtil.getRandomColor();

            if (pbClear)
            {
                myPane.CurveList.Clear();
                graphColor = Color.Black;
            }

            // Set the titles
            myPane.Title.Text = string.Empty;
            myPane.XAxis.Title.Text = string.Empty;
            myPane.YAxis.Title.Text = string.Empty;

            // symbols, and in the legend
            CurveItem myCurve = myPane.AddCurve( psSymbol, pxPoints.List,
                                                    graphColor, SymbolType.None);

            // Set the XAxis to date type
            myPane.XAxis.Type = AxisType.Date;

            // Tell ZedGraph to refigure the axes since the data 
            // has changed
            zg1.AxisChange();
            zg1.Refresh();
        }

        private static bool updateMe()
        {
            try
            {
                System.Diagnostics.Process.Start(CxGlobal.THE_UPDATER);
                //
                Application.Exit();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Err=" + ex); 
            }

            return false;
        }

        private void cmbGraphStocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 
            this.Cursor = Cursors.WaitCursor;
            //
            string symbol = this.cmbGraphStocks.Text.Substring(0, 4);
            string thexml = CxUtil.getWebPage("");
            CxMarketStockPointPairList points = new CxMarketStockPointPairList(symbol, thexml);
            // Is the control key on...
            this.createGraph(this.chStocks, symbol, points, Control.ModifierKeys != Keys.Control);
            this.chStocks.Refresh();

            this.Cursor = Cursors.Default;
        }

        private void tmCheckForAlarm_Tick(object sender, EventArgs e)
        {
            Logger.Debug("Alert timer, " + CxUtil.getTimeString(DateTime.Now));
            
            // Give 5 minutes before resetting 
            if (DateTime.Now.Subtract(mdtLastAlert) > new TimeSpan(0, 1, 0) && CxGlobal.LastThreeCryptsyTrades > 0)
            {

                // Is it in range 
                if (CxGlobal.LastThreeCryptsyTrades > CxGlobal.OverPrice  || CxGlobal.LastThreeCryptsyTrades < CxGlobal.UnderPrice)
                {
                    // Play the siren, unless the user does not want it
                    if (CxGlobal.NO_SOUND == false)
                        mSoundPlayer.Play();
                    
                    // Then activate it on the looper
                    this.tmPlayAlarmSound.Enabled = true;

                    // Not anymore, it's already showing 
                    this.TopMost = false;

                    // Show 
                    this.showMe();
                    this.tpPanel.SelectedIndex = FxMain.ALERT_TAB;

                    this.lbAlertMsg.Text = "Last three trades on Cryptsy average price = " + CxGlobal.LastThreeCryptsyTrades.ToString(CxUtil.FORMAT_PRICE);
                
                } // checkFor

            }
        }

        
        // Stock detail        
        private void mnuDetails_Click(object sender, EventArgs e)
        {
            CxMarketStock st = this.getSelectedStock(sender, e);
            if (st != null) this.showStockDetails(st);
        }
        
        // Navigate to stock trading page
        private void mnuStockTrading_Click(object sender, EventArgs e)
        { 
            this.gotoStockPage(this.getSelectedStock(sender, e)); 
        }
        
        private void listView_DoubleClick(object sender, EventArgs e)
        { 
            this.gotoStockPage(this.getSelectedStock(sender, e)); 
        }

        // Directs a browser to the stock trading page
        private void gotoStockPage( CxMarketStock pxStock )
        {
            if (pxStock != null)
            {
                this.Cursor = Cursors.WaitCursor;
                CxUiUtil.showTradeOrders(pxStock);
                this.Cursor = Cursors.Default;
            }
        }

        private CxMarketStock getSelectedStock(object sender, EventArgs e )
        {

            CxMarketStock st = null;
            ListViewItem item = null;
            //
            if (tpPanel.SelectedIndex == SUMMARY_TAB)
            {
                if (this.lvStocks.SelectedItems.Count == 0) return new CxMarketStock();
                item = this.lvStocks.SelectedItems[0];
            }
            else if (tpPanel.SelectedIndex == PORTFOLIO_TAB)
            {
                if (this.lvPortfolio.SelectedItems.Count == 0) return new CxMarketStock();
                item = this.lvPortfolio.SelectedItems[0];
            }
            
            // Watch out for the total line, non stocks
            if (item.Tag != null && item.Tag is CxMarketStock )
                st = (CxMarketStock)item.Tag;
            
            //
            return st;
        }

        private void ctxListMenu_Opening(object sender, CancelEventArgs e)
        {
            //
            if (tpPanel.SelectedIndex == SUMMARY_TAB)
                this.mnuRemove.Enabled = false;
            else if (tpPanel.SelectedIndex == PORTFOLIO_TAB)
                this.mnuRemove.Enabled = true;
        }

        private void mnuRemove_Click(object sender, EventArgs e)
        {
            this.RemovePortfolioStock();
        }
        
        private void mnuSell_Click(object sender, EventArgs e)
        {
            /*
            CxMarketStock st = this.getSelectedStock(sender, e);
            if (st != null)
            {
                this.Cursor = Cursors.WaitCursor;
                CxUiUtil.showBuySellDialog(this, ExAction.Sell, st.Symbol, 
                                            st.Player, 1, 0 );
                this.Cursor = Cursors.Default;
            }
            */
        }

        private void mnuBuy_Click(object sender, EventArgs e)
        {
            /*
            CxMarketStock st = this.getSelectedStock(sender, e);
            if (st != null)
            {
                this.Cursor = Cursors.WaitCursor;
                CxUiUtil.showBuySellDialog(this, ExAction.Buy, st.Symbol, 
                                            st.Player, 1, 0 );
                this.Cursor = Cursors.Default;
            }
             */ 
        }

        private void mnuConfig_Click(object sender, EventArgs e)
        {
            FxConfig conf = new FxConfig();
            conf.Show(this);
        }

        private void mnuHiLo_Click(object sender, EventArgs e)
        {
            this.showHiLo(this.getSelectedStock(sender, e));
        }

        private void showHiLo( CxMarketStock pxStock )
        {
            this.Cursor = Cursors.WaitCursor;
            CxUiUtil.showHiLo( this, pxStock);
            this.Cursor = Cursors.Default;
        }

        private void mnuMyOrders_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            // show them
            FxListOrders list = new FxListOrders( ExListOrderType.MyOpen , true, true );
            list.Show( this );

            this.Cursor = Cursors.Default;
        }

        private void mnuExecutedOrders_Click(object sender, EventArgs e)
        {
            
            this.Cursor = Cursors.WaitCursor;

            // show them
            FxListOrders list = new FxListOrders(ExListOrderType.MyExecuted, true, false );
            list.Show(this);

            this.Cursor = Cursors.Default;

        }

        private void mnuOsLatestExecuted_Click(object sender, EventArgs e)
        {

            /*
            this.Cursor = Cursors.WaitCursor;
            // show them
            FxListOrders list = new FxListOrders( ExListOrderType.OsLastPlaced, true, false );
            list.Show(this);

            this.Cursor = Cursors.Default;
             */ 
        }

        private void mnuBuySell_Click(object sender, EventArgs e)
        {
            /*
            this.Cursor = Cursors.WaitCursor;
            FxBuySell tradeorder = new FxBuySell();
            tradeorder.ShowDialog();
            this.Cursor = Cursors.Default;
             */ 
        }

        private void mnuMc_Click(object sender, EventArgs e)
        {
            /* 
            this.Cursor = Cursors.WaitCursor;

            this.tpPanel.SelectedIndex = GRAPH_TAB;
            CxMarketStockPointPairList points = CxDeskUtil.getMarketCapList();
            this.createGraph(this.chStocks, "Market Cap", points, true);

            this.Cursor = Cursors.Default;
             */ 
        }

        private void tmPlayAlarmSound_Tick(object sender, EventArgs e)
        {
            // Play the siren, unless the user does not want it
            if (CxGlobal.NO_SOUND == false)
                mSoundPlayer.Play();
        }




        private void lvStocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.refreshDetails( sender, e );
        }
        private void lvPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.refreshDetails( sender, e );
        }

        // Refresh the detail screen with the info about the choosen stock
        private void refreshDetails(object sender, EventArgs e )
        {
            // If the timer is doing a refresh there is no need
            // to refresh the details screen again 
            if (!mbRefresh)
            {
                CxMarketStock st = this.getSelectedStock(sender, e);
                CxUiUtil.showStockDetails(st, false);
            }
        }

        private void FxMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Don't save if in minimized state 
                if (this.WindowState != FormWindowState.Minimized)
                {
                    // Memorize the main form location
                    CxIniFile.getInstance().writeIntKey(this.Name + "_top", this.Top);
                    CxIniFile.getInstance().writeIntKey(this.Name + "_left", this.Left);
                    CxIniFile.getInstance().writeIntKey(this.Name + "_width", this.Width);
                    CxIniFile.getInstance().writeIntKey(this.Name + "_height", this.Height);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Err=" + ex); 
            }

            try
            {
                // Don't save if in minimized state 
                if (this.WindowState != FormWindowState.Minimized)
                {
                    // Memorize the stock grid columns widths
                    CxIniFile.getInstance().writeIntKey(this.lvStocks.Name + "_0", this.lvStocks.Columns[0].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvStocks.Name + "_1", this.lvStocks.Columns[1].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvStocks.Name + "_2", this.lvStocks.Columns[2].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvStocks.Name + "_3", this.lvStocks.Columns[3].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvStocks.Name + "_4", this.lvStocks.Columns[4].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvStocks.Name + "_5", this.lvStocks.Columns[5].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvStocks.Name + "_6", this.lvStocks.Columns[6].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvStocks.Name + "_7", this.lvStocks.Columns[7].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvStocks.Name + "_8", this.lvStocks.Columns[8].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvStocks.Name + "_9", this.lvStocks.Columns[9].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvStocks.Name + "_10", this.lvStocks.Columns[10].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvStocks.Name + "_11", this.lvStocks.Columns[11].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvStocks.Name + "_12", this.lvStocks.Columns[12].Width);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Err=" + ex);
            }

            try
            {
                // Don't save if in minimized state 
                if (this.WindowState != FormWindowState.Minimized)
                {
                    // Memorize the portfolio grid columns widths
                    CxIniFile.getInstance().writeIntKey(this.lvPortfolio.Name + "_0", this.lvPortfolio.Columns[0].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvPortfolio.Name + "_1", this.lvPortfolio.Columns[1].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvPortfolio.Name + "_2", this.lvPortfolio.Columns[2].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvPortfolio.Name + "_3", this.lvPortfolio.Columns[3].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvPortfolio.Name + "_4", this.lvPortfolio.Columns[4].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvPortfolio.Name + "_5", this.lvPortfolio.Columns[5].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvPortfolio.Name + "_6", this.lvPortfolio.Columns[6].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvPortfolio.Name + "_7", this.lvPortfolio.Columns[7].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvPortfolio.Name + "_8", this.lvPortfolio.Columns[8].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvPortfolio.Name + "_9", this.lvPortfolio.Columns[9].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvPortfolio.Name + "_10", this.lvPortfolio.Columns[10].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvPortfolio.Name + "_11", this.lvPortfolio.Columns[11].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvPortfolio.Name + "_12", this.lvPortfolio.Columns[12].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvPortfolio.Name + "_13", this.lvPortfolio.Columns[13].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvPortfolio.Name + "_14", this.lvPortfolio.Columns[14].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvPortfolio.Name + "_15", this.lvPortfolio.Columns[15].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvPortfolio.Name + "_16", this.lvPortfolio.Columns[16].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvPortfolio.Name + "_17", this.lvPortfolio.Columns[17].Width);
                    CxIniFile.getInstance().writeIntKey(this.lvPortfolio.Name + "_18", this.lvPortfolio.Columns[18].Width);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Err=" + ex);
            }
            
        }

        private void mnuGameReport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            FxGameReport gRpt = new FxGameReport(this.mlPortfolioStocks);
            gRpt.ShowDialog();
        }



    }  // EOC
}
