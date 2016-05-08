namespace DataAccessLayer.Repositories.Repositories.DataBaseRepositories
{
    using DataAccessLayer.Models.Models;
    using DataAccessLayer.Repositories.Interfaces;
    using DataAccessLayer.Repositories.Repositories;

    public class ConditionRepository:BaseRepository<Condition>
    {
        public ConditionRepository(IHotelDbContext dataContext)
            : base(dataContext)
        {
        }
    }
}
