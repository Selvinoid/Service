namespace DataAccessLayer.Repositories.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using DataAccessLayer.Models.Models;

    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            this.ToTable("Order");

            this.HasKey(p => p.Id);

            this.Property(p => p.ArrivalDate).IsRequired();

            this.Property(p => p.LeaveDate).IsRequired();
            
            this.HasRequired(p => p.Hotel).WithMany(p => p.Orders).HasForeignKey(p => p.IdHotel).WillCascadeOnDelete(false);

            this.HasRequired(p => p.Room).WithMany(p => p.Orders).HasForeignKey(p => p.IdRoom).WillCascadeOnDelete(true);

            this.HasRequired(p => p.User).WithMany(p => p.Orders).HasForeignKey(p => p.IdUser).WillCascadeOnDelete(true);
        }
    }
}
