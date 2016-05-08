namespace DataAccessLayer.Repositories
{
    using System.Data.Entity;
    using System.Text.RegularExpressions;

    using DataAccessLayer.Models.Models;
    using DataAccessLayer.Repositories.Interfaces;
    using DataAccessLayer.Repositories.Mapping;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class HotelDbContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>, IHotelDbContext
    {
        public HotelDbContext()
              : base("name=Hotel")
        {
        }

        public static HotelDbContext Create()
        {
            return new HotelDbContext();
        }

        public IDbSet<Condition> Conditionses { get; set; }

        public IDbSet<Hotel> Hotels { get; set; }

        public IDbSet<Image> Images { get; set; }

        public IDbSet<Order> Orders { get; set; }

        public IDbSet<Room> Rooms { get; set; }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.Configuration.LazyLoadingEnabled = false;
            modelBuilder.Configurations.Add(new ImageMap());
            modelBuilder.Configurations.Add(new HotelMap());
            modelBuilder.Configurations.Add(new RoomMap());
            modelBuilder.Configurations.Add(new ConditionMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new UserMap());

            modelBuilder.Entity<UserRole>().ToTable("UserRoles", "Security").HasKey(p => new { p.UserId, p.RoleId });
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins", "Security").HasKey(p => p.Id);
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims", "Security");
            modelBuilder.Entity<Role>().ToTable("Roles", "Security");
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
