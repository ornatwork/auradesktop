//
using System;
using System.Xml;
//
using org.auroracoin.aurcore.util;


namespace org.auroracoin.aurcore.stocks
{
    public class CxMarketStockTrigger
    {
        
        // That's knowing who you are
        public int TriggerId = 0; 
        public int UserId = 0;
        public int StockId = 0;
        // The strike value, greater or less than
        public double Value = 0;
        // If the trigger price is higher than the value, otherwise it's lower
        public bool HigherThan = true;

        //
        public CxMarketStockTrigger() { }



    }  // EOC
}