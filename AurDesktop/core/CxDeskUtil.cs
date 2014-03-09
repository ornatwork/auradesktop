//
using System;
using System.Collections.Generic;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Net;
using System.IO;
using System.Text;
using System.Globalization;
using System.Web;
//
using MIL.Html;
using log4net;
//
using org.auroracoin.desktop.background;
using org.auroracoin.aurcore.stocks;
using org.auroracoin.aurcore.util;


namespace org.auroracoin.desktop.core
{
    public static class CxDeskUtil
    {

        private static ILog Logger = LogManager.GetLogger(typeof(CxDeskUtil));
        //  Must have cookie to keep the session alive
        //private static CookieContainer myContainer = new CookieContainer();
        private static string sessionID = string.Empty;
        //private static WxGeneral mWebService = new WxGeneral();
        public const string EMAIL_SUCCESSFULLY_SENT = "Your email was sent successfully.";

        static CxDeskUtil()
        {
            // start up with a dummy cookie
            //myContainer.Add( new Cookie("ASP.NET_SessionId", sessionID, "/",
                             //"https://x.com/"));
        }


        // Logs into os for the first time
        public static string loginToOs()
        {
            // Trap
            if (!CxGlobal.isLoginSet())
                throw new Exception(CxGlobal.LOGIN_NOT_CONFIGURED);

            //
            string sRet = "Already logged in";
            /*
            if( CxGlobal.LoggedIn )
            {
                return sRet;
            }
            else
            {
              
                // As we can't figure out if the login expires or not at OS
                // without login in again, we will just always login when 
                // contacting OS 
                //
                // This function will throw exception if unsuccessful loging into OS
                sRet = loginToOs(CxGlobal.OS_USERNAME, CxGlobal.OS_PASSWORD);
                // It's good
                CxGlobal.LoggedIn = true;
            //}
            */
            return sRet;
        }

        /*
        // Logs into os with the given credentials
        public static string loginToOs( string psUser, string psPassw)
        {
            //Console.WriteLine(CxUtil.getTimeString(DateTime.Now));
            Logger.Info("Login to os=" + CxUtil.getTimeString(DateTime.Now) );

            string sPage = CxUtil.sendHttpAuthenticatedPingPOST(
                CxGlobal.LOGIN_URL,
                "username=" + psUser + "&password=" + psPassw +  "&rememberMe=1&smbBtn=Submit&smbImg.x=27&smbImg.y=14",
                psUser,
                psPassw,
                myContainer
                );

            // Console.WriteLine(sPage);
            // Console.WriteLine("-----------------------------------------------------");

            try
            {
                DateTime OsTime = DateTime.MinValue;
                // <div id="marketHoursWrapper">Market OPEN: closes in 04:27:20</div>
                // <div id="marketHoursWrapper">Trading is closed on Saturdays</div>
                // class="nav_screen_name">soph</a>
                //sTest = "<div id=\"marketHoursWrapper\">Market OPEN: closes in 12:00:00</div>";
                //sTest = "<div id=\"marketHoursWrapper\">Market CLOSED: opens in 02:00:00</div>";


                // Extract the offset and the user, write to the ini file
                string sStartMarker = "nav_screen_name";
                string tmp = string.Empty;
                int iStart = sPage.IndexOf( sStartMarker );
                int iEnd = 0;
                if (iStart > -1)
                {
                    iStart += sStartMarker.Length + 2;
                    iEnd = sPage.IndexOf("</a>", iStart);
                    tmp = sPage.Substring(iStart, iEnd - iStart);
                    // Write the nickname to ini file
                    CxGlobal.OS_NICKNAME = tmp;
                    CxIniFile.getInstance().writeStringKey(CxIniFile.OS_NICKNAME_KEY, CxGlobal.OS_NICKNAME);
                }


                // Server time offset
                TimeSpan howLong = new TimeSpan();
                TimeSpan theDiff = new TimeSpan();
                sStartMarker = "marketHoursWrapper";
                iStart = sPage.IndexOf( sStartMarker );
                if (iStart > -1)
                {
                    iStart += sStartMarker.Length + 2;
                    iEnd = sPage.IndexOf( "</div>", iStart);
                    tmp = sPage.Substring(iStart, iEnd - iStart);

                    // On saturdays there is no clock 
                    if( tmp.ToLower().IndexOf("saturday") == -1)
                    {
                        // How long till open or close, depending on the time 
                        howLong = new TimeSpan(
                            int.Parse(tmp.Substring(tmp.Length - 8, 2)),
                            int.Parse(tmp.Substring(tmp.Length - 5, 2)),
                            int.Parse(tmp.Substring(tmp.Length - 2, 2))
                            );

                        // closes / opens ?
                        if (tmp.ToLower().IndexOf("closes") > -1)
                        {
                            //
                            DateTime OsEstMidnightTime = CxUtil.getEstTimeFromLocalTime( DateTime.Now ).Date.AddDays(1);
                            DateTime myEstTime = CxUtil.getEstTimeFromLocalTime( DateTime.Now );

                            // local diff
                            TimeSpan localspan = OsEstMidnightTime.Subtract(myEstTime);
                            // the diff betw local and server 
                            theDiff = howLong.Subtract( localspan );
                        }
                        else  // opens 
                        {
                            //
                            DateTime osOpenHour = CxUtil.getEstTimeFromLocalTime(DateTime.Now).Date.AddHours( 10 );
                            DateTime myEstTime = CxUtil.getEstTimeFromLocalTime(DateTime.Now);

                            // local diff
                            TimeSpan localspan = osOpenHour.Subtract(myEstTime);
                            // the diff betw local and server 
                            theDiff = howLong.Subtract(localspan);
                        }
                    }
                }
                // Set the offset
                CxGlobal.OS_TIME_OFFSET = theDiff;
            }
            catch (Exception ex) 
            {
                Logger.Error("Err=" + ex);
                //Console.WriteLine("Err=" + ex ); 
            }


            if (validateLogin(sPage))
                return "Login into was Successful";
            else
                throw new Exception("Login was NOT successful !");
        }
        */

