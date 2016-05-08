namespace DataAccessLayer.Repositories.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using DataAccessLayer.Models.Models;

    public class HotelMap : EntityTypeConfiguration<Hotel>
    {
        public HotelMap()
        {
            this.ToTable("Hotel");

            this.HasKey(p => p.Id);

            this.Property(p => p.Id).IsRequired();

            this.Property(p => p.Name).IsRequired().HasMaxLength(60);

            this.Property(p => p.Stars).IsRequired();
            
            this.HasMany(p => p.Images).WithMany(p => p.Hotels).Map(
                p =>
                {
                    p.MapLeftKey("HotelId");
                    p.MapRightKey("ImageId");
                    p.ToTable("Hotel_Image");
                });
            this.HasMany(p => p.Conditionses).WithMany(p => p.Hotels).Map(
                p =>
                {
                    p.MapLeftKey("HotelId");
                    p.MapRightKey("ConditionId");
                    p.ToTable("Hotel_Condition");
                });

            this.HasMany(p => p.Rooms).WithRequired(p => p.Hotel).HasForeignKey(p => p.IdHotel).WillCascadeOnDelete(true);

            this.HasMany(p => p.Orders).WithRequired(p => p.Hotel).HasForeignKey(p => p.IdHotel).WillCascadeOnDelete(false);
        }
    }
}
