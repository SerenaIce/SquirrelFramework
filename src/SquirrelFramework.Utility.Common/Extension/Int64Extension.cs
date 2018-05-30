// ReSharper disable CheckNamespace

namespace System
// ReSharper restore CheckNamespace
{
    /// <Summary>
    ///     extension of the System.Int64 class
    /// </Summary>
    public static class Int64Extension
    {
        /// <summary>
        ///     when the date time is before 1970 1 1 0 0 , the ticks in java is a minus number
        /// </summary>
        /// <param name="dotNetTimeInLong">dot net date time in long ticks</param>
        /// <returns></returns>
        public static Int64 DotNetToJavaTime(this Int64 dotNetTimeInLong)
        {
            var javaTicksTime = dotNetTimeInLong - new DateTime(1970, 1, 1, 0, 0, 0).Ticks;
            return javaTicksTime/10000;
        }

        /// <summary>
        ///     The java long time to dot net time ticks
        /// </summary>
        /// <param name="javaTimeInLong">java time in long</param>
        /// <returns>dot time in ticks</returns>
        public static Int64 JavaToDotNetTimeInLong(this Int64 javaTimeInLong)
        {
            return (javaTimeInLong*10000L) + new DateTime(1970, 1, 1, 0, 0, 0).Ticks;
        }

        /// <summary>
        ///     when the date time is before 1970 1 1 0 0 , the ticks in java is a minus number
        /// </summary>
        /// <param name="dotNetTimeInLong">dot net date time in long ticks</param>
        /// <returns></returns>
        public static Int64 DotNetToWindowsFileTime(this Int64 dotNetTimeInLong)
        {
            return dotNetTimeInLong - new DateTime(1601, 1, 1, 0, 0, 0).Ticks;
        }

        /// <summary>
        ///     Convert time in long type of java to DateTime
        /// </summary>
        /// <param name="javaTimeInLong">date time in java long</param>
        /// <returns>the dot net time</returns>
        public static DateTime JavaToDotNetTime(this Int64 javaTimeInLong)
        {
            return new DateTime(javaTimeInLong.JavaToDotNetTimeInLong());
        }

        /// <summary>
        ///     Unix time is offset second of 1970, 1, 1, 0, 0, 0
        /// </summary>
        /// <param name="unixTimeInLong">time in long</param>
        /// <returns>the dot net date time</returns>
        public static DateTime UnixToDotNetTime(this Int64 unixTimeInLong)
        {
            return unixTimeInLong.UnixToJavaTime().JavaToDotNetTime();
        }

        /// <summary>
        ///     Unix time is offset second of 1970, 1, 1, 0, 0, 0
        /// </summary>
        /// <param name="unixTimeInLong">dre time in long type</param>
        /// <returns>java time in long type</returns>
        public static Int64 UnixToJavaTime(this Int64 unixTimeInLong)
        {
            return unixTimeInLong*1000L;
        }

        /// <summary>
        ///     Unix time is offset second of 1970, 1, 1, 0, 0, 0
        /// </summary>
        /// <param name="unixTimeInLong">time in long</param>
        /// <returns>the dot net date time</returns>
        public static Int64 UnixToDotNetTimeInLong(this Int64 unixTimeInLong)
        {
            return unixTimeInLong.UnixToJavaTime().JavaToDotNetTimeInLong();
        }

        /// <summary>
        ///     Unix time is offset second of 1970, 1, 1, 0, 0, 0
        /// </summary>
        /// <returns>the dot net date time</returns>
        public static DateTime WindowsFileTimeToDotNetTime(this Int64 windowsFileTimeInLong)
        {
            return DateTime.FromFileTimeUtc(windowsFileTimeInLong);
        }

        /// <summary>
        ///     Unix time is offset second of 1970, 1, 1, 0, 0, 0
        /// </summary>
        /// <returns>the dot net date time</returns>
        public static Int64 WindowsFileTimeToDotNetTimeInLong(this Int64 windowsFileTimeInLong)
        {
            return DateTime.FromFileTimeUtc(windowsFileTimeInLong).ToUniversalTime().Ticks;
        }

        public static DateTime FixTimeToDay(this long time)
        {
            var currentTime = new DateTime(time);
            var fixToDay = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day);
            if (fixToDay.Ticks < time)
            {
                if (time - fixToDay.Ticks == 1)
                {
                    return new DateTime(time - 1);
                }
                return fixToDay.AddDays(1);
            }
            return new DateTime(time);
        }
    }
}