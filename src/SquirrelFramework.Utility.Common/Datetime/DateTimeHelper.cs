namespace SquirrelFramework.Utility.Common.Datetime
{
    using System;

    public class DateTimeHelper
    {
        public static DateTime GetNextWorkday(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Friday)
            {
                return date.AddDays(3);
            }
            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                return date.AddDays(2);
            }
            return date.AddDays(1);
        }

        public static DateTime GetPreviousWorkday(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Monday)
            {
                return date.AddDays(-3);
            }
            if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                return date.AddDays(-2);
            }
            return date.AddDays(-1);
        }
    }
}