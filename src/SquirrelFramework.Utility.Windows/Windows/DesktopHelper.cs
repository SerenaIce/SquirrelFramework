namespace SquirrelFramework.Utility.Windows
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    public class DesktopHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SystemParametersInfo(UInt32 uiAction, UInt32 uiParam, String pvParam, UInt32 fWinIni);

        public const int SPI_SETDESKWALLPAPER = 20;
        public const int SPIF_SENDCHANGE = 0x2;

        public static void SetPictureAsDesktopBackground(string path)
        {
            if (!SystemParametersInfo(SPI_SETDESKWALLPAPER, 1, path, SPIF_SENDCHANGE))
            {
                throw new System.ComponentModel.Win32Exception();
            }
        }

        public static string GetWallpaperFullpath(string fileName)
        {
            return Path.Combine(CheckAndGetMyPictureWallpaperFolder(), fileName);
        }

        /// <summary>
        /// Due to the execution user is different, DO NOT use this function for Windows service process
        /// </summary>
        public static string CheckAndGetMyPictureWallpaperFolder()
        {
            var myPicturesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            var wallpaperFolder = Path.Combine(myPicturesFolder, "Wallpaper");
            if (!Directory.Exists(wallpaperFolder))
            {
                Directory.CreateDirectory(wallpaperFolder);
            }
            return wallpaperFolder;
        }

        public static string GetWallpaperByDate(DateTime date)
        {
            var searchResult = Directory.GetFiles(CheckAndGetMyPictureWallpaperFolder(), $"{date.ToString("yyyyMMdd")}_*");
            if (searchResult == null || searchResult.Length == 0)
            {
                return null;
            }
            else
            {
                return searchResult[0];
            }
        }
    }
}