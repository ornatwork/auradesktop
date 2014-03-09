﻿//
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
//
using org.auroracoin.aurcore.util;



namespace org.auroracoin.desktop.ui
{

    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class CxListViewColumnSorter : IComparer
    {
	    /// <summary>
	    /// Specifies the column to be sorted
	    /// </summary>
	    private int ColumnToSort;
	    /// <summary>
	    /// Specifies the order in which to sort (i.e. 'Ascending').
	    /// </summary>
	    private SortOrder OrderOfSort;
	    /// <summary>
	    /// Case insensitive comparer object
	    /// </summary>
        private CaseInsensitiveComparer ObjectCompare = new CaseInsensitiveComparer();
        private CxNumberComparer NumberCompare = new CxNumberComparer();
        private int[] miTextColums;

	    /// <summary>
	    /// Class constructor.  Initializes various elements
	    /// </summary>
        public CxListViewColumnSorter( int[] piTextColums  )
	    {
		    // Initialize the column to '0'
		    ColumnToSort = 0;

		    // Initialize the sort order to 'none'
		    OrderOfSort = SortOrder.None;
            miTextColums = piTextColums;

	    }

	    /// <summary>
	    /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
	    /// </summary>
	    /// <param name="x">First object to be compared</param>
	    /// <param name="y">Second object to be compared</param>
	    /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
	    public int Compare(object x, object y)
	    {
		    int compareResult;
		    ListViewItem listviewX, listviewY;

		    // Cast the objects to be compared to ListViewItem objects
		    listviewX = (ListViewItem)x;
		    listviewY = (ListViewItem)y;

            // Don't sort on requested tags 
            if( listviewX.Tag != null && listviewX.Tag.ToString().Equals(CxUiUtil.DONT_SORT_TAG))
                return compareResult = 1;
            if (listviewX.Tag != null && listviewY.Tag.ToString().Equals(CxUiUtil.DONT_SORT_TAG))
                return compareResult = -1;

            // Text cols are passed in constructor
            if( this.isTextColum( ColumnToSort ))
            {
                // Sorting text 
                compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);
            }
            else
            {
                // Sorting numbers
                compareResult = NumberCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);
            }
    			
		    // Calculate correct return value based on object comparison
		    if (OrderOfSort == SortOrder.Ascending)
		    {
			    // Ascending sort is selected, return normal result of compare operation
			    return compareResult;
		    }
		    else if (OrderOfSort == SortOrder.Descending)
		    {
			    // Descending sort is selected, return negative result of compare operation
			    return (-compareResult);
		    }
		    else
		    {
			    // Return '0' to indicate they are equal
			    return 0;
		    }
	    }

        //
        private bool  isTextColum(int piCol)
        {
            foreach (int col in this.miTextColums)
                if (col == piCol) return true;
            
            //
            return false;
        }


	    /// <summary>
	    /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
	    /// </summary>
	    public int SortColumn
	    {
		    set
		    {
			    ColumnToSort = value;
		    }
		    get
		    {
			    return ColumnToSort;
		    }
	    }

	    /// <summary>
	    /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
	    /// </summary>
	    public SortOrder Order
	    {
		    set
		    {
			    OrderOfSort = value;
		    }
		    get
		    {
			    return OrderOfSort;
		    }
	    }
        
    }  // EOC
}
