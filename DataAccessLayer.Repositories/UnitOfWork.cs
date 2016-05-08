namespace DataAccessLayer.Repositories
{
    using System;

    using DataAccessLayer.Repositories.Interfaces;
    using DataAccessLayer.Repositories.Repositories;

    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IHotelDbContext context)
        {
            this.Context = context;
        }

        private IHotelDbContext Context { get; set; }

        public TRepo GetRepository<TRepo>() where TRepo : BaseSimpleRepository
        {
            return Activator.CreateInstance(typeof(TRepo), this.Context) as TRepo;
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }
    }
}
