//
using System;
using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Collections;
using System.Collections.Generic;
//
using org.auroracoin.aurcore.util;


namespace org.auroracoin.aurcore.stocks
{

    //  [{"date":"2014-03-09 23:09:39","type":"sell","rate":"0.04","amount":"1","total":"0.04"},
    //   {"date":"2014-03-09 22:47:36","type":"sell","rate":"0.0389","amount":"0.15","total":"0.005835"},
    //   {"date":"2014-03-09 22:38:56","type":"sell","rate":"0.046","amount":"0.36199","total":"0.01665154"}
    [Serializable]
    public class CxPoloniexTrade
    {
        //
        public string date = "";
        public string type = "";
        public double rate = 0;
        public double amount= 0;
        public double total = 0;
                
        //
        public CxPoloniexTrade() { }
        

    }  // EOC
}