        /*
        // Logs out from os with the given credentials
        public static string logoutFromOs(string psUser, string psPassw)
        {
            string sRet = string.Empty;

            try
            {
                sRet = CxUtil.sendHttpAuthenticatedPingPOST(
                    CxGlobal.LOGOUT_URL,
                    string.Empty,
                    psUser,
                    psPassw,
                    myContainer
                    );
            }
            catch (Exception ex)
            {
                Logger.Error("Err=" + ex);
            }

            return sRet;
        }
        */


        /*
        // Get the portfolio for the user
        // Another HTML parsing HACK JOB
        public static IList<CxPortfolioStock> getPortfolio()
        {


            // Make sure login is good, otherwise no reason to continue
            CxDeskUtil.loginToOs();

            int table = 0;
            IList<CxPortfolioStock> rList = new List<CxPortfolioStock>();

            string sRet = CxUtil.sendHttpAuthenticatedPingPOST(
                CxGlobal.PORTFOLIO_URL,
                string.Empty,
                CxGlobal.OS_USERNAME,
                CxGlobal.OS_PASSWORD,
                myContainer
                );


            string[] lines = sRet.Split('\n');
            //Console.WriteLine("lines=" + lines.Length);

            for( int i=0; i<lines.Length-1; i++ )
            {

                // Sniff out the cash, then the account table
                if (lines[i].IndexOf("</span>Cash:") > -1)
                {
                    CxPortfolioStock st = new CxPortfolioStock();
                    string cash = CxDeskUtil.getValue(">$", "</span>", lines[i]);
                    cash = cash.Replace("*", string.Empty);
                    //
                    st.Paid = CxUtil.getDouble(cash);
                    st.Info = true;
                    st.Symbol = CxPortfolioStock.CASH;
                    rList.Add(st);
                    //Console.WriteLine("******* cash=" + cash);
                }
                else if (lines[i].IndexOf("Reserved&nbsp;Cash:") > -1)
                {
                    
                    string cash = CxDeskUtil.getValue(">$", "</span>", lines[i]);
                    //
                    CxPortfolioStock st = new CxPortfolioStock();
                    st.Paid = CxUtil.getDouble(cash);
                    st.Info = true;
                    st.Symbol = CxPortfolioStock.RESV_CASH;
                    rList.Add(st);
                    //Console.WriteLine("******* reserved cash=" + cash);
                }
                else if (lines[i].IndexOf("account_table") > -1)
                {
                    // found table
                    table++;
                    // Diff in table structure
                    if (table == 1)
                        i += 13;
                    else
                        i += 15;


                    // process the portfolio stocks
                    while( i < lines.Length-1 )
                    {
                        if (table == 1)
                        {

                            // Don't process if no stock avail in this section
                            if (lines[i].IndexOf("</table>") > -1)
                                break;


                            CxPortfolioStock st = new CxPortfolioStock();
                            // Pick the ipo data 
                            st.Ipo = true;

                            // Symbol
                            st.Symbol = CxDeskUtil.getSymbol( "</a>", lines[i]);
                            //Console.WriteLine(lines[i]);
                            i++;
                            // Name 
                            //Console.WriteLine(lines[i]);
                            i++;
                            // Day change
                            //Console.WriteLine(lines[i]);
                            i++;
                            // Price
                            //Console.WriteLine(lines[i]);
                            i++;
                            // # of shares
                            st.Shares = CxUtil.getDouble( CxDeskUtil.getValue( "top\">", "</td>", lines[i] ) );
                            //Console.WriteLine(lines[i]);
                            i++;
                            // Market value
                            //Console.WriteLine(lines[i]);
                            i++;
                            // Shares outstanding
                            //Console.WriteLine(lines[i]);
                            i++;
                            // % of ownership
                            //Console.WriteLine(lines[i]);
                            i++;

                            // Add the stock
                            rList.Add(st);
                            
                            // closing
                            //Console.WriteLine(lines[i]);
                            i++;

                            // Check for table end
                            //Console.WriteLine(lines[i]);
                            if( lines[i].IndexOf( "</table>" )> -1 ) 
                                break;
                        }
                        else if (table == 2)
                        {

                            // Don't process if no stock avail in this section
                            if (lines[i].IndexOf("</table>") > -1)
                                break;


                            CxPortfolioStock st = new CxPortfolioStock();

                            // Symbol
                            st.Symbol = CxDeskUtil.getSymbol("</a>", lines[i]);
                            //Console.WriteLine(lines[i]);
                            i++;
                            // Name 
                            //Console.WriteLine(lines[i]);
                            i++;
                            // Day change
                            //Console.WriteLine(lines[i]);
                            i++;
                            // Price
                            //Console.WriteLine(lines[i]);
                            i++;
                            // Price paid
                            st.Paid = CxUtil.getDouble(CxDeskUtil.getValue("top\">", "</td>", lines[i]));
                            //Console.WriteLine(lines[i]);
                            i++;
                            // Sportfolio change
                            //Console.WriteLine(lines[i]);
                            i++;
                            // # of shares
                            st.Shares = CxUtil.getDouble(CxDeskUtil.getValue("top\">", "</td>", lines[i]));
                            //Console.WriteLine(lines[i]);
                            i++;
                            // Market Value
                            //Console.WriteLine(lines[i]);
                            i++;
                            // Sares outstanding
                            //Console.WriteLine(lines[i]);
                            i++;
                            // % of ownership
                            //Console.WriteLine(lines[i]);
                            i++;

                            // Add the stock
                            rList.Add(st);

                            // closing
                            //Console.WriteLine(lines[i]);
                            i++;

                            // Check for table end
                            //Console.WriteLine(lines[i]);
                            if (lines[i].IndexOf("</table>") > -1)
                                break;
                        }

                    }
                    
                    
                }
            }
                
            //Console.WriteLine( sRet );
            //Console.WriteLine("-----------------------------------------------------");
            return rList;
        }
        */
        
        
        
        
        // Connect to server for MarketCap
        public static CxMarketStockPointPairList getMarketCapList()
        {
            /* 
            // No need to login, this is HTTP 
            CxMarketStockPointPairList rList = new CxMarketStockPointPairList();
            try
            {
                // Get the nodes from the webservice 
                XmlNode node = mWebService.getMarketCapList();
                XmlNodeReader reader = new XmlNodeReader(node);
                XmlSerializer ser = new XmlSerializer(typeof(CxMarketStockPointPairList));
                rList = (CxMarketStockPointPairList)ser.Deserialize(reader);

            }
            catch (Exception ex)
            {
                Logger.Error("Err=" + ex); 
                //Console.WriteLine("Err=" + ex);
            }
            
            return rList;
            */

            return new CxMarketStockPointPairList();
        }
        
        

