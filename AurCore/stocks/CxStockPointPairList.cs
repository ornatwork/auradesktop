//
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
//
using ZedGraph;
//
using org.auroracoin.aurcore.util;


namespace org.auroracoin.aurcore.stocks
{
    public class CxMarketStockPointPairList
    {
        [XmlArray("PointPairs"), XmlArrayItem(typeof(PointPair))]
        public PointPairList List = new PointPairList();
        private const string XML_DAYS_NODES = "methodResponse/soi/days/day";
        public string Symbol = string.Empty;

        
        // Serialize need empty construct
        public CxMarketStockPointPairList() {}

        // Populates the point list for the given symbol
        public CxMarketStockPointPairList(string psSymbol, IList<CxStockOrder> plOrders )
        {
            try
            {
                // Mark the symbol
                this.Symbol = psSymbol;

                foreach (CxStockOrder order in plOrders)
                {
                    DateTime DatePoint = order.AsOf;
                    double Average = order.Price;
                    // convert 
                    double dblDate = (double)new XDate(DatePoint);
                    // set it 
                    this.List.Add(dblDate, Average);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error=" + ex);
            }
        }


        // Populates the point list for the given symbol
        public CxMarketStockPointPairList( string psSymbol, string psXmlDoc )
        {
            try
            {
                // Mark the symbol
                this.Symbol = psSymbol; 

                // load the xml
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(psXmlDoc);

                // Those are all of the stock points
                XmlNodeList nodes = doc.SelectNodes(XML_DAYS_NODES);
                foreach (XmlNode node in nodes)
                {
                    DateTime DatePoint = CxUtil.getDateTime(CxUtil.getNodeText(node, "Date"));
                    double Average = CxUtil.getDouble(CxUtil.getNodeText(node, "Average"));
                    
                    // convert 
                    double dblDate = (double)new XDate(DatePoint);
                    // set it 
                    this.List.Add(dblDate, Average);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine( "Error=" + ex );
            }
        }


        // Populates the point list for the given symbol
        public CxMarketStockPointPairList(string psSymbol, string[] plRows)
        {
// Date Hour,Volume,MarketCap
//2009-03-29 21,2,955310.88
//2009-03-29 20,11,955613.92
//2009-03-29 19,44,955127.05


            try
            {
                // Mark the symbol
                this.Symbol = psSymbol;

                foreach( string line in plRows )
                {
                    string[] slots = line.Split(' ');
                    string[] secondHalf = slots[1].Split(',');
                    string datestring = slots[0] + " " + secondHalf[0].PadLeft(2, '0') + ":00" + ":00";
                    //
                    //Console.WriteLine( datestring );
                    DateTime DatePoint = DateTime.Parse( datestring );
                    double Average = double.Parse(secondHalf[2]);
                    // convert 
                    double dblDate = (double)new XDate(DatePoint);
                    // set it 
                    this.List.Add(dblDate, Average);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error=" + ex);
            }
        }



    }  // EOC
}