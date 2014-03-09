//
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
//
using log4net;
//
using org.auroracoin.desktop.ui;
using org.auroracoin.aurcore.util;


// Initializes log4net, reads from the configuration file of the running assembly
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
//
namespace org.auroracoin.desktop.core
{
    static class CxStart
    {

        private static ILog Logger = LogManager.GetLogger(typeof(CxStart));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            //
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Start background thread
                CxGlobal.startBackground();
                //
                Application.Run(new FxMain());
            }
            catch (Exception ex)
            {
                Logger.Error("error=" + ex);
                showError(ex);
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            showError(e.Exception);
        }

        private static void showError(Exception pxEx)
        {
            Logger.Error("error=" + pxEx);
            MessageBox.Show("Error=" + pxEx);
        }

        private static void updateMe()
        {
            try
            {
                System.Diagnostics.Process.Start("updater.exe");
            }
            catch (Exception ex)
            {
                Logger.Error("Err=" + ex); 
            }
        }

        // Check file version
        private static string getFileVersion(string psFileName)
        {
            string sRet = string.Empty;
            //
            FileVersionInfo myFile = FileVersionInfo.GetVersionInfo( psFileName );
            sRet = myFile.FileVersion;
            //
            return sRet;
        }


    }  // EOC
}