        /*
        // Gets CxStockOrderList from the webservice, deserializes and returns
        public static IList<CxStockOrder> getLastOsOrders()
        {
            // No need to login, this is HTTP 
            CxStockOrderList rList = new CxStockOrderList();
            try
            {
                // Get the nodes from the webservice 
                XmlNode node = mWebService.getLastOsOrders();
                XmlNodeReader reader = new XmlNodeReader( node );
                XmlSerializer ser = new XmlSerializer(typeof( CxStockOrderList) );
                rList = (CxStockOrderList)ser.Deserialize(reader);

            }
            catch (Exception ex)
            {
                Logger.Error("Err=" + ex); 
                //Console.WriteLine("Err=" + ex);
            }

            return rList.list;
        }
        */

        /*
        // Gets stocks orders for a particular stock
        public static List<CxStockOrder> getStockOrders(CxMarketStock pxStock)
        {
            CxStockOrderList rList = new CxStockOrderList();
            try
            {
                // Get the nodes from the webservice 
                XmlNode node = mWebService.getStockOrders(pxStock.OsId, pxStock.Symbol );
                XmlNodeReader reader = new XmlNodeReader(node);
                XmlSerializer ser = new XmlSerializer(typeof(CxStockOrderList));
                rList = (CxStockOrderList)ser.Deserialize(reader);
            }
            catch (Exception ex)
            {
                Logger.Error("Err=" + ex); 
                //Console.WriteLine("Err=" + ex);
            }

            return rList.list;
        }
        /*


        //
        public static string cancelTrade( string psOsId, bool pbIpo )
        {
            string sRet = "Your order was NOT processed";


            try
            {
                // The params for cancel
                string cancelParams = "data={\"function\":\"cancelOrder\",\"params\":\"" + psOsId + "\"}";
                // If it's IPO, it's a little different
                if (pbIpo)
                    cancelParams = "data={\"function\":\"cancelRequest\",\"params\":\"" + psOsId + "\"}";


                // Make sure login is good, otherwise no reason to continue
                CxDeskUtil.loginToOs();

                //
                string sPage = CxUtil.sendHttpAuthenticatedPingPOST(
                        CxGlobal.TRADES_CANCEL,
                        cancelParams,
                        CxGlobal.OS_USERNAME,
                        CxGlobal.OS_PASSWORD,
                        myContainer
                        );


                // Look for that text to validate
                if (sPage.IndexOf("Success") > -1)
                    sRet = "Your order was successfully cancelled!";


                //Console.WriteLine(sPage);
                //Console.WriteLine("------------------------------------");
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Err=" + ex );
                Logger.Error("Err=" + ex);
            }

            return sRet;
        }

        //
        public static string trade( string psStock, string psAction, int piShares, double pdPrice )
        {
            string sRet = "Your order was NOT processed";

            try
            {
                // Make sure login is good, otherwise no reason to continue
                CxDeskUtil.loginToOs();

                //
                string sPage = CxUtil.sendHttpAuthenticatedPingPOST(
                        CxGlobal.TRADES_ACTION,
                        "data={\"function\":\"parseMain\",\"params\":\"0\"}&buySell=" + psAction.ToLower() + "&numShares=" + piShares.ToString() + "&allOrNone=&symbol=" + psStock + "&orderType=limit&duration=canceled&prevSelect=&smbBtn=Complete Order&price=" + pdPrice.ToString(),
                        CxGlobal.OS_USERNAME,
                        CxGlobal.OS_PASSWORD,
                        myContainer
                        );


                // Look for that text to validate
                if (sPage.IndexOf("Your trade was successfully submitted!") > -1)
                {
                    sRet = "Your order was successfully submitted!";
                }
                else
                {
                    string temp = CxDeskUtil.getValue("msg_error\">", "</li>", sPage);
                    temp = temp.Replace("<ul>", string.Empty);
                    temp = temp.Replace("<li>", string.Empty);
                    temp = temp.Replace("<strong>&nbsp;</strong>", string.Empty);
                    sRet = temp.Trim();
                }

                //Console.WriteLine(sPage);
                //Console.WriteLine("------------------------------------");
            }
            catch (Exception ex)
            {
                Logger.Error("Err=" + ex); 
                //Console.WriteLine("Err=" + ex);
            }

            return sRet;
        }

        //  string ssss = ipotrade("460", "RVRS", 1 );
        // Another HTML hardcode scrpe job
        public static string getIpoChallangeQuestion( string psName )
        {
            // Make sure login is good, otherwise no reason to continue
            CxDeskUtil.loginToOs();

            // Take out any dots
            psName = psName.Replace(".", string.Empty);

            string sRet = CxUtil.getWebPage(
                    "" + "/" + psName,
                    myContainer
                    );


            // Process the question
            string[] lines = sRet.Split('\n');
            //Console.WriteLine("lines=" + lines.Length);

            for( int i=0; i<lines.Length-1; i++ )
            {
                if( lines[i].IndexOf("ipoCaptchaBox") > -1)
                {
                    sRet = CxDeskUtil.getValue("</span><br/>", "<br />", lines[i + 1]);
                    break;
                }
            }

            return sRet;            
        }


        // Buys the new IPO on OS, includes challange question
        public static string ipotrade(string psAnswer, string psSymbol, string psPlayerName, int piShares )
        {
            string sRet = string.Empty;
            string sPage = string.Empty;

            // Make sure login is good, otherwise no reason to continue
            CxDeskUtil.loginToOs();

            // Answer
            sPage = CxUtil.sendHttpAuthenticatedPingPOST(
                    "",
                    "data={\"function\":\"checkAnswer\",\"params\":\"" + psAnswer + "\"}",
                    CxGlobal.OS_USERNAME,
                    CxGlobal.OS_PASSWORD,
                    myContainer
                    );
            
            // Look for that text to validate
            if (sPage.IndexOf("true") > -1)
                sRet = "Your answer is correct.";
            else
                return "Your answer was NOT accepted";
            
            // Complete
            sPage = CxUtil.sendHttpAuthenticatedPingPOST(
                    "",
                    "data={\"function\":\"parseMain\",\"params\":\"0\"}&&numShares=" + piShares.ToString() + "&symbol=" + psSymbol.ToUpper() + "&smbBtn=Complete Order",
                    CxGlobal.OS_USERNAME,
                    CxGlobal.OS_PASSWORD,
                    myContainer
                    );

            // Look for that text to validate
            if (sPage.IndexOf("Your IPO request was successfully submitted") > -1)
                sRet = "Your IPO request was successfully submitted.";
            else
                sRet = "Your IPO request was NOT submitted.";

            //Console.WriteLine(sPage);
            //Console.WriteLine("------------------------------------");

            return sRet;
        }



        // Extracts MyTrades using the MIL html extract lib
        public static IList<CxStockOrder> getMyTrades()
        {
            IList<CxStockOrder> rList = new List<CxStockOrder>();

            try
            {
                // Make sure login is good, otherwise no reason to continue
                CxDeskUtil.loginToOs();

                string sRet = CxUtil.sendHttpAuthenticatedPingPOST(
                    CxGlobal.MY_REPORT_URL,
                    "type=trades&monthSelect=&daySelect=30&fromMonth=12&fromDay=17&fromYear=2008&toMonth=03&toDay=27&toYear=2009&output=online&smbBtn.x=34&smbBtn.y=7",
                    CxGlobal.OS_USERNAME,
                    CxGlobal.OS_PASSWORD,
                    myContainer
                    );


                // Isolate the table html, Load up the html into the html extract lib
                int iStart = sRet.IndexOf( "<table" );
                int iEnd = sRet.IndexOf( "</table>" ) + 8;
                sRet = sRet.Substring(iStart, iEnd - iStart);
                //
                MIL.Html.HtmlDocument mDocument = MIL.Html.HtmlDocument.Create(sRet, false);
                MIL.Html.HtmlElement elm = (MIL.Html.HtmlElement)mDocument.Nodes[0];

                //
                int iCount = 0;
                // Table rows
                foreach (MIL.Html.HtmlElement rowNodes in elm.Nodes)
                {
                    // Table data, don't proecess header
                    if (iCount > 0)
                    {
                        CxStockOrder order = new CxStockOrder();
                        //
                        MIL.Html.HtmlElement node = (MIL.Html.HtmlElement)rowNodes.Nodes[0];
                        order.AsOf = CxUtil.getDateTime(node.Text);

                        node = (MIL.Html.HtmlElement)rowNodes.Nodes[1];
                        order.Symbol = node.FirstChild.FirstChild.ToString();

                        node = (MIL.Html.HtmlElement)rowNodes.Nodes[3];
                        order.Price = CxUtil.getDouble(node.Text);

                        node = (MIL.Html.HtmlElement)rowNodes.Nodes[4];
                        order.Shares = CxUtil.getDouble(node.Text);

                        // Upper the first char
                        node = (MIL.Html.HtmlElement)rowNodes.Nodes[5];
                        order.Action = CxUtil.getAction( CultureInfo.CurrentCulture.TextInfo.ToTitleCase(node.Text) );

                        node = (MIL.Html.HtmlElement)rowNodes.Nodes[6];
                        int ipos = CxUtil.getInt(node.Text);
                        if (ipos > 0)
                            order.Ipo = true;

                        // Add the orders
                        rList.Add(order);
                    }
                    iCount++;
                }


                //Console.WriteLine(sRet);
                //Console.WriteLine("------------------------------------------");
            }
            catch (Exception ex)
            {
                Logger.Error("Err=" + ex); 
                //Console.WriteLine("err=" + ex);
            }

            return rList;
        }
        
        

        // Recurse for the nodes
        private static void getNodes(HtmlNodeCollection plInNodes, HtmlNodeCollection plOutNodes, string psText)
        {
            foreach( HtmlNode node in plInNodes)
            {
                if (node is MIL.Html.HtmlElement)
                {
                    if( node.ToString().IndexOf(psText) == CxGlobal.STRING_NOT_FOUND)
                        plOutNodes.Add(node);
                    //
                    MIL.Html.HtmlElement elm = (MIL.Html.HtmlElement)node;
                    getNodes( elm.Nodes, plOutNodes, psText);
                }
            }
        }

        /*
                <table width="100%" border="0" cellspacing="0" cellpadding="6">
          <tr>
            <td><strong>Action</strong></td>
            <td><strong># Shares</strong></td>
            <td><strong>SOI</strong></td>
            <td><strong>Price</strong></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr id="row_686779">
            <td>Sell</td>
            <td>7</td>
            <td><a href="">AIRJ</a></td>
<!--            <td>Limit</td> -->
            <td>4.45</td>
<!--            <td>2500-04-09 12:32:02</td> -->
            <td>
                <div class="bubble_button_wht">
                  <a href="javascript: cancelOrder( 'row_686779', 'error_686779', 686779 );">
                  <ul>
                    <li class="l"></li>
                    <li class="c"><strong>Cancel Order</strong></li>
                    <li class="r"></li>
                  </ul>
                  </a>
                </div>
            </td>
            <td><p id="error_686779"></p></td>
          </tr><tr id="row_687206">
            <td>Buy</td>
            <td>10</td>
            <td><a href="">LNCM</a></td>
<!--            <td>Limit</td> -->
            <td>0.01</td>
<!--            <td>2500-04-09 16:25:07</td> -->

         */ 

