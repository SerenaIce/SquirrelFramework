// ReSharper disable CheckNamespace
namespace System.Reflection
// ReSharper restore CheckNamespace
{
    public static class ReflectionExtension
    {
        public static T GetAttribute<T>(this PropertyInfo propertyInfo)
            where T : Attribute
        {
            return Attribute.GetCustomAttribute(propertyInfo, typeof(T)) as T;
        }
    }
}