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

/*
{"success":1,"return":{"markets":{"AUR":{"marketid":"160","label":"AUR\/BTC","lasttradeprice":"0.13032400","volume":"55967.92353022","lasttradetime":"2014-03-04 09:13:58",
"primaryname":"AuroraCoin","primarycode":"AUR","secondaryname":"BitCoin","secondarycode":"BTC","recenttrades":
[
{"id":"27927604","time":"2014-03-04 09:40:18","price":"0.12999999","quantity":"0.01148640","total":"0.00149323"},
....
*/
    [Serializable, DataContract(Name = "SOME")]
    public class CxCryptsy
    {

        //
        public ExExchange mxExchange = ExExchange.None;
        private double mdmarket_id = 0;
        private string msLabel = "";
        private double mdlasttradeprice = 0;
        private double mdvolume = 0;
        private string mslasttradetime= "";
        private string msprimaryname = "";
        private string msprimarycode = "";
        private string mssecondaryname = "";
        private string mssecondarycode = "";
        private IList<CxCryptsyRecentTrades> recentTrades = new List<CxCryptsyRecentTrades>();
        
        //
        public CxCryptsy(){ }

        public ExExchange Exchange 
        {
            get{ return mxExchange; }
            set{ mxExchange = value; }
        }


        [DataMember]
        public double market_id
        {
            get { return mdmarket_id; }
            set { mdmarket_id = value; }
        }

        
        [DataMember]
        public string Label
        {
            get { return msLabel; }
            set { msLabel = value; }
        }

        
        [DataMember]
        public double lasttradeprice 
        {
            get { return mdlasttradeprice; }
            set { mdlasttradeprice = value; }
        }
        
        
        [DataMember]
        public double volume
        {
            get { return mdvolume; }
            set { mdvolume = value; }
        }
        
        [DataMember]
        public string lasttradetime 
        {
            get { return mslasttradetime; }
            set { mslasttradetime = value; }
        }
        
        [DataMember]
        public string primaryname
        {
            get { return msprimaryname; }
            set { msprimaryname = value; }
        }
        
        [DataMember]
        public string primarycode
        {
            get { return msprimarycode; }
            set { msprimarycode = value; }
        }
        
        [DataMember]
        public string secondaryname
        {
            get { return mssecondaryname; }
            set { mssecondaryname = value; }
        }

        [DataMember]
        public string secondarycode
        {
            get { return mssecondarycode; }
            set { mssecondarycode = value; }
        }

        [DataMember]
        public IList<CxCryptsyRecentTrades> recenttrades
        {
            get { return this.recentTrades; }
            set { recentTrades = value; }
        }


        
        
        // convert the serialized object to CxMarketStock
        static public CxMarketStock getStock( CxCryptsy crush )
        {

            CxMarketStock stock = new CxMarketStock();

            try
            {
                stock.Exchange = ExExchange.Cryptsy;
                stock.Symbol = "BTC_AUR";
                //
                stock.Volume = crush.volume;
                stock.LastTrade = crush.lasttradeprice;
                stock.Price = crush.lasttradeprice;
                stock.Id = crush.market_id;
                
                // Recent trades, have to reverse the list as extracting the orders  
                /*
                for (int i = crush.recenttrades.Count - 1; i >= 0; i--)
                {
                    CxCryptsyRecentTrades trade = crush.recenttrades[i];
                    stock.RecentTrades.Add(new CxStockOrder(trade.price, trade.quantity, trade.time, trade.id, ""));
                }*/
                foreach( CxCryptsyRecentTrades trade in crush.recenttrades )               
                {
                    stock.RecentTrades.Add( new CxStockOrder(trade.price, trade.quantity, trade.time, trade.id, "") );
                }
                // REMEBER WHEN ADDING NEW PROPERTIES, ADD TO THE CLONE ALSO !!!!!!
                // REMEBER WHEN ADDING NEW PROPERTIES, ADD TO THE CLONE ALSO !!!!!!
            }
            catch (Exception ex)
            {
                Console.WriteLine( "Error=" + ex );
            }

            return stock;
        }
        

        // To display in combos etc
        public override string ToString()
        {
            return this.Exchange.ToString() + CxUtil.COMMA  + this.primarycode;
        }


        /*
        // Clones the object ( deep copy )
        public Object Clone()
        {
            // new deep clone object
            CxMarketStock stRet = new CxMarketStock();

            // copy all values
            stRet.Ask = this.Ask;
            stRet.AvgVolume = this.AvgVolume;
            stRet.Bid = this.Bid;
            stRet.Change = this.Change;
            stRet.DayRange = this.DayRange;
            stRet.FiftyTwoWKRange = this.FiftyTwoWKRange;
            stRet.FirstName = this.FirstName;
            stRet.IPODate = this.IPODate ;
            stRet.LastName = this.LastName;
            stRet.LastSplitDate = this.LastSplitDate;
            stRet.LastTrade = this.LastTrade;
            stRet.MarketCap = this.MarketCap;
            stRet.NoOfSplits = this.NoOfSplits;
            stRet.Open = this.Open;
            //stRet.Paid = this.Paid;
            stRet.PictureLink = this.PictureLink;
            stRet.WebLink = this.WebLink;
            stRet.PrevClose = this.PrevClose;
            stRet.Price = this.Price;
            //stRet.Shares = this.Shares;
            stRet.TotalShares = this.TotalShares;
            stRet.Exchange = this.Exchange;
            stRet.Symbol = this.Symbol;
            stRet.TradeDateTime = this.TradeDateTime;
            stRet.Volume = this.Volume;
            stRet.OsId = this.OsId;
            stRet.DebutYear = this.DebutYear;
            // 
            return stRet;
        }
        */

        // Equals
        public override bool Equals(object pxCompare)
        {
            if( !(pxCompare is CxMarketStock) ) return false;

            // Compare them on the symbol
            CxMarketStock compare = (CxMarketStock ) pxCompare;
            if (this.primarycode == compare.Symbol)
                return true;
            else
                return false;
        }

        // Equals
        public override int GetHashCode()
        {
            return this.primarycode.GetHashCode();
        }



    }  // EOC
}