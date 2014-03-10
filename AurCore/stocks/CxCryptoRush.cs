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
    { "AUR/BTC" : { "market_id": "222", "last_trade": "0.12000000", "last_sell": "0.12000000", "last_buy": "0.12000000", "current_ask":"0.11999998", 
     "current_ask_volume":"0.51101405", "current_bid":"0.11900000", "current_bid_volume":"1.10280168", "highest_24h": "0.90000000", 
     "lowest_24h": "0.00670000", "volume_base_24h": "756.83538801", "min_tradable": "0.10000000", "Average": "0.03400583" } } 
    */
    [Serializable, DataContract(Name = "SOME")]
    public class CxCryptoRush
    {

        //
        public ExExchange mxExchange = ExExchange.None;
        private string msSymbol = "";
        private double mdmarket_id = 0;
        private double mdlast_trade = 0;
        private double mdlast_sell = 0;
        private double mdlast_buy = 0;
        private double mdcurrent_ask = 0;
        private double mdcurrent_ask_volume = 0;
        private double mdcurrent_bid = 0;
        private double mdcurrent_bid_volume = 0;
        private double mdhighest_24h = 0;
        private double mdlowest_24h = 0;
        private double mdvolume_base_24h = 0;
        private double mdmin_tradable = 0;
        private double mdAverage = 0;
        
        //
        public CxCryptoRush(){ }

        public ExExchange Exchange 
        {
            get{ return mxExchange; }
            set{ mxExchange = value; }
        }

        
        [DataMember]
        public string Symbol 
        {
            get{ return msSymbol; }
            set{ msSymbol = value; }
        }

        [DataMember]
        public double market_id
        {
            get { return mdmarket_id; }
            set { mdmarket_id = value; }
        }

        [DataMember]
        public double last_trade
        {
            get { return mdlast_trade; }
            set { mdlast_trade = value; }
        }
        [DataMember]
        public double last_sell 
        {
            get { return mdlast_sell; }
            set { mdlast_sell = value; }
        }

        [DataMember]
        public double last_buy
        {
            get { return mdlast_buy; }
            set { mdlast_buy = value; }
        }


        [DataMember]
        public double current_ask
        {
            get { return mdcurrent_ask; }
            set { mdcurrent_ask = value; }
        }

        [DataMember]
        public double current_ask_volume
        {
            get { return mdcurrent_ask_volume; }
            set { mdcurrent_ask_volume = value; }
        }

        [DataMember]
        public double current_bid
        {
            get { return mdcurrent_bid; }
            set { mdcurrent_bid = value; }
        }

        [DataMember]
        public double current_bid_volume 
        {
            get { return mdcurrent_bid_volume; }
            set { mdcurrent_bid_volume = value; }
        }

        [DataMember]
        public double highest_24h
        {
            get { return mdhighest_24h; }
            set { mdhighest_24h = value; }
        }

        [DataMember]
        public double lowest_24h
        {
            get { return mdlowest_24h; }
            set { mdlowest_24h = value; }
        }

        [DataMember]
        public double volume_base_24h 
        {
            get { return mdvolume_base_24h; }
            set { mdvolume_base_24h = value; }
        }

        [DataMember]
        public double min_tradable
        {
            get { return mdmin_tradable; }
            set { mdmin_tradable = value; }
        }

        [DataMember]
        public double Average 
        {
            get { return mdAverage; }
            set { mdAverage = value; }
        }


        
        
        // convert the serialized object to CxMarketStock
        static public CxMarketStock getStock( CxCryptoRush crush )
        {

            CxMarketStock stock = new CxMarketStock();

            try
            {
                stock.Exchange = ExExchange.CryptoRush;
                stock.Symbol = "BTC_AUR";
                //
                stock.Volume = crush.volume_base_24h;
                stock.LastTrade = crush.last_trade;
                stock.Price = crush.last_trade;
                stock.Ask = crush.last_sell;
                stock.Bid = crush.last_buy;
                stock.Id = crush.market_id;
                //stock.RecentTrades.Add(new CxStockOrder(crush.last_trade, 0, "", 1, ""));

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
            return this.Exchange.ToString() + CxUtil.COMMA  + this.Symbol;
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
            if (this.Symbol == compare.Symbol)
                return true;
            else
                return false;
        }

        // Equals
        public override int GetHashCode()
        {
            return Symbol.GetHashCode();
        }



    }  // EOC
}