namespace SquirrelFramework.Domain.Service
{
    #region using directives

    using System;
    using System.Collections.Generic;
    using Model;
    using Repository;

    #endregion

    public abstract class ServiceBase<TDomain, TRepository>
        where TDomain : DomainModel
        where TRepository : RepositoryBase<TDomain>
    {
        protected TRepository Repository { get; set; } = (TRepository) Activator.CreateInstance(typeof(TRepository));

        public long GetCount()
        {
            return this.Repository.GetCount();
        }

        public IEnumerable<TDomain> GetAll()
        {
            return this.Repository.GetAll();
        }

        public TDomain Get(string id)
        {
            return this.Repository.Get(id);
        }

        public void Add(TDomain model)
        {
            this.Repository.Add(model);
        }

        public void AddAsync(TDomain model)
        {
            this.Repository.AddAsync(model);
        }

        public void AddManyAsync(IEnumerable<TDomain> items)
        {
            this.Repository.AddManyAsync(items);
        }

        public void Update(TDomain model)
        {
            this.Repository.Update(model);
        }

        public void UpdateAsync(TDomain model)
        {
            this.Repository.UpdateAsync(model);
        }

        public void UpdateManyAsync(IEnumerable<TDomain> items)
        {
            this.Repository.UpdateManyAsync(items);
        }

        public void Delete(string id)
        {
            this.Repository.Delete(id);
        }

        public void DeleteAsync(string id)
        {
            this.Repository.DeleteAsync(id);
        }
    }
}