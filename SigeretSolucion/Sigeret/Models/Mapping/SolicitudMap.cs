using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sigeret.Models.Mapping
{
    public class SolicitudMap : EntityTypeConfiguration<Solicitud>
    {
        public SolicitudMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Descripcion)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Solicitud");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.HoraInicio).HasColumnName("HoraInicio");
            this.Property(t => t.HoraFin).HasColumnName("HoraFin");
            this.Property(t => t.Descripcion).HasColumnName("Descripcion");
            this.Property(t => t.IdUserProfile).HasColumnName("IdUserProfile");
            this.Property(t => t.IdAulaEdificio).HasColumnName("IdAulaEdificio");
            this.Property(t => t.EstatusSolicitud).HasColumnName("EstatusSolicitud");
            this.Property(t => t.Fecha).HasColumnName("Fecha");
            this.Property(t => t.TipoSolicitud).HasColumnName("TipoSolicitud");

            // Relationships
            this.HasRequired(t => t.AulaEdificio)
                .WithMany(t => t.Solicituds)
                .HasForeignKey(d => d.IdAulaEdificio);
            this.HasRequired(t => t.UserProfile)
                .WithMany(t => t.Solicituds)
                .HasForeignKey(d => d.IdUserProfile);

        }
    }
}
