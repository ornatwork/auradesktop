//
using System;
using System.Collections.Generic;
using System.Collections;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;
//
using log4net;
//
using org.auroracoin.aurcore.stocks;



namespace org.auroracoin.aurcore.util
{
    public class CxUtil
    {
        //
        public const string FORMAT_DOUBLE = "#,###,##0.00";
        public const string FORMAT_PERCENT = "#0.00";
        public const string FORMAT_OWNERSHIP = "00.0000";  // Small percents usually, give 4 
        public const string FORMAT_INT = "#,#,#,0";
        public const string FORMAT_PRICE = "###,##0.00000";
        public const string DATE_FORMAT = "yyyy-MM-dd";
        public const string TIME_FORMAT = "HH:mm:ss";
        //
        public const int HTTP_TIMEOUT = 5 * 1000;
        public const char PIPE = '|';
        public const char AND = '&';
        public const string PERIOD = ".";

        //
        public static string XML_FILE = "portfolio.xml";
        // private 
        private const string NOTAVAIL = "N/A";
        private static string msDataDir = string.Empty;
        private const string APPDIR = "aur";
        private const string AGENT_NAME = "AurDesktop";
        //
        public const string MARKET_STOCK_TICKER = "  MC";
        public const string MARKET_STOCK_TICKER_DETAIL = "Market Average";
        //
        public const char BACK_SLASH  = '/';
        
        //
        public const string API_KEY         = "?api_key=f1939fd188dacc90a09f75faa96e3db1";
        public const string API_USERNAME    = "&user=";
        public const string XML_FORMAT      = "&format=xml";
        // Xml nodes
        public const string XML_SOI_NODES               = "methodResponse/soi";
        public const string XML_PORTFOLIO_SOI_NODES     = "methodResponse/sois/soi";
        
        //
        public const string DESCENDING_ARROW = " \u25BC";
        public const string ASCENDING_ARROW = " \u25B2";
        //
        private const int MAX_US_PHONE_NUMBER = 10;
        public const string SPACE = " ";
        public const string COMMA = ",";
        public const string TAB = "\t";
        private const string DOT = ".";
        private const string HYPHEN = "-";
        private const string OPEN_PARENTHESES = "(";
        private const string CLOSE_PARENTHESES = ")";
        
        // gmail.com, send through program email address
        // warning anybody using the program can put whatever in receive address
        // -----------------------------
        private const string EMAIL_SERVER = "smtp.gmail.com";
        private const int SMTP_PORT = 587;
        private const string PUBLIC_EMAIL = "flippzer@gmail.com";
        private const string PUBLIC_EMAIL_PASSW = "Coin!187";
        
        //
        public const string AUR_ADRESS = "ATemHbCBgbMFBYk1wGdsjqhsXhuuQwxkBB";
        public const string BTC_ADRESS = "113FR417KcgFF5gaiuJLFanFCUHF6QWhac";
        private static ILog Logger = LogManager.GetLogger(typeof(CxUtil));
        //
        private CxUtil(){}


    // Downloads a file from URL, saves locally
    public static bool getFileFromUrl( string psUrl, string psFileName )
    {
        bool bRet = false;

        try
        {
            WebClient Client = new WebClient();
            Client.DownloadFile(psUrl, psFileName);
            bRet = true;
        }
        catch (Exception ex)
        {
            Logger.Error( "Err=" + ex );
        }

        return bRet;
    }

        // Get image from the given Url
        // returns the image or null if not found
        public static Image getUrlImage(string psUrl)
        {
            WebResponse result = null;
            Image rImage = null;
            
            try
            {
                WebRequest request = WebRequest.Create(psUrl);
                request.Timeout = HTTP_TIMEOUT; 
                byte[] rBytes;

                // Get the content
                result = request.GetResponse();
                Stream rStream = result.GetResponseStream();

                // Bytes from address
                using( BinaryReader br = new BinaryReader(rStream))
                {
                    // Ask for bytes bigger than the actual stream
                    rBytes = br.ReadBytes(1000000);
                    br.Close();
                }
                // close down the web response object
                result.Close();

                // Bytes into image
                using( MemoryStream imageStream = new MemoryStream(rBytes, 0, rBytes.Length))
                {
                    imageStream.Write(rBytes, 0, rBytes.Length);
                    rImage = Image.FromStream(imageStream, true);
                    imageStream.Close();
                }

            }
            catch (Exception ex)
            {
                Logger.Error("Err=" + ex);
            }
            finally
            {
                if (result != null) result.Close();
            }

            return rImage;
        }

