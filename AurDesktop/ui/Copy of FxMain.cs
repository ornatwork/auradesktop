//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using System.Reflection;
//
using com.seasonalerts.desktop.util;
using com.seasonalerts.desktop.stocks;


namespace com.seasonalerts.desktop.ui
{
    public partial class FxMain : Form
    {

        // The listview control needs to have Arial as it's font
        // otherwise the sort  up / down arrow won't show proper

        //
        private IList<CxStock> mlStocks = new List<CxStock>();
        private const string MARKET_INFO_URL = "http://www.oneseason.com/services/feeds/tradingInformation/?api_key=";
        private const string API_KEY = "f1939fd188dacc90a09f75faa96e3db1";
        private const string XML_FORMAT = "&format=xml";
        private const string NEW_VERSION = "There is a new version of the software available, would you like to download it ?";
        private const string VERSION_CHECK_URL = "http://www.seasonalerts.com/cgi-bin/app/version.cgi";
        private const char PIPE = '|';
        private const char AND = '&';
        
        
        //        
        private CxListViewColumnSorter lvwColumnSorter = new CxListViewColumnSorter();
		
				
        //
        public FxMain()
        {
            InitializeComponent();
            
            // init grid etc
            this.init();
            //  initially two seconds
            this.tmLoop.Interval = 2 * 1000;
        }

        private void CxMain_Load(object sender, EventArgs e)
        {
        }

        private void init()
        {
            // Create an instance of a ListView column sorter and assign it 
            // to the ListView control.
            this.lvStocks.ListViewItemSorter = lvwColumnSorter;

            //Console.WriteLine(this.lvStocks.Columns.Count);

            ColumnHeader colHead;
            //
            colHead = new ColumnHeader();
            colHead.Text = "Stock" + CxUiUtil.ASCENDING_ARROW;
            colHead.Width = 70;
            this.lvStocks.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Price";
            colHead.Width = 50;
            this.lvStocks.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Change";
            colHead.Width = 75;
            this.lvStocks.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Value";
            colHead.Width = 75;
            this.lvStocks.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Volume";
            //colHead.Width = this.lvStocks.Width - ( 70+50+75+75+5);
            colHead.Width = -2;
            //colHead.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            //
            this.lvStocks.Columns.Add(colHead);

        }

        private void btLoad_Click(object sender, EventArgs e)
        {

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
            // 
            CxUiUtil.setArrow(this.lvStocks, e, lvwColumnSorter.Order );

            //
            //
            this.lvStocks.EndUpdate();
        }

        private void initialSort()
        {
            // Set the column number that is to be sorted; default to ascending.
            lvwColumnSorter.SortColumn = 0;
            lvwColumnSorter.Order = SortOrder.Ascending;

            this.lvStocks.BeginUpdate();

            // Perform the sort with these new sort options.
            this.lvStocks.Sort();
            //
            CxUiUtil.shadeGrid(this.lvStocks);
            // 
            CxUiUtil.setArrow(this.lvStocks, new ColumnClickEventArgs(0), lvwColumnSorter.Order );
            //
            this.lvStocks.EndUpdate();
        }

        private void tmLoop_Tick(object sender, EventArgs e)
        {
            //
            bool bFirstTime = false;
            if (this.tmLoop.Interval == 2000)
            {
                this.tmLoop.Interval = 30 * 1000;
                bFirstTime = true;
            }

            this.refreshGrid();
            if (bFirstTime) this.initialSort();


            // set interval to 30 sec if here for the first time
            if( bFirstTime )
            {
                // 
                string sVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                                             // version in the format 0.0.0.85
                bool bNewVersion = this.newVersionAvailable( sVersion );
                if (bNewVersion)
                {
                    DialogResult YesNoMaybe = MessageBox.Show(NEW_VERSION, "New version", MessageBoxButtons.YesNo );
                    if (YesNoMaybe == DialogResult.Yes)
                        using (System.Diagnostics.Process.Start(FxAbout.DOWNLOAD_URL)) { }
                }

            }



        }

