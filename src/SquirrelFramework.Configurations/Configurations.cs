namespace SquirrelFramework.Configurations
{
    #region using directives

    using System;
    using System.IO;

    #endregion using directives

    public class Configurations

    {
        public static string MongoDBConnectionString { get; private set; }
        public static string MongoDBDefaultDatabaseName { get; private set; }
        public static string RedisConnectionConfiguration { get; private set; }
        public static string InstallPath { get; }

        static Configurations()
        {
            InstallPath = GetInstallPath();
            InitializeMongoDBSettings();
            InitializeRedisSettings();
        }

        private static void InitializeMongoDBSettings()
        {
            var mongodbConfigFilePath = Path.Combine(InstallPath, @"Config\mongodb.config");
            var manager = new XmlManager<MongoDBConfiguration>(mongodbConfigFilePath);
            var model = manager.GetModel();
            MongoDBConnectionString = model.MongoDB.MongoDBClient.ConnectionString;
            MongoDBDefaultDatabaseName = model.MongoDB.MongoDBClient.DefaultDatabase;
        }

        private static void InitializeRedisSettings()
        {
            var redisConfigFilePath = Path.Combine(InstallPath, @"Config\redis.config");
            var manager = new XmlManager<RedisConfiguration>(redisConfigFilePath);
            var model = manager.GetModel();
            RedisConnectionConfiguration = model.Redis.RedisClient.ConnectionConfiguration;
        }

        private static string GetInstallPath()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            if (File.Exists(Path.Combine(path, "web.config")))
            {
                path = Path.Combine(path, "bin");
            }
            return path;
        }
    }
}