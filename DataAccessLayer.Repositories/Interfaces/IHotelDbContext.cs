namespace DataAccessLayer.Repositories.Interfaces
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Text.RegularExpressions;

    using DataAccessLayer.Models.Models;

    public interface IHotelDbContext : IDisposable
    {
        IDbSet<Condition> Conditionses { get; set; }
        IDbSet<Hotel> Hotels { get; set; }   
        IDbSet<Image> Images { get; set; }  
        IDbSet<Order> Orders { get; set; }
        IDbSet<Room> Rooms { get; set; }  

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry Entry(object entity);

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void SaveChanges();

    }
}