        /*
        // Get open orders for the user
        // Another HTML parsing HACK JOB
        public static IList<CxStockOrder> getMyOrders()
        {

            // Make sure login is good, otherwise no reason to continue
            CxDeskUtil.loginToOs();

            int table = 0;
            IList<CxStockOrder> rList = new List<CxStockOrder>();

            string sRet = CxUtil.sendHttpAuthenticatedPingPOST(
                CxGlobal.MY_ORDERS_URL,
                string.Empty,
                CxGlobal.OS_USERNAME,
                CxGlobal.OS_PASSWORD,
                myContainer
                );

            string[] lines = sRet.Split('\n');
            //Console.WriteLine("lines=" + lines.Length);

            for (int i = 0; i < lines.Length - 1; i++)
            {

                if (lines[i].IndexOf("Pending Orders") > CxGlobal.STRING_NOT_FOUND )
                {
                    // keep using i 
                    for( ; i < lines.Length - 1; i++)
                    {
                        //
                        if (lines[i].IndexOf("<table") > CxGlobal.STRING_NOT_FOUND)
                        {
                            //
                            table++;
                            // Advance to data
                            if (table == 1)
                                i += 9;
                            else
                                i += 9;

                            while (i < lines.Length - 1)
                            {

                                CxStockOrder order = new CxStockOrder();
                                if (table == 1)
                                {
                                    if (lines[i + 1].IndexOf("</table>") > -1)
                                        break;

                                    // The order id
                                    order.OsId = CxDeskUtil.getValue("row_", "\">", lines[i]);

                                    // Buy / sell 
                                    i++;
                                    //Console.WriteLine(lines[i]);
                                    order.Action = CxUtil.getAction(CxDeskUtil.getValue("<td>", "</td>", lines[i]));

                                    // Number of shares
                                    i++;
                                    //Console.WriteLine(lines[i]);
                                    order.Shares = CxUtil.getDouble(CxDeskUtil.getValue("<td>", "</td>", lines[i]));
                                    // Symbol ?
                                    i++;
                                    //Console.WriteLine(lines[i]);
                                    order.Symbol = CxDeskUtil.getSymbol("</a>", lines[i]);

                                    // comment line
                                    i++;
                                    //Console.WriteLine(lines[i]);

                                    // Price
                                    i++;
                                    //Console.WriteLine(lines[i]);
                                    order.Price = CxUtil.getDouble(CxDeskUtil.getValue("<td>", "</td>", lines[i]));

                                    // Date
                                    i++;
                                    //Console.WriteLine(lines[i]);
                                    order.AsOf = CxUtil.getDateTime(CxDeskUtil.getValue("<td>", "</td>", lines[i]));
                                    // Fixit, That's a HARDCODE, bad data, the year is always 2500
                                    if( order.AsOf.Year == 2500 ) 
                                        order.AsOf = order.AsOf.AddYears( 2009 - order.AsOf.Year ); 

                                    //
                                    rList.Add(order);

                                    // Advance
                                    i += 13;

                                    // Check for end of table
                                    //Console.WriteLine(lines[i]);
                                    if (lines[i + 1].IndexOf("</table>") > -1)
                                        break;
                                }
                                else
                                {
                                    // IPO buy orders 
                                    order.Ipo = true;
                                    order.Action = ExAction.Buy;

                                    // trap
                                    if (lines[i + 1].IndexOf("</table>") > -1)
                                        break;

                                    // The order id 
                                    order.OsId = CxDeskUtil.getValue("ipoRow_", "\">", lines[i]);

                                    // Number of shares
                                    i++;
                                    //Console.WriteLine(lines[i]);
                                    order.Shares = CxUtil.getDouble(CxDeskUtil.getValue("<td>", "</td>", lines[i]));
                                    
                                    // Symbol ?
                                    i++;
                                    //Console.WriteLine(lines[i]);
                                    order.Symbol = CxDeskUtil.getSymbol("</a>", lines[i]);

                                    // Price
                                    i++;
                                    //Console.WriteLine(lines[i]);
                                    order.Price = CxUtil.getDouble(CxDeskUtil.getValue("<td>", "</td>", lines[i]));

                                    // Date
                                    i++;
                                    //Console.WriteLine(lines[i]);
                                    order.AsOf = CxUtil.getDateTime(CxDeskUtil.getValue("<td>", "</td>", lines[i]));

                                    // Advance
                                    i += 13;
                                    
                                    if (i >= lines.Length)
                                        break;

                                    //
                                    rList.Add(order);

                                    // Check for end of table
                                    //Console.WriteLine(lines[i]);
                                    if (lines[i + 1].IndexOf("</table>") > -1)
                                        break;
                                }
                            
                            } // while

                        } // if table

                    }
                }
            }

            return rList;
        }
        */


