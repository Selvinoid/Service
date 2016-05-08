namespace DataAccessLayer.Repositories.Repositories.DataBaseRepositories
{
    using DataAccessLayer.Models.Models;
    using DataAccessLayer.Repositories.Interfaces;
    using DataAccessLayer.Repositories.Repositories;

    public class ImageRepository : BaseRepository<Image>
    {
        public ImageRepository(IHotelDbContext dataContext)
            : base(dataContext)
        {
        }
    }
}
