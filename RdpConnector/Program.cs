using System;
using System.Diagnostics;
using System.Net;

namespace RdpConnector
{
    internal class Program
    {
        private static void Main()
        {
            using (var client = new WebClient())
            {
                client.DownloadFile("https://clickoncetest1.blob.core.windows.net/clickoncetest/cpub-wordpad-EliorSession-CmsRdsh.rdp", "cpub-wordpad-EliorSession-CmsRdsh.rdp");
            }


            var proc = new Process
            {
                StartInfo = new ProcessStartInfo(Environment.CurrentDirectory +
                                                 "\\cpub-wordpad-EliorSession-CmsRdsh.rdp")
                {
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
            };

            proc.Start();
            proc.WaitForExit();
            //Process.Start("C:\\Users\\badre-addine.fouad\\Desktop\\gw-vm.rdp");

        }
    }
}
