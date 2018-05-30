namespace SquirrelFramework.Domain.Model
{
    #region using directives

    using System;

    #endregion using directives

    /// <summary>
    /// 此标签用于指定存储于数据库的实体对应的数据库名，如果已经在配置文件中设置了默认数据库名，则不必添加本标签
    /// 当本标签和配置文件的默认数据库名同时存在时，会优先选择本标签所制定的数据库名
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DatabaseAttribute : Attribute
    {
        public DatabaseAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}