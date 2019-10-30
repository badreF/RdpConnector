using System;
using System.Diagnostics;
using System.Net;

namespace RdpConnector
{
    class Program
    {
     
        static void Main(string[] args)
        {
            //_ = ResponseAsync().Result;
            using (var client = new WebClient())
            {
                client.DownloadFile("https://clickoncetest1.blob.core.windows.net/clickoncetest/cpub-wordpad-EliorSession-CmsRdsh.rdp", "cpub-wordpad-EliorSession-CmsRdsh.rdp");
            }


            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo(Environment.CurrentDirectory+ "\\cpub-wordpad-EliorSession-CmsRdsh.rdp")
            };

            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
            //Process.Start("C:\\Users\\badre-addine.fouad\\Desktop\\gw-vm.rdp");

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
