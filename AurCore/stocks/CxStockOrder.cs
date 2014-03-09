//
using System;
//
using org.auroracoin.aurcore.util;


namespace org.auroracoin.aurcore.stocks
{
    public class CxStockOrder : IComparable
    {
        public double Price = 0;
        public double Shares = 0;
        public ExAction Action = ExAction.Buy;
        public DateTime AsOf = DateTime.MinValue;
        public String Symbol = string.Empty;
        public bool Ipo = false;
        public string OsId = string.Empty;
        public bool Executed = true;
        
        
        //
        public CxStockOrder(){}

        public CxStockOrder(double pdPrice, double pdShares, string psAsOf, double psId, string psTime)
        {
            try
            {
                Price =  pdPrice;
                Shares = pdShares;
                AsOf = DateTime.Parse( psTime );
                OsId = psId.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error=" + ex);
            }

       }

        // sorting in ascending order
        public int CompareTo(object obj)
        {
            CxStockOrder order = (CxStockOrder)obj;
            return
                this.AsOf.CompareTo(order.AsOf);
        }


    }  // EOC
}