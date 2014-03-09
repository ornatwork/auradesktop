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
    public class CxStockPointList
    {

        [XmlArray("StockPoints"), XmlArrayItem(typeof(StockPt))]
        public StockPointList List = new StockPointList();
        public string Symbol = string.Empty;
        //
        private const string XML_DAYS_NODES = "methodResponse/soi/days/day";
        
               

        // Populates the point list for the given symbol
        public CxStockPointList( string psSymbol, string psXmlDoc )
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
                
                // Reverse the order, to have it by date first to last
                List<XmlNode> revNodes = new List<XmlNode>( nodes.Count ); 
                foreach (XmlNode node in nodes)
                    revNodes.Insert(0, node );

                // Create points from list 
                foreach (XmlNode node in revNodes)
                {
                    DateTime DatePoint = CxUtil.getDateTime(CxUtil.getNodeText(node, "Date"));
                    double dblDate = (double)new XDate(DatePoint);
                    //Console.WriteLine("date=" + DatePoint.ToString());

                    //
                    double Hi = CxUtil.getDouble(CxUtil.getNodeText(node, "High"));
                    double Lo = CxUtil.getDouble(CxUtil.getNodeText(node, "Low"));
                    double Open = CxUtil.getDouble(CxUtil.getNodeText(node, "Open"));
                    double Close = CxUtil.getDouble(CxUtil.getNodeText(node, "Close"));
                    double Volume = CxUtil.getDouble(CxUtil.getNodeText(node, "Volume"));


                    // Create a StockPt 
                    string tag = "High " + Hi + ", Low " + Lo + ", Open " + Open + ", Close " + Close + ", Vol " + Volume;
                    StockPt pt = new StockPt( dblDate, Hi, Lo, Open, Close, Volume, tag );
                    // if price is increasing color = red = 1, else color = black = 2
                    pt.ColorValue = Close > Open ? 2 : 1;
                    //
                    this.List.Add(pt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine( "Error=" + ex );
            }
        }



    }  // EOC
}