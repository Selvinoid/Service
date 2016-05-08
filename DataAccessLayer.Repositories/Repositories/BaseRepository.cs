namespace DataAccessLayer.Repositories.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using DataAccessLayer.Repositories.Interfaces;

    public class BaseRepository<TEntity> : BaseSimpleRepository, IGenericRepository<TEntity> where TEntity : class
    {
        public BaseRepository(IHotelDbContext dataContext)
            : base(dataContext)
        {
        }

        protected IDbSet<TEntity> EntitySet
        {
            get
            {
                return this.Context.Set<TEntity>();
            }
        }

        public virtual void Add(TEntity entity)
        {
            this.EntitySet.Add(entity);
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                this.Add(entity);
            }
        }

        public virtual bool Exist(Expression<Func<TEntity, bool>> func)
        {
            return this.EntitySet
                .Any(func);
        }

        public virtual bool Exist()
        {
            return this.EntitySet
                .Any();
        }

        public virtual int Count()
        {
            return this.EntitySet.Count();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> func)
        {
            return this.EntitySet.Count(func);
        }

        public virtual void Remove(TEntity entity)
        {
            this.EntitySet.Remove(entity);
        }

        public virtual void Modify(TEntity entity)
        {
            var entityEntry = this.Context.Entry(entity);

            if (entityEntry == null)
            {
                throw new ArgumentException("Entity not attached");
            }

            entityEntry.State = EntityState.Modified;
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> func)
        {
            return this.EntitySet
                .SingleOrDefault(func);
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return this.EntitySet;
        }


        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> func)
        {
            return this.EntitySet.Where(func);
        }

        public void Dispose()
        {
        }
    }
}
