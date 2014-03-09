//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
//
using ZedGraph;
//
using org.auroracoin.aurcore.stocks;
using org.auroracoin.aurcore.util;


namespace org.auroracoin.desktop.ui
{
    public partial class FxHiLoChart : Form
    {
        // The stock that orders are going to be displayed for
        private CxMarketStock mxStock;
        private List<CxStockOrder> mlExecuted = new List<CxStockOrder>();
        CxStockPointList mxPoints;


        public FxHiLoChart(CxMarketStock pxStock)
        {
            InitializeComponent();
            //
            mxStock = pxStock;
            this.Text += mxStock.ToString();
        }

        private void FxHiLoChart_Load(object sender, EventArgs e)
        {
            string thexml = CxUtil.getWebPage("");
            mxPoints = new CxStockPointList(mxStock.Symbol, thexml );
            // 
            this.chHiLo.IsShowPointValues = true;
            this.createChart(this.chHiLo, mxPoints, this.rdOHLC.Checked);
        }


        // Build the Chart
        private void createChart(ZedGraphControl zg1, CxStockPointList pxPoints, bool pbHIOC)
        {
            // Get a reference to the GraphPane
            GraphPane myPane = zg1.GraphPane;
            // First clear it
            myPane.CurveList.Clear();

            // Set the titles
            myPane.Title.Text = string.Empty;
            myPane.XAxis.Title.Text = string.Empty;
            myPane.YAxis.Title.Text = string.Empty;


            // Setup the gradient fill...
            // Use Red for negative days and black for positive days
            Color[] colors = { Color.Red, Color.Gray };
            Fill myFill = new Fill(colors);
            myFill.Type = FillType.GradientByColorValue;
            myFill.SecondaryValueGradientColor = Color.Empty;
            myFill.RangeMin = 1;
            myFill.RangeMax = 2;



            if (pbHIOC)
            {
                // symbols, and in the legend
                OHLCBarItem myCurve = myPane.AddOHLCBar(string.Empty, pxPoints.List,
                                                    Color.Empty);
                myCurve.Bar.GradientFill = myFill;
                myCurve.Bar.Width = 20;
                myCurve.Bar.IsOpenCloseVisible = false;
                myCurve.Bar.IsAutoSize = true;
            }
            else
            {

                JapaneseCandleStickItem myCurve = myPane.AddJapaneseCandleStick(string.Empty, pxPoints.List);
                myCurve.Stick.IsAutoSize = true;
                myCurve.Stick.Color = Color.Blue;
            }

            // Set the XAxis to date type
            myPane.XAxis.Type = AxisType.DateAsOrdinal;

            // Tell ZedGraph to refigure the axes since the data 
            // has changed
            zg1.AxisChange();
            zg1.Invalidate();
        }

        private void rdOHLC_CheckedChanged(object sender, EventArgs e)
        {
            this.createChart(this.chHiLo, mxPoints, this.rdOHLC.Checked);
        }

        private void rdCandle_CheckedChanged(object sender, EventArgs e)
        {
            this.createChart(this.chHiLo, mxPoints, this.rdOHLC.Checked);
        }

   
    }  // EOC
}
