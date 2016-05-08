namespace DataAccessLayer.Repositories.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using DataAccessLayer.Models.Models;

    public class RoomMap : EntityTypeConfiguration<Room>
    {
        public RoomMap()
        {
            this.ToTable("Room");

            this.HasKey(p => p.Id);
            
            this.Property(p => p.Number).IsRequired();
            
            this.HasRequired(p => p.Hotel).WithMany(p => p.Rooms).HasForeignKey(p => p.IdHotel).WillCascadeOnDelete(true);

            this.HasMany(p => p.Images).WithMany(p => p.Rooms).Map(
               p =>
               {
                   p.MapLeftKey("RoomId");
                   p.MapRightKey("ImageId");
                   p.ToTable("Room_Image");
               });

            this.HasMany(p => p.Orders).WithRequired(p => p.Room).HasForeignKey(p => p.IdRoom).WillCascadeOnDelete(true);
        }
    }
}
