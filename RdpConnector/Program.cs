using System;
using System.Diagnostics;
using System.Net;

namespace RdpConnector
{
    class Program
    {
        static void Main(string[] args)
        {
            var rdpFileName = "cpub-FTE_Menu-PRO5-CmsRdsh.rdp";
            //_ = ResponseAsync().Result;
            using (var client = new WebClient() {
                UseDefaultCredentials = true
            })
            {
                //_api/web/GetFileByServerRelativeUrl('/Lists/Applications/Attachments/1/Gestion%20MP_Rec%20(Elior).rdp')/$value
                //https://remoteapps.elior.net/RDWeb/Pages/rdp/cpub-FTE_Menu-PRO5-CmsRdsh.rdp
                try
                {
                    client.DownloadFile("http://helios-rec.elior.net/_api/web/GetFileByServerRelativeUrl('/Lists/Applications/Attachments/1/Gestion%20MP_Rec%20(Elior).rdp')/$value", rdpFileName);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    throw;
                }
            }


            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo(Environment.CurrentDirectory + "\\" + rdpFileName)
            };

            try
            {
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                throw;
            }
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
