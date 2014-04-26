using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sigeret.Models.Mapping
{
    public class EquipoMap : EntityTypeConfiguration<Equipo>
    {
        public EquipoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Serie)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Equipo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Serie).HasColumnName("Serie");
            this.Property(t => t.IdEstatusEquipo).HasColumnName("IdEstatusEquipo");
            this.Property(t => t.IdModelo).HasColumnName("IdModelo");

            // Relationships
            this.HasRequired(t => t.EstatusEquipo)
                .WithMany(t => t.Equipoes)
                .HasForeignKey(d => d.IdEstatusEquipo);
            this.HasRequired(t => t.ModeloEquipo)
                .WithMany(t => t.Equipoes)
                .HasForeignKey(d => d.IdModelo);

        }
    }
}
