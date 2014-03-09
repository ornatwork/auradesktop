//
using System;
using System.Collections.Generic;
using System.Xml;
//
using log4net;
//
using org.auroracoin.desktop.background;
using org.auroracoin.aurcore.stocks;
using org.auroracoin.aurcore.util;

namespace org.auroracoin.desktop.core
{
    public static class CxGlobal
    {

        // Background stuff
        private static IList<CxMarketStock> mlStocks = new List<CxMarketStock>();
        private static Dictionary<CxMarketStock, IList<CxStockOrder>> mdiStockOrderLists = new Dictionary<CxMarketStock, IList<CxStockOrder>>();
        private static IList<CxMarketStock> mlStockRequestSymbols = new List<CxMarketStock>();
        //
        private static CxMarketStockUpdater mStockUpdater = new CxMarketStockUpdater();
        private static CxStockOrderUpdater mStockOrderUpdater = new CxStockOrderUpdater();
        // Logger
        private static ILog Logger = LogManager.GetLogger(typeof(CxGlobal));
                
        
        //
        public static double LastThreeCryptsyTrades = 0;
        //
        public static bool NO_SOUND = false;
        public static string OS_USERNAME = string.Empty;
        public static string OS_PASSWORD = string.Empty;
        public static string OS_NICKNAME = string.Empty;
        public static string OS_ENCODED_PASSWORD = string.Empty;
        public static string IpoCheck = string.Empty;
        public static double OverPrice = 0.9;
        public static double UnderPrice = 0.9;
        public static string IPO_STOCK = string.Empty;
        public static bool REFRESH_ON = true;
        //
        private static TimeSpan mtsOffset = new TimeSpan();
        private static DateTime mdtEstTime = CxUtil.getEstTimeFromLocalTime(DateTime.Now);
        
        //
        public const string DOWNLOAD_EXE = "new_sadesktop.zip";
        public const string THE_EXE = "sadesktop.exe";
        public const string THE_UPDATER = "updater.exe";

        //
        public const string WIKI_RESREARCH_URL = "http://en.wikipedia.org/w/index.php?title=Special%3ASearch&go=Go&search=";



        public static bool LoggedIn = false;
        // Used by FxListOrders
        public const int DEFAULT_INTERVAL = 10 * 1000;          
        
        //
        public const string LOGIN_NOT_CONFIGURED = "To import your protfolio you need to set your email and password by choosing File -> Configuration";
        //
        public const int STRING_NOT_FOUND = -1;


        // See if the user has setup OS login info
        public static bool isLoginSet()
        {
            if (OS_USERNAME != string.Empty &&
                OS_PASSWORD != string.Empty)
                return true;
            else
                return false;
        }

        // Kick off the background thread that fetches the latest stock prices
        public static void startBackground()
        {
            System.Diagnostics.Debug.WriteLine("StartMe called");
            mStockUpdater.start();
            mStockOrderUpdater.start();
        }

        // Shuts down the background thread
        public static void endBackground()
        {
            System.Diagnostics.Debug.WriteLine("EndMe called");
            mStockUpdater.stop();
            mStockOrderUpdater.stop();
        }


        // Sets the current stock list 
        public static void setStocks(IList<CxMarketStock> plStocks)
        {
            // Clone the list for the other thread to play with
            IList<CxMarketStock> list = CxUtil.deepCloneList(plStocks);

            // Update the stock list if there is something to update
            if (list.Count > 0)
            {
                // Make it thread safe
                lock (mlStocks)
                {
                    mlStocks = list;
                }
            }
        }


        // The current stock list 
        public static IList<CxMarketStock> getStocks()
        {
            return mlStocks;
        }





        
        
        // Adds a new symbol to the request list, 
        public static IList<CxStockOrder> addOrderRequestStock(CxMarketStock pxStock)
        {
            
            // Add a new stock to the request list 
            lock (mlStockRequestSymbols)
            {
                mlStockRequestSymbols.Add(pxStock);
            }

            //return CxDeskUtil.getStockOrders( pxStock );
            
            
            // Fixit - order updates 
            return pxStock.RecentTrades;
        }

        public static void removeRequestStock(CxMarketStock pxStock)
        {
            // Add a new stock to the request list 
            lock (mlStockRequestSymbols)
            {
                mlStockRequestSymbols.Remove(pxStock);
            }

        }

        public static IList<CxMarketStock> getStockOrderRequestList()
        {
            IList<CxMarketStock> list = new List<CxMarketStock>();

            // Round up unique list 
            foreach (CxMarketStock stock in mlStockRequestSymbols)
                if (!list.Contains(stock))
                    list.Add(stock);

            return list;
        }
        // The orders for the given stock symbol 
        public static IList<CxStockOrder> getStockOrders(CxMarketStock pxSymbol)
        {
            // Fixit -- Need to lock and deep clone ?
            if( mdiStockOrderLists.ContainsKey( pxSymbol ) )
            {
                IList<CxStockOrder> list = mdiStockOrderLists[pxSymbol];
                if( list != null && list.Count > 0)
                    return list;
                else
                    return new List<CxStockOrder>();
            }
            else
                return new List<CxStockOrder>();
        }
        
        // Deep clone and hand over 
        public static void setStockOrderRequestList(Dictionary<CxMarketStock, IList<CxStockOrder>> pxStockOrderList)
        {
            /*
            Dictionary<string, List<CxStockOrder>> diStockOrderLists = new Dictionary<string, List<CxStockOrder>>();
            // Clone it 
            foreach( string symb in diStockOrderLists.Keys )
            {
                diStockOrderLists.Add(symb, CxUtil.deepCloneList(pxStockOrderList[symb]));
            }
            */

            // Hand of 
            mdiStockOrderLists = pxStockOrderList;
        }

        
        // Get the market time, EST time including time correction
        public static DateTime getMarketEstTime()
        {
            // Adjust to the offset from OS server
            return mdtEstTime.AddTicks(mtsOffset.Ticks);
        }

        public static TimeSpan OS_TIME_OFFSET
        {
            get
            {
                return mtsOffset;
            }
            set
            {
                // 00:00:00:00
                mtsOffset =  value;
            }
        }
        

    
    }  // EOC
}
