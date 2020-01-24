using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Web;
using System.Deployment.Application;
using System.Linq;

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
            NameValueCollection queryStringParameters = GetQueryStringParameters();
            if (queryStringParameters.HasKeys())
            {
                string codeKey = queryStringParameters.AllKeys.FirstOrDefault();
                string codeValue = queryStringParameters[codeKey].ToUpperInvariant();
                LaunchRemoteConnection(codeValue);
            }
            else
            {
                LaunchRemoteConnection("gmp");
                ShowErrorWindowsBox("Query string does not work");
            }

        }
        /// <summary>
        /// This method is used to get the query string parameters 
        /// </summary>
        /// <returns></returns>
        private static NameValueCollection GetQueryStringParameters()
        {
            NameValueCollection nameValueTable = new NameValueCollection();
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                if (ApplicationDeployment.CurrentDeployment.ActivationUri == null) return (nameValueTable);
                string queryString = ApplicationDeployment.CurrentDeployment.ActivationUri.Query;
                nameValueTable = HttpUtility.ParseQueryString(queryString);
            }
            return (nameValueTable);
        }

        /// <summary>
        /// This method is used to download the rdp file and launch the rdp using mstsc process
        /// </summary>
        private static void LaunchRemoteConnection(string appCode)
        {

            // Create a client that use windows authentication
            using (var client = new WebClient
            {
                UseDefaultCredentials = true
            })
            {
                try
                {
                    // Step 1 : download the .rdp file from the server
                    string rdpFileName = ConfigurationManager.AppSettings.Get(string.Format("rdpFileName_{0}", appCode));
                    if (string.IsNullOrEmpty(rdpFileName))
                    {
                        rdpFileName = string.Format("{0}.rdp", appCode);
                    }
                    string rdpUrl = ConfigurationManager.AppSettings.Get(string.Format("rdpFileUrl_{0}", appCode));
                    if (string.IsNullOrEmpty(rdpUrl))
                    {
                        ShowErrorWindowsBox("The url containing the rdp file does not exists");
                        return;
                    }
                    client.DownloadFile(rdpUrl, rdpFileName);
                    
                    // Step 2 : start the process that run the remote desktop application from command line
                    using (var proc = new Process { StartInfo = new ProcessStartInfo(Environment.CurrentDirectory + Constants.BackslashSymbol + rdpFileName) })
                    {
                        proc.Start();
                        proc.WaitForExit();
                    }
                }
                catch (WebException ex)
                {
                    Logger.Error(Constants.GetRdpFromServerErrorMessage + ex.Message);
                    ShowErrorWindowsBox(ex.Message + "\n" + Constants.InternetErrorMessage + "\n" + Constants.NetworkErrorMessage + "\n" + Constants.AuthorizationErrorMessage);
                }
                catch (Exception ex)
                {
                    Logger.Error(Constants.GenericErrorMessage + ex.Message);
                    ShowErrorWindowsBox(Constants.GenericErrorMessage);
                }
            }
        }

        /// <summary>
        /// This is used to show an error popup if something wrong is coming
        /// </summary>
        /// <param name="errorMessage"></param>
        private static void ShowErrorWindowsBox(string errorMessage)
        {
            MessageBox.Show(errorMessage, Constants.GenericErrorMessageWindowsBoxTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
