namespace SquirrelFramework.Utility.Common.Http
{
    using System;
    using System.Net;

    public class HttpHelper
    {
        public static void DownloadPicture(string url, string fileFullPath)
        {
            var mywebclient = new WebClient();
            mywebclient.DownloadFile(url, fileFullPath);
        }

        public static void DownloadPictureAsync(string url, string fileFullPath)
        {
            var mywebclient = new WebClient();
            mywebclient.DownloadFileAsync(new Uri(url), fileFullPath);
        }
    }
}