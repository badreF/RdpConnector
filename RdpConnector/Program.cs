using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace RdpConnector
{
    /// <summary>
    /// This application is used to download an rdp file and launch it dynamically
    /// Specify the rdp file name and file url in the app config 
    /// </summary>
    class Program
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main()
        {
            LaunchRemoteConnection();
        }

        private static void LaunchRemoteConnection()
        {
            // Create a client that use windows authentication
            using (var client = new WebClient()
            {
                UseDefaultCredentials = true
            })
            {
                try
                {
                    // Step 1 : download the .rdp file from the server
                    var rdpFileName = Constants.rdpFileName;

                    // If the rdp file does not exists in the directory
                    //if (!File.Exists(rdpFileName))
                    //{
                    var rdpUrl = Constants.rdpFileUrl;
                    client.DownloadFile(rdpUrl, rdpFileName);
                    //}
                    // Step 2 : start the process that run the remote desktop application from command line
                    using (Process proc = new Process { StartInfo = new ProcessStartInfo(Environment.CurrentDirectory + Constants.backslashSymbole + rdpFileName) })
                    {
                        proc.Start();
                        proc.WaitForExit();
                    }
                }
                catch (WebException ex)
                {
                    logger.Error(Constants.getRdpFromServerErrorMessage + ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    logger.Error(Constants.genericErrorMessage + ex.Message);
                    throw;
                }
            }
        }
    }
}