        // Gets symbol from the given string using the end marker
        public static string getSymbol( string psEnd, string psText  )
        {
            string sRet = string.Empty;
            try
            {
                int end = psText.IndexOf( psEnd );
                sRet = psText.Substring( end - 4, 4 );
            }
            catch( Exception ex  )
            {
                Logger.Error("Err=" + ex); 
                //Console.WriteLine("Error=" + ex);
            }

            return sRet;
        }


        // Parses out values based on start and end tags in a given string
        public static string getValue( string psStart, string psEnd, string psText  )
        {
            string sRet = string.Empty;
            try
            {
                int start = psText.IndexOf( psStart ) + psStart.Length;
                int end = psText.IndexOf(psEnd, start);
                sRet = psText.Substring( start, end - start);
            }
            catch( Exception ex  )
            {
                Logger.Error("Err=" + ex); 
                //Console.WriteLine("Error=" + ex);
            }

            return sRet;
        }

        // Parses out values based on start and end tags in a given string
        // Starts looking at the end of the string instead of at the beginning
        public static string getValueReverse(string psText, string psStart, string psEnd)
        {
            string sRet = string.Empty;
            try
            {
                int end = psText.LastIndexOf(psEnd);
                int start = psText.LastIndexOf(psStart, end) + 1;
                sRet = psText.Substring(start, end - start);
            }
            catch (Exception ex)
            {
                Logger.Error("Err=" + ex); 
                //Console.WriteLine("Error=" + ex);
            }

            return sRet;
        }

