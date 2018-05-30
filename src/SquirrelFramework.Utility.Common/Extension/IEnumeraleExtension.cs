// ReSharper disable CheckNamespace

namespace System.Linq
// ReSharper restore CheckNamespace
{
    #region using directives

    using Collections.Generic;

    #endregion using directives

    /// <Summary>
    ///     Extended the IEnumerable<T> interface</T>
    /// </Summary>
    public static class IEnumerableExtension
    {
        [ThreadStatic] private static readonly Random random;

        static IEnumerableExtension()
        {
            random = new Random(Guid.NewGuid().GetHashCode());
        }

        /// <summary>
        /// 本方法用来对 可列举集合 进行乱序 排列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(i => random.Next());
        }

        /// <summary>
        ///     This is the ForEach extension of the IEnumerable generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="func"></param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> func)
        {
            foreach (var item in source)
                func(item);
        }

        /// <summary>
        ///     This is the ConvertAll extension method of the IEnuerable generic type
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="source"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IEnumerable<TOutput> ConvertAll<TInput, TOutput>(this IEnumerable<TInput> source,
            Func<TInput, TOutput> func)
        {
            return source.Select(func);
        }
    }
}