        public static string getWebPage(string psUrl, CookieContainer pCookieCont)
        {
            return getWebPage(psUrl, HTTP_TIMEOUT, pCookieCont);
        }
        
        public static string getWebPage(string psUrl, int piTimeOut, CookieContainer pCookieCont )
        {
            string sRet = string.Empty;
            HttpWebRequest req = null;
            WebResponse result = null;

			try 
			{
                req = (HttpWebRequest)HttpWebRequest.Create(psUrl);
                req.Timeout = piTimeOut;
                req.Method = "GET";
                req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                req.UserAgent = AGENT_NAME;
                req.CookieContainer = pCookieCont;

                // Has to process the results if the responding service is spitting it out
                result = req.GetResponse();
                Stream ReceiveStream = result.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader sr = new StreamReader(ReceiveStream, encode);
                // in case the caller is interested 
                sRet = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                // Silent 
                Logger.Error("Err=" + ex);
            }
            finally
            {
                if (result != null) result.Close();
            }

            return sRet;
        }

        public static string getWebPage(string psUrl)
        {
            return getWebPage(psUrl, HTTP_TIMEOUT);
        }

        public static string getWebPage(string psUrl, int piTimeOut)
        {
            string sRet = string.Empty;
            HttpWebRequest req = null;
            WebResponse result = null;

            try
            {
                req = (HttpWebRequest)HttpWebRequest.Create(psUrl);
                req.Timeout = piTimeOut;
                req.Method = "GET";
                req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                req.UserAgent = AGENT_NAME;

                // Has to process the results if the responding service is spitting it out
                result = req.GetResponse();
                Stream ReceiveStream = result.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader sr = new StreamReader(ReceiveStream, encode);
                // in case the caller is interested 
                sRet = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                // Silent 
                Logger.Error("Err=" + ex);
            }
            finally
            {
                if (result != null) result.Close();
            }

            return sRet;
        }



        /// <summary>
		/// Method that sends a ping to a http location ( POST submission ).
		/// This method will NOT throw any errors and will fail silently if there is an error during send.
		/// </summary>
		/// <param name="psHttpAddress">Http location on a remote server.</param>
		/// <param name="psUrlEncodedText">UrlEncoded string to send.</param>
		public static string sendHttpAuthenticatedPingPOST( string psHttpAddress, string psEncodedUrlText, string psUser, string psPass, CookieContainer pCookieCont )
		{
			WebResponse result = null;
			string sRet = string.Empty;
            HttpWebRequest req = null;

			try 
			{
                req = (HttpWebRequest)HttpWebRequest.Create(psHttpAddress);
                //
                req.Timeout = HTTP_TIMEOUT;
				req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                req.UserAgent = AGENT_NAME;
                req.CookieContainer = pCookieCont;
                // Add authentication to request
                req.Credentials = new NetworkCredential( psUser, psPass );
                req.PreAuthenticate = true;

				// Debug
                Logger.Debug("sending to http=" + psHttpAddress + ", string= " + psEncodedUrlText);
				
				// Do the dew
                byte[] SomeBytes = Encoding.UTF8.GetBytes(psEncodedUrlText);
				req.ContentLength = SomeBytes.Length;
				Stream newStream = req.GetRequestStream();
				newStream.Write(SomeBytes, 0, SomeBytes.Length);
				newStream.Close();
                
                
				// Has to process the results if the responding service is spitting it out
				result = req.GetResponse();
				Stream ReceiveStream = result.GetResponseStream();
				Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
				StreamReader sr = new StreamReader( ReceiveStream, encode );
				// in case the caller is interested 
				sRet = sr.ReadToEnd();
			} 
			catch( Exception ex) 
			{
                Logger.Error("Err=" + ex);
			} 
			finally 
			{
				if( result != null ) result.Close();
			}

			return sRet;
		}




