using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sigeret.Models.Mapping
{
    public class EstatusEquipoMap : EntityTypeConfiguration<EstatusEquipo>
    {
        public EstatusEquipoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Estatus)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EstatusEquipo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Estatus).HasColumnName("Estatus");
            this.Property(t => t.Descripcion).HasColumnName("Descripcion");
        }
    }
}
