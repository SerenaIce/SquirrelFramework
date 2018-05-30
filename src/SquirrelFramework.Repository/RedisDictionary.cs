namespace SquirrelFramework.Repository
{
    #region using directives
    
    using StackExchange.Redis;
    using System;
    using Utility.Common.Http;

    #endregion using directives

    public class RedisDictionary
    {
        public static void Add<T>(string key, T data, string hashTableName)
        {
            if (string.IsNullOrWhiteSpace(hashTableName))
            {
                throw new ArgumentNullException(nameof(hashTableName));
            }
            var db = RedisClient.Client.GetDatabase();
            var dataString = JsonHelper.Serialize(data);
            db.HashSetAsync(hashTableName, new[] { new HashEntry(key, JsonHelper.Serialize(new RedisObject(data.GetType(), dataString))) });
        }

        public static void Add(string key, RedisObject redisObject, string hashTableName)
        {
            if (string.IsNullOrWhiteSpace(hashTableName))
            {
                throw new ArgumentNullException(nameof(hashTableName));
            }
            var db = RedisClient.Client.GetDatabase();
            db.HashSetAsync(hashTableName, new[] { new HashEntry(key, JsonHelper.Serialize(redisObject)) });
        }

        public static RedisObject GetAndRemove(string key, string hashTableName)
        {
            if (string.IsNullOrWhiteSpace(hashTableName))
            {
                throw new ArgumentNullException(nameof(hashTableName));
            }
            var db = RedisClient.Client.GetDatabase();
            var data = db.HashGet(hashTableName, key);
            if (data.IsNullOrEmpty)
            {
                return null;
            }
            else
            {
                db.HashDeleteAsync(hashTableName, key);
                return JsonHelper.Deserialize<RedisObject>(data);
            }
        }

        public static long Count(string hashTableName)
        {
            if (string.IsNullOrWhiteSpace(hashTableName))
            {
                throw new ArgumentNullException(nameof(hashTableName));
            }
            var db = RedisClient.Client.GetDatabase();
            return db.HashLength(hashTableName);
        }

        public static void Clear(string hashTableName)
        {
            if (string.IsNullOrWhiteSpace(hashTableName))
            {
                throw new ArgumentNullException(nameof(hashTableName));
            }
            var db = RedisClient.Client.GetDatabase();
            db.KeyDelete(hashTableName);
        }
    }
}