namespace SquirrelFramework.Domain.Model
{
    #region using directives

    using System;

    #endregion using directives

    /// <summary>
    /// 此标签用于指定存储于数据库的实体对应的表名，当一个实体作为需要存储时，必须指定本标签
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CollectionAttribute : Attribute
    {
        public CollectionAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}