        // Check if the login to OS was successful or not 
        public static bool validateLogin( string psText )
        {
            // See if the remember me checkbox is around
            if ( psText.IndexOf( "<input name=\"rememberMe" ) > -1 )
                return false;
            else
                return true;
        }


        // Merge two lists together
        public static List<CxPortfolioStock> mergePortfolio( IList<CxPortfolioStock> plSync, IList<CxPortfolioStock> plCurrent )
        {
            try
            {
                //  Add new ones
                foreach( CxPortfolioStock st in plSync )
                {
                    CxPortfolioStock port = CxUtil.getPortfolioStock( plCurrent, st.Symbol, st.Ipo );
                    if (port == null)
                    {
                        plCurrent.Add(st);
                    }
                    else
                    {
                        // Update the current list, with the latest from the server 
                        port.Paid = st.Paid;
                        port.Shares = st.Shares;
                    }
                }
                
                // Subtract missing ones
                for( int i=plCurrent.Count-1; i > -1; i--  )
                {
                    CxPortfolioStock st = plCurrent[i];
                    CxPortfolioStock port = CxUtil.getPortfolioStock( plSync, st.Symbol, st.Ipo );
                    if( port == null ) plCurrent.Remove( st );
                }

            }
            catch (Exception ex)
            {
                //
                Logger.Error("Err=" + ex); 
                //Console.WriteLine("error=" + ex);
            }

            return (List<CxPortfolioStock>)plCurrent;
        }


