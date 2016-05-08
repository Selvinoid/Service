namespace DataAccessLayer.Models.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class UserLogin : IdentityUserLogin<int>
    {
        public int Id { get; set; }
    }
}
