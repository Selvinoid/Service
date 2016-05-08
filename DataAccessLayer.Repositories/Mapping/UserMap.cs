namespace DataAccessLayer.Repositories.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using DataAccessLayer.Models.Models;

    public class UserMap: EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.ToTable("Users", "Security");
            this.HasMany(p => p.Orders).WithRequired(p => p.User).HasForeignKey(p => p.IdUser).WillCascadeOnDelete(true);
        }
    }
}
