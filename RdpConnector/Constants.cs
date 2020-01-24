using System.Configuration;

namespace RdpConnector
{
    /// <summary>
    /// This is a small utility class on which we will instantiate all our variables
    /// </summary>
    public static class Constants
    {
        //_api/web/GetFileByServerRelativeUrl('/Lists/Applications/Attachments/1/Gestion%20MP_Rec%20(Elior).rdp')/$value
        //https://remoteapps.elior.net/RDWeb/Pages/rdp/cpub-FTE_Menu-PRO5-CmsRdsh.rdp
        // https://clickoncetest1.blob.core.windows.net/clickoncetest/cpub-wordpad-EliorSession-CmsRdsh.rdp


        public static readonly string RdpFileUrlLabel = "rdpFileUrl";
        public static readonly string RdpFileNameLabel = "rdpFileName";
        public static readonly string BackslashSymbol = "\\";
        // App config constants


        // Error message
        public static readonly string GetRdpFromServerErrorMessage = "an error occured when downloading the rdp file : ";
        public static readonly string AuthorizationErrorMessage = "Please make sure you have the rights to launch this app.";
        public static readonly string NetworkErrorMessage = "Please make sure you are on the rights network to launch this app.";
        public static readonly string InternetErrorMessage = "Please make sure you are connected to internet to launch this app.";

        public static readonly string GenericErrorMessage = "an error occured : ";
        public static readonly string GenericErrorMessageWindowsBoxTitle = "ERROR";

        public static readonly string GenericErrorMessageWindowsBox =
            "An error occured when reaching the remote application. Please contact the ITS.";

    }
}
