using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sigeret.Models.Mapping
{
    public class AccionMap : EntityTypeConfiguration<Accion>
    {
        public AccionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Descriptor)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("Accion");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ControladorId).HasColumnName("ControladorId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Descriptor).HasColumnName("Descriptor");

            // Relationships
            this.HasMany(t => t.webpages_Roles)
                .WithMany(t => t.Accions)
                .Map(m =>
                    {
                        m.ToTable("webpages_Roles_Acciones");
                        m.MapLeftKey("AccionId");
                        m.MapRightKey("RoleId");
                    });

            this.HasRequired(t => t.Controlador)
                .WithMany(t => t.Accions)
                .HasForeignKey(d => d.ControladorId);

        }
    }
}