		/// <summary>
		/// Method that sends a ping to a http location ( POST submission ).
		/// This method will NOT throw any errors and will fail silently if there is an error during send.
		/// </summary>
		/// <param name="psHttpAddress">Http location on a remote server.</param>
		/// <param name="psUrlEncodedText">UrlEncoded string to send.</param>
        public static string sendHttpPingPOST(string psHttpAddress, string psEncodedUrlText, CookieContainer pCookieCont)
		{
			WebResponse result = null;
			string sRet = string.Empty;
            HttpWebRequest req = null;

			try 
			{
                req = (HttpWebRequest)HttpWebRequest.Create(psHttpAddress);
                req.Timeout = HTTP_TIMEOUT;
				req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                req.UserAgent = AGENT_NAME;
                req.CookieContainer = pCookieCont;

				// Do the dew
                byte[] SomeBytes = Encoding.UTF8.GetBytes(psEncodedUrlText);
				req.ContentLength = SomeBytes.Length;
				Stream newStream = req.GetRequestStream();
				newStream.Write(SomeBytes, 0, SomeBytes.Length);
				newStream.Close();
                

				// Has to process the results if the responding service is spitting it out
				result = req.GetResponse();
				Stream ReceiveStream = result.GetResponseStream();
				Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
				StreamReader sr = new StreamReader( ReceiveStream, encode );
				// in case the caller is interested 
				sRet = sr.ReadToEnd();
			} 
			catch( Exception ex) 
			{
                Logger.Error("Err=" + ex);
			} 
			finally 
			{
				if( result != null ) result.Close();
			}

			return sRet;
		}


        /// <summary>
        /// Method that sends a ping to a http location ( POST submission ).
        /// This method will NOT throw any errors and will fail silently if there is an error during send.
        /// </summary>
        /// <param name="psHttpAddress">Http location on a remote server.</param>
        /// <param name="psUrlEncodedText">UrlEncoded string to send.</param>
        public static string sendHttpPingPOST(string psHttpAddress, string psEncodedUrlText )
        {
            WebResponse result = null;
            string sRet = string.Empty;
            HttpWebRequest req = null;

            try
            {
                req = (HttpWebRequest)HttpWebRequest.Create(psHttpAddress);
                req.Timeout = HTTP_TIMEOUT;
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                req.UserAgent = AGENT_NAME;

                // Do the dew
                byte[] SomeBytes = Encoding.UTF8.GetBytes(psEncodedUrlText);
                req.ContentLength = SomeBytes.Length;
                Stream newStream = req.GetRequestStream();
                newStream.Write(SomeBytes, 0, SomeBytes.Length);
                newStream.Close();


                // Has to process the results if the responding service is spitting it out
                result = req.GetResponse();
                Stream ReceiveStream = result.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader sr = new StreamReader(ReceiveStream, encode);
                // in case the caller is interested 
                sRet = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                Logger.Error("Err=" + ex);
            }
            finally
            {
                if (result != null) result.Close();
            }

            return sRet;
        }

        /// Reads portfolio stock objects from xml file 
        public static List<CxPortfolioStock> readXML()
        {
            // Trap
            if (!File.Exists( getDataDir() + XML_FILE)) return new List<CxPortfolioStock>();
            
            // Deserialize, load from XML
            FileStream fs = new FileStream( getDataDir() + XML_FILE, FileMode.Open);
            XmlReader reader = new XmlTextReader(fs);
            XmlSerializer ser = new XmlSerializer(typeof(List<CxPortfolioStock>));
            List<CxPortfolioStock> myList = (List<CxPortfolioStock>)ser.Deserialize(reader);
            fs.Close();
            //
            return myList;
        }

        /// Writes portfolio stock objects to xml file 
        public static void writeXML( List<CxPortfolioStock> plStocks )
        {
            // Now create the Xml 
            XmlSerializer s = new XmlSerializer(typeof(List<CxPortfolioStock>));
            TextWriter writer = new StreamWriter( getDataDir() + XML_FILE);
            // Serializes the objects in list
            s.Serialize(writer, plStocks);
            // done, close the file
            writer.Close();

        }




        // Look for the specific symbol in the portfolio list
        public static CxPortfolioStock getPortfolioStock( IList<CxPortfolioStock> plList, string psSymbol, bool pbIpo )
        {
            foreach (CxPortfolioStock stock in plList)
                if (stock.Symbol.Equals(psSymbol) && stock.Ipo == pbIpo )
                    return stock;

            return null;
        }

        // Look for the specific symbol in the portfolio list
        public static CxMarketStock getStock(IList<CxMarketStock> plList, string psSymbol)
        {
            foreach (CxMarketStock stock in plList)
                if (stock.Symbol.Equals(psSymbol))
                    return stock;

            return null;
        }

        // Remove specific symbol in the portfolio list
        public static void removePortfolioStock( IList<CxPortfolioStock> plList, string psSymbol)
        {
            CxPortfolioStock rem = null;
 
            foreach (CxPortfolioStock stock in plList)
                if (stock.Symbol.Equals(psSymbol))
                {
                    rem = stock;
                    break;
                }

            // remove from 
            plList.Remove(rem);
        }

