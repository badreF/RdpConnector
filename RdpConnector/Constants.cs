using System.Configuration;

namespace RdpConnector
{
    public static class Constants
    {
        //_api/web/GetFileByServerRelativeUrl('/Lists/Applications/Attachments/1/Gestion%20MP_Rec%20(Elior).rdp')/$value
        //https://remoteapps.elior.net/RDWeb/Pages/rdp/cpub-FTE_Menu-PRO5-CmsRdsh.rdp
        // https://clickoncetest1.blob.core.windows.net/clickoncetest/cpub-wordpad-EliorSession-CmsRdsh.rdp


        public static readonly string rdpFileUrlLabel = "rdpFileUrl"; 
        public static readonly string rdpFileNameLabel = "rdpFileName";
        public static readonly string backslashSymbole = "\\";
        // App config constants
        public static readonly string rdpFileName = ConfigurationManager.AppSettings.Get(rdpFileNameLabel);
        public static readonly string rdpFileUrl = ConfigurationManager.AppSettings.Get(rdpFileUrlLabel);


        // Error message
        public static readonly string getRdpFromServerErrorMessage = "an error occured when downloading the rdp file : ";
        public static readonly string genericErrorMessage = "an error occured : ";

    }
}
