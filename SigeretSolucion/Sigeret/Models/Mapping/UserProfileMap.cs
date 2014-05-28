using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sigeret.Models.Mapping
{
    public class UserProfileMap : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileMap()
        {
            // Primary Key
            this.HasKey(t => t.UserId);

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Apellido)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Cedula)
                .IsRequired()
                .HasMaxLength(11);

            this.Property(t => t.Matricula)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UserProfile");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Apellido).HasColumnName("Apellido");
            this.Property(t => t.Nombre).HasColumnName("Nombre");
            this.Property(t => t.Cedula).HasColumnName("Cedula");
            this.Property(t => t.Foto).HasColumnName("Foto");
            this.Property(t => t.Matricula).HasColumnName("Matricula");
            this.Property(t => t.IdEstatusUsuario).HasColumnName("IdEstatusUsuario");

            // Relationships
            this.HasRequired(t => t.EstatusUsuario)
                .WithMany(t => t.UserProfiles)
                .HasForeignKey(d => d.IdEstatusUsuario);

        }
    }
}
