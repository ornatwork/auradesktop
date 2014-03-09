//
using System;
using System.Threading;
using System.Runtime.CompilerServices; // for Synchronized
using System.Reflection; // For delegate
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
//
using org.auroracoin.aurcore.stocks;
using org.auroracoin.aurcore.util;
using org.auroracoin.desktop.core;


namespace org.auroracoin.desktop.background
{
    /// <summary>
    /// </summary>
    internal sealed class CxStockOrderUpdater
    {

        //
        public bool Active = false;
        //
        private Thread mThread;
        private Dictionary<CxMarketStock, IList<CxStockOrder>> mdiStockLists = new Dictionary<CxMarketStock, IList<CxStockOrder>>();
        IList<CxMarketStock> mlSymbols = new List<CxMarketStock>();
        //private WxGeneral mWebService = new WxGeneral();
        private const int LOOP_SLEEP = 15 * 1000;


        /// <summary>
        /// Constructor.
        /// </summary>
        public CxStockOrderUpdater() { }

        /// <summary>
        /// Destructor, shuts down the background thread / sockets nicely.
        /// </summary>
        ~CxStockOrderUpdater()
        {
            this.stop();
        }


        /// <summary>
        /// </summary>
        public void start()
        {
            // Start the thread
            mThread = new Thread(new ThreadStart(this.listen));
            mThread.Name = "Stock order updater";
            // Background threads are stopped automatically if needed 
            mThread.IsBackground = true;
            this.Active = true;

            // start it off
            mThread.Start();
        }

        /// <summary>
        /// Shuts down the listener thread and releases the resources. 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void stop()
        {
            try
            {
                if (mThread != null)
                {
                    this.Active = false;
                    // give it a millisecond
                    TimeSpan waitTime = new TimeSpan(10);
                    mThread.Join(waitTime);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("error=" + ex);
            }
        }


        /// <summary>
        /// Background thread 
        /// </summary>
        private void listen()
        {
            // Keeps going until shutdown, even if exceptions are thrown at it
            while (this.Active)
            {
                try
                {
                    // Get the list of symbols to process, lock and deep clone 
                    // so the diff threads won't collide
                    lock (mlSymbols)
                    {
                        // Make a copy of the list, the other thread is welcome to update it while
                        // the background thread has it's own copy 
                        mlSymbols = CxUtil.deepCloneList(CxGlobal.getStockOrderRequestList());
                    }
                    // Make sure there is only one for each symbol
                    setStockList( mlSymbols, mdiStockLists);
                    
                    
                    // Count out all the stock symbols that need orders retrieved
                    IList<CxMarketStock> stocks = CxGlobal.getStocks();
                    foreach (CxMarketStock xStock in mlSymbols)
                    {
                        // Find the correct stock market and symbol               
                        foreach (CxMarketStock tmp in stocks )
                            if( xStock.Exchange.Equals(tmp.Exchange) && xStock.Symbol.Equals( tmp.Symbol ))
                                if( tmp.RecentTrades.Count > 0 )
                                    mdiStockLists[xStock] = tmp.RecentTrades;
                    }
                    

                    //
                    CxGlobal.setStockOrderRequestList(mdiStockLists);

                    // Update every 15 secs
                    Thread.Sleep( LOOP_SLEEP );
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("error=" + ex);
                }
            }
        }


        // Set the stocks from the given list
        private void setStockList(IList<CxMarketStock> plStocks, Dictionary<CxMarketStock, IList<CxStockOrder>> pdiList)
        {

            // Add those not already in list
            foreach (CxMarketStock stock in plStocks)
            {
                if (!pdiList.ContainsKey(stock))
                {
                    pdiList.Add(stock, new List<CxStockOrder>());
                }
            }

            
            // Round up the ones not in use 
            IList<CxMarketStock> lRemove = new List<CxMarketStock>();
            //
            foreach (CxMarketStock stock in pdiList.Keys)
            {
                if (!plStocks.Contains(stock))
                    lRemove.Add(stock);
            }


            // Then trim those not needed
            foreach (CxMarketStock stock in lRemove)
                pdiList.Remove(stock);

        }








    }  //  EOC
}
