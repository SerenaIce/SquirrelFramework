namespace SquirrelFramework.Repository
{
    #region using directives
    
    using System;
    using Utility.Common.Http;

    #endregion using directives

    public class RedisQueue
    {
        public static void Push<T>(T data, string queueName)
        {
            if (string.IsNullOrWhiteSpace(queueName))
            {
                throw new ArgumentNullException(nameof(queueName));
            }
            var db = RedisClient.Client.GetDatabase();
            var dataString = JsonHelper.Serialize(data);
            db.ListRightPushAsync(queueName, JsonHelper.Serialize(new RedisObject(data.GetType(), dataString)));
        }

        public static void Push(RedisObject redisObject, string queueName)
        {
            if (string.IsNullOrWhiteSpace(queueName))
            {
                throw new ArgumentNullException(nameof(queueName));
            }
            var db = RedisClient.Client.GetDatabase();
            db.ListLeftPushAsync(queueName, JsonHelper.Serialize(redisObject));
        }

        public static RedisObject Pop(string queueName)
        {
            if (string.IsNullOrWhiteSpace(queueName))
            {
                throw new ArgumentNullException(nameof(queueName));
            }
            var db = RedisClient.Client.GetDatabase();
            var data = db.ListRightPop(queueName);
            return data.IsNullOrEmpty ? null : JsonHelper.Deserialize<RedisObject>(data);
        }

        public static long Count(string queueName)
        {
            if (string.IsNullOrWhiteSpace(queueName))
            {
                throw new ArgumentNullException(nameof(queueName));
            }
            var db = RedisClient.Client.GetDatabase();
            return db.ListLength(queueName);
        }

        public static void Clear(string queueName)
        {
            if (string.IsNullOrWhiteSpace(queueName))
            {
                throw new ArgumentNullException(nameof(queueName));
            }
            var db = RedisClient.Client.GetDatabase();
            db.KeyDelete(queueName);
        }
    }
}