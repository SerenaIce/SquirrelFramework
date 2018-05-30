namespace SquirrelFramework.Utility.Windows.Mime
{
    #region using directives

    using System;
    using System.IO;
    using System.Net.Mail;

    #endregion using directives

    /// <summary>
    ///     This interface use the .net mail internal api
    ///     to generate a .eml stream which in mime format
    /// </summary>
    public interface IMimeCompatibleFormatGenerator
    {
        void Generate(MailMessage message, Stream outStream);

        Byte[] Generate(MailMessage message);
    }
}