        // This will fetch the one season info from the website
        // and load it into the grid and calculate the market cap/change/volume
        private void refreshGrid()
        {
            //
            this.Cursor = Cursors.WaitCursor;

            try
            {
                mlStocks = new List<CxStock>();

                // Get the xml from the site
                string thexml = CxUtil.getWebPage( MARKET_INFO_URL + API_KEY + XML_FORMAT );
                //Console.WriteLine("look=" + thexml);
                //
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(thexml);
                // Those are all of the stocks 
                XmlNodeList nodes = doc.SelectNodes("methodResponse/soi");
                foreach (XmlNode nod in nodes)
                    mlStocks.Add(new CxStock(nod));

                //        
                double cap = 0;
                double percent = 0;
                int volume = 0;
                ListViewItem lvi;
                ListViewItem.ListViewSubItem lvsi;

                this.lvStocks.Items.Clear();
                this.lvStocks.BeginUpdate();

                foreach (CxStock st in mlStocks)
                {
                    // Get market totals
                    cap += st.MarketCap;
                    percent += st.Change;
                    volume += st.Volume;

                    // Symbol / stock
                    lvi = new ListViewItem();
                    lvi.Tag = st;
                    lvi.Text = st.Symbol;
                    lvi.UseItemStyleForSubItems = false;

                    // Price 
                    lvsi = new ListViewItem.ListViewSubItem();
                    lvsi.Text = st.Price.ToString();
                    lvi.SubItems.Add(lvsi);

                    // Change %
                    lvsi = new ListViewItem.ListViewSubItem();
                    if (st.Change < 0)
                        lvsi.ForeColor = Color.Red;
                    else
                        lvsi.ForeColor = Color.Green;
                    lvsi.Text = st.Change.ToString() + "%";
                    lvi.SubItems.Add(lvsi);

                    // Value
                    lvsi = new ListViewItem.ListViewSubItem();
                    lvsi.Text = "$" + st.MarketCap.ToString("0,0,0,0.00");
                    lvi.SubItems.Add(lvsi);

                    // Volume
                    lvsi = new ListViewItem.ListViewSubItem();
                    lvsi.Text = st.Volume.ToString();
                    lvi.SubItems.Add(lvsi);

                    this.lvStocks.Items.Add(lvi);
                }
                CxUiUtil.shadeGrid(this.lvStocks);
                this.lvStocks.Columns[4].Width = -2;
                this.lvStocks.EndUpdate();


                //  Market numbers
                this.lbValueText.Text = "$ " + cap.ToString("0,0,0,0.00");
                if ((percent / mlStocks.Count) > 0)
                    this.lbChangeText.ForeColor = Color.Green;
                else
                    this.lbChangeText.ForeColor = Color.Red;
                // change
                this.lbChangeText.Text = (percent / mlStocks.Count).ToString("00.00") + " %";
                // volume
                this.lbVolumeText.Text = "" + volume.ToString("0,0,0,0");

                // update the label in minimized state
                if (this.WindowState == FormWindowState.Minimized)
                    this.Text = this.lbChangeText.Text;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error=" + ex);
            }


            this.Cursor = Cursors.Default;
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            FxAbout about = new FxAbout();
            about.ShowDialog();
        }

        private void lvStocks_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem item = this.lvStocks.SelectedItems[0];
            CxStock st = (CxStock)item.Tag;
            //
            FxSockDetail detail = new FxSockDetail(st);
            detail.ShowDialog();
        }

        private void FxMain_SizeChanged(object sender, EventArgs e)
        {
            // If minimized, just set the header to market change
            if (this.WindowState == FormWindowState.Minimized)
                this.Text = this.lbChangeText.Text;
            else
                this.Text = "One Season market summary";
        }

        // Checks to see if there is a new version of the program available for download
        private bool newVersionAvailable(string psVersion)
        {
            // Operating system + ver
            OperatingSystem os = Environment.OSVersion;
            Version vs = os.Version;
            string sVer = vs.Major + "." + vs.Minor + "." + vs.Build + "." + vs.Revision;
            // Runtime / CLR
            string sRuntime = Environment.Version.ToString();
            // machine name
            string machine = Environment.MachineName;
            string sPing = "os=" + os.Platform + "," + sVer + AND + "clr=" + sRuntime + AND + "machine=" + machine;
            
            // check for version 
            string latestVersion = CxUtil.sendHttpPing(VERSION_CHECK_URL, sPing);
            
            // if they are the same return false 
            if (latestVersion.IndexOf("version=") == -1)
            {
                return false;
            }
            else
            {
                latestVersion = latestVersion.Replace("\n", string.Empty);
                latestVersion = latestVersion.Replace("version=", string.Empty).Trim();
                return !latestVersion.Equals(psVersion);
            }
        }

    
    }  // EOC
}
