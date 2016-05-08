namespace DataAccessLayer.Repositories.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using DataAccessLayer.Models.Models;

    public class ConditionMap : EntityTypeConfiguration<Condition>
    {
        public ConditionMap()
        {
            this.ToTable("Condition");

            this.HasKey(p => p.Id);

            this.Property(p => p.Id).IsRequired();

            this.Property(p => p.Name).HasMaxLength(60);

            this.HasMany(p => p.Images).WithMany(p => p.Conditionses).Map(
               p =>
               {
                   p.MapLeftKey("ConditionId");
                   p.MapRightKey("ImageId");
                   p.ToTable("Condition_Image");
               });
        }
    }
}
