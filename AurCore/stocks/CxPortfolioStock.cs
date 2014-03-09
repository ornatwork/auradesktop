//
using System;
using System.Xml;
//
using org.auroracoin.aurcore.util;


namespace org.auroracoin.aurcore.stocks
{
    public class CxPortfolioStock 
    {
        public string Symbol = string.Empty;
        public double Paid = 0;
        public double Shares = 0;
        public bool Ipo = false;
        // Used when carrying data such as cash
        public bool Info = false;
        // For the cash hack
        public const string CASH        = "  CH";
        public const string RESV_CASH   = " RCH";

        //
        public CxPortfolioStock()
        { }

        public CxPortfolioStock(string psSymbol, string psPaid, string psShares, bool pbIpo )
        {
            try
            {
                Symbol = psSymbol;
                Paid = CxUtil.getDouble(psPaid);
                Shares = CxUtil.getDouble(psShares);
                Ipo = pbIpo;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error=" + ex);
            }

       }

        //
        public CxPortfolioStock(XmlNode pxNode)
        {
            try
            {
                Symbol = CxUtil.getNodeText(pxNode, "symbol");
            }
            catch (Exception ex)
            {
                Console.WriteLine( "Error=" + ex );
            }
        }


    }  // EOC
}