        /// <summary>
        /// Given a datetime returns the time as a human readable string for debug purpose etc
        /// </summary>
        /// <param name="dtAsOf">The DateTime to convert.</param>
        /// <returns>String representing time of day.</returns>
        public static string getTimeString(DateTime dtAsOf)
        {
            // hrs,min,sec,millisec
            return dtAsOf.ToString("hh:mm:ss:ffff");
        }



        // Util methoods
        public static ExSport getSport(string psText)
        {
            if (getString(psText).Equals(string.Empty))
                return ExSport.None;
            else
                return (ExSport)Enum.Parse(typeof(ExSport), psText);
        }

        public static DateTime getDateTime(string psText)
        {
            if (getString(psText).Equals(string.Empty))
                return DateTime.Now;
            else
                return DateTime.Parse(psText);
        }

        public static double getDouble(string psText)
        {
            // Make sure no dollar signs get through
            psText = psText.Replace("$", string.Empty);

            if (getString(psText).Equals(string.Empty))
                return 0;
            else
                return double.Parse(psText);
        }

        public static int getInt(string psText)
        {
            if (getString(psText).Equals(string.Empty))
                return 0;
            else
                return int.Parse(psText);
        }

        static string getString(string psText)
        {
            if (psText == null || psText.Equals(NOTAVAIL))
                return string.Empty;
            else
                return psText;
        }
        
        public static string getCurrentDir()
        {
           return Environment.CurrentDirectory + Path.DirectorySeparatorChar;
        }

        public static string getDataDir()
        {

            if (msDataDir == null || msDataDir.Equals(string.Empty))
            {
                StringBuilder path = new StringBuilder();
                // Get local path wheres a user's applications data is saved, defined on all
                // OS's that support .Net
                path.Append(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData));
                // Add folder for data
                path.Append(Path.DirectorySeparatorChar + APPDIR + Path.DirectorySeparatorChar);

                // Create the directory if it does not exist
                msDataDir = path.ToString();
                if( !File.Exists( msDataDir) )
                    Directory.CreateDirectory( msDataDir );

            }
            return msDataDir;
        }

        // Make sure that infinity and negative infinity are set to zero
        public static double getNonInfinity( double pfNum )
        {
            // Make sure non numbers are zero set
            if (double.IsNegativeInfinity( pfNum ) ||
                    double.IsPositiveInfinity( pfNum )||
                    double.IsInfinity(pfNum)||
                    double.IsNaN( pfNum ) )

                return 0;
            else
                return pfNum;
        }


        // Get string for a node in a safe manner
        public static string getNodeText(XmlNode pxNode, string psMarker)
        {
            XmlNode theNode = pxNode.SelectSingleNode(psMarker);
            if (theNode != null)
                return theNode.InnerText;
            else
                return string.Empty;
        }


