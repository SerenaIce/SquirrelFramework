namespace SquirrelFramework.Repository
{
    #region using directives

    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Domain.Model;
    using MongoDB.Driver;

    #endregion

    /// <summary>
    ///     在使用时现指定 Collection 名，特别适合分表存储的场景
    /// </summary>
    public abstract class CustomizedRepositoryBase<TDomain> where TDomain : DomainModel
    {
        public const double EarthRadius = 6378.137; // KM

        private readonly DataCollectionSelector dataSelector;

        protected CustomizedRepositoryBase()
        {
            this.dataSelector = new DataCollectionSelector(MongoDBClient.Client, MongoDBClient.DefaultDatabaseName);
        }

        protected IMongoCollection<TDomain> GetCollection(string collectionName)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
            {
                return this.dataSelector.GetDataCollection<TDomain>();
            }
            return this.dataSelector.GetDataCollection<TDomain>(collectionName);
        }

        //redius unit: Meters
        public IEnumerable<TDomain> GetNearBy(string collectionName, Geolocation centerPoint, double redius /* Meter */)
        {
            // 获取表
            var collection = this.GetCollection(collectionName);

            // 建立空间搜索 索引
            collection.Indexes.CreateOneAsync(Builders<TDomain>.IndexKeys.Geo2DSphere(m => m.Geolocation)).Wait();

            // 空间搜索算法
            var rangeInKm = redius / 1000; // KM
            var radians = rangeInKm / EarthRadius;

            var filter = Builders<TDomain>.Filter.NearSphere(m => m.Geolocation, centerPoint.Longitude,
                centerPoint.Latitude,
                radians);
            return collection.Find(filter).ToList();
        }

        public long GetCount(string collectionName)
        {
            return this.GetCollection(collectionName).Count(_ => true);
        }

        public long GetCount(string collectionName, Expression<Func<TDomain, bool>> filter)
        {
            return this.GetCollection(collectionName).Count(filter);
        }

        public int GetPageCount(string collectionName, int pageSize)
        {
            return Convert.ToInt32(
                Math.Ceiling(
                    this.GetCollection(collectionName).Count(_ => true) * 1.0
                    / pageSize));
        }

        public int GetPageCount(string collectionName, int pageSize, Expression<Func<TDomain, bool>> filter)
        {
            return Convert.ToInt32(
                Math.Ceiling(
                    this.GetCollection(collectionName).Count(filter) * 1.0
                    / pageSize));
        }

        public IEnumerable<TDomain> GetAll(string collectionName)
        {
            return this.GetCollection(collectionName).Find(_ => true).ToList();
        }

        public IEnumerable<TDomain> GetAllByPage(string collectionName, int pageIndex, int pageSize)
        {
            return this.GetCollection(collectionName).Find(_ => true).Skip(pageIndex * pageSize).Limit(pageSize)
                .ToList();
        }

        public IEnumerable<TDomain> GetAllByPageSortBy(string collectionName, int pageIndex, int pageSize,
            Expression<Func<TDomain, object>> sortBy, bool isSortByDescending = false)
        {
            return isSortByDescending
                ? this.GetCollection(collectionName).Find(_ => true).SortByDescending(sortBy).Skip(pageIndex * pageSize)
                    .Limit(pageSize).ToList()
                : this.GetCollection(collectionName).Find(_ => true).SortBy(sortBy).Skip(pageIndex * pageSize)
                    .Limit(pageSize).ToList();
        }

        public IEnumerable<TDomain> GetAll(string collectionName, Expression<Func<TDomain, bool>> filter)
        {
            return this.GetCollection(collectionName).Find(filter).ToList();
        }

        public IEnumerable<TDomain> GetAllByPage(string collectionName, int pageIndex, int pageSize,
            Expression<Func<TDomain, bool>> filter)
        {
            return this.GetCollection(collectionName).Find(filter).Skip(pageIndex * pageSize).Limit(pageSize).ToList();
        }

        public IEnumerable<TDomain> GetAllByPageSortBy(string collectionName, int pageIndex, int pageSize,
            Expression<Func<TDomain, bool>> filter, Expression<Func<TDomain, object>> sortBy,
            bool isSortByDescending = false)
        {
            return isSortByDescending
                ? this.GetCollection(collectionName).Find(filter).SortByDescending(sortBy).Skip(pageIndex * pageSize)
                    .Limit(pageSize).ToList()
                : this.GetCollection(collectionName).Find(filter).SortBy(sortBy).Skip(pageIndex * pageSize)
                    .Limit(pageSize).ToList();
        }

        public IEnumerable<TDomain> GetTop(
            string collectionName,
            int resultNumber,
            Expression<Func<TDomain, object>> sortBy,
            bool isSortByDescending = false)
        {
            return this.GetAllByPageSortBy(collectionName, 0, resultNumber, sortBy, isSortByDescending);
        }

        public IEnumerable<TDomain> GetTop(
            string collectionName,
            double percent,
            Expression<Func<TDomain, object>> sortBy,
            bool isSortByDescending = false)
        {
            if (percent < 0d || percent > 1d)
            {
                throw new Exception("Please specific a valid percent value from 0 to 1, e.g. 0.45 (means 45%)");
            }
            var pageCount = this.GetCount(collectionName);
            var resultNumber = Convert.ToInt32(pageCount * percent);
            // Caution the loss of accuracy (long to int)
            return this.GetAllByPageSortBy(collectionName, 0, resultNumber, sortBy, isSortByDescending);
        }

        public IEnumerable<TDomain> GetTop(
            string collectionName,
            int resultNumber,
            Expression<Func<TDomain, bool>> filter,
            Expression<Func<TDomain, object>> sortBy,
            bool isSortByDescending = false)
        {
            return this.GetAllByPageSortBy(collectionName, 0, resultNumber, filter, sortBy, isSortByDescending);
        }

        public IEnumerable<TDomain> GetTop(
            string collectionName,
            double percent,
            Expression<Func<TDomain, bool>> filter,
            Expression<Func<TDomain, object>> sortBy,
            bool isSortByDescending = false)
        {
            if (percent < 0d || percent > 1d)
            {
                throw new Exception("Please specific a valid percent value from 0 to 1, e.g. 0.45 (means 45%)");
            }
            var pageCount = this.GetCount(collectionName, filter);
            var resultNumber = Convert.ToInt32(pageCount * percent);
            // Caution the loss of accuracy (long to int)
            return this.GetAllByPageSortBy(collectionName, 0, resultNumber, filter, sortBy, isSortByDescending);
        }

        public TDomain Get(string collectionName, string id)
        {
            return this.GetCollection(collectionName).Find(doc => doc.Id == id).FirstOrDefault();
        }

        public TDomain Get(string collectionName, Expression<Func<TDomain, bool>> filter)
        {
            return this.GetCollection(collectionName).Find(filter).FirstOrDefault();
        }

        public void Add(string collectionName, TDomain model)
        {
            this.GetCollection(collectionName).InsertOne(model);
        }

        public Task AddAsync(string collectionName, TDomain model)
        {
            return this.GetCollection(collectionName).InsertOneAsync(model);
        }

        public void AddMany(string collectionName, IEnumerable<TDomain> models)
        {
            this.GetCollection(collectionName).InsertMany(models);
        }

        public Task AddManyAsync(string collectionName, IEnumerable<TDomain> models)
        {
            return this.GetCollection(collectionName).InsertManyAsync(models);
        }

        public long Update(string collectionName, TDomain model)
        {
            var executeResult = this.GetCollection(collectionName).ReplaceOne(doc => doc.Id == model.Id, model);
            if (executeResult.IsModifiedCountAvailable)
            {
                return executeResult.ModifiedCount;
            }
            return -1;
        }

        public Task UpdateAsync(string collectionName, TDomain model)
        {
            return this.GetCollection(collectionName).ReplaceOneAsync(doc => doc.Id == model.Id, model);
        }

        public void UpdateMany(string collectionName, IEnumerable<TDomain> items)
        {
            var collection = this.GetCollection(collectionName);
            foreach (var item in items)
            {
                collection.ReplaceOne(doc => doc.Id == item.Id, item);
            }
        }

        public void UpdateManyAsync(string collectionName, IEnumerable<TDomain> items)
        {
            var collection = this.GetCollection(collectionName);
            foreach (var item in items)
            {
                collection.ReplaceOneAsync(doc => doc.Id == item.Id, item);
            }
        }

        public long Delete(string collectionName, string id)
        {
            return this.GetCollection(collectionName).DeleteOne(doc => doc.Id == id).DeletedCount;
        }

        public Task DeleteAsync(string collectionName, string id)
        {
            return this.GetCollection(collectionName).DeleteOneAsync(doc => doc.Id == id);
        }

        public long DeleteMany(string collectionName, Expression<Func<TDomain, bool>> filter)
        {
            return this.GetCollection(collectionName).DeleteMany(filter).DeletedCount;
        }

        public Task DeleteManyAsync(string collectionName, Expression<Func<TDomain, bool>> filter)
        {
            return this.GetCollection(collectionName).DeleteManyAsync(filter);
        }

        public IEnumerable<string> GetAllDataCollectionsName(string targetDatabaseName)
        {
            return this.dataSelector.GetDataCollectionList(targetDatabaseName);
        }

        public void DropCollection(string collectionName)
        {
            this.GetCollection(collectionName).Database.DropCollection(collectionName);
        }
    }
}