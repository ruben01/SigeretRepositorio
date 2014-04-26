using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sigeret.Models.Mapping
{
    public class LugarMap : EntityTypeConfiguration<Lugar>
    {
        public LugarMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Edificio)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Lugar");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Edificio).HasColumnName("Edificio");
        }
    }
}
