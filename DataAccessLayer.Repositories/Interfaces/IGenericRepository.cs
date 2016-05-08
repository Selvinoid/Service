namespace DataAccessLayer.Repositories.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    interface IGenericRepository<TEntity> : IRepository where TEntity : class
    {
        void Add(TEntity entity);

        void Add(IEnumerable<TEntity> entities);

        void Modify(TEntity entity);

        TEntity GetSingle(Expression<Func<TEntity, bool>> func);

        IEnumerable<TEntity> Get();

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> func);

        bool Exist(Expression<Func<TEntity, bool>> func);

        int Count(Expression<Func<TEntity, bool>> func);

        void Remove(TEntity entity);
    }
}
