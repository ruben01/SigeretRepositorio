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
            this.Property(t => t.IdLugar).HasColumnName("IdLugar");
            this.Property(t => t.IdEstatusSolicitud).HasColumnName("IdEstatusSolicitud");
            this.Property(t => t.Fecha).HasColumnName("Fecha");

            // Relationships
            this.HasRequired(t => t.AulaEdificio)
                .WithMany(t => t.Solicituds)
                .HasForeignKey(d => d.IdLugar);
            this.HasRequired(t => t.EstatusSolicitud)
                .WithMany(t => t.Solicituds)
                .HasForeignKey(d => d.IdEstatusSolicitud);
            this.HasRequired(t => t.UserProfile)
                .WithMany(t => t.Solicituds)
                .HasForeignKey(d => d.IdUserProfile);

        }
    }
}
