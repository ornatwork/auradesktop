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
                try
                {

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
                }
                catch( Exception ex )
                {
                    Logger.Error( "Cryptsy, err=" + ex);
                }


                // Crypto Rush
                try
                {
                    /*
                     { "AUR/BTC" : { "market_id": "222", "last_trade": "0.12000000", "last_sell": "0.12000000", "last_buy": "0.12000000", "current_ask":"0.11999998", 
                      "current_ask_volume":"0.51101405", "current_bid":"0.11900000", "current_bid_volume":"1.10280168", "highest_24h": "0.90000000", 
                      "lowest_24h": "0.00670000", "volume_base_24h": "756.83538801", "min_tradable": "0.10000000", "Average": "0.03400583" } } 
                    */
                    string theJson = CxUtil.getWebPage("http://cryptorush.in/api.php?get=market&m=aur&b=btc&key=f707fe822b398a93f3e7c3f4717e66325d0f2af7&id=14386&json=true");
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
                }
                catch (Exception ex)
                {
                    Logger.Error("CryptoRush, err=" + ex);
                }


                // Poloniex
                //---------------------------------------
                try
                {
                    CxMarketStock polStock = new CxMarketStock();
                    polStock.Exchange = ExExchange.Poloniex;
                    polStock.Symbol = "BTC_AUR";

                    
                    // Volume
                    string theJson = CxUtil.getWebPage("https://poloniex.com/public?command=return24hVolume");
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


                    // Trade history
                    //  [{"date":"2014-03-09 23:09:39","type":"sell","rate":"0.04","amount":"1","total":"0.04"},
                    //   {"date":"2014-03-09 22:47:36","type":"sell","rate":"0.0389","amount":"0.15","total":"0.005835"},
                    //   {"date":"2014-03-09 22:38:56","type":"sell","rate":"0.046","amount":"0.36199","total":"0.01665154"}
                    theJson = CxUtil.getWebPage("https://poloniex.com/public?command=returnTradeHistory&currencyPair=BTC_AUR");
                    //theJson = @"[{""date"":""2014-03-09 23:09:39"",""type"":""sell"",""rate"":""0.04"",""amount"":""1"",""total"":""0.04""},{""date"":""2014-03-09 22:47:36"",""type"":""sell"",""rate"":""0.0389"",""amount"":""0.15"",""total"":""0.005835""},{""date"":""2014-03-09 22:38:56"",""type"":""sell"",""rate"":""0.046"",""amount"":""0.36199"",""total"":""0.01665154""},{""date"":""2014-03-09 22:38:39"",""type"":""sell"",""rate"":""0.04601"",""amount"":""1.24595066"",""total"":""0.05732618""},{""date"":""2014-03-09 22:27:57"",""type"":""sell"",""rate"":""0.045"",""amount"":""0.29038044"",""total"":""0.01306711""},{""date"":""2014-03-09 22:15:29"",""type"":""buy"",""rate"":""0.04999"",""amount"":""0.01437678"",""total"":""0.00071869""},{""date"":""2014-03-09 21:28:39"",""type"":""sell"",""rate"":""0.04001"",""amount"":""0.0144056"",""total"":""0.00057636""},{""date"":""2014-03-09 21:21:26"",""type"":""buy"",""rate"":""0.045"",""amount"":""9.11111111"",""total"":""0.40999999""},{""date"":""2014-03-09 21:21:26"",""type"":""buy"",""rate"":""0.045"",""amount"":""2"",""total"":""0.09""},{""date"":""2014-03-09 20:41:10"",""type"":""buy"",""rate"":""0.0384"",""amount"":""1.52334882"",""total"":""0.05849659""},{""date"":""2014-03-09 20:41:00"",""type"":""buy"",""rate"":""0.0384"",""amount"":""0.0183019"",""total"":""0.00070279""},{""date"":""2014-03-09 20:40:58"",""type"":""sell"",""rate"":""0.0384"",""amount"":""0.8806468"",""total"":""0.03381683""},{""date"":""2014-03-09 20:11:20"",""type"":""sell"",""rate"":""0.03701"",""amount"":""0.01142205"",""total"":""0.00042273""},{""date"":""2014-03-09 20:07:16"",""type"":""sell"",""rate"":""0.04"",""amount"":""5"",""total"":""0.2""},{""date"":""2014-03-09 20:06:56"",""type"":""sell"",""rate"":""0.0403"",""amount"":""0.28350074"",""total"":""0.01142507""},{""date"":""2014-03-09 20:03:32"",""type"":""sell"",""rate"":""0.042"",""amount"":""0.42"",""total"":""0.01764""},{""date"":""2014-03-09 20:03:32"",""type"":""sell"",""rate"":""0.042"",""amount"":""2"",""total"":""0.084""},{""date"":""2014-03-09 19:35:24"",""type"":""buy"",""rate"":""0.0599"",""amount"":""0.18987714"",""total"":""0.01137364""},{""date"":""2014-03-09 19:35:24"",""type"":""buy"",""rate"":""0.0599"",""amount"":""1.17226777"",""total"":""0.07021883""},{""date"":""2014-03-09 19:35:24"",""type"":""buy"",""rate"":""0.058"",""amount"":""2"",""total"":""0.116""},{""date"":""2014-03-09 19:35:24"",""type"":""buy"",""rate"":""0.05799"",""amount"":""0.18342777"",""total"":""0.01063697""},{""date"":""2014-03-09 19:35:24"",""type"":""buy"",""rate"":""0.056"",""amount"":""0.20442732"",""total"":""0.01144792""},{""date"":""2014-03-09 19:34:39"",""type"":""buy"",""rate"":""0.055"",""amount"":""2"",""total"":""0.11""},{""date"":""2014-03-09 19:34:39"",""type"":""buy"",""rate"":""0.054991"",""amount"":""0.498"",""total"":""0.02738551""},{""date"":""2014-03-09 19:34:39"",""type"":""buy"",""rate"":""0.0545"",""amount"":""0.02191934"",""total"":""0.00119460""},{""date"":""2014-03-09 19:34:39"",""type"":""buy"",""rate"":""0.052991"",""amount"":""2"",""total"":""0.105982""},{""date"":""2014-03-09 19:34:39"",""type"":""buy"",""rate"":""0.0529"",""amount"":""0.3"",""total"":""0.01587""},{""date"":""2014-03-09 19:34:39"",""type"":""buy"",""rate"":""0.0500091"",""amount"":""0.49399999"",""total"":""0.02470449""},{""date"":""2014-03-09 19:34:28"",""type"":""buy"",""rate"":""0.0485"",""amount"":""0.063"",""total"":""0.0030555""},{""date"":""2014-03-09 19:31:54"",""type"":""sell"",""rate"":""0.04849"",""amount"":""0.06421744"",""total"":""0.00311390""},{""date"":""2014-03-09 19:31:50"",""type"":""buy"",""rate"":""0.04849"",""amount"":""1.55209953"",""total"":""0.07526130""},{""date"":""2014-03-09 19:27:23"",""type"":""sell"",""rate"":""0.0425"",""amount"":""0.5"",""total"":""0.02125""},{""date"":""2014-03-09 19:26:39"",""type"":""sell"",""rate"":""0.0426"",""amount"":""4.9653211"",""total"":""0.21152267""},{""date"":""2014-03-09 18:55:08"",""type"":""sell"",""rate"":""0.0445"",""amount"":""0.02836539"",""total"":""0.00126225""},{""date"":""2014-03-09 18:50:57"",""type"":""buy"",""rate"":""0.045"",""amount"":""0.06243465"",""total"":""0.00280955""},{""date"":""2014-03-09 18:50:54"",""type"":""sell"",""rate"":""0.045"",""amount"":""0.43656535"",""total"":""0.01964544""},{""date"":""2014-03-09 18:49:52"",""type"":""sell"",""rate"":""0.045"",""amount"":""0.56343465"",""total"":""0.02535455""},{""date"":""2014-03-09 18:49:52"",""type"":""sell"",""rate"":""0.04501"",""amount"":""1.55520995"",""total"":""0.06999999""},{""date"":""2014-03-09 18:41:08"",""type"":""buy"",""rate"":""0.045"",""amount"":""0.12009858"",""total"":""0.00540443""},{""date"":""2014-03-09 18:40:09"",""type"":""buy"",""rate"":""0.045"",""amount"":""0.5"",""total"":""0.0225""},{""date"":""2014-03-09 18:39:29"",""type"":""buy"",""rate"":""0.045"",""amount"":""0.04051441"",""total"":""0.00182314""},{""date"":""2014-03-09 18:39:05"",""type"":""buy"",""rate"":""0.045"",""amount"":""9.62556103"",""total"":""0.43315024""},{""date"":""2014-03-09 18:38:53"",""type"":""buy"",""rate"":""0.04499"",""amount"":""0.42144626"",""total"":""0.01896086""},{""date"":""2014-03-09 18:38:44"",""type"":""buy"",""rate"":""0.04499"",""amount"":""0.57436294"",""total"":""0.02584058""},{""date"":""2014-03-09 18:38:32"",""type"":""buy"",""rate"":""0.045"",""amount"":""2.03294079"",""total"":""0.09148233""},{""date"":""2014-03-09 18:38:30"",""type"":""sell"",""rate"":""0.045"",""amount"":""0.5"",""total"":""0.0225""},{""date"":""2014-03-09 18:38:30"",""type"":""sell"",""rate"":""0.0452"",""amount"":""5"",""total"":""0.226""},{""date"":""2014-03-09 18:38:30"",""type"":""sell"",""rate"":""0.045412"",""amount"":""0.5"",""total"":""0.022706""},{""date"":""2014-03-09 18:38:30"",""type"":""sell"",""rate"":""0.046"",""amount"":""1"",""total"":""0.046""},{""date"":""2014-03-09 18:38:30"",""type"":""sell"",""rate"":""0.0470001"",""amount"":""1.84"",""total"":""0.08648018""},{""date"":""2014-03-09 18:38:30"",""type"":""sell"",""rate"":""0.0470001"",""amount"":""1.79488517"",""total"":""0.08435978""},{""date"":""2014-03-09 18:32:56"",""type"":""sell"",""rate"":""0.0470001"",""amount"":""0.20511483"",""total"":""0.00964041""},{""date"":""2014-03-09 18:07:42"",""type"":""sell"",""rate"":""0.0501"",""amount"":""0.1"",""total"":""0.00501""},{""date"":""2014-03-09 18:07:42"",""type"":""sell"",""rate"":""0.0501"",""amount"":""8.98556798"",""total"":""0.45017695""},{""date"":""2014-03-09 18:07:32"",""type"":""sell"",""rate"":""0.0501"",""amount"":""0.20283971"",""total"":""0.01016226""},{""date"":""2014-03-09 18:07:16"",""type"":""sell"",""rate"":""0.0501"",""amount"":""1.82236197"",""total"":""0.09130033""},{""date"":""2014-03-09 18:06:58"",""type"":""sell"",""rate"":""0.05011"",""amount"":""0.12439453"",""total"":""0.00623340""},{""date"":""2014-03-09 18:06:48"",""type"":""sell"",""rate"":""0.05011"",""amount"":""0.87341029"",""total"":""0.04376658""},{""date"":""2014-03-09 17:50:39"",""type"":""sell"",""rate"":""0.05"",""amount"":""2"",""total"":""0.1""},{""date"":""2014-03-09 17:49:14"",""type"":""buy"",""rate"":""0.055"",""amount"":""4.63979031"",""total"":""0.25518846""},{""date"":""2014-03-09 17:49:04"",""type"":""buy"",""rate"":""0.055"",""amount"":""0.05729091"",""total"":""0.00315100""},{""date"":""2014-03-09 17:48:54"",""type"":""buy"",""rate"":""0.05499"",""amount"":""0.499"",""total"":""0.02744001""},{""date"":""2014-03-09 17:48:35"",""type"":""buy"",""rate"":""0.055"",""amount"":""1.85917913"",""total"":""0.10225485""},{""date"":""2014-03-09 17:48:02"",""type"":""buy"",""rate"":""0.055"",""amount"":""5.12004226"",""total"":""0.28160232""},{""date"":""2014-03-09 17:47:38"",""type"":""buy"",""rate"":""0.055"",""amount"":""0.19543333"",""total"":""0.01074883""},{""date"":""2014-03-09 17:46:28"",""type"":""buy"",""rate"":""0.052"",""amount"":""1.87481995"",""total"":""0.09749063""},{""date"":""2014-03-09 17:46:11"",""type"":""buy"",""rate"":""0.05199"",""amount"":""0.2270684"",""total"":""0.01180528""},{""date"":""2014-03-09 17:44:11"",""type"":""sell"",""rate"":""0.0453"",""amount"":""0.233296"",""total"":""0.01056830""},{""date"":""2014-03-09 17:16:30"",""type"":""sell"",""rate"":""0.05009"",""amount"":""1"",""total"":""0.05009""},{""date"":""2014-03-09 17:12:25"",""type"":""sell"",""rate"":""0.05302"",""amount"":""0.16716472"",""total"":""0.00886307""},{""date"":""2014-03-09 17:11:47"",""type"":""sell"",""rate"":""0.05302"",""amount"":""0.03767228"",""total"":""0.00199738""},{""date"":""2014-03-09 17:05:18"",""type"":""sell"",""rate"":""0.05311"",""amount"":""0.22752345"",""total"":""0.01208377""},{""date"":""2014-03-09 16:39:14"",""type"":""sell"",""rate"":""0.0531"",""amount"":""11.73553556"",""total"":""0.62315693""},{""date"":""2014-03-09 16:39:14"",""type"":""sell"",""rate"":""0.0535"",""amount"":""8.63"",""total"":""0.461705""},{""date"":""2014-03-09 16:39:14"",""type"":""sell"",""rate"":""0.054"",""amount"":""0.18379537"",""total"":""0.00992494""},{""date"":""2014-03-09 16:39:14"",""type"":""sell"",""rate"":""0.054"",""amount"":""0.02012906"",""total"":""0.00108696""},{""date"":""2014-03-09 16:39:14"",""type"":""sell"",""rate"":""0.054"",""amount"":""4.43054001"",""total"":""0.23924916""},{""date"":""2014-03-09 16:23:56"",""type"":""sell"",""rate"":""0.058"",""amount"":""3.51604186"",""total"":""0.20393042""},{""date"":""2014-03-09 16:23:00"",""type"":""sell"",""rate"":""0.058"",""amount"":""3.96969699"",""total"":""0.23024242""},{""date"":""2014-03-09 16:22:35"",""type"":""sell"",""rate"":""0.058"",""amount"":""0.35429907"",""total"":""0.02054934""},{""date"":""2014-03-09 16:22:24"",""type"":""sell"",""rate"":""0.058"",""amount"":""0.52046791"",""total"":""0.03018713""},{""date"":""2014-03-09 16:22:13"",""type"":""sell"",""rate"":""0.058"",""amount"":""0.69923278"",""total"":""0.04055550""},{""date"":""2014-03-09 16:20:41"",""type"":""sell"",""rate"":""0.058"",""amount"":""0.76908601"",""total"":""0.04460698""},{""date"":""2014-03-09 16:18:57"",""type"":""buy"",""rate"":""0.058"",""amount"":""0.0998"",""total"":""0.0057884""},{""date"":""2014-03-09 16:18:57"",""type"":""buy"",""rate"":""0.058"",""amount"":""0.09137538"",""total"":""0.00529977""},{""date"":""2014-03-09 16:12:27"",""type"":""sell"",""rate"":""0.054"",""amount"":""1.3"",""total"":""0.0702""},{""date"":""2014-03-09 16:08:14"",""type"":""sell"",""rate"":""0.054"",""amount"":""0.26945999"",""total"":""0.01455083""},{""date"":""2014-03-09 16:03:34"",""type"":""sell"",""rate"":""0.059"",""amount"":""0.35017115"",""total"":""0.02066009""},{""date"":""2014-03-09 16:03:28"",""type"":""buy"",""rate"":""0.059"",""amount"":""0.06109766"",""total"":""0.00360476""},{""date"":""2014-03-09 16:02:10"",""type"":""sell"",""rate"":""0.05899"",""amount"":""0.46846599"",""total"":""0.02763480""},{""date"":""2014-03-09 16:02:06"",""type"":""sell"",""rate"":""0.05899"",""amount"":""0.90850522"",""total"":""0.05359272""},{""date"":""2014-03-09 16:02:06"",""type"":""sell"",""rate"":""0.059"",""amount"":""0.4694048"",""total"":""0.02769488""},{""date"":""2014-03-09 16:02:01"",""type"":""buy"",""rate"":""0.05899"",""amount"":""0.46406998"",""total"":""0.02737548""},{""date"":""2014-03-09 15:57:17"",""type"":""sell"",""rate"":""0.05401"",""amount"":""0.46499998"",""total"":""0.02511464""},{""date"":""2014-03-09 15:26:01"",""type"":""buy"",""rate"":""0.0599"",""amount"":""0.12513222"",""total"":""0.00749541""},{""date"":""2014-03-09 15:23:24"",""type"":""buy"",""rate"":""0.06"",""amount"":""0.49231615"",""total"":""0.02953896""},{""date"":""2014-03-09 15:23:24"",""type"":""buy"",""rate"":""0.06"",""amount"":""6.03872946"",""total"":""0.36232376""},{""date"":""2014-03-09 15:23:24"",""type"":""buy"",""rate"":""0.0599"",""amount"":""0.083"",""total"":""0.0049717""},{""date"":""2014-03-09 15:23:24"",""type"":""buy"",""rate"":""0.0599"",""amount"":""1.71928772"",""total"":""0.10298533""},{""date"":""2014-03-09 15:22:35"",""type"":""buy"",""rate"":""0.0599"",""amount"":""3.57893155"",""total"":""0.21437799""},{""date"":""2014-03-09 15:22:21"",""type"":""sell"",""rate"":""0.0592"",""amount"":""4.55313112"",""total"":""0.26954536""},{""date"":""2014-03-09 15:22:11"",""type"":""sell"",""rate"":""0.0592"",""amount"":""0.50119637"",""total"":""0.02967082""},{""date"":""2014-03-09 15:22:00"",""type"":""sell"",""rate"":""0.0592"",""amount"":""0.05"",""total"":""0.00296""},{""date"":""2014-03-09 15:21:49"",""type"":""sell"",""rate"":""0.0592"",""amount"":""3.23917131"",""total"":""0.19175894""},{""date"":""2014-03-09 15:21:37"",""type"":""sell"",""rate"":""0.0592"",""amount"":""2.25650121"",""total"":""0.13358487""},{""date"":""2014-03-09 15:21:27"",""type"":""buy"",""rate"":""0.05899"",""amount"":""4.98999999"",""total"":""0.29436009""},{""date"":""2014-03-09 15:21:27"",""type"":""buy"",""rate"":""0.05898"",""amount"":""0.6"",""total"":""0.035388""},{""date"":""2014-03-09 15:17:20"",""type"":""sell"",""rate"":""0.056"",""amount"":""1.4705744"",""total"":""0.08235216""},{""date"":""2014-03-09 15:16:49"",""type"":""sell"",""rate"":""0.056"",""amount"":""0.05838979"",""total"":""0.00326982""},{""date"":""2014-03-09 15:07:29"",""type"":""sell"",""rate"":""0.05898"",""amount"":""0.2"",""total"":""0.011796""},{""date"":""2014-03-09 15:07:27"",""type"":""buy"",""rate"":""0.05898"",""amount"":""0.6"",""total"":""0.035388""},{""date"":""2014-03-09 15:04:39"",""type"":""buy"",""rate"":""0.05898"",""amount"":""0.05869286"",""total"":""0.00346170""},{""date"":""2014-03-09 15:03:20"",""type"":""buy"",""rate"":""0.05898"",""amount"":""0.1"",""total"":""0.005898""},{""date"":""2014-03-09 15:03:02"",""type"":""buy"",""rate"":""0.05898"",""amount"":""0.1"",""total"":""0.005898""},{""date"":""2014-03-09 14:57:46"",""type"":""sell"",""rate"":""0.058"",""amount"":""0.57147398"",""total"":""0.03314549""},{""date"":""2014-03-09 14:57:35"",""type"":""sell"",""rate"":""0.0589"",""amount"":""0.01701625"",""total"":""0.00100225""},{""date"":""2014-03-09 14:57:09"",""type"":""sell"",""rate"":""0.0589"",""amount"":""0.23272367"",""total"":""0.01370742""},{""date"":""2014-03-09 14:57:05"",""type"":""buy"",""rate"":""0.0589"",""amount"":""0.61264012"",""total"":""0.03608450""},{""date"":""2014-03-09 14:57:05"",""type"":""buy"",""rate"":""0.05889"",""amount"":""0.95029517"",""total"":""0.05596288""},{""date"":""2014-03-09 14:56:58"",""type"":""sell"",""rate"":""0.058"",""amount"":""0.52356856"",""total"":""0.03036697""},{""date"":""2014-03-09 14:56:44"",""type"":""sell"",""rate"":""0.058"",""amount"":""0.05"",""total"":""0.0029""},{""date"":""2014-03-09 14:56:34"",""type"":""sell"",""rate"":""0.05801"",""amount"":""0.13698824"",""total"":""0.00794668""},{""date"":""2014-03-09 14:56:24"",""type"":""sell"",""rate"":""0.05801"",""amount"":""0.76590668"",""total"":""0.04443024""},{""date"":""2014-03-09 14:56:12"",""type"":""sell"",""rate"":""0.05801"",""amount"":""0.04930466"",""total"":""0.00286016""},{""date"":""2014-03-09 14:56:02"",""type"":""buy"",""rate"":""0.058"",""amount"":""0.02996"",""total"":""0.00173768""},{""date"":""2014-03-09 14:56:02"",""type"":""buy"",""rate"":""0.0579"",""amount"":""0.17964"",""total"":""0.01040115""},{""date"":""2014-03-09 14:56:02"",""type"":""buy"",""rate"":""0.0575"",""amount"":""13.04335662"",""total"":""0.74999300""},{""date"":""2014-03-09 14:56:02"",""type"":""buy"",""rate"":""0.0574"",""amount"":""6.73440502"",""total"":""0.38655484""},{""date"":""2014-03-09 14:56:02"",""type"":""buy"",""rate"":""0.0574"",""amount"":""5"",""total"":""0.287""},{""date"":""2014-03-09 14:26:18"",""type"":""buy"",""rate"":""0.0575"",""amount"":""1.2173913"",""total"":""0.06999999""},{""date"":""2014-03-09 14:07:14"",""type"":""buy"",""rate"":""0.0582"",""amount"":""0.02"",""total"":""0.001164""},{""date"":""2014-03-09 13:57:53"",""type"":""buy"",""rate"":""0.0583"",""amount"":""1.11875953"",""total"":""0.06522368""},{""date"":""2014-03-09 13:57:53"",""type"":""buy"",""rate"":""0.0565"",""amount"":""0.09735275"",""total"":""0.00550043""},{""date"":""2014-03-09 13:57:53"",""type"":""buy"",""rate"":""0.05649"",""amount"":""0.90677811"",""total"":""0.05122389""},{""date"":""2014-03-09 13:37:58"",""type"":""sell"",""rate"":""0.055"",""amount"":""0.78765723"",""total"":""0.04332114""},{""date"":""2014-03-09 13:37:39"",""type"":""sell"",""rate"":""0.055"",""amount"":""7.02358586"",""total"":""0.38629722""},{""date"":""2014-03-09 13:37:29"",""type"":""sell"",""rate"":""0.05501"",""amount"":""0.01257335"",""total"":""0.00069165""},{""date"":""2014-03-09 13:36:57"",""type"":""sell"",""rate"":""0.05502"",""amount"":""1.40859531"",""total"":""0.07750091""},{""date"":""2014-03-09 13:36:43"",""type"":""sell"",""rate"":""0.05503"",""amount"":""0.90859531"",""total"":""0.04999999""},{""date"":""2014-03-09 13:33:37"",""type"":""buy"",""rate"":""0.05519"",""amount"":""0.90677811"",""total"":""0.05004508""},{""date"":""2014-03-09 13:19:35"",""type"":""sell"",""rate"":""0.05502"",""amount"":""3.59140469"",""total"":""0.19759908""},{""date"":""2014-03-09 13:19:35"",""type"":""sell"",""rate"":""0.05503"",""amount"":""0.90859531"",""total"":""0.04999999""},{""date"":""2014-03-09 13:08:50"",""type"":""sell"",""rate"":""0.05503"",""amount"":""0.31386786"",""total"":""0.01727214""},{""date"":""2014-03-09 13:00:41"",""type"":""sell"",""rate"":""0.05503"",""amount"":""0.3"",""total"":""0.016509""},{""date"":""2014-03-09 12:59:13"",""type"":""sell"",""rate"":""0.0589"",""amount"":""0.67866938"",""total"":""0.03997362""},{""date"":""2014-03-09 12:59:02"",""type"":""sell"",""rate"":""0.05899"",""amount"":""0.1"",""total"":""0.005899""},{""date"":""2014-03-09 12:57:29"",""type"":""buy"",""rate"":""0.0589"",""amount"":""0.62133062"",""total"":""0.03659637""},{""date"":""2014-03-09 12:54:38"",""type"":""buy"",""rate"":""0.0599"",""amount"":""0.12"",""total"":""0.007188""},{""date"":""2014-03-09 12:54:36"",""type"":""buy"",""rate"":""0.0599"",""amount"":""0.3801207"",""total"":""0.02276922""},{""date"":""2014-03-09 12:53:02"",""type"":""buy"",""rate"":""0.05991"",""amount"":""0.019"",""total"":""0.00113829""},{""date"":""2014-03-09 12:48:02"",""type"":""buy"",""rate"":""0.05992"",""amount"":""0.5"",""total"":""0.02996""},{""date"":""2014-03-09 12:42:02"",""type"":""buy"",""rate"":""0.06"",""amount"":""3.96127054"",""total"":""0.23767623""},{""date"":""2014-03-09 12:42:02"",""type"":""buy"",""rate"":""0.06"",""amount"":""0.5"",""total"":""0.03""},{""date"":""2014-03-09 12:42:02"",""type"":""buy"",""rate"":""0.06"",""amount"":""0.025"",""total"":""0.0015""},{""date"":""2014-03-09 12:42:02"",""type"":""buy"",""rate"":""0.0595"",""amount"":""0.51372946"",""total"":""0.03056690""},{""date"":""2014-03-09 12:42:02"",""type"":""buy"",""rate"":""0.059499"",""amount"":""1"",""total"":""0.059499""},{""date"":""2014-03-09 12:42:02"",""type"":""buy"",""rate"":""0.059"",""amount"":""4"",""total"":""0.236""},{""date"":""2014-03-09 12:37:51"",""type"":""buy"",""rate"":""0.0595"",""amount"":""8.33654968"",""total"":""0.49602470""},{""date"":""2014-03-09 12:37:51"",""type"":""buy"",""rate"":""0.059"",""amount"":""0.998"",""total"":""0.058882""},{""date"":""2014-03-09 12:37:51"",""type"":""buy"",""rate"":""0.059"",""amount"":""2"",""total"":""0.118""},{""date"":""2014-03-09 12:37:51"",""type"":""buy"",""rate"":""0.059"",""amount"":""1.66545032"",""total"":""0.09826156""},{""date"":""2014-03-09 12:35:26"",""type"":""buy"",""rate"":""0.059"",""amount"":""0.33054968"",""total"":""0.01950243""},{""date"":""2014-03-09 12:35:26"",""type"":""buy"",""rate"":""0.059"",""amount"":""0.1404554"",""total"":""0.00828686""},{""date"":""2014-03-09 12:29:18"",""type"":""buy"",""rate"":""0.059"",""amount"":""0.01153"",""total"":""0.00068027""},{""date"":""2014-03-09 12:20:41"",""type"":""buy"",""rate"":""0.056"",""amount"":""0.68992738"",""total"":""0.03863593""},{""date"":""2014-03-09 12:19:00"",""type"":""sell"",""rate"":""0.0521"",""amount"":""0.69131"",""total"":""0.03601725""},{""date"":""2014-03-09 12:18:13"",""type"":""buy"",""rate"":""0.0589"",""amount"":""0.11399645"",""total"":""0.00671439""},{""date"":""2014-03-09 12:17:51"",""type"":""buy"",""rate"":""0.0589"",""amount"":""0.07611855"",""total"":""0.00448338""},{""date"":""2014-03-09 12:16:30"",""type"":""sell"",""rate"":""0.052"",""amount"":""1"",""total"":""0.052""},{""date"":""2014-03-09 12:15:50"",""type"":""buy"",""rate"":""0.0589"",""amount"":""1"",""total"":""0.0589""},{""date"":""2014-03-09 12:13:45"",""type"":""buy"",""rate"":""0.0579"",""amount"":""0.319"",""total"":""0.0184701""},{""date"":""2014-03-09 12:13:27"",""type"":""buy"",""rate"":""0.0579"",""amount"":""1.03"",""total"":""0.059637""},{""date"":""2014-03-09 12:13:01"",""type"":""sell"",""rate"":""0.0501"",""amount"":""1.1925"",""total"":""0.05974425""},{""date"":""2014-03-09 12:12:29"",""type"":""sell"",""rate"":""0.0547"",""amount"":""0.18"",""total"":""0.009846""},{""date"":""2014-03-09 12:11:05"",""type"":""buy"",""rate"":""0.0578"",""amount"":""3.59818846"",""total"":""0.20797529""},{""date"":""2014-03-09 12:10:57"",""type"":""buy"",""rate"":""0.0578"",""amount"":""0.28674965"",""total"":""0.01657412""},{""date"":""2014-03-09 12:10:14"",""type"":""buy"",""rate"":""0.0578"",""amount"":""0.09465745"",""total"":""0.00547120""},{""date"":""2014-03-09 12:10:03"",""type"":""buy"",""rate"":""0.0578"",""amount"":""6.72533772"",""total"":""0.38872452""},{""date"":""2014-03-09 12:09:31"",""type"":""buy"",""rate"":""0.0578"",""amount"":""0.99506672"",""total"":""0.05751485""},{""date"":""2014-03-09 12:05:20"",""type"":""buy"",""rate"":""0.0578"",""amount"":""1.3"",""total"":""0.07514""},{""date"":""2014-03-09 12:04:19"",""type"":""buy"",""rate"":""0.05779"",""amount"":""0.62772245"",""total"":""0.03627608""},{""date"":""2014-03-09 11:59:38"",""type"":""sell"",""rate"":""0.0577"",""amount"":""0.57879797"",""total"":""0.03339664""},{""date"":""2014-03-09 11:59:25"",""type"":""sell"",""rate"":""0.0577"",""amount"":""1.95853574"",""total"":""0.11300751""},{""date"":""2014-03-09 11:59:13"",""type"":""sell"",""rate"":""0.0577"",""amount"":""0.14369209"",""total"":""0.00829103""},{""date"":""2014-03-09 11:59:01"",""type"":""sell"",""rate"":""0.0577"",""amount"":""2"",""total"":""0.1154""},{""date"":""2014-03-09 11:58:51"",""type"":""sell"",""rate"":""0.0577"",""amount"":""1.08329258"",""total"":""0.06250598""},{""date"":""2014-03-09 11:58:41"",""type"":""sell"",""rate"":""0.0577"",""amount"":""0.1968116"",""total"":""0.01135602""},{""date"":""2014-03-09 11:58:28"",""type"":""sell"",""rate"":""0.05771"",""amount"":""0.62898042"",""total"":""0.03629846""},{""date"":""2014-03-09 11:58:22"",""type"":""buy"",""rate"":""0.0577"",""amount"":""5"",""total"":""0.2885""},{""date"":""2014-03-09 11:46:18"",""type"":""buy"",""rate"":""0.055"",""amount"":""0.19785934"",""total"":""0.01088226""},{""date"":""2014-03-09 11:44:20"",""type"":""buy"",""rate"":""0.054"",""amount"":""0.28181703"",""total"":""0.01521811""},{""date"":""2014-03-09 11:43:47"",""type"":""buy"",""rate"":""0.054"",""amount"":""0.5015"",""total"":""0.027081""},{""date"":""2014-03-09 11:43:10"",""type"":""buy"",""rate"":""0.054"",""amount"":""1.24097802"",""total"":""0.06701281""},{""date"":""2014-03-09 11:42:57"",""type"":""buy"",""rate"":""0.054"",""amount"":""1.65302086"",""total"":""0.08926312""},{""date"":""2014-03-09 11:39:58"",""type"":""buy"",""rate"":""0.054"",""amount"":""0.5075226"",""total"":""0.02740622""},{""date"":""2014-03-09 11:39:14"",""type"":""buy"",""rate"":""0.054"",""amount"":""0.20806301"",""total"":""0.01123540""},{""date"":""2014-03-09 11:38:15"",""type"":""buy"",""rate"":""0.054"",""amount"":""0.3"",""total"":""0.0162""},{""date"":""2014-03-09 11:37:11"",""type"":""buy"",""rate"":""0.054"",""amount"":""3.27893855"",""total"":""0.17706268""},{""date"":""2014-03-09 11:35:10"",""type"":""buy"",""rate"":""0.054"",""amount"":""1.39189452"",""total"":""0.07516230""},{""date"":""2014-03-09 11:34:07"",""type"":""buy"",""rate"":""0.0537"",""amount"":""1.25225176"",""total"":""0.06724591""}]";
                    //Logger.Debug("json poloniex=" + theJson);
                    DateTime fake = DateTime.Now;
                    // Deserialize 
                    if (theJson.Length > 0)
                    {
                        List<CxPoloniexTrade> poloTrades = Activator.CreateInstance<List<CxPoloniexTrade>>();
                        using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(theJson)))
                        {
                            DataContractJsonSerializer serializer = new DataContractJsonSerializer(poloTrades.GetType());
                            poloTrades = (List<CxPoloniexTrade>)serializer.ReadObject(ms);
                            //mlStocks.Add(CxCryptsy.getStock(cryptsy));
                        }

                        bool foundLastExec = false;
                        foreach (CxPoloniexTrade trade in poloTrades)
                        {
                            CxStockOrder order = new CxStockOrder(trade.rate, trade.amount, trade.date, 0, "");
                            order.Executed = true;
                            if (trade.type.Equals("sell"))
                                order.Action = ExAction.Sell;
                            else
                                order.Action = ExAction.Buy;

                            if (!foundLastExec)
                            {
                                foundLastExec = true;
                                polStock.LastTrade = trade.amount;
                            }
                            //
                            polStock.RecentTrades.Add(order);
                        }
                    }

                    // Order book, asks and bids
                    // {"asks":[[0.04599,1.24345875],[0.046,0.9979999],[0.05051,0.24605584],[0.054,0.28293373],[0.055,4.99],[0.057,7.79562059],[0.059,26.51081919],[0.0598,1.33480227] ...
                    //  "bids":[[0.04,1],[0.0391,1],[0.0385,1],[0.0383,0.103],[0.0382,10.06709476],[0.036,10],[0.03599,1],[0.0351,0.3],[0.035,7.7. ...
                    //string theJson = @"{""asks"":[[0.04599,1.24345875],[0.046,0.9979999],[0.05051,0.24605584],[0.054,0.28293373],[0.055,4.99],[0.057,7.79562059],[0.059,26.51081919],[0.0598,1.33480227],[0.0599,13.96074096],[0.05999,12.08888888],[0.059991,0.5],[0.06,23.16812975],[0.0615,0.0998],[0.062,0.01983047],[0.0621,2.59997999],[0.0625,0.5],[0.063,2.39519999],[0.0634,0.4],[0.0635,1],[0.0645,1],[0.065,9.98612662],[0.066889,1],[0.0674,3.75],[0.0675,0.5],[0.068,44.9274309],[0.0695,2.45],[0.0697,0.2],[0.069999,24.9373869],[0.0699999,1.01935465],[0.07,21.14730676],[0.072,0.499],[0.07389,0.8679871],[0.0745,2.8493],[0.075,13.55764677],[0.0761,0.36637985],[0.0769,0.8],[0.0771,0.5988],[0.0776987,6.46693569],[0.0785,3.45],[0.079,0.48162854],[0.0798,1.5],[0.08,3.51946135],[0.081,0.83892407],[0.0838,1.5],[0.08389,30.5585062],[0.085,4.30463598],[0.0885,4.45],[0.089,6],[0.08999,1.2796744],[0.09,7.33314155],[0.0909,0.998],[0.091,3.15422401],[0.094,0.649898],[0.094482,0.30176554],[0.095,5.82073865],[0.09526,1.5],[0.096,5.38201296],[0.097,5],[0.097795,3],[0.098,0.05893499],[0.09989,2.92252481],[0.09999,2.34904701],[0.0999998,61],[0.0999999,5],[0.1,26.53539394],[0.105,6.45],[0.10789,2.7],[0.112,8.95],[0.115,8.498],[0.119,5.97599999],[0.1197,1],[0.12,10.94019222],[0.1249,2.96745909],[0.125,2.8780574],[0.1272,4],[0.12777,6.17],[0.12778,2.75538476],[0.128,2.99399999],[0.129,2.82386424],[0.13,10.04027014],[0.13089,1.2],[0.131,1],[0.135,1],[0.1385,3],[0.13999,0.12053346],[0.14,13.24146264],[0.141,10],[0.14688,1.3],[0.149,1.01421143],[0.1499,4.91402623],[0.15,13.32799657],[0.1549,1.692],[0.15785,1.98999997],[0.16,1.996],[0.161,10],[0.17,8.13932046],[0.1785,2.20679415],[0.1845,6.49489615],[0.185,1.12517346],[0.19,26.96999],[0.2,2.70560751],[0.207007,0.11353413],[0.26,0.30707707],[0.28,4.98999998],[0.3,1.40778032],[0.349,2],[0.39,10],[0.4,1],[0.42,0.41916],[0.5,10.20069479],[0.59,10],[0.6,2.13261014],[0.7,0.0998],[0.98,0.4926796],[0.99,5],[1,1]],""bids"":[[0.04,1],[0.0391,1],[0.0385,1],[0.0383,0.103],[0.0382,10.06709476],[0.036,10],[0.03599,1],[0.0351,0.3],[0.035,7.71371457],[0.034,9.12],[0.0329,10.5],[0.0321,1],[0.032,15],[0.0313,2],[0.0312,2.22244094],[0.031,15.5716129],[0.03,14.13333333],[0.0231,6.5],[0.023,10],[0.022,15],[0.02,10],[0.01502,10],[0.01501,1.68],[0.015,30],[0.0147,6.56249727],[0.013,32],[0.0125,13],[0.01234001,126.30228176],[0.012,8],[0.0101,10],[0.01,30],[0.008,50],[0.00347562,1],[0.0016,301],[0.0001,100],[0.00001000,100],[0.00000128,100],[0.00000121,100],[0.00000010,10]]}";
                    theJson = CxUtil.getWebPage("https://poloniex.com/public?command=returnOrderBook&currencyPair=BTC_AUR");
                    //Logger.Debug("json poloniex=" + theJson);
                    // Deserialize 
                    if (theJson.Length > 0)
                    {
                        var serializer = new JavaScriptSerializer();
                        dynamic value = serializer.DeserializeObject(theJson);
                        
                        // asks
                        bool foundAsk = false;
                        bool foundSell = false;
                        var asks = value["asks"];
                        foreach( dynamic dyn in asks )
                        {
                            CxStockOrder order = new CxStockOrder( (double)dyn[0], (double)dyn[1], "", 0, "");
                            order.Executed = false;
                            order.Action = ExAction.Sell;
                            polStock.RecentTrades.Add(order);
                            // The first ask in line    
                            if( !foundSell )
                            {
                                foundSell = true;
                                polStock.Ask = (double)dyn[0];
                            }
                        }
                        // bids
                        var bids = value["bids"];
                        foreach( dynamic dyn in bids )
                        {
                            CxStockOrder order = new CxStockOrder( (double)dyn[0], (double)dyn[1], "", 0.0, "");
                            order.Executed = false;
                            order.Action = ExAction.Buy;
                            polStock.RecentTrades.Add(order);
                            // The first bid in line    
                            if (!foundAsk)
                            {
                                foundAsk = true;
                                polStock.Bid = (double)dyn[0];
                            }
                        }

                    }

                    // Pol stock
                    mlStocks.Add(polStock);
                }
                catch (Exception ex)
                {
                    Logger.Error("Poloniex, err=" + ex);
                }

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
