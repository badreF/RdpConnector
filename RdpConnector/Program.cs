using System;
using System.Diagnostics;
using System.Net;
using System.Windows;

namespace RdpConnector
{
    /// <summary>
    /// This application is used to download an rdp file and launch it dynamically
    /// Specify the rdp file name and file url in the app config 
    /// </summary>
    internal class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private static void Main()
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
                    var rdpFileName = Constants.RdpFileName;

                    // If the rdp file does not exists in the directory
                    //if (!File.Exists(rdpFileName))
                    //{
                    var rdpUrl = Constants.RdpFileUrl;
                    client.DownloadFile(rdpUrl, rdpFileName);
                    //}
                    // Step 2 : start the process that run the remote desktop application from command line
                    using (var proc = new Process { StartInfo = new ProcessStartInfo(Environment.CurrentDirectory + Constants.BackslashSymbole + rdpFileName) })
                    {
                        proc.Start();
                        proc.WaitForExit();
                    }
                }
                catch (WebException ex)
                {
                    Logger.Error(Constants.GetRdpFromServerErrorMessage + ex.Message);
                    ShowErrorWindowsBox();
                }
                catch (Exception ex)
                {
                    Logger.Error(Constants.GenericErrorMessage + ex.Message);
                    ShowErrorWindowsBox();
                }
            }
        }

        private static void ShowErrorWindowsBox()
        {
            MessageBox.Show(Constants.GenericErrorMessageWindowsBox, Constants.GenericErrorMessageWindowsBoxTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
