﻿//
using System;
using System.Xml;
using System.Collections.Generic;
//
using org.auroracoin.aurcore.util;


namespace org.auroracoin.aurcore.stocks
{
    public class CxMarketStock : ICloneable
    {
        //
        public ExExchange mxExchange = ExExchange.None;
        private string msSymbol = string.Empty;
        private double mdChange = 0;
        private double mdVolume = 0;
        private double mdBid = 0;
        private double mdAsk = 0;
        private double mdPrice = 0;
        private double mdTotalShares = 0;

        //
        public double LastTrade = 0;
        //
        public string DayRange = string.Empty;
        public double Open = 0;
        public double PrevClose = 0;
        public DateTime TradeDateTime = DateTime.Now;
        public string FiftyTwoWKRange = string.Empty;
        public double MarketCap = 0;
        public DateTime LastSplitDate = DateTime.Now;
        public double AvgVolume = 0;
        //
        public int NoOfSplits = 0;
        public DateTime IPODate = DateTime.Now;
        public string PictureLink = string.Empty;
        public string WebLink = string.Empty;
        public string DebutYear = string.Empty;
        public double Id = 0;
        IList<CxStockOrder> recentTrades = new List<CxStockOrder>();

        //
        public CxMarketStock(){ }

        public ExExchange Exchange 
        {
            get{ return mxExchange; }
            set{ mxExchange = value; }
        }

        
        public string Symbol 
        {
            get{ return msSymbol; }
            set{ msSymbol = value; }
        }
        

        public double Change 
        {
            get{ return mdChange; }
            set{ mdChange = value; }
        }
        public double Value
        {
            get{ return TotalShares * Price; }
            //set{ mdValue = value; }
        }
        public double Bid
        {
            get { return mdBid; }
            set { mdBid = value; }
        }
        public double Price 
        {
            get{ return mdPrice; }
            set{ mdPrice = value; }
        }
        public double Ask 
        {
            get{ return mdAsk; }
            set { mdAsk = value; }
        }
        public double Volume
        {
            get{ return mdVolume; }
            set{ mdVolume = value; }
        }
        public double TotalShares
        {
            get { return mdTotalShares; }
            set { mdTotalShares = value; }
        }
        public IList<CxStockOrder> RecentTrades 
        {
            get { return recentTrades; }
            set { recentTrades = value; }
        }
        
        
        
        //
        public CxMarketStock(XmlNode pxNode)
        {
            try
            {
                DayRange = CxUtil.getNodeText(pxNode,"DayRange");
                Open = CxUtil.getDouble(CxUtil.getNodeText(pxNode, "Open"));
                PrevClose = CxUtil.getDouble(CxUtil.getNodeText(pxNode, "PrevClose"));

                Change = CxUtil.getDouble(CxUtil.getNodeText(pxNode, "Change"));
                Volume = CxUtil.getInt(CxUtil.getNodeText(pxNode, "Volume"));
                mdBid = CxUtil.getDouble(CxUtil.getNodeText(pxNode, "Bid"));
                mdAsk = CxUtil.getDouble(CxUtil.getNodeText(pxNode, "Ask"));
                TradeDateTime = CxUtil.getDateTime(CxUtil.getNodeText(pxNode, "TradeDateTime"));
                LastTrade = CxUtil.getDouble(CxUtil.getNodeText(pxNode, "LastTrade"));

                FiftyTwoWKRange = CxUtil.getNodeText(pxNode, "FiftyTwoWKRange");
                Price = CxUtil.getDouble(CxUtil.getNodeText(pxNode, "Price"));
                TotalShares = CxUtil.getInt(CxUtil.getNodeText(pxNode, "SOIsOutstanding"));
                MarketCap = CxUtil.getDouble(CxUtil.getNodeText(pxNode, "MarketCap"));
                LastSplitDate = CxUtil.getDateTime(CxUtil.getNodeText(pxNode, "LastSplitDate"));
                AvgVolume = CxUtil.getDouble(CxUtil.getNodeText(pxNode, "AvgVolume"));

                NoOfSplits = CxUtil.getInt(CxUtil.getNodeText(pxNode, "NoOfSplits"));
                Symbol = CxUtil.getNodeText(pxNode, "Symbol");
                //Sport = CxUtil.getSport(CxUtil.getNodeText(pxNode, "Sport"));
                IPODate = CxUtil.getDateTime(CxUtil.getNodeText(pxNode, "IPODate"));
                PictureLink = CxUtil.getNodeText(pxNode, "Photo85x85");
                // Set weblink for the stock, on the trading tab
                WebLink = "URL";
                // sniff out the OS id and Debut from the picture link
                string[] stemp = PictureLink.Split(CxUtil.BACK_SLASH);
                DebutYear = stemp[4];
                

                // REMEBER WHEN ADDING NEW PROPERTIES, ADD TO THE CLONE ALSO !!!!!!
                // REMEBER WHEN ADDING NEW PROPERTIES, ADD TO THE CLONE ALSO !!!!!!
            }
            catch (Exception ex)
            {
                Console.WriteLine( "Error=" + ex );
            }
        }

        // To display in combos etc
        public override string ToString()
        {
            return this.Exchange.ToString() + CxUtil.COMMA  + this.Symbol;
        }



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
            stRet.IPODate = this.IPODate ;
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
            stRet.Id = this.Id;
            stRet.DebutYear = this.DebutYear;
            stRet.RecentTrades = this.RecentTrades;
            // 
            return stRet;
        }

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