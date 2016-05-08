namespace DataAccessLayer.Repositories.Repositories.DataBaseRepositories
{
    using DataAccessLayer.Models.Models;
    using DataAccessLayer.Repositories.Interfaces;
    using DataAccessLayer.Repositories.Repositories;

    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(IHotelDbContext dataContext)
            : base(dataContext)
        {
        }
    }
}
