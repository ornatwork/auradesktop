//
using System;
using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

//
using org.auroracoin.aurcore.util;


namespace org.auroracoin.aurcore.stocks
{

/*
{"success":1,"return":{"markets":{"AUR":{"marketid":"160","label":"AUR\/BTC","lasttradeprice":"0.13032400","volume":"55967.92353022","lasttradetime":"2014-03-04 09:13:58",
"primaryname":"AuroraCoin","primarycode":"AUR","secondaryname":"BitCoin","secondarycode":"BTC","recenttrades":
[
{"id":"27927604","time":"2014-03-04 09:40:18","price":"0.12999999","quantity":"0.01148640","total":"0.00149323"},
....
*/
    [Serializable]
    public class CxCryptsyRecentTrades
    {

        //
        public double id = 0;
        public string time = "";
        public double price = 0;
        public double quantity = 0;
        public double total = 0;
        //
        public CxCryptsyRecentTrades() { }



    }  // EOC
}