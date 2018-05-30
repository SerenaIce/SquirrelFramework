// ReSharper disable CheckNamespace

namespace System
// ReSharper restore CheckNamespace
{
    #region using directives

    using Globalization;

    #endregion using directives

    /// <Summary>
    ///     Extended the System.Char Structure.
    /// </Summary>
    public static class CharExtension
    {
        /// <summary>
        ///     Convert a char value to a enum
        /// </summary>
        /// <typeparam name="T">the enum type</typeparam>
        /// <param name="value">the underline char value</param>
        /// <returns>the enum result</returns>
        public static T ToEnum<T>(this Char value)
        {
            return Convert.ToInt32(value).ToEnum<T>();
        }

        /// <summary>
        ///     Convert char to invariant string
        /// </summary>
        /// <param name="value">the char value</param>
        /// <returns>the invariant string value</returns>
        public static String ToStringInvariant(this Char value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}