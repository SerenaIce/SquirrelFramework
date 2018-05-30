namespace SquirrelFramework.Utility.Windows.Mime
{
    #region using directives

    using System;
    using System.Globalization;
    using System.IO;
    using System.Net.Mail;
    using System.Reflection;

    #endregion using directives

    /// <summary>
    ///     The mail writer class is a internal class of .net mail api
    /// </summary>
    public class MimeCompatibleFormatGenerator
        : IMimeCompatibleFormatGenerator
    {
        private static readonly Type mailWriterType;
        private static readonly BindingFlags flags;
        private static readonly MethodInfo sendMethod;

        static MimeCompatibleFormatGenerator()
        {
            flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
            mailWriterType = typeof (SmtpClient).Assembly.GetType("System.Net.Mail.MailWriter");
            sendMethod = typeof (MailMessage).GetMethod("Send", flags);
        }

        public void Generate(MailMessage message, Stream outPutStream)
        {
            var mailWriter = Activator.CreateInstance(mailWriterType, flags, null, new Object[] {outPutStream},
                CultureInfo.InvariantCulture);
            sendMethod.Invoke(message, new[] {mailWriter, true});
        }

        public Byte[] Generate(MailMessage message)
        {
            using (var ms = new MemoryStream())
            {
                this.Generate(message, ms);
                return ms.ToArray();
            }
        }
    }
}