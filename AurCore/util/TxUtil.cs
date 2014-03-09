//
#if DEBUG
//
using System;
using System.Security;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

//
using NUnit.Framework;
using log4net;
//
using org.auroracoin.aurcore.stocks;


// Initializes log4net, reads from the configuration file of the running assembly
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace org.auroracoin.aurcore.util
{

    [TestFixture]
    public class TxUtil
    {
        private static ILog Logger = LogManager.GetLogger(typeof(TxUtil));

        [Test]
        public void testDates()
        {
            DateTime locall = DateTime.Now;
            DateTime estTime = CxUtil.getEstTimeFromLocalTime(locall);
            Logger.Debug("time=" + estTime.ToShortDateString());

            //Assert.AreEqual("Wayne Gretzky", tester);
        }

        [Test]
        public void TestgetWebPage()
        {
            //
            string tmp = CxUtil.getWebPage("https://cryptorush.in/api.php?get=market&m=aur&b=btc&key=f707fe822b398a93f3e7c3f4717e66325d0f2af7&id=14386&json=true");
            Logger.Debug("return=" + tmp);
            Assert.IsNotNull( tmp );
        }

        [Test]
        public void testJsonDeser()
        {
 /*
 { "AUR/BTC" : { "market_id": "222", "last_trade": "0.12000000", "last_sell": "0.12000000", "last_buy": "0.12000000", "current_ask":"0.11999998", 
  "current_ask_volume":"0.51101405", "current_bid":"0.11900000", "current_bid_volume":"1.10280168", "highest_24h": "0.90000000", 
  "lowest_24h": "0.00670000", "volume_base_24h": "756.83538801", "min_tradable": "0.10000000", "Average": "0.03400583" } } 
*/

            try
            {
                // Get the xml from the site
                //System.Diagnostics.Debug.WriteLine("Get stocks, " + CxUtil.getTimeString(DateTime.Now));
                string theJson = CxUtil.getWebPage("https://cryptorush.in/api.php?get=market&m=aur&b=btc&key=f707fe822b398a93f3e7c3f4717e66325d0f2af7&id=14386&json=true", 5*1000);
                // Brute hack out the "AUR/BTC" element
                theJson = theJson.Replace("{\n\"AUR/BTC\" : ", "");
                theJson = theJson.Remove(theJson.Length - 1);

                System.Diagnostics.Debug.WriteLine("response=" + theJson );


                CxCryptoRush crypt = Activator.CreateInstance<CxCryptoRush>();
                using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(theJson)))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(crypt.GetType());
                    crypt = (CxCryptoRush)serializer.ReadObject(ms);
                }







                //
                Logger.Debug("crypt id=" + crypt.market_id + ", last_sell=" + crypt.last_sell + ", " + crypt.ToString());
            }
            catch( Exception ex )
            {
                Logger.Error( "Error=" + ex);
            }
        }

    }  // EOC

}
#endif
