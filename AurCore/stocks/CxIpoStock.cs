//
using System;
using System.Xml;
//
using org.auroracoin.aurcore.util;


namespace org.auroracoin.aurcore.stocks
{
    public class CxIpoStock 
    {
        //
        public string Symbol = string.Empty;
        public string FirstName = string.Empty;
        public string LastName = string.Empty;
        public string Range = string.Empty;
        public string ChallengeQuestion = string.Empty;
        public ExSport mxSport = ExSport.None;
        public int Rank = 0;
        public ExIpoStatus Status = ExIpoStatus.Request;

        //
        public CxIpoStock(){ }

        
        public string Player 
        {
            get{ return FirstName + " " + LastName; }
        }

        public string PlayerLinkName
        {
            get { return FirstName.ToLower() + "_" + LastName.ToLower(); }
        }


        // Ipo stock is $5 
        public double Price 
        {
            get{ return 5; }
        }

        // To display in combos etc
        public override string ToString()
        {
            if( this.Symbol != string.Empty )
                return this.FirstName + CxUtil.SPACE + this.LastName + CxUtil.COMMA + CxUtil.SPACE + this.Symbol + CxUtil.COMMA + CxUtil.SPACE + this.Status.ToString();
            else
                return this.FirstName + CxUtil.SPACE + this.LastName + CxUtil.COMMA + CxUtil.SPACE + this.Status.ToString();
        }


    }  // EOC
}