namespace DataAccessLayer.Repositories.Repositories.DataBaseRepositories
{
    using DataAccessLayer.Models.Models;
    using DataAccessLayer.Repositories.Interfaces;
    using DataAccessLayer.Repositories.Repositories;

    public class RoomRepository : BaseRepository<Room>
    {
        public RoomRepository(IHotelDbContext dataContext)
            : base(dataContext)
        {
        }
    }
}
