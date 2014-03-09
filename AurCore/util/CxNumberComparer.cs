using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;


namespace org.auroracoin.aurcore.util
{

    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class CxNumberComparer : IComparer
    {
	    /// <summary>
	    /// Class constructor.  Initializes various elements
	    /// </summary>
	    public CxNumberComparer()
	    {
	    }

        public int Compare(Object x, Object y)
        {
            int result = 0;
            try
            {
                //
                double comp1 = 0;
                double comp2 = 0;
                // wing out the % and $ symbols 
                if( !x.ToString().Equals( string.Empty )) 
                    comp1 = (double)double.Parse( x.ToString().Replace( "$", string.Empty).Replace( "%", string.Empty).Replace( CxUtil.DESCENDING_ARROW, string.Empty ).Replace( CxUtil.ASCENDING_ARROW, string.Empty ) );
                if (!y.ToString().Equals(string.Empty))
                    comp2 = (double)double.Parse(y.ToString().Replace("$", string.Empty).Replace("%", string.Empty).Replace(CxUtil.DESCENDING_ARROW, string.Empty).Replace(CxUtil.ASCENDING_ARROW, string.Empty));
                //
                result = comp1.CompareTo(comp2);
            }
            catch( Exception )
            {
                throw new ArgumentException("This item is not a number.");
            }
            return result;
        }

    }  // EOC
}
