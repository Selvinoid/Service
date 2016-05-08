namespace DataAccessLayer.Repositories.Identity
{
    using DataAccessLayer.Models.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationRoleManager : RoleManager<Role, int>
    {
        public ApplicationRoleManager(RoleStore<Role, int, UserRole> store)
                    : base(store)
        { }
    }
}
