namespace DataAccessLayer.Repositories.Repositories.DataBaseRepositories
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using DataAccessLayer.Models.Models;
    using DataAccessLayer.Repositories.Interfaces;
    using DataAccessLayer.Repositories.Repositories;

    public class UserRepository: BaseRepository<User>
    {
        public UserRepository(IHotelDbContext dataContext)
            : base(dataContext)
        {
        }
        public Task<User> GetByName(string userName)
        {
            return this.EntitySet.FirstOrDefaultAsync(p => p.UserName == userName);
        }
        public Task<User> GetByEmail(string email)  
        {
            return this.EntitySet.FirstOrDefaultAsync(p => p.Email == email);
        }
    }
}
