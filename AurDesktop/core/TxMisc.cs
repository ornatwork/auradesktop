//
#if DEBUG
//
using System;
using System.Security;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Text;
//
using NUnit.Framework;
//
using org.auroracoin.aurcore.util;
using org.auroracoin.aurcore.stocks;
using org.auroracoin.desktop.core;



namespace org.auroracoin.desktop.core
{

    [TestFixture]
    public class TxMisc
    {

        [Test]
        public void testSome()
        {

            // "http://x.com/services/WxGeneral.asmx/getTest",
            //    "http://localhost:1100/services/WxGeneral.asmx/getTest",
/*
            string here = CxUtil.sendHttpPingPOST
                (
                "http://localhost:1100/services/WxGeneral.asmx/getTest",
                "psStockOSId=1548&psStockSymbol=AIRJ"
                );
*/
            string here = CxUtil.sendHttpPingPOST
            (
                "http://x.com/services/WxGeneral.asmx/getStockOrders",
                "psStockOSId=1548&psStockSymbol=AIRJ"
            );
            

            Console.WriteLine(here);

        }


        /*
        [Test]
        public void testLogin()
        {
            //
            string resp = CxDeskUtil.loginToOs(string.Empty, string.Empty);
            Assert.IsNotNull(resp);
            //
            Console.WriteLine( "MarketTime=" + CxGlobal.getMarketEstTime().ToString() );
        }
        */


    }  // EOC

}
#endif