        // Returns random color 
        public static Color getRandomColor()
        {
            //Color[] values = (Color[])Enum.GetValues(typeof(Color));
            //return values[new Random().Next(0, values.Length)];

            Random rnd = new Random();
            SolidBrush sb = new SolidBrush(Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));
            return sb.Color;
        }

        // Calculates percent change between two values
        public static double getPercentChange(double pdOldValue, double pdNewValue)
        {
            double change = CxUtil.getNonInfinity(((pdNewValue / pdOldValue) * 100) - 100);
            return change;
        }

        // Calculates percent gain / loss in stock price
        public static double getStockPercentChange(double pdPaidPrice, double pdCurrentPrice)
        {
            double change = CxUtil.getNonInfinity(
                                ((pdCurrentPrice- pdPaidPrice) / pdPaidPrice) * 100);
            return change;
        }

        // return valid phone numbers strips out known user input garbage
        public static string getPhoneString(string psText)
        {
            StringBuilder sbRet = new StringBuilder(psText);
            //
            sbRet = sbRet.Replace(SPACE, string.Empty);
            sbRet = sbRet.Replace(DOT, string.Empty);
            sbRet = sbRet.Replace(HYPHEN, string.Empty);
            sbRet = sbRet.Replace(OPEN_PARENTHESES, string.Empty);
            sbRet = sbRet.Replace(CLOSE_PARENTHESES, string.Empty);

            // If the user added 1 or something in front of the phone 
            // number cut it out, keep the 10 relevant digits
            if (sbRet.Length > MAX_US_PHONE_NUMBER)
            {
                string temp = sbRet.ToString();
                sbRet = new StringBuilder(
                    temp.Substring(temp.Length - MAX_US_PHONE_NUMBER, MAX_US_PHONE_NUMBER));
            }

            //
            return sbRet.ToString();
        }


        // Sets the current stock list 
        public static IList<CxMarketStock> deepCloneList(IList<CxMarketStock> plStocks)
        {
            IList<CxMarketStock> list = new List<CxMarketStock>(plStocks.Count);
            
            // Then deep clone the passed list
            for (int i = 0; i < plStocks.Count; i++)
                list.Add((CxMarketStock)plStocks[i].Clone());

            // Give back the new one
            return list;
        }

        public static ExAction getAction(string psAction)
        {
            ExAction rAction = ExAction.Buy;

            try
            {
                rAction = (ExAction)Enum.Parse(typeof(ExAction), psAction);

            }
            catch (Exception ex)
            {
                Logger.Error("Err=" + ex);
            }

            return rAction;
        }


        /// <summary>
        /// This method will return the local time of the passed in 
        /// date.This assumes the passed in Date is in UTC / GMT 
        /// </summary>
        /// <param name="dtDate"></param>
        /// <returns></returns>
        public static DateTime GetLocalTime(System.DateTime dtDate)
        {
            if (System.DateTime.MinValue == dtDate)
                return DateTime.MinValue;
            else
            {
                return dtDate.ToLocalTime();
            }
        }

        // Get time in GTM timezone
        public static DateTime GetUTCTime(System.DateTime dtDate)
        {
            if (System.DateTime.MinValue == dtDate)
                return DateTime.MinValue;
            else
            {
                return dtDate.ToUniversalTime();
            }
        }

        // Here we are going to hack our way to EST time
        // Until we start using .net 3.5 then it will be trivial
        public static DateTime getEstTimeFromLocalTime( DateTime pdToConvert )
        {
            DateTime local = DateTime.Now;
            DateTime easternTime = pdToConvert;
            //string easternZoneId = "Eastern Standard Time";
            try
            {
               // Timezones in the US hold hands so just use local to figure that out
               // We know EST is either -4 or -5 from GMT 
               TimeZone localZone = TimeZone.CurrentTimeZone;
               if (localZone.IsDaylightSavingTime(local))
               {
                   easternTime = easternTime.ToUniversalTime().AddHours(-4);
               }
               else
               {
                   easternTime = easternTime.ToUniversalTime().AddHours(-5);
               }

               // What if we just used this ( when we have .net 3.5 )
               // DateTime timeNow = DateTime.Now;
               // System.TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
               // DateTime easternTimeNow = TimeZoneInfo.ConvertTime(timeNow, TimeZoneInfo.Local, easternZone);

            }
            catch( Exception ex )
            {
                Logger.Error("Err=" + ex);
            }

            return easternTime;
        }


        // Send the actual message 
        private static void sendEmail(MailMessage pxMessage, string psEmailAccount, string psPassword)
        {
            try
            {
                // Setup smtp
                SmtpClient client = new SmtpClient(EMAIL_SERVER);
                // Default is port 25, set the one your isp doesn't block
                client.Port = SMTP_PORT;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                // set the email user / password for authentication
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(psEmailAccount, psPassword);
                // For google this is needed
                client.EnableSsl = true;


                // do the actual send
                client.Send(pxMessage);
            }
            catch (Exception ex)
            {
                // Fixit, need global exception handler
                Logger.Error("Err=" + ex);
            }

        }

        public static void sendEmail(string psSubject, string psBody, string psToEmail, string psAttachmentFileName )
        {
            // Create a message and set up the recipients.
            MailMessage message = new MailMessage(
               PUBLIC_EMAIL,
               psToEmail,
               psSubject,
               psBody +
               Environment.NewLine +
               Environment.NewLine +
               "This email box is only used for outgoing messages.  Email to flipzer.gmail.com if you need help.");

            // Add attachemnt if set 
            if( psAttachmentFileName != string.Empty )
                message.Attachments.Add(new Attachment(psAttachmentFileName));
            
            // Send it 
            sendEmail(message, PUBLIC_EMAIL, PUBLIC_EMAIL_PASSW);
        }


        public static void writeTextToFile(string psFileName, string psText)
        {
            using( StreamWriter sw = new StreamWriter( psFileName ) ) 
            {
                // Add some text to the file.
                sw.WriteLine( psText );
            }
        }

    }  // EOC
}