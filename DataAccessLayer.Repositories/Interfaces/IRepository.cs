namespace DataAccessLayer.Repositories.Interfaces
{
    using System;

    public interface IRepository : IDisposable
    {
        bool Exist();

        int Count();
    }
}
