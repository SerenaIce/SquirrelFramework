namespace SquirrelFramework.Utility.Windows
{
    #region using directives

    using System;
    using System.Diagnostics;
    using System.IO;

    #endregion using directives

    public class Explorer
    {
        public static void OpenFileLocationInWindowsExplorer(String fileFullPath)
        {
            if (fileFullPath == null || !File.Exists(fileFullPath)) return;
            Process.Start("Explorer.exe", "/select," + fileFullPath);
        }

        public static void OpenFolder(string folderFullPath)
        {
            Process.Start("Explorer", folderFullPath);
        }

        public static Boolean OpenMailProgram(String receiver, String subject, String body = null,
           String attachFileFullPath = null, String CC = null,
           String BCC = null)
        {
            var mailtoCommand = String.Format(
                "mailto:{0}?subject={1}&body={2}&CC={3}&BCC={4}&attachment='file:///{5}'",
                receiver ?? " ", subject ?? " ", body ?? " ",
                CC ?? " ", BCC ?? " ", attachFileFullPath ?? " ");
            try
            {
                Process.Start(mailtoCommand);
                // for Outlook see mor
                // http: //msdn.microsoft.com/en-us/library/ms268870(v=vs.90).aspx for thunderbird
                // use -compose
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void OpenImageViewerProgram(String imageFullPath)
        {
            if (imageFullPath == null || !File.Exists(imageFullPath)) return;
            var process = new Process
            {
                StartInfo =
                {
                    FileName = imageFullPath,
                    // Todo
                    Arguments = "rundll32.exe C://WINDOWS//system32//shimgvw.dll,ImageView_Fullscreen",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
            };
            process.Start();
        }        
    }
}