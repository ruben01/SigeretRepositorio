using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sigeret.Models.Mapping
{
    public class SolicitudEquipoMap : EntityTypeConfiguration<SolicitudEquipo>
    {
        public SolicitudEquipoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("SolicitudEquipo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdSolicitud).HasColumnName("IdSolicitud");
            this.Property(t => t.idEquipo).HasColumnName("idEquipo");

            // Relationships
            this.HasRequired(t => t.Equipo)
                .WithMany(t => t.SolicitudEquipoes)
                .HasForeignKey(d => d.idEquipo);
            this.HasRequired(t => t.Solicitud)
                .WithMany(t => t.SolicitudEquipoes)
                .HasForeignKey(d => d.IdSolicitud);

        }
    }
}
