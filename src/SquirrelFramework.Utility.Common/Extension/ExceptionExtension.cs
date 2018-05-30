// ReSharper disable CheckNamespace

namespace System
// ReSharper restore CheckNamespace
{
    #region using directives

    using Text;

    #endregion using directives

    public static class ExceptionExtension
    {
        public static String GetExceptionDetail(this Exception exception)
        {
            var detailBuilder = new StringBuilder();
            exception = exception.InnerException ?? exception;
            while (exception != null)
            {
                detailBuilder.AppendFormat(
                    "{0}{1}",
                    exception,
                    Environment.NewLine);
                exception = exception.InnerException;
            }
            return detailBuilder.ToString();
        }

        public static String GetExpandedMessage(this Exception exception)
        {
            var messageBuilder = new StringBuilder();
            exception = exception.InnerException ?? exception;
            while (exception != null)
            {
                messageBuilder.AppendFormat(
                    "{0}{1}",
                    exception.Message,
                    Environment.NewLine);
                exception = exception.InnerException;
            }
            return messageBuilder.ToString();
        }

        public static String GetRawMessage(this Exception exception)
        {
            var rawException = exception.InnerException ?? exception;
            return rawException.Message;
        }
    }
}