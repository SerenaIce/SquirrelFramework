namespace SquirrelFramework.Repository
{
    #region using directives

    using Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    #endregion using directives

    public abstract class RepositoryBase<TDomain> : CustomizedRepositoryBase<TDomain> where TDomain : DomainModel
    {
        //redius unit: Meters
        public IEnumerable<TDomain> GetNearBy(Geolocation centerPoint, double redius /* Meter */) => base.GetNearBy(null, centerPoint, redius);

        public long GetCount() => base.GetCount(null);

        public long GetCount(Expression<Func<TDomain, bool>> filter) => base.GetCount(null, filter);

        public int GetPageCount(int pageSize) => base.GetPageCount(null, pageSize);

        public int GetPageCount(int pageSize, Expression<Func<TDomain, bool>> filter) => base.GetPageCount(null, pageSize, filter);

        public IEnumerable<TDomain> GetAll() => base.GetAll(null);

        public IEnumerable<TDomain> GetAllByPage(int pageIndex, int pageSize) => base.GetAllByPage(null, pageIndex, pageSize);

        public IEnumerable<TDomain> GetAllByPageSortBy(int pageIndex, int pageSize,
            Expression<Func<TDomain, object>> sortBy, bool isSortByDescending = false) =>
            base.GetAllByPageSortBy(null, pageIndex, pageSize, sortBy, isSortByDescending);

        public IEnumerable<TDomain> GetAll(Expression<Func<TDomain, bool>> filter) => base.GetAll(null, filter);

        public IEnumerable<TDomain> GetAllByPage(int pageIndex, int pageSize,
            Expression<Func<TDomain, bool>> filter) => base.GetAllByPage(null, pageIndex, pageSize, filter);

        public IEnumerable<TDomain> GetAllByPageSortBy(int pageIndex, int pageSize,
            Expression<Func<TDomain, bool>> filter, Expression<Func<TDomain, object>> sortBy,
            bool isSortByDescending = false) => base.GetAllByPageSortBy(null, pageIndex, pageSize, filter, sortBy);

        public TDomain Get(string id) => base.Get(null, id);

        public TDomain Get(Expression<Func<TDomain, bool>> filter) => base.Get(null, filter);

        public void Add(TDomain model) => base.Add(null, model);

        public Task AddAsync(TDomain model) => base.AddAsync(null, model);

        public void AddMany(IEnumerable<TDomain> models) => base.AddMany(null, models);

        public Task AddManyAsync(IEnumerable<TDomain> models) => base.AddManyAsync(null, models);

        public long Update(TDomain model) => base.Update(null, model);

        public Task UpdateAsync(TDomain model) => base.UpdateAsync(null, model);

        public void UpdateMany(IEnumerable<TDomain> items) => base.UpdateMany(null, items);

        public void UpdateManyAsync(IEnumerable<TDomain> items) => base.UpdateManyAsync(null, items);

        public long Delete(string id) => base.Delete(null, id);

        public Task DeleteAsync(string id) => base.DeleteAsync(null, id);

        public long DeleteMany(Expression<Func<TDomain, bool>> filter) => base.DeleteMany(null, filter);

        public Task DeleteManyAsync(Expression<Func<TDomain, bool>> filter) => base.DeleteManyAsync(null, filter);
    }
}