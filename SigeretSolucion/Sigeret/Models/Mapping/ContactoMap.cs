using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sigeret.Models.Mapping
{
    public class ContactoMap : EntityTypeConfiguration<Contacto>
    {
        public ContactoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Descripcion)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("Contacto");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Descripcion).HasColumnName("Descripcion");
            this.Property(t => t.IdUserProfile).HasColumnName("IdUserProfile");
            this.Property(t => t.IdTipoContacto).HasColumnName("IdTipoContacto");

            // Relationships
            this.HasRequired(t => t.TipoContacto)
                .WithMany(t => t.Contactoes)
                .HasForeignKey(d => d.IdTipoContacto);
            this.HasRequired(t => t.UserProfile)
                .WithMany(t => t.Contactoes)
                .HasForeignKey(d => d.IdUserProfile);

        }
    }
}
