using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sigeret.Models.Mapping
{
    public class TipoContactoMap : EntityTypeConfiguration<TipoContacto>
    {
        public TipoContactoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Descripcion)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TipoContacto");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Descripcion).HasColumnName("Descripcion");
        }
    }
}