        /*
        // Checks to see if there is a new version of the program available for download
        // version in the format 0.0.0.85
        public static bool newVersionAvailable(string psVersion)
        {
            try
            {
                // Operating system + ver
                OperatingSystem os = Environment.OSVersion;
                Version vs = os.Version;
                string sVer = vs.Major + "." + vs.Minor + "." + vs.Build + "." + vs.Revision;
                // Runtime / CLR
                string sRuntime = Environment.Version.ToString();
                // machine name
                string machine = Environment.MachineName;
                string sPing = "os=" + os.Platform + "," + sVer + CxUtil.AND + "clr=" + sRuntime + CxUtil.AND +
                    "machine=" + machine + CxUtil.AND + "appver=" + psVersion;

                // check for version 
                XmlNode xStatuses = mWebService.getStatuses(CxGlobal.KEY, os.Platform + "," + sVer,
                                                                sRuntime, machine, psVersion, string.Empty);

                // Got this from server 
                string currentDeployedVersion = xStatuses.SelectSingleNode("ver/net").InnerText;

                // Take out the periods for comparison
                currentDeployedVersion = currentDeployedVersion.Replace(CxUtil.PERIOD, string.Empty);
                psVersion = psVersion.Replace(CxUtil.PERIOD, string.Empty);

                //  If the deployed version on the website is greater than this one, need to upgrade 
                return CxUtil.getDouble(currentDeployedVersion) > CxUtil.getDouble(psVersion);
            }
            catch (Exception ex)
            {
                Logger.Error("Err=" + ex); 
                //Console.WriteLine("Err=" + ex);
            }

            return false;
        }
        */


        // Checks the OS API feed for isseud ipos
        public static IList<CxIpoStock> getIpoStocks()
        {
            // Those are all of the stocks 
            IList<CxIpoStock> rList = new List<CxIpoStock>();

            try
            {
                string thexml = CxUtil.getWebPage( "" );
                //
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(thexml);

                XmlNodeList nodes = doc.SelectNodes("methodResponse/upcoming/player");
                foreach (XmlNode nod in nodes)
                {
                    CxIpoStock ipstock = new CxIpoStock();
                    //
                    ipstock.Symbol = CxUtil.getNodeText(nod, "symbol");
                    //
                    string[] names = CxUtil.getNodeText(nod, "name").Split(CxUtil.SPACE[0]);
                    ipstock.FirstName = names[0];
                    ipstock.LastName = names[1];
                    //
                    ipstock.Rank = int.Parse(CxUtil.getNodeText(nod, "rank"));
                    ipstock.Status = (ExIpoStatus)Enum.Parse(typeof(ExIpoStatus), CxUtil.getNodeText(nod, "status") );
                    
                    //
                    rList.Add(ipstock);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Err=" + ex); 
                //Console.WriteLine(ex);
            }


            return rList;
        }


    }  // EOC
}
