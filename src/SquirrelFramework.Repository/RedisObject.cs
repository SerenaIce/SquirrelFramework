namespace SquirrelFramework.Repository
{
    #region using directives

    using System;

    #endregion using directives

    public class RedisObject
    {
        public string Data;
        public Type Type;

        public RedisObject(Type type, string data)
        {
            this.Type = type;
            this.Data = data;
        }
    }
}