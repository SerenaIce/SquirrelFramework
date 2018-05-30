namespace SquirrelFramework.Domain.Service
{
    #region using directives

    using System;
    using System.Collections.Generic;
    using Model;
    using Repository;

    #endregion

    public abstract class CustomizedServiceBase<TDomain, TCustomizedRepository>
        where TDomain : DomainModel
        where TCustomizedRepository : CustomizedRepositoryBase<TDomain>
    {
        protected TCustomizedRepository Repository { get; set; } =
            (TCustomizedRepository) Activator.CreateInstance(typeof(TCustomizedRepository));

        public long GetCount(string collectionName)
        {
            return this.Repository.GetCount(collectionName);
        }

        public IEnumerable<TDomain> GetAll(string collectionName)
        {
            return this.Repository.GetAll(collectionName);
        }

        public TDomain Get(string collectionName, string id)
        {
            return this.Repository.Get(collectionName, id);
        }

        public void Add(string collectionName, TDomain model)
        {
            this.Repository.Add(collectionName, model);
        }

        public void AddAsync(string collectionName, TDomain model)
        {
            this.Repository.AddAsync(collectionName, model);
        }

        public void AddManyAsync(string collectionName, IEnumerable<TDomain> items)
        {
            this.Repository.AddManyAsync(collectionName, items);
        }

        public void Update(string collectionName, TDomain model)
        {
            this.Repository.Update(collectionName, model);
        }

        public void UpdateAsync(string collectionName, TDomain model)
        {
            this.Repository.UpdateAsync(collectionName, model);
        }

        public void UpdateManyAsync(string collectionName, IEnumerable<TDomain> items)
        {
            this.Repository.UpdateManyAsync(collectionName, items);
        }

        public void Delete(string collectionName, string id)
        {
            this.Repository.Delete(collectionName, id);
        }

        public void DeleteAsync(string collectionName, string id)
        {
            this.Repository.DeleteAsync(collectionName, id);
        }
    }
}