// ReSharper disable CheckNamespace
namespace System
// ReSharper restore CheckNamespace
{
    #region using directives

    using System.Collections.Generic;
    using System.Linq;

    #endregion using directives

    ///<Summary>
    /// extension of the System.Object class
    ///</Summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// In extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Boolean In<T>(this T obj, params T[] parameters)
        {
            return parameters.Any(i => i.Equals(obj));
        }

        /// <summary>
        /// In generic extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static Boolean In<T>(this T obj, IEnumerable<T> collection)
        {
            return collection.Contains(obj);
        }

        /// <summary>
        /// If reference extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="predicate"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T If<T>(this T obj, Predicate<T> predicate, Action<T> action)
            where T : class
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (predicate(obj)) action(obj);
            return obj;
        }

        /// <summary>
        /// If value type extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="predicate"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public static T If<T>(this T obj, Predicate<T> predicate, Func<T, T> function)
            where T : struct
        {
            return predicate(obj) ? function(obj) : obj;
        }

        /// <summary>
        /// while extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="predicate"></param>
        /// <param name="actions"></param>
        public static void While<T>(this T obj, Predicate<T> predicate, params Action<T>[] actions)
            where T : class
        {
            while (predicate(obj))
            {
                foreach (var action in actions)
                { action(obj); }
                break;
            }
        }

        /// <summary>
        /// Do extension, this extension is just to write code as human nature language style
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T Do<T>(this T obj, Action<T> action)
        {
            action(obj);
            return obj;
        }

        // Summary:
        //     Returns an System.Object with the specified System.Type and whose value is
        //     equivalent to the specified object.
        //
        // Parameters:
        //   value:
        //     An System.Object that implements the System.IConvertible interface.
        //
        //   conversionType:
        //     A System.Type.
        //
        // Returns:
        //     An object whose System.Type is conversionType and whose value is equivalent
        //     to value.  -or- null, if value is null and conversionType is not a value
        //     type.
        //
        // Exceptions:
        //   System.InvalidCastException:
        //     This conversion is not supported. -or- value is null and conversionType is
        //     a value type.
        //
        //   System.ArgumentNullException:
        //     conversionType is null.
        public static Object ChangeToType(this Object obj, Type conversionType)
        {
            return Convert.ChangeType(obj, conversionType);
        }

        public static Boolean IsType(this Object obj, Type targetType)
        {
            return obj.GetType() == targetType;
        }
    }
}