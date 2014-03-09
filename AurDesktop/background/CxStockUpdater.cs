//
using System;
using System.Threading;
using System.Runtime.CompilerServices; // for Synchronized
using System.Reflection; // For delegate
using System.Collections.Generic;
using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;
//
using log4net;
//
using org.auroracoin.aurcore.stocks;
using org.auroracoin.aurcore.util;
using org.auroracoin.desktop.core;


namespace org.auroracoin.desktop.background
{
    /// <summary>
    /// </summary>
    internal sealed class CxMarketStockUpdater
    {

        //
        public bool Active = false;
        //
        private Thread mThread;
        private string msToken = string.Empty;
        private IList<CxMarketStock> mlStocks = new List<CxMarketStock>();
        //private WxGeneral mWebService = new WxGeneral();
        private const int LOOP_SLEEP = 15 * 1000;
        private static ILog Logger = LogManager.GetLogger(typeof(CxMarketStockUpdater));


        /// <summary>
        /// Constructor.
        /// </summary>
        public CxMarketStockUpdater() { }

        /// <summary>
        /// Destructor, shuts down the background thread / sockets nicely.
        /// </summary>
        ~CxMarketStockUpdater()
        {
            this.stop();
        }


        /// <summary>
        /// </summary>
        public void start()
        {
            // Start the thread
            mThread = new Thread(new ThreadStart(this.listen));
            mThread.Name = "Stock updater";
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
                    this.updateStocks();
                    //this.updateSesonInfo();
                    // Update every 15 secs
                    Thread.Sleep(LOOP_SLEEP);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("error=" + ex);
                }
            }
        }



        private void updateStocks()
        {
            try
            {

                //        
                mlStocks = new List<CxMarketStock>();




 
                // CRYPTSY 

                /*
                {"success":1,"return":{"markets":{"AUR":{"marketid":"160","label":"AUR\/BTC","lasttradeprice":"0.13032400","volume":"55967.92353022","lasttradetime":"2014-03-04 09:13:58",
                    "primaryname":"AuroraCoin","primarycode":"AUR","secondaryname":"BitCoin","secondarycode":"BTC","recenttrades":
                [
                {"id":"27927604","time":"2014-03-04 09:40:18","price":"0.12999999","quantity":"0.01148640","total":"0.00149323"},
                ....
                */

                // Get the xml from the site
                string theJson = CxUtil.getWebPage("http://pubapi.cryptsy.com/api.php?method=singlemarketdata&marketid=160", 5 * 1000);
                // Brute hack out the json header 
                if (theJson.Length > 0)
                {
                    theJson = theJson.Replace("{\"success\":1,\"return\":{\"markets\":{\"AUR\":", "");
                    theJson = theJson.Substring(0, theJson.Length - 3);
                    //Logger.Debug("json cryptorush=" + theJson);


                    CxCryptsy cryptsy = Activator.CreateInstance<CxCryptsy>();
                    using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(theJson)))
                    {
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(cryptsy.GetType());
                        cryptsy = (CxCryptsy)serializer.ReadObject(ms);
                        mlStocks.Add(CxCryptsy.getStock(cryptsy));
                    }

                    // Reset the last three cryptsy trades for the alert to compare to
                    double tmp = 0;
                    if (cryptsy.recenttrades.Count > 2)
                    {
                        tmp += cryptsy.recenttrades[0].price;
                        tmp += cryptsy.recenttrades[1].price;
                        tmp += cryptsy.recenttrades[2].price;
                        // Average for the three 
                        CxGlobal.LastThreeCryptsyTrades = tmp / 3;
                    }
                }


                // Crypto Rush
                /*
                 { "AUR/BTC" : { "market_id": "222", "last_trade": "0.12000000", "last_sell": "0.12000000", "last_buy": "0.12000000", "current_ask":"0.11999998", 
                  "current_ask_volume":"0.51101405", "current_bid":"0.11900000", "current_bid_volume":"1.10280168", "highest_24h": "0.90000000", 
                  "lowest_24h": "0.00670000", "volume_base_24h": "756.83538801", "min_tradable": "0.10000000", "Average": "0.03400583" } } 
                */
                theJson = CxUtil.getWebPage("http://cryptorush.in/api.php?get=market&m=aur&b=btc&key=f707fe822b398a93f3e7c3f4717e66325d0f2af7&id=14386&json=true");
                //Logger.Debug("json cryptorush=" + theJson);

                // Brute hack out the "AUR/BTC" element
                if (theJson.Length > 0)
                {
                    theJson = theJson.Replace("{\n\"AUR/BTC\" : ", "");
                    theJson = theJson.Remove(theJson.Length - 1);

                    CxCryptoRush crypt = Activator.CreateInstance<CxCryptoRush>();
                    using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(theJson)))
                    {
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(crypt.GetType());
                        crypt = (CxCryptoRush)serializer.ReadObject(ms);
                        mlStocks.Add(CxCryptoRush.getStock(crypt));
                    }
                }


                // Poloniex
                //---------------------------------------
                CxMarketStock polStock = new CxMarketStock();
                polStock.Exchange = ExExchange.Poloniex;
                polStock.Symbol = "AUR";
                
                
                
                // Volume
                theJson = CxUtil.getWebPage("https://poloniex.com/public?command=return24hVolume");
                //Logger.Debug("json poloniex=" + theJson);
                if (theJson.Length > 0)
                {
                    var serializer = new JavaScriptSerializer();
                    dynamic value = serializer.DeserializeObject(theJson);
                    // Find the AUR 
                    polStock.Volume = double.Parse(value["BTC_AUR"]["AUR"]);
                }


                // Current price
                theJson = CxUtil.getWebPage("https://poloniex.com/public?command=returnTicker");
                //Logger.Debug("json poloniex=" + theJson);
                // Deserialize 
                if (theJson.Length > 0)
                {
                    var serializer = new JavaScriptSerializer();
                    dynamic value = serializer.DeserializeObject(theJson);
                    // Find the AUR 
                    polStock.Price = double.Parse(value["BTC_AUR"]);
                }

                /*
                // Order book, asks and bids
                theJson = CxUtil.getWebPage("https://poloniex.com/public?command=returnOrderBook&currencyPair=BTC_AUR");
                //Logger.Debug("json poloniex=" + theJson);
                // Deserialize 
                if (theJson.Length > 0)
                {
                    var serializer = new JavaScriptSerializer();
                    dynamic value = serializer.DeserializeObject(theJson);
                    // Find the AUR 
                    polStock.Price = value["AUR"];
                }


                // Trade history
                theJson = CxUtil.getWebPage("https://poloniex.com/public?command=returnTradeHistory&currencyPair=BTC_AUR");
                //Logger.Debug("json poloniex=" + theJson);
                // Deserialize 
                if (theJson.Length > 0)
                {
                    var serializer = new JavaScriptSerializer();
                    dynamic value = serializer.DeserializeObject(theJson);
                    // Find the AUR 
                    polStock.Price = value["AUR"];
                }
                */
                // Pol stock
                mlStocks.Add(polStock);


                //
                System.Diagnostics.Debug.WriteLine("stocks loaded, count=" + mlStocks.Count + ", " + CxUtil.getTimeString(DateTime.Now));
                CxGlobal.setStocks(mlStocks);

            }
            catch (Exception ex)
            {
                Logger.Error("error=" + ex);
            }

        }

        /*
        // Check for information on server
        private void updateSesonInfo()
        {
            try
            {
                XmlNode xStatuses = mWebService.getStatuses(CxGlobal.KEY, string.Empty,
                                                string.Empty, string.Empty, string.Empty, string.Empty);

                // Got this from server 
                // Fixit
                //CxGlobal.IpoCheck = "New IPO announced, Approx 4/6/2009 7:14:00 PM EST~LNCM, Tim Lincecum";
                CxGlobal.IpoCheck = xStatuses.SelectSingleNode("ipo").InnerText;


                string tmp = xStatuses.SelectSingleNode("yesterdmarketcap").InnerText;
                if (tmp != string.Empty) CxGlobal.YesterdayMarketCap = double.Parse(tmp);

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error=" + ex);
                Logger.Error("Error=" + ex );
            }
        }
        */




    }  //  EOC
}
