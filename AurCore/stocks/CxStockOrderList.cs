//
using System;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace org.auroracoin.aurcore.stocks
{
    // Wrapper class used to serialize and de-serialize CxStockOrder list
    public class CxStockOrderList
    {
        [XmlArray("Orders"), XmlArrayItem(typeof(CxStockOrder))]
        public List<CxStockOrder> list = new List<CxStockOrder>();
        //public ArrayList list = new ArrayList();

        public CxStockOrderList() {}

    }  // EOC
}
