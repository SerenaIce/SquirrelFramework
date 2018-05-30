// ReSharper disable CheckNamespace
namespace System
// ReSharper restore CheckNamespace
{
    #region using directives

    using SquirrelFramework.Utility.Windows.Mime;

    #endregion using directives

#pragma warning disable 1587
    /// <Summary>
    ///     extension of the System.String class
    /// </Summary>
#pragma warning restore 1587
    public static class StringExtension
    {
        /// <summary>
        ///     To get the mime type of the special file name
        /// </summary>
        /// <param name="fileNameOrExtension">the file name or file name extension with dot </param>
        /// <returns>the mime type string</returns>
        public static string GetMimeType(this string fileNameOrExtension)
        {
            var mimeHelper = new MimeTypeHelper();
            return mimeHelper.GetMimeTypeForFile(fileNameOrExtension);
        }

        /// <summary>
        ///     To get the mime type from the special file name data
        /// </summary>
        /// <returns>the mime type string</returns>
        public static string GetMimeTypeFromFileData(this string filePath)
        {
            var mimeHelper = new MimeTypeHelper();
            return mimeHelper.GetMimeFromFile(filePath);
        }
    }
}