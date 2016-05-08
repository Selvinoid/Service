namespace DataAccessLayer.Repositories.Interfaces
{
    using System;

    using DataAccessLayer.Repositories.Repositories;

    public interface IUnitOfWork : IDisposable
    {
        TRepo GetRepository<TRepo>() where TRepo : BaseSimpleRepository;
        void SaveChanges();
    }
}
