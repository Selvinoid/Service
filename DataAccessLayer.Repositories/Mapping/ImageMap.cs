namespace DataAccessLayer.Repositories.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using DataAccessLayer.Models.Models;

    public class ImageMap : EntityTypeConfiguration<Image>
    {
        public ImageMap()
        {
            this.ToTable("Image");

            this.HasKey(p => p.Id);
        }
    }
}
