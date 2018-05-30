// ReSharper disable CheckNamespace

using System.ComponentModel;

namespace System
// ReSharper restore CheckNamespace
{
    /// <Summary>
    ///     Extended the System.Enum class
    /// </Summary>
    public static class EnumExtension
    {
        /// <summary>
        ///     To get the attribute of a enum field
        /// </summary>
        /// <typeparam name="T">the attribute type</typeparam>
        /// <param name="value">an enum object</param>
        /// <returns> To get the enum attribute <c>null</c> </returns>
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var field = value.GetType().GetField(value.ToString());
            return Attribute.GetCustomAttribute(field, typeof (T)) as T;
        }

        /// <summary>
        /// 结合 enum 的 [Description] 标签使用，获取标签中设置的值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Description(this Enum value)
        {
            return value.GetAttribute<DescriptionAttribute>().Description;
        }
    }
}