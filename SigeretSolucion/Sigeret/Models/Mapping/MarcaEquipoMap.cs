using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sigeret.Models.Mapping
{
    public class MarcaEquipoMap : EntityTypeConfiguration<MarcaEquipo>
    {
        public MarcaEquipoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Marca)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MarcaEquipo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Marca).HasColumnName("Marca");
        }
    }
}
