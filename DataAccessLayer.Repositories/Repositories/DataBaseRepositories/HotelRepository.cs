namespace DataAccessLayer.Repositories.Repositories.DataBaseRepositories
{
    using DataAccessLayer.Models.Models;
    using DataAccessLayer.Repositories.Interfaces;
    using DataAccessLayer.Repositories.Repositories;

    public class HotelRepository : BaseRepository<Hotel>
    {
        public HotelRepository(IHotelDbContext dataContext)
            : base(dataContext)
        {
        }
    }
}
