using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sigeret.Models.Mapping
{
    public class SolicitudSmMap : EntityTypeConfiguration<SolicitudSm>
    {
        public SolicitudSmMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("SolicitudSms");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdSolicitud).HasColumnName("IdSolicitud");
            this.Property(t => t.IdContacto).HasColumnName("IdContacto");

            // Relationships
            this.HasRequired(t => t.Contacto)
                .WithMany(t => t.SolicitudSms)
                .HasForeignKey(d => d.IdContacto);
            this.HasRequired(t => t.Solicitud)
                .WithMany(t => t.SolicitudSms)
                .HasForeignKey(d => d.IdSolicitud);

        }
    }
}
