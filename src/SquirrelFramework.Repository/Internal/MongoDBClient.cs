namespace SquirrelFramework.Repository
{
    #region using directives

    using Configurations;
    using Domain.Model;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.IdGenerators;
    using MongoDB.Driver;

    #endregion

    internal class MongoDBClient
    {
        static MongoDBClient()
        {
            BsonClassMap.RegisterClassMap<DomainModel>(map =>
            {
                map.AutoMap();
                map.SetIdMember(map.GetMemberMap(m => m.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance));
            });
        }

        public static MongoClient Client { get; } = new MongoClient(Configurations.MongoDBConnectionString);

        public static string DefaultDatabaseName { get; } = Configurations.MongoDBDefaultDatabaseName;
    }
}