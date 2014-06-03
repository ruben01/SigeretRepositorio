using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sigeret.Models.Mapping
{
    public class ModeloEquipoMap : EntityTypeConfiguration<ModeloEquipo>
    {
        public ModeloEquipoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Marca)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Modelo)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ModeloEquipo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Nombre).HasColumnName("Nombre");
            this.Property(t => t.Marca).HasColumnName("Marca");
            this.Property(t => t.Descripcion).HasColumnName("Descripcion");
            this.Property(t => t.Modelo).HasColumnName("Modelo");
        }
    }
}
