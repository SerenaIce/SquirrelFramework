namespace System
{
    using SquirrelFramework.Utility.Common.Datetime;

    /// <Summary>
    ///     Extended the System.DateTime structure
    /// </Summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// Converts a DateTime to a javascript timestamp.
        /// http://stackoverflow.com/a/5117291/13932
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The javascript timestamp.</returns>
        public static long ToJavaScriptTimestamp(this DateTime input)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var time = input.Subtract(new TimeSpan(epoch.Ticks));
            return (long)(time.Ticks / 10000);
        }

        /// <summary>
        ///     convert a date time to java time in long
        /// </summary>
        /// <param name="dateTime">date time</param>
        /// <returns>the java date time in long type</returns>
        public static Int64 DotNetToJavaTime(this DateTime dateTime)
        {
            return dateTime.Ticks.DotNetToJavaTime();
        }

        /// <summary>
        ///     convert a date time to java time in long
        /// </summary>
        /// <param name="dateTime">date time</param>
        /// <returns>the java date time in long type</returns>
        public static Int64 ToJavaTime(this DateTime dateTime)
        {
            return dateTime.Ticks.DotNetToJavaTime();
        }

        /// <summary>
        ///     Unix time is offset second of 1970, 1, 1, 0, 0, 0
        /// </summary>
        /// <param name="dateTime" />
        /// <returns></returns>
        public static Int64 ToUnixTime(this DateTime dateTime)
        {
            return dateTime.Ticks.DotNetToJavaTime() / 1000L;
        }

        /// <summary>
        ///     Windows file  time is offset second of 1600, 1, 1, 0, 0, 0
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static Int64 ToWindowsFileTime(this DateTime dateTime)
        {
            return dateTime.ToFileTimeUtc();
        }

        public static String ToRfc822TimeFormatString(this DateTime datetime)
        {
            return new Rfc822DateTime(datetime.ToUniversalTime()).ToString(TimeZoneInfo.Utc);
        }
    }
}