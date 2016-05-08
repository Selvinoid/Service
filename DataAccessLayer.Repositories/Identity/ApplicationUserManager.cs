namespace DataAccessLayer.Repositories.Identity
{
    using DataAccessLayer.Models.Models;

    using Microsoft.AspNet.Identity;

    public class ApplicationUserManager : UserManager<User,int>
    {
        public ApplicationUserManager(IUserStore<User,int> store)
                : base(store)
        {
        }
    }
}
