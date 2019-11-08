using System;
using System.Configuration;
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
        static void Main()
        {
            var rdpFileName = ConfigurationManager.AppSettings.Get("rdpFileName");
            //_ = ResponseAsync().Result;
            using (var client = new WebClient() {
                UseDefaultCredentials = true
            })
            {
                //_api/web/GetFileByServerRelativeUrl('/Lists/Applications/Attachments/1/Gestion%20MP_Rec%20(Elior).rdp')/$value
                //https://remoteapps.elior.net/RDWeb/Pages/rdp/cpub-FTE_Menu-PRO5-CmsRdsh.rdp
                // https://clickoncetest1.blob.core.windows.net/clickoncetest/cpub-wordpad-EliorSession-CmsRdsh.rdp
                try
                {
                    // If the rdp file does not exists in the directory
                    if (!File.Exists(rdpFileName))
                    {
                        var rdpUrl = ConfigurationManager.AppSettings.Get("rdpFileUrl");
                        client.DownloadFile(rdpUrl, rdpFileName);
                    }
                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    throw;
                }
            }
            try
            {
                Process proc = new Process
                {
                    StartInfo = new ProcessStartInfo(Environment.CurrentDirectory + "\\" + rdpFileName)
                };
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();
                proc.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                throw;
            }

        }






        //        public static async Task<string> ResponseAsync()
        //        {
        //            HttpClient client = new HttpClient();
        //            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/html"));
        //            client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
        //            client.DefaultRequestHeaders.AcceptLanguage.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("fr-FR"));

        //            var values = new Dictionary<string, string>
        //{
        //{ "WorkSpaceID", "broker.contoso.com" },
        //{ "RedirectorName", "broker.contoso.com" },
        //{ "DomainUserName", "contoso\admin13" },
        //{ "UserPass", "Avanade1234!" },
        //{ "MachineType", "private" },

        //};

        //            var content = new FormUrlEncodedContent(values);

        //            var response = await client.PostAsync("https://contoso.francecentral.cloudapp.azure.com/RDWeb/Pages/en-US/login.aspx?ReturnUrl=/RDWeb/Pages/rdp/cpub-win32calc-EliorSession-CmsRdsh.rdp", content);

        //            var responseString = await response.Content.ReadAsStringAsync();
        //            client.Dispose();
        //            return responseString;
        //        }
    }
}
