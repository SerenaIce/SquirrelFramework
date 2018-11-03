namespace SquirrelFramework.Repository
{
    #region using directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Model;
    using MongoDB.Bson;
    using MongoDB.Driver;

    #endregion using directives

    internal class DataCollectionSelector
    {
        private readonly MongoClient currentClient;
        private readonly string defaultDatabaseName;

        public DataCollectionSelector(MongoClient mongoClient, string defaultDatabaseName)
        {
            this.currentClient = mongoClient;
            this.defaultDatabaseName = defaultDatabaseName;
        }

        public virtual IEnumerable<string> GetDataCollectionList(string databaseName)
        {
            var collections =
                Enumerate(this.currentClient.GetDatabase(databaseName).ListCollections()).Select(
                    c => c.GetValue("name").AsString);
            return collections;
        }

        public static IEnumerable<BsonDocument> Enumerate(IAsyncCursor<BsonDocument> docs)
        {
            while (docs.MoveNext())
            {
                foreach (var item in docs.Current)
                {
                    yield return item;
                }
            }
        }

        public virtual IMongoCollection<T> GetDataCollection<T>()
        {
            // 优先选择 实体标签所指定的数据库名
            var databaseName = typeof(T).GetAttributeValue<DatabaseAttribute, string>(t => t.Name);
            if (databaseName == null)
            {
                databaseName = this.defaultDatabaseName;
                if (databaseName == null)
                {
                    throw new Exception(
                        "You must specified the [Database] attribute for the domain model or set the default database name at the configuration file.");
                }
            }

            // <!> Since 1.0.14 [Collection] attribute is no longer required.
            // Get collection name from [Collection] Attribute
            // If not specified the [Collection] attribute, use the class name as the collection name
            var collectionName = typeof(T).GetAttributeValue<CollectionAttribute, string>(t => t.Name) ?? typeof(T).Name;
            // throw new Exception("You must specified the [Collection] attribute for the domain model.");

            return MongoDBCollection.GetCollection<T>(this.currentClient, databaseName, collectionName);
        }

        public virtual IMongoCollection<T> GetDataCollection<T>(string collectionName)
        {
            // 优先选择 实体标签所指定的数据库名
            var databaseName = typeof(T).GetAttributeValue<DatabaseAttribute, string>(t => t.Name);
            if (databaseName == null)
            {
                databaseName = this.defaultDatabaseName;
                if (databaseName == null)
                {
                    throw new Exception(
                        "You must specified the [Database] attribute for the domain model or set the default database name at the configuration file.");
                }
            }
            return MongoDBCollection.GetCollection<T>(this.currentClient, databaseName, collectionName);
        }

        public virtual IMongoCollection<T> GetDataCollection<T>(string databaseName, string collectionName)
        {
            return MongoDBCollection.GetCollection<T>(this.currentClient, databaseName, collectionName);
        }

        public virtual IMongoCollection<T> GetDataCollection<T>(MongoClient client, string databaseName,
            string collectionName)
        {
            return MongoDBCollection.GetCollection<T>(client, databaseName, collectionName);
        }
    }
}