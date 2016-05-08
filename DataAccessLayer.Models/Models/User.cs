namespace DataAccessLayer.Models.Models
{
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        public User()
        {
            this.Orders = new HashSet<Order>();
        }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
