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
            this.Property(t => t.EstatusEquipo).HasColumnName("EstatusEquipo");
            this.Property(t => t.IdModeloEquipo).HasColumnName("IdModeloEquipo");

            // Relationships
            this.HasMany(t => t.Solicituds)
                .WithMany(t => t.Equipoes)
                .Map(m =>
                    {
                        m.ToTable("SolicitudEquipo");
                        m.MapLeftKey("idEquipo");
                        m.MapRightKey("IdSolicitud");
                    });

            this.HasRequired(t => t.ModeloEquipo)
                .WithMany(t => t.Equipoes)
                .HasForeignKey(d => d.IdModeloEquipo);

        }
    }
}
