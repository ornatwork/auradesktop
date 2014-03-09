//
using System;
using System.Xml;
using System.IO;


namespace org.auroracoin.aurcore.util
{
    /// <summary>
    /// Client ini file reader writer, for configuration.
    /// </summary>
    public sealed class CxIniFile
    {
        #region Member constants
        //  
        private string msFileName = string.Empty;
        private static string INI_FILE = CxUtil.getDataDir() + "desktop.ini";
        private XmlDocument mXmlIniDoc;
        private string msRootName = "desktop";
        private string msSection = "General";
        private char[] mcDelimiter;
        #endregion

        public const string NOSOUND_KEY = "nosound";
        public const string WARN_PRICE_OVER_KEY = "warnpriceover";
        public const string WARN_PRICE_UNDER_KEY = "warnpriceunder";


        #region Constructor (singleton)

        /// <summary>
        /// Singleton implementation.
        /// </summary>
        public static readonly CxIniFile mInstance = new CxIniFile();

        /// <summary>
        /// Constructor.
        /// </summary>
        public CxIniFile()
        {
            this.msFileName = INI_FILE;
            this.setFile();
            // should this be part of setFile()? don't we always want this in the file?
            //this.writeStringKey(REPORT_DIR, Environment.CurrentDirectory + "\\reports");
            string delimStr = "|";
            mcDelimiter = delimStr.ToCharArray();
        }

        public static CxIniFile getInstance()
        {
            return mInstance;
        }

        #endregion

        #region File handling

        /// <summary>
        /// Removes all previously saved preferences and reloads the file.
        /// </summary>
        public void restore()
        {
            // Del bad file
            File.Delete(this.msFileName);
            // Create and load
            this.createFile();
            this.loadFile();
        }

        #endregion

        #region Querying

        /// <summary>
        /// True if the key exists in the file.
        /// </summary>
        /// <param name="psKey">The key to check.</param>
        /// <returns></returns>
        public bool containsKey(string psKey)
        {
            bool contains = false;
            XmlNode s = getRootSection();
            if (s != null)
            {
                XmlNode n = s.SelectSingleNode(psKey);
                contains = (n != null);
            }
            return contains;
        }

        #endregion

        #region Specialized key handling

        /// <summary>
        /// Removes the given key from the ini file. If the key does not
        /// exist, no action is taken.
        /// </summary>
        /// <param name="psKey">The key to be removed.</param>
        public void removeKey(string psKey)
        {
            if (this.containsKey(psKey))
            {
                XmlNode s = getRootSection();
                XmlNode n = s.SelectSingleNode(psKey);
                s.RemoveChild(n);
                this.mXmlIniDoc.Save(this.msFileName);
            }
        }

        #endregion

        #region Read keys

        public bool readBoolKey(string psKey, bool pbDefault)
        {
            return Convert.ToBoolean(this.readIntKey(psKey, Convert.ToInt32(pbDefault)));
        }

        public double readDoubleKey(string psKey, double pdDefault)
        {
            double dVal = pdDefault;
            string s = this.readStringKey(psKey, pdDefault.ToString());
            try
            {
                dVal = Convert.ToDouble(s);
            }
            catch (Exception)
            { }
            return dVal;
        }


        public int readIntKey(string psKey, int piDefault)
        {
            int intVal = piDefault;
            string s = this.readStringKey(psKey, piDefault.ToString());
            try
            {
                intVal = Convert.ToInt32(s);
            }
            catch (Exception)
            { }
            return intVal;
        }

        public DateTime readDateKey(string psKey, DateTime pdtDefault)
        {
            long ticks = Convert.ToInt64(this.readStringKey(psKey, pdtDefault.Ticks.ToString()));
            return new DateTime(ticks);
        }

        public string readStringKey(string psKey, string psDefault)
        {
            string strVal = psDefault;
            try
            {
                XmlNode s = getRootSection();
                if (s != null)
                {
                    XmlNode n = s.SelectSingleNode(psKey);
                    if (n != null)
                    {
                        XmlAttributeCollection attrs = n.Attributes;
                        foreach (XmlAttribute attr in attrs)
                        {
                            if (attr.Name == "value")
                                strVal = attr.Value;
                        }
                    }
                }
                return strVal;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns a string array from a comma-delimited value list in the .ini file.
        /// Assumes the key exists.
        /// </summary>
        /// <param name="psKey">The name of the key.</param>
        /// <returns>The array.</returns>
        public string[] readArrayKey(string psKey)
        {
            string strVal = readStringKey(psKey, null);
            return strVal.Split(mcDelimiter);
        }

        #endregion

        #region Write keys

        public void writeStringKey(string psKey, string psValue)
        {
            XmlNode s = getRootSection();
            XmlNode n = s.SelectSingleNode(psKey);

            if (n == null)
                n = s.AppendChild(mXmlIniDoc.CreateElement(psKey));

            XmlAttribute attr = ((XmlElement)n).SetAttributeNode("value", string.Empty);
            attr.Value = psValue;

            // Auto save
            this.mXmlIniDoc.Save(this.msFileName);
        }

        public void writeBoolKey(string psKey, bool pbThis)
        {
            this.writeIntKey(psKey, Convert.ToInt32(pbThis));
        }

        public void writeIntKey(string psKey, int piInt)
        {
            this.writeStringKey(psKey, piInt.ToString());
        }

        public void writeDoubleKey(string psKey, double pdVal )
        {
            this.writeStringKey(psKey, pdVal.ToString());
        }

        public void writeDateKey(string psKey, DateTime pdtDate)
        {
            this.writeStringKey(psKey, pdtDate.Ticks.ToString());
        }

        #endregion

        #region Private methods

        private void setFile()
        {
            this.mXmlIniDoc = new XmlDocument();
            try
            {
                if (!File.Exists(this.msFileName))
                    this.createFile();
                this.loadFile();
            }
            catch (XmlException)
            {
                this.restore();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error=" + ex);
            }

        }

        private void loadFile()
        {
            // Load the doc
            mXmlIniDoc.Load(this.msFileName);
        }

        private void createFile()
        {
            // Set the file up for usage
            mXmlIniDoc = new XmlDocument();
            mXmlIniDoc.AppendChild(mXmlIniDoc.CreateXmlDeclaration("1.0", null, null));
            mXmlIniDoc.AppendChild(mXmlIniDoc.CreateElement(msRootName));
            mXmlIniDoc.Save(this.msFileName);
        }

        private XmlNode getRootSection()
        {
            XmlNode root = mXmlIniDoc.DocumentElement;
            XmlNode sectionNode = root.SelectSingleNode('/' + this.msRootName + '/' + this.msSection);
            if (sectionNode == null)
                sectionNode = root.AppendChild(mXmlIniDoc.CreateElement(this.msSection));
            return sectionNode;
        }

        #endregion

    }  //  EOC
}
