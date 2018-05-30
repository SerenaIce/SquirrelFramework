namespace SquirrelFramework.Repository
{
    #region using directives

    using Configurations;
    using StackExchange.Redis;

    #endregion

    internal class RedisClient
    {
        public static ConnectionMultiplexer Client { get; } =
            ConnectionMultiplexer.Connect(Configurations.RedisConnectionConfiguration);
    }
}